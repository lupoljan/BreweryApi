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
                    opt => opt.MapFrom(src => ParseNullableDouble(src.longitude)))
                .ForMember(dest => dest.Latitude,
                    opt => opt.MapFrom(src => ParseNullableDouble(src.latitude)))
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
