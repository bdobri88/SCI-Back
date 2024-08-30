using WSInformatica.Models;

namespace WSInformatica.DTOs.ConsultaDTO
{
    public class ConsultaPorDependenciaDTO
    {
        public int Id { get; set; }
        public int Movil { get; set; }
        public string Lugar { get; set; } = null!;
        public int Idjuridiccion { get; set; }        

        public virtual ICollection<Arma> Armas { get; set; }
        public virtual ICollection<Automotor> Automotors { get; set; }
        public virtual ICollection<Persona> Personas { get; set; }
    }
}
