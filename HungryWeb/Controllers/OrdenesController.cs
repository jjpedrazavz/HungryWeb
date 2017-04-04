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

            Ordenes orden = db.Ordenes.Find(id);

            if (orden == null)
            {
                return HttpNotFound();
            }

            DetailedOrderViewModel details = new DetailedOrderViewModel();

            details.OrdenID = orden.OrdenID;

            details.estado = db.Estado.Find(orden.EstadoID);
            details.comensal = db.Comensales.Find(orden.ComensalID);

            var MenuActual = db.Menu.Find(orden.Menu.FirstOrDefault().MenuID);

            if(MenuActual !=null)
            {
                details.menu = MenuActual;
            }
            else
            {
                return HttpNotFound();
            }

            if(!string.IsNullOrWhiteSpace(MenuActual.bebidaID.ToString()))
                MenuActual.bebida = db.Alimentos.Find(MenuActual.bebidaID);
                details.totalMenu += (double)MenuActual.bebida.Precio;

            if (!string.IsNullOrWhiteSpace(MenuActual.sopaID.ToString()))
                MenuActual.sopa = db.Alimentos.Find(MenuActual.sopaID);
                details.totalMenu += (double)MenuActual.sopa.Precio;

            if (!string.IsNullOrWhiteSpace(MenuActual.platoFuerteID.ToString()))
                MenuActual.platoFuerte = db.Alimentos.Find(MenuActual.platoFuerteID);
                details.totalMenu += (double)MenuActual.platoFuerte.Precio;

            if (!string.IsNullOrWhiteSpace(MenuActual.postreID.ToString()))
                MenuActual.postre = db.Alimentos.Find(MenuActual.postreID);
                details.totalMenu += (double)MenuActual.postre.Precio;

            if (!string.IsNullOrWhiteSpace(MenuActual.bocadilloID.ToString()))
                MenuActual.bocadillo = db.Alimentos.Find(MenuActual.bocadilloID);
                details.totalMenu += (double)MenuActual.bocadillo.Precio;

            if (!string.IsNullOrWhiteSpace(MenuActual.complementoID.ToString()))
                MenuActual.complemento = db.Alimentos.Find(MenuActual.complementoID);
                details.totalMenu += (double)MenuActual.complemento.Precio;

            

            return View(details);
        }

        // GET: Ordenes/Create
        public ActionResult Create()
        {
            OrdenesViewModel viewModel = new OrdenesViewModel();

            viewModel.Estados = new SelectList(db.Estado, "EstadoID", "Descripcion", "");
            viewModel.MenusSeleccionar = new List<SelectList>();            

                    var sopas = (from element in db.Alimentos
                            where element.tipoID == 1
                            select element).ToList();

                viewModel.MenusSeleccionar.Add(new SelectList(sopas, "ID", "Nombre", ""));

            var platosfuertes = (from element in db.Alimentos
                         where element.tipoID == 3
                         select element).ToList();

            viewModel.MenusSeleccionar.Add(new SelectList(platosfuertes, "ID", "Nombre",""));



            var bebidas = (from element in db.Alimentos
                                 where element.tipoID == 2
                                 select element).ToList();

            viewModel.MenusSeleccionar.Add(new SelectList(bebidas, "ID", "Nombre", ""));



            var postres = (from element in db.Alimentos
                                 where element.tipoID == 4
                                 select element).ToList();

            viewModel.MenusSeleccionar.Add(new SelectList(postres, "ID", "Nombre", ""));



            var complementos = (from element in db.Alimentos
                           where element.tipoID == 5
                           select element).ToList();

            viewModel.MenusSeleccionar.Add(new SelectList(complementos, "ID", "Nombre", ""));


            return View(viewModel);
        }

        // POST: Ordenes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrdenesViewModel viewModel)
        {
            Ordenes orden = new Ordenes();

            //buscamos si existe el comensal.
            var comensal = db.Comensales.Find(int.Parse(viewModel.ComensalID));


            if (comensal != null)
            {
                orden.ComensalID = int.Parse(viewModel.ComensalID);
            }
            else
            {
                return HttpNotFound();
            }

            orden.EstadoID = int.Parse(viewModel.EstadoID);
            orden.Menu = new List<Menu>();

            Menu menu = new Menu();
            menu.OrdenID = orden.OrdenID;


            if (!string.IsNullOrWhiteSpace(viewModel.bebidaID))
                menu.bebidaID = int.Parse(viewModel.bebidaID);

            if (!string.IsNullOrWhiteSpace(viewModel.sopaID))
                menu.sopaID = int.Parse(viewModel.sopaID);

            if (!string.IsNullOrWhiteSpace(viewModel.platoFuerteID))
                menu.platoFuerteID = int.Parse(viewModel.platoFuerteID);

            if (!string.IsNullOrWhiteSpace(viewModel.postreID))
                menu.postreID = int.Parse(viewModel.postreID);

            if (!string.IsNullOrWhiteSpace(viewModel.complementoID))
                menu.complementoID = int.Parse(viewModel.complementoID);

            if (!string.IsNullOrWhiteSpace(viewModel.bocadilloID))
                menu.bocadilloID = int.Parse(viewModel.bocadilloID);


            orden.Menu.Add(menu);


            if (ModelState.IsValid)
            {
                db.Ordenes.Add(orden);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
             
            viewModel.Estados = new SelectList(db.Estado, "EstadoID", "Descripcion", orden.EstadoID);
            return View(orden);
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
