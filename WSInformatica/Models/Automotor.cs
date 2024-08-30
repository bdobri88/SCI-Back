
namespace WSInformatica.Models
{
    public partial class Automotor
    {
        public int Id { get; set; }
        public int ConsultaId { get; set; }
        public int? TipoAutomotor { get; set; }
        public string? Dominio { get; set; }
        public string? Chasis { get; set; }
        public string? Motor { get; set; }
        public bool Resultado { get; set; }
    }
}
