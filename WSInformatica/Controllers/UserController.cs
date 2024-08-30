using Microsoft.AspNetCore.Mvc;
using WSInformatica.Models.Request;
using WSInformatica.Models.Response;
using WSInformatica.Services;

namespace WSInformatica.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<BaseResponse<bool>>> Autentificar([FromBody] AuthRequest model) // el model se manda al body
        {
            Respuesta respuesta = new Respuesta();

            var useerresponse = _userService.Auth(model);

            if (useerresponse == null)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Usuario o contraseña incorrecta";
                return BadRequest(respuesta);
            }
            respuesta.Exito = 1;
            respuesta.data = useerresponse;
            return Ok(respuesta);
        }
    }
}
