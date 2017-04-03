using HungryWeb.Models3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HungryWeb.ViewModels
{
    public class DetailedOrderViewModel
    {
        public int OrdenID { get; set; }

        public Estado estado { get; set; }

        public Menu menu { get; set; }

        public Comensales comensal { get; set; }
        

    }
}