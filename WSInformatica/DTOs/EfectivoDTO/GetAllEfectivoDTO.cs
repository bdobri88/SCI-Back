using System.ComponentModel.DataAnnotations;

namespace WSInformatica.DTOs.EfectivoDTO
{
    public class GetAllEfectivoDTO
    {     
        public int Id { get; set; }        
        public int Legajo { get; set; }        
        public string? Nombre { get; set; }        
        public string? Apellido { get; set; }
        public int? IdDependencia { get; set; }
        public string? NombreDependencia { get; set; }
    }
}
