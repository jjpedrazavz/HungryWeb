namespace HungryWeb.Models2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comensales
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comensales()
        {
            Ordenes = new HashSet<Ordenes>();
        }

        [Key]
        public int ComensalID { get; set; }

        [Required]
        [StringLength(80)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(80)]
        public string Apellido { get; set; }

        [Required]
        [StringLength(200)]
        public string Email { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ordenes> Ordenes { get; set; }
    }
}
