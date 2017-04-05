using Microsoft.VisualStudio.TestTools.UnitTesting;
using HungryWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HungryWeb.ViewModels;
using System.Web.Mvc;

namespace HungryWeb.Controllers.Tests
{
    [TestClass()]
    public class OrdenesControllerTests
    {
        /// <summary>
        /// Verificacion de los valores devueltos por una orden.
        /// </summary>
        [TestMethod()]
        public void DetailsTest()
        {
            DetailedOrderViewModel model;
            OrdenesController ordenes = new OrdenesController();
            var resultado = ordenes.Details(3).Result as ViewResult;
            model = (DetailedOrderViewModel)resultado.Model;


            Assert.AreEqual(null, model.bocadillo);
            //Assert.AreEqual("",model.menu.ToString());
        }
    }
}