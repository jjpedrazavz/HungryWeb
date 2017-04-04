using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HungryWeb.Models3;

namespace HungryWeb.Controllers
{
    public class FoodImagesController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: FoodImages
        public ActionResult Index()
        {
            return View(db.FoodImages.ToList());
        }

        // GET: FoodImages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodImages foodImages = db.FoodImages.Find(id);
            if (foodImages == null)
            {
                return HttpNotFound();
            }
            return View(foodImages);
        }

        // GET: FoodImages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FoodImages/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NameFile")] FoodImages foodImages)
        {
            if (ModelState.IsValid)
            {
                db.FoodImages.Add(foodImages);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(foodImages);
        }

        // GET: FoodImages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodImages foodImages = db.FoodImages.Find(id);
            if (foodImages == null)
            {
                return HttpNotFound();
            }
            return View(foodImages);
        }

        // POST: FoodImages/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NameFile")] FoodImages foodImages)
        {
            if (ModelState.IsValid)
            {
                db.Entry(foodImages).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(foodImages);
        }

        // GET: FoodImages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FoodImages foodImages = db.FoodImages.Find(id);
            if (foodImages == null)
            {
                return HttpNotFound();
            }
            return View(foodImages);
        }

        // POST: FoodImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FoodImages foodImages = db.FoodImages.Find(id);
            db.FoodImages.Remove(foodImages);
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
