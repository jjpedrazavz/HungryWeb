using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HungryWeb.Models3;
using HungryWeb.ViewModels;
using HungryWeb.Contratos;
using HungryWeb.Servicios;
using System.Threading.Tasks;

namespace HungryWeb.Controllers
{
    public class FoodController : Controller
    {
        private StoreContext db = new StoreContext();

        private IServiceFood _service = new ServiceFood();

        // GET: Food
        public async Task<ActionResult> Index()
        {
            var elements = await _service.GetAllFood();
            return View(elements);
        }

        // GET: Food/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FoodViewModel alimento = await _service.GetDetailedFood(id.Value);

            if (alimento == null)
            {
                return HttpNotFound();
            }
            return View(alimento);
        }

        // GET: Food/Create
        public ActionResult Create()
        {
            FoodViewModel viewModel = new FoodViewModel();
            viewModel.Categories = new SelectList(db.Categorias, "CategoriaID", "Nombre");
            viewModel.Tipos = new SelectList(db.Tipos, "TipoID", "Nombre");
            viewModel.ImagenesSeleccionadas = new List<SelectList>();

            //devolvemos todas las imagenes en la bd
            for (int i = 0; i < Constantes.NumeroImagenes; i++)
            {
                //agregamos todas las imagenes tantas veces como formularios para agregar una imagen tengamos, en este caso es 1 solo formulario
                viewModel.ImagenesSeleccionadas.Add(new SelectList(db.FoodImages, "ID", "NameFile"));

            }


            return View(viewModel);
        }

        // POST: Food/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FoodViewModel viewModel)
        {
            Alimentos alimento = new Alimentos();

            alimento.CategoriaId = viewModel.CategoriaID;
            alimento.Nombre = viewModel.Nombre;
            alimento.Precio = viewModel.Precio;
            alimento.TipoId = viewModel.tipoID;
            alimento.FoodImageMapping = new List<FoodImageMapping>();

            //Devolvemos todas las ID's de las imagenes que fueron seleccionadas por cada uno de los formularios, descartando los formularios en blanco.
            string[] ImagenesProducto = viewModel.ImagenesProducto.Where(elemento => !string.IsNullOrWhiteSpace(elemento)).ToArray();

            //iteramos sobre la cantidad de imagenes devueltas, asociadas al alimento y devolvemos todas las imagenes que correspondan con la ID devuelta
            for (int i = 0; i < ImagenesProducto.Length; i++)
            {
                alimento.FoodImageMapping.Add(new FoodImageMapping
                {
                    FoodImages = db.FoodImages.Find(int.Parse(ImagenesProducto[i]))
                });
            }

            //si todo esta bien se ingresa el producto
            if (ModelState.IsValid)
            {
                db.Alimentos.Add(alimento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //de lo contrario se devuelven los valores imgresandos
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nombre", alimento.CategoriaId);
            ViewBag.tipoID = new SelectList(db.Tipos, "TipoID", "Nombre", alimento.TipoId);
            return View(alimento);
        }

        // GET: Food/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var foodItem = await _service.GetDetailedFood(id.Value);

            foodItem.ImagenesSeleccionadas = new List<SelectList>();
            foodItem.Categories = new SelectList(foodItem.CategoriasStock,"CategoriaId", "Nombre", foodItem.CategoriaID);
            foodItem.Tipos = new SelectList(foodItem.TiposStock, "TipoId", "Nombre", foodItem.tipoID);
            foodItem.ImagenesSeleccionadas.Add(new SelectList(foodItem.ImagenesStock, "Id", "NameFile", foodItem.SelectedImage));

            return View(foodItem);
        }

        // POST: Food/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FoodViewModel viewModel)
        {
            //obtenemos el alimento original en base a la ID del viewModel devuelto.
            var productToUpdate = db.Alimentos.Include(p => p.FoodImageMapping).Where(p => p.Id == viewModel.ID).Single();


            if(TryUpdateModel(productToUpdate,"", new string[] { "Nombre", "CategoriaID", "Precio", "tipoID" }))
            {
                //verificamos si el alimento original no tuvo imagenes asociadas, de ser asi creamos una coleccion antes.
                if(productToUpdate.FoodImageMapping == null)
                {
                    productToUpdate.FoodImageMapping = new List<FoodImageMapping>();

                }


                //seleccionamos todas las ID's de las imagenes que actualmente tiene el producto
                //evitando los campos en blanco, en caso de que no asociara una imagen al momento de editar
                string[] ImagenesProducto = viewModel.ImagenesProducto.Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();


                //iteramos en base al numero de imagenes asociadas al producto
                for (int i = 0; i < ImagenesProducto.Length; i++)
                {

                    //obtenemos la imagen ORIGINAL ligada al producto con base en el numero de formulario.
                    var originalImageMap = db.FoodImageMapping.Where(p => p.ImageNumber == i).FirstOrDefault();

                    //obtenemos la IMAGEN NUEVA en base a la ID registrada
                    var currentImage = db.FoodImages.Find(int.Parse(ImagenesProducto[i]));

                    //verificamos si el formulario oriignal estaba nulo
                   if(originalImageMap == null)
                    {
                        productToUpdate.FoodImageMapping.Add(new FoodImageMapping { FoodImages = currentImage });
                    }
                    else
                    {
                        if(originalImageMap.FoodImages.Id != int.Parse(ImagenesProducto[i]))
                        {
                            originalImageMap.FoodImages = currentImage;
                        }
                    }
                }


                 //elimina cualquier otra aplicacion de imagen que el usuario no incluyo en sus selecciones para el producto.
                for (int i = ImagenesProducto.Length; i < Constantes.NumeroImagenes; i++)
                {
                    var imageMappingToEdit = productToUpdate.FoodImageMapping.Where(p => p.ImageNumber == i).FirstOrDefault();

                    if (imageMappingToEdit != null)
                    {
                        db.FoodImageMapping.Remove(imageMappingToEdit);
                    }
                }


                db.SaveChanges();

                return RedirectToAction("Index");
            }


            return View(viewModel);

           
        }

        // GET: Food/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Alimentos alimento = db.Alimentos.Find(id);

            if (alimento == null)
            {
                return HttpNotFound();
            }
            return View(alimento);
        }

        // POST: Food/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Alimentos alimentos = db.Alimentos.Find(id);
            db.Alimentos.Remove(alimentos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
