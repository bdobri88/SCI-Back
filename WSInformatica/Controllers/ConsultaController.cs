using AutoMapper;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using WSInformatica.DTOs.ConsultaDTO;
using WSInformatica.Models;
using WSInformatica.Models.Request;
using WSInformatica.Models.Response;
using WSInformatica.Services;

namespace WSInformatica.Controllers
{
    [Route("api/Consulta")]
    [ApiController]
  //  [Authorize] //autorizacion por token

    public class ConsultaController : ControllerBase
    {
        private IConsultaService _consulta;
        private readonly IMapper _mapper;
        private readonly InfoContext _context;

        public ConsultaController(IConsultaService consul,IMapper mapper, InfoContext context)
        {
            this._consulta = consul;
            _mapper = mapper;
            _context = context;
        }

        [HttpPost("CrearConsulta")]
        public async Task<ActionResult<BaseResponse<bool>>> AddConsulta(CreateConsultaDTO model)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                var consulta = _mapper.Map<Consulta>(model);
                consulta.Fecha = DateTime.Now;
                _context.Consulta.Add(consulta);
                await _context.SaveChangesAsync();
                var idDeNuevaConsulta = consulta.Id;

                if (model.ListPersona != null)
                {
                    foreach (var lPersonas in model.ListPersona)
                    {
                        lPersonas.ConsultaId = idDeNuevaConsulta;
                        var persona = _mapper.Map<Persona> (lPersonas);
                        _context.Personas.Add(persona);
                        await _context.SaveChangesAsync();
                    }
                }
                if (model.ListAutomotor != null)
                {
                    foreach (var lAutomotor in model.ListAutomotor)
                    {
                        lAutomotor.ConsultaId = idDeNuevaConsulta;
                        var automotor = _mapper.Map<Automotor> (lAutomotor);
                        _context.Add(automotor);
                        await _context.SaveChangesAsync();
                    }
                }

                if (model.ListArma != null)
                {
                    foreach (var lArmas in model.ListArma)
                    {
                        lArmas.ConsultaId = idDeNuevaConsulta;
                        var arma = _mapper.Map<Arma> (lArmas);
                        _context.Add(arma);
                        await _context.SaveChangesAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                // Accede a la excepción interna para obtener más detalles sobre el error
                var innerException = ex.InnerException;
                // Puedes imprimir o registrar los detalles del error para su depuración
                Console.WriteLine(innerException.Message);
                // También puedes lanzar una excepción personalizada o manejar el error de acuerdo a tus necesidades
            }
            return Ok(oRespuesta);
        }


        [HttpGet("CantidadConsultasdiariasPorDependencia")]
        public async Task<ActionResult<List<ConsultaCantDiarioDTO>>> QuantityDailyConsultaByDependence([FromQuery] string? date )
        {
            Respuesta oRespuesta = new Respuesta();           
 
            DateTime datee = DateTime.ParseExact(date, "yyyy-MM-dd", null);
            
            try
            {
                var consultasPorDependencia = _context.Consulta
                                    .Where(consulta => consulta.Fecha.Date == datee)
                                    .Include(consulta => consulta.Armas)
                                    .Include(consulta => consulta.Personas)
                                    .Include(consulta => consulta.Automotors)
                                    .ToList()
                                    .GroupBy(consulta => consulta.Idjuridiccion)
                                    .ToList();

                var consultasConDetalles = consultasPorDependencia
                    .OrderByDescending(group => group.Count())
                    .Select(group => new
                    {
                        DependenciaId = group.Key,
                         NombreDependencia = _context.Dependencia
                        .Where(dependencia => dependencia.Id == group.Key)
                        .Select(dependencia => dependencia.Nombre)
                        .FirstOrDefault(),
                        CantArmas = group.Sum(consulta => consulta.Armas?.Count() ?? 0),
                        CantAutomotores = group.Sum(consulta => consulta.Automotors?.Count() ?? 0),
                        CantPersonas = group.Sum(consulta => consulta.Personas?.Count() ?? 0),
                        CantArmasPositivo = group.Sum(consulta => consulta.Armas?.Count(arma => arma.Resultado) ?? 0),
                        CantAutomotoresPositivo = group.Sum(consulta => consulta.Automotors?.Count(automotor => automotor.Resultado) ?? 0),
                        CantPersonasPositivo = group.Sum(consulta => consulta.Personas?.Count(persona => persona.Resultado) ?? 0),

                        CantConsultas = group.Sum(consulta => (consulta.Armas?.Count() ?? 0) +
                                                 (consulta.Automotors?.Count() ?? 0) +
                                                 (consulta.Personas?.Count() ?? 0)),

                        CantPositivos = group.Sum(consulta =>
                            (consulta.Armas?.Count(arma => arma.Resultado) ?? 0) +
                            (consulta.Automotors?.Count(automotor => automotor.Resultado) ?? 0) +
                            (consulta.Personas?.Count(persona => persona.Resultado) ?? 0))

                    }).ToList();                


                if (consultasConDetalles.Count == 0)
                {
                    oRespuesta.Mensaje = "No se encontraron consultas el dia indicado";
                    return Ok(oRespuesta);
                }

                oRespuesta.Exito = 1;
                oRespuesta.data = consultasConDetalles;                

            }
            catch (Exception ex)
            {    oRespuesta.Mensaje = ex.Message;    }

            return Ok(oRespuesta);
        }

