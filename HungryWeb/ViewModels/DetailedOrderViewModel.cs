using HungryWeb.Models3;
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

        public Alimentos sopa { get; set; }

        public Alimentos platoFuerte { get; set; }

        public Alimentos bebida { get; set; }

        public Alimentos postre { get; set; }

        public Alimentos complemento { get; set; }

        public Alimentos bocadillo { get; set; }

        public Comensales comensal { get; set; }

        [DataType(DataType.Currency)]
        [DisplayFormat(DataFormatString ="{0:c}")]
        public double totalMenu { get; set; }

        public IEnumerable<Estado> estadosList { get; set; }

    }
}