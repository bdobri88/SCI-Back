
using System.ComponentModel.DataAnnotations;

namespace WSInformatica.Models
{
    public partial class Efectivo
    {
        [Key]
        public int Id { get; set; }
        public int Legajo { get; set; }
        public string Nombre { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public int? IdDependencia { get; set; }

        // Propiedad de navegación hacia Dependencia
        public virtual Dependencia Dependencia { get; set; }

        // Esta propiedad representa la relación uno-a-uno o uno-a-cero con la tabla User.
        public virtual User User { get; set; } 
    }
}
