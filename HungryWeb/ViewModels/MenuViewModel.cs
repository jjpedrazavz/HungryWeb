using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HungryWeb.ViewModels
{
    public class MenuViewModel
    {
        public string AlimentoID { get; set; }

        public int Cantidad { get; set; }

    }
}