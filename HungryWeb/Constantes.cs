using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HungryWeb
{
    public static class Constantes
    {
        //rutas fijas para localizar las imagenes en cada producto.
        public static string ImagePathDefault = "~/Content/ImagenesProducto/";
        public static string ImagePathSopas = "~/Content/ImagenesProducto/Sopas/";
        public static string ImagePathPlatosFuertes = "~/Content/ImagenesProducto/PlatosFuertes/";
        public static string ImagePathPostres = "~/Content/ImagenesProducto/Postres/";
        public static string ImagePathBebidas = "~/Content/ImagenesProducto/Bebidas/";
        public static string ImagePathComplementos = "~/Content/ImagenesProducto/Complementos/";
        public static string ImagePathbocadillos = "~/Content/ImagenesProducto/Bocadillos/";

        //se usar para especificar el numero de imagenes a adjuntar por producto
        public static int NumeroImagenes = 1;

        public static SelectList CantidadAlimentos = new SelectList(new []{ 1, 2, 3, 4, 5 });


    }
}