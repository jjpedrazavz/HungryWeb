namespace HungryWeb.Models3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Categorias
    {
        public Categorias()
        {
            Alimentos = new HashSet<Alimentos>();
        }

        [Key]
        public int CategoriaID { get; set; }

        [Required]
        [StringLength(80)]
        [Display(Name = "Categoria")]
        public string Nombre { get; set; }

        public virtual ICollection<Alimentos> Alimentos { get; set; }
    }
}
