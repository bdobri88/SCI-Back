using Microsoft.AspNetCore.Mvc;
using WSInformatica.Models.Response;
using WSInformatica.Models;
using WSInformatica.Models.Request;

namespace WSInformatica.Controllers
{
    [Route("api/Arma")]
    [ApiController]
    public class ArmaController : ControllerBase
    {
        private InfoContext _context;

        public ArmaController(InfoContext context)
        {
            _context = context;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<BaseResponse<Arma>>> Get()
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;

            try
            {
                var lst = _context.Arma.OrderByDescending(d => d.Id).ToList(); //Busco Todos los registros de forma ordena y desendiente por id
                oRespuesta.Exito = 1;
                oRespuesta.data = lst;
                if (lst == null)
                {
                    BadRequest("No hay efectivos en la Base de datos");
                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpPost] //Insertar Un Arma
        public async Task<ActionResult<BaseResponse<bool>>> add(ArmaRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;

            try
            {
                var oArma = new Arma();
                oArma.NumArma = oModel.NumArma;
                oArma.Marca = oModel.Marca;
                oArma.Tipo = oModel.Tipo;
                oArma.Calibre = oModel.Calibre;
                _context.Arma.Add(oArma);
                _context.SaveChanges();
                oRespuesta.Exito = 1;
                oRespuesta.data = oArma;
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpPut("Update")] //Modificar Arma
        public async Task<ActionResult<BaseResponse<bool>>> Edit(ArmaRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();

            try
            {
                Arma oArma = await _context.Arma.FindAsync(oModel.Id);
                oArma.NumArma = oModel.NumArma;
                oArma.Marca = oModel.Marca;
                oArma.Tipo = oModel.Tipo;
                oArma.Calibre = oModel.Calibre;

                _context.Entry(oArma).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                oRespuesta.Exito = 1;
                oRespuesta.data = oArma;

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
                Arma oArma = await _context.Arma.FindAsync(Id);
                _context.Remove(oArma);
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

