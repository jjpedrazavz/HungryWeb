using HungryWeb.Models3;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HungryWeb.ViewModels
{
    public class OrdenesViewModel
    {
        public int OrdenID { get; set; }

        [Required]
        public string ComensalID { get; set; }

        [Required]
        public string EstadoID { get; set; }

        public string sopaID { get; set; }
        public string platoFuerteID { get; set; }
        public string bebidaID { get; set; }
        public string postreID { get; set; }
        public string complementoID { get; set; }

        public string bocadilloID { get; set; }


        public SelectList Estados { get; set; }

        public List<SelectList> MenusSeleccionar { get; set; }


    }
}