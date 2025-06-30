namespace WSInformatica.Models.Response
{
    public class UserResponse
    {
        public int Legajo { get; set; }
        public string Token { get; set; }
        public bool EsAdmin { get; set; }
        public int IdEfectivo { get; set; }
    }
}
