using System;
using System.Collections.Generic;

namespace WSInformatica.Models
{
    public partial class Dependencia
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Efectivo> Efectivos { get; set; } = new List<Efectivo>();
    }
}
