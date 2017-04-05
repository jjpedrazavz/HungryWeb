using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HungryWeb.ViewModels
{
    public class SlimOrdersViewModel
    {
        [Display(Name = "Numero Orden")]
        public int OrdenID { get; set; }

        [Required]
        [Display(Name = "Comensal")]
        public int ComensalID { get; set; }

        [Required]
        [Display(Name = "Estatus")]
        public string EstadoDescripcion { get; set; }
    }
}