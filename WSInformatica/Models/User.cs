using System;
using System.Collections.Generic;

namespace WSInformatica.Models
{
    public partial class User
    { 
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int? IdDependencia { get; set; }
        public int Legajo { get; set; }
    }
}
