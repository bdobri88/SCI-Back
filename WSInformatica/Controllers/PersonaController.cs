using Microsoft.AspNetCore.Mvc;
using WSInformatica.Models.Request;
using WSInformatica.Models.Response;
using WSInformatica.Models;

namespace WSInformatica.Controllers
{
    [Route("api/Persona")]
    [ApiController]

    public class PersonaController : ControllerBase
    {
        private readonly InfoContext _context;

        public PersonaController(InfoContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<BaseResponse<Persona>>> Get()
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;

            try
            {
                var lst = _context.Personas.OrderByDescending(d => d.Id).ToList(); //Busco Todos los registros de forma ordena y desendiente por id
                oRespuesta.Exito = 1;
                oRespuesta.data = lst;
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
        [HttpPost] //Insertar Un PersonPersona

        public async Task<ActionResult<BaseResponse<bool>>> add(PersonaRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                Persona oPersona = new Persona();

                oPersona.Dni = oModel.Dni;
                oPersona.Nombre1 = oModel.Nombre1;
                oPersona.Nombre2 = oModel.Nombre2;
                oPersona.Apellido1 = oModel.Apellido1;
                oPersona.Apellido2 = oModel.Apellido2;
                oPersona.Clase = oModel.Clase;

                _context.Personas.Add(oPersona);
                _context.SaveChanges();
                oRespuesta.Exito = 1;
                oRespuesta.data = oPersona;
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpPut] //Modificar Persona
        public async Task<ActionResult<BaseResponse<bool>>> Edit(PersonaRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                Persona oPersona = await _context.Personas.FindAsync(oModel.Id);
                oPersona.Dni = oModel.Dni;
                oPersona.Nombre1 = oModel.Nombre1;
                oPersona.Nombre2 = oModel.Nombre2;
                oPersona.Apellido1 = oModel.Apellido1;
                oPersona.Apellido2 = oModel.Apellido2;
                oPersona.Clase = oModel.Clase;

                _context.Entry(oPersona).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                oRespuesta.Exito = 1;
                oRespuesta.data = oPersona;
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpDelete("Delete")] //Eliminar por ID
        public async Task<ActionResult<BaseResponse<bool>>> Delete(int Id)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                Persona Opersona = await _context.Personas.FindAsync(Id);
                _context.Remove(Opersona);
                _context.SaveChanges();
                oRespuesta.Exito = 1;
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
    }
}
