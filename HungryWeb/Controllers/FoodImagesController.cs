using HungryWeb.Contratos;
using HungryWeb.Models;
using HungryWeb.Servicios;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace HungryWeb.Controllers
{
    public class FoodImagesController : Controller
    {
        private readonly IServiceFoodImages serviceImages;
        private readonly IServiceFoodImageMap serviceMapping;


        public FoodImagesController()
        {
            serviceImages = new ServiceFoodImages();
            serviceMapping = new ServiceFoodImageMappings();
        }


        // GET: FoodImages
        public async Task<ActionResult> Index()
        {
            var imagenesList = await serviceImages.GetFoodImages();


            return View(imagenesList);
        }
        
        
        // GET: FoodImages/Upload
        public ActionResult Upload()
        {
            return View();
        }

        // POST: FoodImages/Upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Upload(HttpPostedFileBase file)
        {

            bool valid = true;
            string InvalidFile = "";

            Debug.WriteLine("entrando a la llamada");

            if (file != null)
            {
                Debug.WriteLine("tomo la imagen: "+file.FileName);

                if (!ValidateFile(file))
                {
                    valid = false;
                    InvalidFile += ", " + file.FileName;
                }

                if (valid)
                {
                    try
                    {
                        SaveFileToDisk(file);
                    }
                    catch (Exception)
                    {
                        ModelState.AddModelError("File", "Un error ocurrio mientras se guardaba el archivo");
                    }
                }
                else
                {
                    ModelState.AddModelError("FileName", "El archivo: " + InvalidFile+" debe ser de tipo gift,png,jpeg,jpg y menor de 2Mb");
                }

            }
            else
            {
                ModelState.AddModelError("FileName", "Seleccione un archivo primero");
            }


            if (ModelState.IsValid)
            {

                string duplicateFile = "";

                var ImageToAdd = new FoodImages { NameFile = file.FileName };

        
                duplicateFile = await serviceImages.SaveImage(ImageToAdd);

                if (!string.IsNullOrWhiteSpace(duplicateFile))
                {
                    ModelState.AddModelError("FileName", "El archivo:" + duplicateFile + "Ya existe!");
                    return View();
                }

                return RedirectToAction("Index");
            }

            return View();


        }

        private void SaveFileToDisk(HttpPostedFileBase file)
        {
            //obtenemos on ubjeto WebImage Manipulable en base al archivo pasado por parametro
            WebImage img = new WebImage(file.InputStream);

            //verificamos el ancho de la imagen, en caso deser mas grande 
            //se redimensiona
            if (img.Width > 190)
            {
                img.Resize(190, img.Height);
            }

            //guardamos la ruta completa de la imagen.
            img.Save(Constantes.ImagePathDefault + file.FileName);


            //verificamos lo mismo.
            if (img.Width > 100)
            {
                img.Resize(100, img.Height);
            }

            img.Save(Constantes.ImagePathDefault + file.FileName);

        }


        private bool ValidateFile(HttpPostedFileBase file)
        {
            string fileExtension = System.IO.Path.GetExtension(file.FileName).ToLower();

            string[] allowedFileTypes = { ".gif", ".png", ".jpeg", ".jpg" };


            //verificamos tamaño y extension

            if(file.ContentLength > 0 && file.ContentLength<2097152 && allowedFileTypes.Contains(fileExtension))
            {
                return true;
            }

            return false;


        }

        // GET: FoodImages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
             if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FoodImages foodImage = await serviceImages.GetOne(id.Value);

            return View(foodImage);

        }

        // POST: FoodImages/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(FoodImages image)
        {

            if (ModelState.IsValid)
            {
                if (await serviceImages.UpdateImage(image))
                {
                    return RedirectToAction("Index");
                }

                return View();
            }

            return View();
            
        }

        // GET: FoodImages/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            return View(await serviceImages.GetOne(id));
        }

        // POST: FoodImages/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfim(int id)
        {
            FoodImages image = await serviceImages.GetOne(id);

            //buscamos todos los mappings que usan la imagen

            var mappings = image.FoodImageMapping.Where(p => p.AlimentosImageId == id);

            foreach (var item in mappings)
            {
                item.AlimentosImageId = null;
            }

            System.IO.File.Delete(Request.MapPath(Constantes.ImagePathDefault + image.NameFile));


           return await serviceImages.DeleteImage(image) ? RedirectToAction("Index") : RedirectToAction("Delete");


        }
    }
}
