namespace WSInformatica.DTOs.ConsultaDTO
{
    public class ConsultasDTO
    {
        public int Id { get; set; }
        public int IdDespachante { get; set; }
        public int IdSolicitante { get; set; }
        public int Movil { get; set; }
        public string? Lugar { get; set; }
        public int IdJuridiccion { get; set; }
        public string? DependenciaNombre { get; set; }
        public DateTime Fecha { get; set; }
    }
}
