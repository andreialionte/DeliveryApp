using AutoMapper;
using DeliveryApp.API.DTOs;
using DeliveryAppBackend.DataLayers.Entities;

namespace DeliveryApp.API.Configurations
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<Restaurant, RestaurantDTO>().ReverseMap()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.OperatingHours, opt => opt.MapFrom(src => src.OperatingHours));
            /*            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));*/
        }
    }
}
