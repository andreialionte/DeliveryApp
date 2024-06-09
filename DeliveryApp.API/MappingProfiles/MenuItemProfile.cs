using AutoMapper;
using DeliveryApp.API.DTOs;
using DeliveryAppBackend.DataLayers.Entities;

public class MenuItemProfile : Profile
{
    public MenuItemProfile()
    {
        CreateMap<MenuItemDTO, MenuItem>()
                    .ForMember(dest => dest.MenuItemId, opt => opt.Ignore())
                    .ForMember(dest => dest.Restaurant, opt => opt.Ignore()); // Assuming Restaurant should also be ignored
    }
}
