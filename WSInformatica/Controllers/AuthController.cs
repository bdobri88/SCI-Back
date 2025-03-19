using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WSInformatica.Models;

namespace WSInformatica.Controllers
{

    
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly InfoContext _context;

        public AuthController(InfoContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            var kindeUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var name = User.FindFirst(ClaimTypes.Name)?.Value;

            if (kindeUserId == null)
            {
                return Unauthorized("No se pudo obtener información del usuario.");
            }

            // Buscar si el usuario ya existe en Efectivo
            var efectivo = await _context.Efectivo
                 .FirstOrDefaultAsync(e => e.Legajo.ToString() == kindeUserId);

            if (efectivo == null)
            {
                return NotFound("Este usuario no está registrado como efectivo.");
            }

            return Ok(new
            {
                Legajo = efectivo.Legajo,
                Nombre = efectivo.Nombre,
                Apellido = efectivo.Apellido
            });
        }

    }
}
