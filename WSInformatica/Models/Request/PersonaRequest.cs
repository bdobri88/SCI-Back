namespace WSInformatica.Models.Request
{
    public class PersonaRequest
    {
        public int Id { get; set; }
        public int? Dni { get; set; }
        public string? Nombre1 { get; set; }
        public string? Nombre2 { get; set; }
        public string? Apellido1 { get; set; }
        public string? Apellido2 { get; set; }
        public int? Clase { get; set; }
    }
}
