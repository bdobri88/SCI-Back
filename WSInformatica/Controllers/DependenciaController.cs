using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WSInformatica.DTOs.DependenciaDTO;
using WSInformatica.DTOs.EfectivoDTO;
using WSInformatica.Models;
using WSInformatica.Models.Request;
using WSInformatica.Models.Response;

namespace WSInformatica.Controllers
{
    [Route("api/Dependencia")]
    [ApiController]
    public class DependenciaController : ControllerBase
    {
        private readonly InfoContext _context;
        private readonly IMapper _mapper;

        public DependenciaController(InfoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet("Find")]
        public async Task<ActionResult<BaseResponse<GetAllDependenciaDTO>>> Get()
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                var lst = _context.Dependencia.OrderByDescending(d => d.Id).ToList(); //Busco Todos los registros de forma ordena y desendiente por id
                oRespuesta.Exito = 1;
                oRespuesta.data = _mapper.Map<List<GetAllDependenciaDTO>>(lst); ;
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);
        }

        [HttpPost("Add")]
        public async Task<ActionResult<BaseResponse<bool>>> Add([FromBody]DependenciaRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                Dependencia oDependencia = new Dependencia();
                oDependencia.Nombre = oModel.Nombre;
                _context.Dependencia.Add(oDependencia);
                _context.SaveChanges();
                oRespuesta.Exito = 1;
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpPut("Edit")]
        public async Task<ActionResult<BaseResponse<bool>>> Edit([FromBody] DependenciaRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                Dependencia oDependencia = await _context.Dependencia.FindAsync(oModel.Id);
                if (oDependencia is null)
                    BadRequest($"No Se encontro la dependencia{oDependencia.Nombre}");

                oDependencia.Nombre = oModel.Nombre;
                _context.Entry(oDependencia).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                oRespuesta.Exito = 1;
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete([FromBody] int id)
        {
            Respuesta oRepuesta = new Respuesta();
            oRepuesta.Exito = 0;
            try
            {
                Dependencia oDependencia = await _context.Dependencia.FindAsync(id);
                if (oDependencia is null)
                   BadRequest($"No se encontro la dependencia{oDependencia.Nombre}");
                _context.Dependencia.Remove(oDependencia);
                _context.SaveChanges();
                oRepuesta.Exito = 1;
            }
            catch (Exception ex)
            {
                oRepuesta.Mensaje = ex.Message;
            }
            return Ok(oRepuesta);
        }
    }
}
