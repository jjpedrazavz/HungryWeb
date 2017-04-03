namespace HungryWeb.Models3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        public int MenuID { get; set; }

        public int? OrdenID { get; set; }

        public int? sopaID { get; set; }

        public int? platoFuerteID { get; set; }

        public int? bebidaID { get; set; }

        public int? postreID { get; set; }

        public int? complementoID { get; set; }

        public int? bocadilloID { get; set; }

        public virtual Alimentos sopa { get; set; }

        public virtual Alimentos platoFuerte { get; set; }

        public virtual Alimentos bebida { get; set; }

        public virtual Alimentos postre { get; set; }

        public virtual Alimentos complemento { get; set; }

        public virtual Alimentos bocadillo { get; set; }

        public virtual Ordenes Ordenes { get; set; }
    }
}
