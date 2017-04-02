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

namespace HungryWeb.Controllers
{
    public class FoodController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Food
        public ActionResult Index()
        {
            var alimentos = db.Alimentos.Include(a => a.Categorias).Include(a => a.Tipos);
            return View(alimentos.ToList());
        }

        // GET: Food/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alimentos alimentos = db.Alimentos.Find(id);
            if (alimentos == null)
            {
                return HttpNotFound();
            }
            return View(alimentos);
        }

        // GET: Food/Create
        public ActionResult Create()
        {
            AlimentoViewModel viewModel = new AlimentoViewModel();
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
        public ActionResult Create(AlimentoViewModel viewModel)
        {
            Alimentos alimento = new Alimentos();

            alimento.CategoriaID = viewModel.CategoriaID;
            alimento.Nombre = viewModel.Nombre;
            alimento.Precio = viewModel.Precio;
            alimento.tipoID = viewModel.tipoID;
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
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nombre", alimento.CategoriaID);
            ViewBag.tipoID = new SelectList(db.Tipos, "TipoID", "Nombre", alimento.tipoID);
            return View(alimento);
        }

        // GET: Food/Edit/5
        public ActionResult Edit(int? id)
        {
            AlimentoViewModel viewModel = new AlimentoViewModel();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Alimentos alimento = db.Alimentos.Find(id);

            if (alimento == null)
            {
                return HttpNotFound();
            }

            viewModel.Categories = new SelectList(db.Categorias, "CategoriaID", "Nombre", alimento.CategoriaID);
            viewModel.Tipos = new SelectList(db.Tipos, "TipoID", "Nombre", alimento.tipoID);
            viewModel.ImagenesSeleccionadas = new List<SelectList>();

            //El loop se encarga de devolver la imagen especifica asociada con el producto
            foreach (var ImageMap in alimento.FoodImageMapping)
            {
                //agregamos una nueva seleccion dentro de la lista donde el primer paramentro es la tabla de imagenes
                //el segundo parametro es el valor que representara internamente el tercer paramentro el que aparecera en la vista 
                //por ulimo se selecciona un elemento el cual es la imagen original asociada al alimento.
                viewModel.ImagenesSeleccionadas.Add(new SelectList(db.FoodImages, "ID", "NameFile", ImageMap.AlimentosImageID));
            }

            viewModel.ID = alimento.ID;
            viewModel.Nombre = alimento.Nombre;
            viewModel.Precio = alimento.Precio;


            return View(viewModel);
        }

        // POST: Food/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AlimentoViewModel viewModel)
        {
            //obtenemos el alimento original en base a la ID del viewModel devuelto.
            var productToUpdate = db.Alimentos.Include(p => p.FoodImageMapping).Where(p => p.ID == viewModel.ID).Single();


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
                    var originalImageMap = db.FoodImageMapping.Where(p => p.ID == productToUpdate.FoodImageMapping.ElementAt(i).ID).FirstOrDefault();

                    //obtenemos la IMAGEN NUEVA
                    var currentImage = db.FoodImages.Find(ImagenesProducto[i]);

                    //verificamos si el formulario oriignal estaba nulo
                   if(originalImageMap == null)
                    {
                        productToUpdate.FoodImageMapping.Add(new FoodImageMapping { FoodImages = currentImage });
                    }
                    else
                    {
                        if(originalImageMap.FoodImages.ID != int.Parse(ImagenesProducto[i]))
                        {
                            productToUpdate.FoodImageMapping.ElementAt(0).FoodImages = currentImage;
                        }
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
            Alimentos alimentos = db.Alimentos.Find(id);
            if (alimentos == null)
            {
                return HttpNotFound();
            }
            return View(alimentos);
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
