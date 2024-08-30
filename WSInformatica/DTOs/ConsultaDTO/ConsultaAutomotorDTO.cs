using WSInformatica.Models;

namespace WSInformatica.DTOs.ConsultaDTO
{
    public class ConsultaAutomotorDTO
    {        
        public int? ConsultaId { get; set; }        
        public string? Dominio { get; set; }
        public string? Chasis { get; set; }
        public string? Motor { get; set; }
        public int? TipoAutomotor { get; set; }
        public bool? Resultado { get; set; }
    }
    
}
