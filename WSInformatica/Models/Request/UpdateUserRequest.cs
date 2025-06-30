namespace WSInformatica.Models.Request
{
    public class UpdateUserRequest
    {
        public string? NewPassword { get; set; } 
        public bool? NewIsAdmin { get; set; } 
    }
}
