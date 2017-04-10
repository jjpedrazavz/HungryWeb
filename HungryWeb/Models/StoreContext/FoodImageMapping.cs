using System;
using System.Collections.Generic;

namespace HungryWeb.Models
{
    public partial class FoodImageMapping
    {
        public int Id { get; set; }
        public int? AlimentosId { get; set; }
        public int? ImageNumber { get; set; }
        public int? AlimentosImageId { get; set; }

        public virtual Alimentos Alimentos { get; set; }
        public virtual FoodImages AlimentosImage { get; set; }
    }
}
