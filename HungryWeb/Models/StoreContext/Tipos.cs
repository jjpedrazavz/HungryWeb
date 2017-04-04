namespace HungryWeb.Models3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tipos
    {
        
        public Tipos()
        {
            Alimentos = new HashSet<Alimentos>();
        }

        [Key]
        public int TipoID { get; set; }

        [Required]
        [StringLength(80)]
        [Display(Name ="Tipo")]
        public string Nombre { get; set; }

        
        public virtual ICollection<Alimentos> Alimentos { get; set; }
    }
}
