namespace HungryWeb.Models3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FoodImages
    {
        
        public FoodImages()
        {
            FoodImageMapping = new HashSet<FoodImageMapping>();
        }

        public int Id { get; set; }

        [StringLength(120)]
        [Index(IsUnique =true)] //lo marcamos con la anotacion para evitar nombres de archivo duplicados.
        public string NameFile { get; set; }

       
        public virtual ICollection<FoodImageMapping> FoodImageMapping { get; set; }
    }
}
