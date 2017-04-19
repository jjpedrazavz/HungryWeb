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

        //se usar para especificar el numero de imagenes a adjuntar por producto
        public static int NumeroImagenes = 1;

        public static SelectList CantidadAlimentos = new SelectList(new []{ 1, 2, 3, 4, 5 });


    }
}