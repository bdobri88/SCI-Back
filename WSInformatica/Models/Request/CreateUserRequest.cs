using System.ComponentModel.DataAnnotations;

namespace WSInformatica.Models.Request
{
    public class CreateUserRequest
    {
        public int Legajo { get; set; } 
        public string Password { get; set; }        
        public bool EsAdmin { get; set; }
    }
}
