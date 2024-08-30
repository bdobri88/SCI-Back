namespace WSInformatica.Models.Request
{
    public class AutomotorRequest
    {
        public int Id { get; set; }
        public int? TipoAutomotor { get; set; }
        public string? Dominio { get; set; }
        public string? Chasis { get; set; }
        public string? Motor { get; set; }
    }
}
