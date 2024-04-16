using DeliveryApp.API.DataLayers.Entities.Enums;

namespace DeliveryApp.API.DTOs
{
    public class MenuItemDTO
    {
        /*        public int MenuItemId { get; set; }*/
        /*        public int RestaurantId { get; set; }*/
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public MenuItemCategory Category { get; set; }
    }
}
