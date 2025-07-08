using Microsoft.AspNetCore.Mvc;
using WSInformatica.Models.Request;
using WSInformatica.Models.Response;
using WSInformatica.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WSInformatica.DTOs.DependenciaDTO;
using WSInformatica.DTOs.AutomotoresDTO;

namespace WSInformatica.Controllers
{
    [Route("api/Automotor")]
    [ApiController]
    public class AutomotorController : ControllerBase
    {
        private readonly InfoContext _context;
        private readonly IMapper _mapper;

        public AutomotorController(IMapper mapper, InfoContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet] //Muestro Automotores
        public async Task<ActionResult<BaseResponse<Automotor>>> Get()
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                var lst = _context.Automotor.OrderByDescending(d => d.Id).ToList(); //Busco Todos los registros de forma ordena y desendiente por id
                oRespuesta.Exito = 1;
                oRespuesta.data = lst;
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
        [HttpPost] //Insertar Un Automotores
        public async Task<ActionResult<BaseResponse<bool>>> add([FromBody] AutomotorRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                var oAutomotor = _mapper.Map<Automotor>(oModel);
                _context.Add(oAutomotor);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok();
        }

        [HttpPut] //Modificar Automotores
        public async Task<ActionResult<BaseResponse<bool>>> Edit(AutomotorRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                Automotor oAutomotor = await _context.Automotor.FindAsync(oModel.Id);
                oAutomotor.TipoAutomotorId = oModel.TipoAutomotor;
                oAutomotor.Dominio = oModel.Dominio;
                oAutomotor.Chasis = oModel.Chasis;
                oAutomotor.Motor = oModel.Motor;

                _context.Entry(oAutomotor).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                oRespuesta.Exito = 1;
                oRespuesta.data = oAutomotor;
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
                Automotor oAutomotor = await _context.Automotor.FindAsync(Id);
                _context.Remove(oAutomotor);
                _context.SaveChanges();
                oRespuesta.Exito = 1;
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpGet("FindTipoAutomotores")]
        public async Task<ActionResult<BaseResponse<GetAllTipoAutomotoresDTO>>> GetTipoAutomotores()
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                var lst = await _context.TipoAutomotor.ToListAsync();
                oRespuesta.Exito = 1;
                oRespuesta.data = lst;
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);
        }
    }

}

