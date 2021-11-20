using AutoMapper;
using GastosAppCoreEF.Models;

namespace GastosAppApi.Dto
{
    public class MapProfiles: Profile
    {
        public MapProfiles()
        {
            CreateMap<Transaccion, TransaccionDto>()
            .ForMember(
                dest => dest.ConceptoNombre,
                opt => opt.MapFrom(src => src.ConceptoId.HasValue ? src.Concepto.Nombre : null)
            )
            .ForMember(
                dest => dest.CuentaNombre,
                opt => opt.MapFrom(src => src.Cuenta.Nombre)
            )
            .ForMember(
                dest => dest.CuentaTransfNombre,
                opt => opt.MapFrom(src => src.CuentaIdTransf.HasValue ? src.CuentaTransf.Nombre : null)
            )
            .ForMember(
                dest => dest.MontoEquivalente,
                opt => opt.MapFrom(src => src.Monto * src.Cuenta.Moneda.Tasa)
            )
            .ForMember(
                dest => dest.AbreviaturaMoneda,
                opt => opt.MapFrom(src => src.Cuenta.Moneda.Abreviatura)
            );
        }
    }
}