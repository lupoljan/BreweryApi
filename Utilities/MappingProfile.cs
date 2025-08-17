using AutoMapper;
using BreweryApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BreweryApi.Utilities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // External API → Domain
            CreateMap<OpenBreweryResponse, BreweryModel>()
                .ForMember(dest => dest.Longitude,
                    opt => opt.MapFrom(src => src.longitude))
                 .ForMember(dest => dest.Latitude,
                    opt => opt.MapFrom(src => src.latitude))
                .ForMember(dest => dest.BreweryType, opt => opt.MapFrom(src => src.brewery_type))
                .ForMember(dest => dest.Address1, opt => opt.MapFrom(src => src.address_1))
                .ForMember(dest => dest.Address2, opt => opt.MapFrom(src => src.address_2))
                .ForMember(dest => dest.Address3, opt => opt.MapFrom(src => src.address_3))
                .ForMember(dest => dest.StateProvince, opt => opt.MapFrom(src => src.state_province))
                .ForMember(dest => dest.PostalCode, opt => opt.MapFrom(src => src.postal_code))
                .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.website_url));

            // Domain → DTO
            CreateMap<BreweryModel, BreweryDto>()
                .ForMember(dest => dest.Distance, opt => opt.Ignore()); // Distance calculated manually
        }

        private static double? ParseNullableDouble(string value)
        {
            return double.TryParse(value, out var result) ? result : (double?)null;
        }
    }
}
