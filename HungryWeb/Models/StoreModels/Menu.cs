namespace HungryWeb.Models2
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

        public virtual Alimentos Alimentos { get; set; }

        public virtual Alimentos Alimentos1 { get; set; }

        public virtual Alimentos Alimentos2 { get; set; }

        public virtual Alimentos Alimentos3 { get; set; }

        public virtual Alimentos Alimentos4 { get; set; }

        public virtual Alimentos Alimentos5 { get; set; }

        public virtual Ordenes Ordenes { get; set; }
    }
}
