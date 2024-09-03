using System.ComponentModel.DataAnnotations;

namespace WSInformatica.DTOs.EfectivoDTO
{
    public class EfectivoUpdateDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Legajo { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? Apellido { get; set; }
        public int? IdDependencia { get; set; }
    }
}
