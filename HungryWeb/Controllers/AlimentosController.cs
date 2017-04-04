using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HungryWeb.Context;

namespace HungryWeb.Controllers
{
    public class AlimentosController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Alimentos
        public ActionResult Index()
        {
            var alimentos = db.Alimentos.Include(a => a.Categorias).Include(a => a.Tipos);
            return View(alimentos.ToList());
        }

        // GET: Alimentos/Details/5
        public ActionResult Details(string id)
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

        // GET: Alimentos/Create
        public ActionResult Create()
        {
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nombre");
            ViewBag.tipoID = new SelectList(db.Tipos, "TipoID", "Nombre");
            return View();
        }

        // POST: Alimentos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,CategoriaID,Precio,tipoID")] Alimentos alimentos)
        {
            if (ModelState.IsValid)
            {
                db.Alimentos.Add(alimentos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nombre", alimentos.CategoriaID);
            ViewBag.tipoID = new SelectList(db.Tipos, "TipoID", "Nombre", alimentos.tipoID);
            return View(alimentos);
        }

        // GET: Alimentos/Edit/5
        public ActionResult Edit(string id)
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
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nombre", alimentos.CategoriaID);
            ViewBag.tipoID = new SelectList(db.Tipos, "TipoID", "Nombre", alimentos.tipoID);
            return View(alimentos);
        }

        // POST: Alimentos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Nombre,CategoriaID,Precio,tipoID")] Alimentos alimentos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alimentos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaID = new SelectList(db.Categorias, "CategoriaID", "Nombre", alimentos.CategoriaID);
            ViewBag.tipoID = new SelectList(db.Tipos, "TipoID", "Nombre", alimentos.tipoID);
            return View(alimentos);
        }

        // GET: Alimentos/Delete/5
        public ActionResult Delete(string id)
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

        // POST: Alimentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
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
