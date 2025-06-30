using System.ComponentModel.DataAnnotations;
using WSInformatica.Models;
using WSInformatica.Models.Request;

namespace WSInformatica.DTOs.ConsultaDTO
{
    public class CreateConsultaDTO
    {
        public int IdDespachante { get; set; }
        public int IdSolicitante { get; set; }        
        public int? Movil { get; set; }
        public string? Lugar { get; set; }
        public int IdJuridiccion { get; set; }
        public DateTime Fecha { get; set; }

        public List<ConsultaPersonaDTO>? ListPersona { get; set; }
        public List<ConsultaAutomotorDTO>? ListAutomotor { get; set; }
        public List<ConsultaArmaDTO>? ListArma { get; set; }
    }
}
