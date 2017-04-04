namespace HungryWeb.Models3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FoodImageMapping")]
    public partial class FoodImageMapping
    {
        public int ID { get; set; }

        public int? AlimentosID { get; set; }

        public int ImageNumber { get; set; }

        public int? AlimentosImageID { get; set; }

        public virtual Alimentos Alimentos { get; set; }

        public virtual FoodImages FoodImages { get; set; }
    }
}
