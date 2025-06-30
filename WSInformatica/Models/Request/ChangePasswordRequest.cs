namespace WSInformatica.Models.Request
{
    public class ChangePasswordRequest
    {
        public int IdUsuario { get; set; } 
        public string OldPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
    }
}