        [HttpGet("totalConsultasDiarias")]
        public async Task<ActionResult<ConsultaCantTotalDiarioDTO>> QuantityDailyConsulta([FromQuery] string date)
        {
            Respuesta oRespuesta = new Respuesta();

         //   string dateStr = "2024-02-06"; //modificar sin harcodear
            DateTime datee = DateTime.ParseExact(date, "yyyy-MM-dd", null);

            try
            {
                ConsultaCantTotalDiarioDTO totalConsultas = new ConsultaCantTotalDiarioDTO();

                var cantidadConsultas = _context.Consulta
                    .Where(consulta => consulta.Fecha.Date == datee)
                    .Include(consulta => consulta.Armas)
                    .Include(consulta => consulta.Personas)
                    .Include(consulta => consulta.Automotors)
                    .ToList();
                 

                totalConsultas.CantArmas = cantidadConsultas.Sum(consulta => consulta.Armas.Count);
                totalConsultas.CantPersonas = cantidadConsultas.Sum(consulta => consulta.Personas.Count);
                totalConsultas.CantAutomotores = cantidadConsultas.Sum(consulta => consulta.Automotors.Count);

                totalConsultas.CantArmasPositivo = cantidadConsultas.Sum(consulta => consulta.Armas.Count(arma => arma.Resultado));
                totalConsultas.CantPersonasPositivo = cantidadConsultas.Sum(consulta => consulta.Personas.Count(persona => persona.Resultado));
                totalConsultas.CantAutomotoresPositivo = cantidadConsultas.Sum(consulta => consulta.Automotors.Count(automotor => automotor.Resultado));

                totalConsultas.CantConsultas = totalConsultas.CantArmas + totalConsultas.CantPersonas + totalConsultas.CantAutomotores;
                totalConsultas.CantPositivos = totalConsultas.CantArmasPositivo + totalConsultas.CantPersonasPositivo + totalConsultas.CantAutomotoresPositivo;

                if (cantidadConsultas.Count == 0)
                {
                    oRespuesta.Mensaje = "No se encontraron consultas el dia indicado";
                    return Ok(oRespuesta);
                }


                oRespuesta.Exito = 1;
                oRespuesta.data = totalConsultas;

            }
            catch (Exception ex)
            { oRespuesta.Mensaje = ex.Message; }

            return Ok(oRespuesta);
        }

