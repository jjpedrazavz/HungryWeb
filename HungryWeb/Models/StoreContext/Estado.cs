namespace HungryWeb.Models3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Estado")]
    public partial class Estado
    {
        public Estado()
        {
            Ordenes = new HashSet<Ordenes>();
        }

        public int EstadoID { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name ="Estatus")]
        public string Descripcion { get; set; }


        public virtual ICollection<Ordenes> Ordenes { get; set; }
    }
}
