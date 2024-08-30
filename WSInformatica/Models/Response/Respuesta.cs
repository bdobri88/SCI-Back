namespace WSInformatica.Models.Response
{
    public class Respuesta
    {
        public int Exito { get; set; }
        public string Mensaje { get; set; }
        public object data { get; set; }

        public Respuesta()
        { this.Exito = 0; }
    }
}
