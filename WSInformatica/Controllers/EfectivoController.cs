using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WSInformatica.DTOs.EfectivoDTO;
using WSInformatica.Models;
using WSInformatica.Models.Request;
using WSInformatica.Models.Response;

namespace WSInformatica.Controllers
{
    [Route("api/Efectivo")]
    [ApiController]
    // [Authorize]

    public class EfectivoController : ControllerBase
    {
        private readonly InfoContext _context;
        private readonly IMapper _mapper;

        public EfectivoController(InfoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("FindEfectivos")] //Lo utilizo para traer todos los registros
        public async Task<ActionResult<BaseResponse<List<GetAllEfectivoDTO>>>> Get()
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {

                // Incluye la entidad Dependencia en la consulta
                var lst = await _context.Efectivo
                    .Include(e => e.Dependencia) // Incluir la dependencia relacionada
                    .ToListAsync();

                // Mapear la lista de Efectivos a una lista de GetAllEfectivoDTO
                var efectiveDTOList = lst.Select(e => new GetAllEfectivoDTO
                {
                    Id = e.Id,
                    Legajo = e.Legajo,
                    Nombre = e.Nombre,
                    Apellido = e.Apellido,
                    IdDependencia = e.IdDependencia,
                    NombreDependencia = e.Dependencia != null ? e.Dependencia.Nombre : null // Mapear el nombre de la dependencia
                }).ToList();

                oRespuesta.data = efectiveDTOList;
                oRespuesta.Exito = 1;
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpGet("FindEfectivo")]
        public async Task<ActionResult<GetAllEfectivoDTO>> FindEfectivo([FromQuery] int legajo)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                //var oEfectivo = await _context.Efectivos.Include(e => e.Dependencia).FirstOrDefaultAsync(e => e.Legajo == legajo);

                var oEfectivo = await _context.Efectivo
                .Where(e => e.Legajo == legajo)
                .Include(e => e.Dependencia)
                .Select(e => new GetAllEfectivoDTO
                {
                    Id = e.Id,
                    Legajo = e.Legajo,
                    Nombre = e.Nombre,
                    Apellido = e.Apellido,
                    IdDependencia = e.IdDependencia,
                    NombreDependencia = e.Dependencia != null ? e.Dependencia.Nombre : null
                })
                .FirstOrDefaultAsync();
                

                if (oEfectivo == null)
                {
                    return Ok(oRespuesta);
                }

                oRespuesta.Exito = 1;
                // var dependencEfectivo = oEfectivo.Dependencia.Nombre;

                oRespuesta.data = _mapper.Map<GetAllEfectivoDTO>(oEfectivo);

                return Ok(oRespuesta);
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
                throw;
            }
        }

        [HttpPost("CreateEfectivo")] //Insertar Un Efectivo
        public async Task<ActionResult<BaseResponse<bool>>> add([FromBody] EfectivoCreateDTO model)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                var efectivo = await _context.Efectivo.FirstOrDefaultAsync(d => d.Legajo == model.Legajo);
                if (efectivo is not null)
                    return BadRequest($"Ya existe un efectivo con legajo: {efectivo.Legajo} ");

                efectivo = _mapper.Map<Efectivo>(model);
                _context.Efectivo.Add(efectivo);
                await _context.SaveChangesAsync();

                oRespuesta.Exito = 1;
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpPut("Update")] //Modificar Efectivo
        public async Task<ActionResult<BaseResponse<bool>>> Edit([FromBody] EfectivoUpdateDTO model)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                var exist = await _context.Efectivo.AnyAsync(x => x.Legajo == model.Legajo);
                if (!exist)
                    return BadRequest($"No existe un efectivo con legajo: {model.Legajo} ");

                Efectivo efectivo = _mapper.Map<Efectivo>(model);
                _context.Update(efectivo);
                await _context.SaveChangesAsync();
                oRespuesta.Exito = 1;
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<BaseResponse<bool>>> Delete(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                Efectivo oEfectivo = await _context.Efectivo.FindAsync(id);
                if (oEfectivo == null)
                {
                    return NotFound($"El efectivo que quiere eliminar no existe.");
                }
                _context.Efectivo.Remove(oEfectivo);
                await _context.SaveChangesAsync();
                oRespuesta.Exito = 1;
                return Ok(oRespuesta);
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
                return StatusCode(500, oRespuesta);
            }
        }
    }
}
