namespace HungryWeb.Models3
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Alimentos
    {
      public Alimentos()
        {
            FoodImageMapping = new HashSet<FoodImageMapping>();
            Menu = new HashSet<Menu>();
            Menu1 = new HashSet<Menu>();
            Menu2 = new HashSet<Menu>();
            Menu3 = new HashSet<Menu>();
            Menu4 = new HashSet<Menu>();
            Menu5 = new HashSet<Menu>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(80)]
        public string Nombre { get; set; }

        public int CategoriaId { get; set; }

        [Column(TypeName = "numeric")]
        public decimal Precio { get; set; }

        public int TipoId { get; set; }

        public virtual Categorias Categoria { get; set; }

        public virtual ICollection<FoodImageMapping> FoodImageMapping { get; set; }

        [JsonIgnore]
        public virtual ICollection<Menu> Menu { get; set; }

        [JsonIgnore]
        public virtual ICollection<Menu> Menu1 { get; set; }

        [JsonIgnore]
        public virtual ICollection<Menu> Menu2 { get; set; }

        [JsonIgnore]
        public virtual ICollection<Menu> Menu3 { get; set; }

        [JsonIgnore]
        public virtual ICollection<Menu> Menu4 { get; set; }

        [JsonIgnore]
        public virtual ICollection<Menu> Menu5 { get; set; }

        public virtual Tipos Tipo { get; set; }
    }
}
