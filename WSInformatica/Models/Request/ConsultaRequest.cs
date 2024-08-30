namespace WSInformatica.Models.Request
{
    public class ConsultaRequest
    {
         public int IdConsulta { get; set; }
        public int IdDespachante { get; set; }
        public int IdSolicitante { get; set; }
        public int Movil { get; set; }
        public string Lugar { get; set; }
        public int IdJuridiccion { get; set; }

  
    }
}
