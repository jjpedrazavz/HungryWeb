namespace HungryWeb.Models3
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FoodImages
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FoodImages()
        {
            FoodImageMapping = new HashSet<FoodImageMapping>();
        }

        public int ID { get; set; }

        [StringLength(120)]
        public string NameFile { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FoodImageMapping> FoodImageMapping { get; set; }
    }
}
