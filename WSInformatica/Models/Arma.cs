using System;
using System.Collections.Generic;

namespace WSInformatica.Models
{
    public partial class Arma
    {
        public int Id { get; set; }
        public int? ConsultaId { get; set; }
        public string NumArma { get; set; } = null!;
        public string? Marca { get; set; }
        public string? Tipo { get; set; }
        public int? Calibre { get; set; }
        public bool Resultado { get; set; }
        
    }
}
