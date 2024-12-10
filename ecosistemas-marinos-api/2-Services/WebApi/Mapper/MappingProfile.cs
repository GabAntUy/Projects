using ApplicationLogic.Interfaces;
using ApplicationLogic.UseCases.Paises;
using AutoMapper;
using BusinessLogic.DTO;
using BusinessLogic.Entities;

namespace WebApi.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            //Mapeo Ecosistemas
            CreateMap<Ecosistema, EcosistemaDto>()
                .ForMember(ecoDto => ecoDto.Nombre, eco => eco.MapFrom(campo => campo.Nombre.Value));

            CreateMap<CreateEcosistemaDto, Ecosistema>()
                .ForMember(x => x.Ubicacion, opt => opt.MapFrom(dto => new UbicacionEcosistema(dto.Latitud, dto.Longitud)))

                .ForMember(dest => dest.Paises, opt => opt.MapFrom((src, dest, destMember, context) =>
                    (context.Items["GetPaisSelected"] as IGetSelected<Pais>)?.GetSelected(src.PaisesId)))

                .ForMember(dest => dest.Amenazas, opt => opt.MapFrom((src, dest, destMember, context) =>
                    (context.Items["GetAmenazaSelected"] as IGetSelected<Amenaza>)?.GetSelected(src.AmenazasId)))

                .ForMember(dest => dest.EstadoDeConservacion, opt => opt.MapFrom((src, dest, destMember, context) =>
                    (context.Items["GetEstadoSelected"] as IGet<EstadoDeConservacion>)?.GetById(src.EstadoDeConservacionId)));

            //Mapeo Especies
            CreateMap<Especie, EspecieDto>()
                .ForMember(espDto => espDto.NombreCientifico, esp => esp.MapFrom(campo => campo.NombreCientifico.Value))
                .ForMember(espDto => espDto.NombreVulgar, esp => esp.MapFrom(campo => campo.NombreVulgar.Value));
           
            CreateMap<CreateEspecieDto, Especie>()
              .ForMember(x => x.RangoPeso, opt => opt.MapFrom(dto => new RangoPeso(dto.PesoMin, dto.PesoMax)))
              .ForMember(x => x.RangoLargo, opt => opt.MapFrom(dto => new RangoLargo(dto.LargoMin, dto.LargoMax)))

              .ForMember(dest => dest.PuedeHabitar, opt => opt.MapFrom((src, dest, destMember, context) =>
                  (context.Items["GetEcosistemasSelected"] as IGetSelected<Ecosistema>)?.GetSelected(src.EcosistemasId)))

              .ForMember(dest => dest.Amenazas, opt => opt.MapFrom((src, dest, destMember, context) =>
                  (context.Items["GetAmenazaSelected"] as IGetSelected<Amenaza>)?.GetSelected(src.AmenazasId)))

              .ForMember(dest => dest.EstadoConservacion, opt => opt.MapFrom((src, dest, destMember, context) =>
                  (context.Items["GetEstadoSelected"] as IGet<EstadoDeConservacion>)?.GetById(src.EstadoDeConservacionId)));


            CreateMap<EspecieDto,Especie>();

            //Mapeo Paises
            CreateMap<Pais,PaisDto>();
            CreateMap<PaisDto,Pais>();

            //Mapeo Amenazas
            CreateMap<Amenaza,AmenazaDto>();
            CreateMap<AmenazaDto,Amenaza>();

            //Mapeo Estados
            CreateMap<EstadoDeConservacion, EstadoDeConservacionDto>()
                .ForMember(ecDto => ecDto.Nombre, ec => ec.MapFrom(campo => campo.Nombre.Value));

            CreateMap<EstadoDeConservacionDto,EstadoDeConservacion>();

            //Mapeo usuarios
            CreateMap<Usuario,UsuarioDto>();
            CreateMap<UsuarioDto,Usuario>();

            CreateMap<CreateUsuarioDto, Persona>();
        }
    }
}
