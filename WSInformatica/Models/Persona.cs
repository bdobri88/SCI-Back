

namespace WSInformatica.Models
{
    public partial class Persona
    {
        public int Id { get; set; }
        public int ConsultaId { get; set; }
        public int? Dni { get; set; }
        public string? Nombre1 { get; set; }
        public string? Nombre2 { get; set; }
        public string? Apellido1 { get; set; }
        public string? Apellido2 { get; set; }
        public int? Clase { get; set; }
        public bool Resultado { get; set; } = false;
    }
}
