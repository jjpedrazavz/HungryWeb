using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HungryWeb.ViewModels
{
    public class AlimentoViewModel
    {

        public int ID { get; set; }
        public string Nombre { get; set; }

        public int CategoriaID { get; set; }

        public decimal Precio { get; set; }

        public int tipoID { get; set; }

        public int? FoodImageMappingID { get; set; }



        public SelectList Categories { get; set; }

        public SelectList Tipos { get; set; }

        public List<SelectList> ImagenesSeleccionadas { get; set; }

        public string[] ImagenesProducto { get; set; }

    }
}