        [HttpGet("totalConsultasMensuales")]
        public async Task<ActionResult<ConsultaCantTotalDiarioDTO>> QuantityMonthConsulta([FromQuery] string date)
        {
            Respuesta oRespuesta = new Respuesta();
            DateTime dateD = DateTime.ParseExact(date, "yyyy-MM", null);
            var month = dateD.Month; 
            var year = dateD.Year; // Cambiar al año deseado

            try
            {
                ConsultaCantTotalDiarioDTO totalConsultas = new ConsultaCantTotalDiarioDTO();

                var cantidadConsultas = _context.Consulta
                    .Where(consulta => consulta.Fecha.Month == month && consulta.Fecha.Year == year)
                    .Include(consulta => consulta.Armas)
                    .Include(consulta => consulta.Personas)
                    .Include(consulta => consulta.Automotors)
                    .ToList();

                totalConsultas.CantArmas = cantidadConsultas.Sum(consulta => consulta.Armas.Count);
                totalConsultas.CantPersonas = cantidadConsultas.Sum(consulta => consulta.Personas.Count);
                totalConsultas.CantAutomotores = cantidadConsultas.Sum(consulta => consulta.Automotors.Count);

                totalConsultas.CantArmasPositivo = cantidadConsultas.Sum(consulta => consulta.Armas.Count(arma => arma.Resultado));
                totalConsultas.CantPersonasPositivo = cantidadConsultas.Sum(consulta => consulta.Personas.Count(persona => persona.Resultado));
                totalConsultas.CantAutomotoresPositivo = cantidadConsultas.Sum(consulta => consulta.Automotors.Count(automotor => automotor.Resultado));

                totalConsultas.CantConsultas = totalConsultas.CantArmas + totalConsultas.CantPersonas + totalConsultas.CantAutomotores;
                totalConsultas.CantPositivos = totalConsultas.CantArmasPositivo + totalConsultas.CantPersonasPositivo + totalConsultas.CantAutomotoresPositivo;

                if (cantidadConsultas.Count == 0)
                {
                    oRespuesta.Mensaje = "No se encontraron consultas el dia indicado";
                    return Ok(oRespuesta);
                }


                oRespuesta.Exito = 1;
                oRespuesta.data = totalConsultas;

            }
            catch (Exception ex)
            { oRespuesta.Mensaje = ex.Message; }

            return Ok(oRespuesta);
        }

        [HttpGet("CantidadConsultasMensualPorDependencia")]
        public async Task<ActionResult<List<ConsultaCantDiarioDTO>>> QuantityMonthConsultaByDependence([FromQuery] string? date)
        {
            Respuesta oRespuesta = new Respuesta();
            DateTime dateD = DateTime.ParseExact(date, "yyyy-MM", null);
            var month = dateD.Month;
            var year = dateD.Year; // Cambiar al año deseado

            try
            {
                var consultasPorDependencia = _context.Consulta
                                    .Where(consulta => consulta.Fecha.Month == month && consulta.Fecha.Year == year)
                                    .Include(consulta => consulta.Armas)
                                    .Include(consulta => consulta.Personas)
                                    .Include(consulta => consulta.Automotors)
                                    .ToList()
                                    .GroupBy(consulta => consulta.Idjuridiccion)
                                    .ToList();

                var consultasConDetalles = consultasPorDependencia
                    .OrderByDescending(group => group.Count())
                    .Select(group => new
                    {
                        DependenciaId = group.Key,
                        NombreDependencia = _context.Dependencia
                        .Where(dependencia => dependencia.Id == group.Key)
                        .Select(dependencia => dependencia.Nombre)
                        .FirstOrDefault(),
                        CantArmas = group.Sum(consulta => consulta.Armas?.Count() ?? 0),
                        CantAutomotores = group.Sum(consulta => consulta.Automotors?.Count() ?? 0),
                        CantPersonas = group.Sum(consulta => consulta.Personas?.Count() ?? 0),
                        CantArmasPositivo = group.Sum(consulta => consulta.Armas?.Count(arma => arma.Resultado) ?? 0),
                        CantAutomotoresPositivo = group.Sum(consulta => consulta.Automotors?.Count(automotor => automotor.Resultado) ?? 0),
                        CantPersonasPositivo = group.Sum(consulta => consulta.Personas?.Count(persona => persona.Resultado) ?? 0),

                        CantConsultas = group.Sum(consulta => (consulta.Armas?.Count() ?? 0) +
                                                 (consulta.Automotors?.Count() ?? 0) +
                                                 (consulta.Personas?.Count() ?? 0)),

                        CantPositivos = group.Sum(consulta =>
                            (consulta.Armas?.Count(arma => arma.Resultado) ?? 0) +
                            (consulta.Automotors?.Count(automotor => automotor.Resultado) ?? 0) +
                            (consulta.Personas?.Count(persona => persona.Resultado) ?? 0))

                    }).ToList();


                if (consultasConDetalles.Count == 0)
                {
                    oRespuesta.Mensaje = "No se encontraron consultas el dia indicado";
                    return Ok(oRespuesta);
                }

                oRespuesta.Exito = 1;
                oRespuesta.data = consultasConDetalles;

            }
            catch (Exception ex)
            { oRespuesta.Mensaje = ex.Message; }

            return Ok(oRespuesta);
        }
        //[httpget("buscar un consulta por efectivo solicitante")]
        //public async task<actionresult<list<consultasdto>>> findconsultasolicitante([fromquery] int id)
        //{
        //    respuesta orespuesta = new respuesta();
        //    orespuesta.exito = 0;
        //    try
        //    {
        //        var consulta = await _context.consulta.where(d => d.idsolicitante == id).include(c => c.idjuridiccionnavigation).tolistasync();
        //        if (consulta.count == 0)
        //        {
        //           return badrequest("con se encontraron consultas del solicitante indicado");
        //        }
        //        orespuesta.exito = 1;
        //        return _mapper.map<list<consultasdto>>(consulta);
        //    }
        //    catch (exception ex)
        //    {
        //        orespuesta.mensaje = ex.message;
        //    }
        //    return ok(orespuesta);
        //}

