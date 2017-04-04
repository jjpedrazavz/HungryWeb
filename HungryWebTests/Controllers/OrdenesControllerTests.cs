﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using HungryWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HungryWeb.ViewModels;

namespace HungryWeb.Controllers.Tests
{
    [TestClass()]
    public class OrdenesControllerTests
    {
        [TestMethod()]
        public void CreateTest()
        {


        }

        [TestMethod()]
        public void DetailsTest()
        {
            DetailedOrderViewModel model;
            OrdenesController ordenes = new OrdenesController();
            var resultado = ordenes.Details(1) as ViewResult;
            model = (DetailedOrderViewModel)resultado.Model;
            var elemento = model.menu.bebida.ID;


            Assert.AreEqual(7,elemento);
        }
    }
}