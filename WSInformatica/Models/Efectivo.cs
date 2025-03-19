
namespace WSInformatica.Models
{
    public partial class Efectivo
    {
        public int Id { get; set; }
        public int Legajo { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;

        public int? IdDependencia { get; set; }

        // Propiedad de navegación hacia Dependencia
        public virtual Dependencia Dependencia { get; set; }
    }
}