        //[HttpGet("Buscar Consultas Por Movil")]
        //public async Task<ActionResult<List<ConsultasDTO>>> FindConsultaMovil([FromQuery] int movil)
        //{
        //    Respuesta oRespuesta = new Respuesta();
        //    oRespuesta.Exito = 0;
        //    try
        //    {
        //        var consulta = await _context.Consulta.Where(d => d.Movil == movil).Include(c => c.IdjuridiccionNavigation).ToListAsync();
        //        if (consulta.Count == 0)
        //        {
        //            return BadRequest("Con se encontraron consultas del solicitante indicado");
        //        }
        //        oRespuesta.Exito = 1;
        //        return _mapper.Map<List<ConsultasDTO>>(consulta);
        //    }
        //    catch (Exception ex)
        //    {
        //        oRespuesta.Mensaje = ex.Message;
        //    }
        //    return Ok(oRespuesta);
        //}

        //[HttpGet("Buscar Consultas Por Dependencia")]
        //public async Task<ActionResult<List<ConsultasDTO>>> FindConsultasDependencia([FromQuery] int id)
        //{
        //    Respuesta oRespuesta = new Respuesta();
        //    oRespuesta.Exito = 0;
        //    try
        //    {
        //        var consulta = await _context.Consulta.Where(d => d.Idjuridiccion == id).Include(c => c.IdjuridiccionNavigation).ToListAsync();
        //        if (consulta.Count == 0)
        //        {
        //            return BadRequest("Con se encontraron consultas de la dependencia indicada");
        //        }
        //        oRespuesta.Exito = 1;
        //        return _mapper.Map<List<ConsultasDTO>>(consulta);
        //    }
        //    catch (Exception ex)
        //    {
        //        oRespuesta.Mensaje = ex.Message;
        //    }
        //    return Ok(oRespuesta);
        //}

        //[HttpGet("Buscar Consultas varias Dependencia")]
        //public async Task<ActionResult<List<ConsultasDTO>>> FindConsultasDependencia([FromQuery] List<int> ids)
        //{
        //    Respuesta oRespuesta = new Respuesta();
        //    oRespuesta.Exito = 0;
        //    try
        //    {
        //        var consultas = await _context.Consulta.Where(d => ids.Contains(d.Idjuridiccion)).Include(c => c.IdjuridiccionNavigation).ToListAsync();

        //        if (consultas.Count == 0)
        //        {
        //            return BadRequest("No se encontraron consultas de las dependencias indicadas");
        //        }

        //        oRespuesta.Exito = 1;
        //        return _mapper.Map<List<ConsultasDTO>>(consultas);
        //    }
        //    catch (Exception ex)
        //    {
        //        oRespuesta.Mensaje = ex.Message;
        //    }

        //    return Ok(oRespuesta);
        //}

        //[HttpGet("Listado Consultas")] //Lo utilizo para traer todos los registros
        //public async Task<ActionResult<BaseResponse<List<ConsultasDTO>>>> GetConsultas()
        //{
        //    try
        //    {
        //        var lst = await _context.Consulta.Include(c => c.IdjuridiccionNavigation).ToListAsync(); //Busco Todos los registros de forma ordena y desendiente por id
        //        return Ok(_mapper.Map<List<ConsultasDTO>>(lst));
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }

        //}
    }
}
