using HungryWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HungryWeb.ViewModels
{
    public class DetailedOrderViewModel
    {
       [Display(Name ="Numero de Orden")]
        public int OrdenID { get; set; }

        public int ComensalID { get; set; }

        public int EstadoID { get; set; }

        public Estado estado { get; set; }

        public IEnumerable<Menu> menu { get; set; }

        public Comensales comensal { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString ="{0:c}")]
        public double totalMenu { get; set; }

        public IEnumerable<Estado> estadosList { get; set; }

    }
}