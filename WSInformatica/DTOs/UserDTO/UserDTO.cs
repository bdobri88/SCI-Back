namespace WSInformatica.DTOs.UserDTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Legajo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; } 
        public bool IsAdmin { get; set; }
    }
}
