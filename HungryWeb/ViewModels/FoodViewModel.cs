using HungryWeb.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HungryWeb.ViewModels
{
    public class FoodViewModel
    {

        public int ID { get; set; }
        public string Nombre { get; set; }

        public int CategoriaID { get; set; }

        public decimal Precio { get; set; }

        public int tipoID { get; set; }

        public int SelectedImage { get; set; }


        public List<SelectList> ImagenesSeleccionadas { get; set; }


        public IEnumerable<FoodImages> ImagenesStock { get; set; }


        public IEnumerable<Categorias> CategoriasStock { get; set; }


        public IEnumerable<Tipos> TiposStock { get; set; }


        public string[] ImagenesProducto { get; set; }

    }
}