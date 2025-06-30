using Azure.Core;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<BaseResponse<bool>>> Autentificar([FromBody] AuthRequest model) 
        {
            Respuesta respuesta = new Respuesta();
            try
            {  
                var userResponse = _userService.Auth(model);


                if (userResponse == null)
                {
                    respuesta.Exito = 0;
                    respuesta.Mensaje = "Usuario o contraseña incorrecta.";
                    return BadRequest(respuesta); 
                }

                respuesta.Exito = 1;
                respuesta.Mensaje = "Login exitoso";
                respuesta.data = userResponse;
                return Ok(respuesta);
            }
            catch (KeyNotFoundException ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = ex.Message;

                return BadRequest(respuesta);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error interno del servidor en Autentificar: {ex.Message}");

                respuesta.Exito = 0;
                respuesta.Mensaje = "Ocurrió un error interno en el servidor. Por favor, inténtelo de nuevo más tarde.";
                return StatusCode(500, respuesta); 
            }
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")] 
        public async Task<ActionResult<Respuesta>> CreateUser([FromBody] CreateUserRequest request)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                _userService.CreateUser(request);

                respuesta.Exito = 1;
                respuesta.Mensaje = "Usuario creado exitosamente.";
                respuesta.data = true; 

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = ex.Message;
                return BadRequest(respuesta);
            }
        }


        [Authorize] 
        [HttpPost("ChangePassword")]
        public async Task<ActionResult<Respuesta>> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                _userService.ChangePassword(request);

                respuesta.Exito = 1;
                respuesta.Mensaje = "Contraseña cambiada exitosamente.";
                respuesta.data = true;

                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = ex.Message;
                respuesta.data = false;
                return BadRequest(respuesta);
            }
        }



        [HttpPut("update/{userId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Respuesta>> UpdateUser(int userId, [FromBody] UpdateUserRequest model)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                var updatedUser = _userService.UpdateUser(userId, model.NewPassword, model.NewIsAdmin);
                respuesta.Exito = 1;
                respuesta.Mensaje = "Usuario actualizado exitosamente.";
                return Ok(respuesta);
            }
            catch (KeyNotFoundException ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = ex.Message;
                return NotFound(respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Error al actualizar el usuario.";
                return StatusCode(500, respuesta);
            }
        }

        [HttpGet("all")] 
        [Authorize(Roles = "Admin")] 
        public ActionResult<Respuesta> GetAllUsers()
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                var users = _userService.GetAllUsers();
                respuesta.Exito = 1;
                respuesta.Mensaje = "Usuarios cargados exitosamente.";
                respuesta.data = users;
                return Ok(respuesta);
            }
            catch (Exception ex)
            {
                respuesta.Exito = 0;
                respuesta.Mensaje = "Error al obtener la lista de usuarios.";

                return StatusCode(500, respuesta);
            }
        }
    }
}
