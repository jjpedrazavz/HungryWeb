using System;
using System.Collections.Generic;

namespace HungryWeb.Models
{
    public partial class FoodImages
    {
        public FoodImages()
        {
            FoodImageMapping = new HashSet<FoodImageMapping>();
        }

        public int Id { get; set; }
        public string NameFile { get; set; }

        public virtual ICollection<FoodImageMapping> FoodImageMapping { get; set; }
    }
}
