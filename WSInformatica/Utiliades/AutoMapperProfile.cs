using AutoMapper;
using WSInformatica.DTOs.AutomotoresDTO;
using WSInformatica.DTOs.ConsultaDTO;
using WSInformatica.DTOs.DependenciaDTO;
using WSInformatica.DTOs.EfectivoDTO;
using WSInformatica.Models;
using WSInformatica.Models.Request;

namespace WSInformatica.Utiliades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
        //Mapeos

        //Efectivos
            CreateMap<EfectivoCreateDTO, Efectivo>();
            CreateMap<EfectivoUpdateDTO, Efectivo>();
            CreateMap<Efectivo, GetAllEfectivoDTO>().ForMember(dest => dest.NombreDependencia, opt => opt.MapFrom(src => src.Dependencia.Nombre));
 
            CreateMap<AutomotorRequest, Automotor>();

        //Creacion Consulta
            CreateMap<CreateConsultaDTO,Consulta>();
            CreateMap<ConsultaPersonaDTO,Persona>();
            CreateMap<ConsultaAutomotorDTO,Automotor>();
            CreateMap<ConsultaArmaDTO,Arma>();
            //CreateMap<Consulta, ConsultasDTO>()
            //.ForMember(dest => dest.DependenciaNombre, opt => opt.MapFrom(src => src.IdjuridiccionNavigation.Nombre));           

            CreateMap<Consulta,ConsultaCantDiarioDTO>();

        //Dependencia
            CreateMap<Dependencia, GetAllDependenciaDTO>();

        //Automotor
            CreateMap<Automotor, GetAllTipoAutomotoresDTO>();
        }
    }
}
