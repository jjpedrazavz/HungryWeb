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

namespace HungryWeb.Controllers
{
    public class FoodController : Controller
    {

        private readonly IServiceFood _service = new ServiceFood();

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
        public async Task<ActionResult> Create()
        {
            var viewModel = await _service.CreateItemGet();

            if(viewModel != null)
            {

                viewModel.ImagenesSeleccionadas = new List<SelectList>();
                viewModel.ImagenesSeleccionadas.Add(new SelectList(viewModel.ImagenesStock, "Id", "NameFile"));
            }

            return View(viewModel);
        }

        // POST: Food/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(FoodViewModel viewModel)
        {

            return await _service.CreateItem(viewModel) ? RedirectToAction("Index") :  RedirectToAction("Create",viewModel);

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

            if(foodItem.SelectedImage != 0)
            foodItem.ImagenesSeleccionadas.Add(new SelectList(foodItem.ImagenesStock, "Id", "NameFile", foodItem.SelectedImage));
            else
                foodItem.ImagenesSeleccionadas.Add(new SelectList(foodItem.ImagenesStock, "Id", "NameFile"));

            return View(foodItem);
        }

        // Post: Food/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(FoodViewModel viewModel)
        {

           return  await _service.UpdateItem(viewModel) ? RedirectToAction("Index") : RedirectToAction("Edit",viewModel.ID);

        }

        // GET: Food/Desactivate/5
        public async Task<ActionResult> Desactivate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FoodViewModel alimento = await _service.GetDetailedFood(id.Value);
          
            return View(alimento);
        }

        // POST: Food/Desactivate/5
        [HttpPost, ActionName("Desactivate")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DesactivateConfim(int id)
        {
            return await _service.DesactivateItem(id) ? RedirectToAction("Index") : RedirectToAction("Delete", id);
        }


    }
}
