using System;
using System.Collections.Generic;

namespace WSInformatica.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Password { get; set; } = null!;
        public int? IdEfectivo { get; set; } 
        public bool EsAdmin { set; get; }
        public virtual Efectivo Efectivo { get; set; }
    }
}
