using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HungryWeb.Models
{
    public partial class FoodImages
    {
        public FoodImages()
        {
            FoodImageMapping = new HashSet<FoodImageMapping>();
        }

        public int Id { get; set; }

        [Display(Name = "File")]
        [StringLength(100)]
        [Index(IsUnique = true)]
        public string NameFile { get; set; }

        public virtual ICollection<FoodImageMapping> FoodImageMapping { get; set; }
    }
}
