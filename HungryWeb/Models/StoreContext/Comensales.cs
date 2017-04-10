using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HungryWeb.Models
{
    public partial class Comensales
    {
        public Comensales()
        {
            Ordenes = new HashSet<Ordenes>();
        }

        public int ComensalId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public virtual ICollection<Ordenes> Ordenes { get; set; }
    }
}
