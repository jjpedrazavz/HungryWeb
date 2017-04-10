using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HungryWeb.ViewModels;
using HungryWeb.Contratos;
using HungryWeb.Servicios;
using System.Threading.Tasks;
using System.Diagnostics;

namespace HungryWeb.Controllers
{
    public class OrdenesController : Controller
    {
        private readonly IServiceOrders _service = new ServiceOrders();

        public OrdenesController()
        {

        }

        // GET: Ordenes
        public async Task<ActionResult> Index()
        {
           var orders = await _service.GetAllSlimOrders();
           return View(orders);
        }

        // GET: Ordenes/edit/5
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
        public async Task<ActionResult> Create()
        {
            OrderViewModel viewModel = await _service.GetCreateOrderForm();
            viewModel.menuSeleccionado = new List<MenuViewModel>();

            viewModel.Estados = new SelectList(viewModel.EstadosList, "EstadoID", "Descripcion", "");

            viewModel.MenusSeleccionar = new List<SelectList>();

            viewModel.menuSeleccionado = new List<MenuViewModel>();

            for (int i = 0; i < 6; i++)
            {
               viewModel.menuSeleccionado.Add(new MenuViewModel());
            }
            

            var sopas = (from element in viewModel.AlimentosList
                         where element.TipoId == 1
                         select element).ToList();

            viewModel.MenusSeleccionar.Add(new SelectList(sopas, "ID", "Nombre", ""));

            var platosfuertes = (from element in viewModel.AlimentosList
                                 where element.TipoId == 3
                                 select element).ToList();

            viewModel.MenusSeleccionar.Add(new SelectList(platosfuertes, "ID", "Nombre", ""));



            var bebidas = (from element in viewModel.AlimentosList
                           where element.TipoId == 2
                           select element).ToList();

            viewModel.MenusSeleccionar.Add(new SelectList(bebidas, "ID", "Nombre", ""));



            var postres = (from element in viewModel.AlimentosList
                           where element.TipoId == 4
                           select element).ToList();

            viewModel.MenusSeleccionar.Add(new SelectList(postres, "ID", "Nombre", ""));



            var complementos = (from element in viewModel.AlimentosList
                                where element.TipoId == 5
                                select element).ToList();

            viewModel.MenusSeleccionar.Add(new SelectList(complementos, "ID", "Nombre", ""));


            var bocadillos = (from element in viewModel.AlimentosList
                                where element.TipoId == 6
                                select element).ToList();

            viewModel.MenusSeleccionar.Add(new SelectList(bocadillos, "ID", "Nombre", ""));


            Debug.WriteLine("Tamaño viewModel: " + viewModel.menuSeleccionado.Count);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(DetailedOrderViewModel viewModel)
        {
            return await _service.UpdateItem(viewModel) ? RedirectToAction("Index") : RedirectToAction("Details", viewModel.OrdenID);
        }

        // POST: Ordenes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OrderViewModel viewModel)
        {
          
            return await _service.CreateItem(viewModel) ? RedirectToAction("Index") : RedirectToAction("Create");
  
        }

        // GET: Ordenes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View( await _service.GetDetailedOrder(id.Value));

          
        }

        // POST: Ordenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
           return await _service.DeleteItem(id) ? RedirectToAction("Index") : RedirectToAction("Delete");
        }


    }
}
