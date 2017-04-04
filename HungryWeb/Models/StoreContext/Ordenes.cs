namespace HungryWeb.Models3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Ordenes
    {
        
        public Ordenes()
        {
            Menu = new HashSet<Menu>();
        }


        [Key]
        public int OrdenID { get; set; }

        public int ComensalID { get; set; }

        public int EstadoID { get; set; }

        public virtual Comensales Comensales { get; set; }

        public virtual Estado Estado { get; set; }

        public virtual ICollection<Menu> Menu { get; set; }
    }
}
