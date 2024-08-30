namespace WSInformatica.Models.Request
{
    public class ArmaRequest
    {
        public int Id { get; set; }
        public string? NumArma { get; set; } 
        public string? Marca { get; set; }
        public string? Tipo { get; set; }
        public int? Calibre { get; set; }
    }
}
