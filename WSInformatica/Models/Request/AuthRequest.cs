using System.ComponentModel.DataAnnotations;

namespace WSInformatica.Models.Request
{
    public class AuthRequest
    {
        [Required]
        public int Legajo { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
