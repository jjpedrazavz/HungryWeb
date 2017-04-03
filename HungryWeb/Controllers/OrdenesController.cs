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
    public class OrdenesController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Ordenes
        public ActionResult Index()
        {
            var ordenes = db.Ordenes.Include(o => o.Comensales).Include(o => o.Estado);
            return View(ordenes.ToList());
        }

        // GET: Ordenes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordenes ordenes = db.Ordenes.Find(id);
            if (ordenes == null)
            {
                return HttpNotFound();
            }
            return View(ordenes);
        }

        // GET: Ordenes/Create
        public ActionResult Create()
        {
            ViewBag.ComensalID = new SelectList(db.Comensales, "ComensalID", "Nombre");
            ViewBag.EstadoID = new SelectList(db.Estado, "EstadoID", "Descripcion");
            return View();
        }

        // POST: Ordenes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrdenID,ComensalID,EstadoID")] Ordenes ordenes)
        {
            if (ModelState.IsValid)
            {
                db.Ordenes.Add(ordenes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ComensalID = new SelectList(db.Comensales, "ComensalID", "Nombre", ordenes.ComensalID);
            ViewBag.EstadoID = new SelectList(db.Estado, "EstadoID", "Descripcion", ordenes.EstadoID);
            return View(ordenes);
        }

        // GET: Ordenes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordenes ordenes = db.Ordenes.Find(id);
            if (ordenes == null)
            {
                return HttpNotFound();
            }
            ViewBag.ComensalID = new SelectList(db.Comensales, "ComensalID", "Nombre", ordenes.ComensalID);
            ViewBag.EstadoID = new SelectList(db.Estado, "EstadoID", "Descripcion", ordenes.EstadoID);
            return View(ordenes);
        }

        // POST: Ordenes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrdenID,ComensalID,EstadoID")] Ordenes ordenes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ordenes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ComensalID = new SelectList(db.Comensales, "ComensalID", "Nombre", ordenes.ComensalID);
            ViewBag.EstadoID = new SelectList(db.Estado, "EstadoID", "Descripcion", ordenes.EstadoID);
            return View(ordenes);
        }

        // GET: Ordenes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ordenes ordenes = db.Ordenes.Find(id);
            if (ordenes == null)
            {
                return HttpNotFound();
            }
            return View(ordenes);
        }

        // POST: Ordenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ordenes ordenes = db.Ordenes.Find(id);
            db.Ordenes.Remove(ordenes);
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
