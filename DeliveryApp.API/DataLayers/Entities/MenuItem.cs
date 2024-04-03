using DeliveryApp.API.DataLayers.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryAppBackend.DataLayers.Entities
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public Restaurant? Restaurant { get; set; }
        [ForeignKey("RestaurantId")]
        public int RestaurantId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public MenuItemCategory Category { get; set; }
        /*public string? ImageURL { get; set; }*/
    }
}
