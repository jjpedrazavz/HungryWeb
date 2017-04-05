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
    public class OrdenesController : Controller
    {
        private StoreContext db = new StoreContext();
        private readonly IServiceOrders _service = new ServiceOrders();

        // GET: Ordenes
        public async Task<ActionResult> Index()
        {
           var orders = await _service.GetAllSlimOrders();
           return View(orders);
        }

        // GET: Ordenes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DetailedOrderViewModel details = await _service.GetDetailedOrder(id.Value);

            if (details == null)
            {
                return HttpNotFound();
            }

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

            viewModel.MenusSeleccionar.Add(new SelectList(platosfuertes, "ID", "Nombre", ""));



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
