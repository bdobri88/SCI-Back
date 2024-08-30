using WSInformatica.Models.Request;
using WSInformatica.Models.Response;

namespace WSInformatica.Services
{
    public interface IUserService//Uso interface por si en un futuro deciden //cambiar la forma de autentificarse,por ejem utilizar un servicio de autentificacion
    {
        UserResponse Auth(AuthRequest model);        
    }
}
