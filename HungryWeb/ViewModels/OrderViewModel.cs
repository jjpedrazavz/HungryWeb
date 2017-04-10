using HungryWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HungryWeb.ViewModels
{
    public class OrderViewModel
    {
        [Display(Name = "Numero Orden")]
        public int OrdenID { get; set; }

        [Required]
        [Display(Name = "Comensal")]
        public int ComensalID { get; set; }

        [Required]
        public int EstadoID { get; set; }

        public int sopaID { get; set; }
        public int platoFuerteID { get; set; }
        public int bebidaID { get; set; }
        public int postreID { get; set; }
        public int complementoID { get; set; }

        public int bocadilloID { get; set; }


        public SelectList Estados { get; set; }

        public List<SelectList> MenusSeleccionar { get; set; }

        public IEnumerable<Alimentos> AlimentosList { get; set; }

        public IEnumerable<Estado> EstadosList { get; set; }


    }
}