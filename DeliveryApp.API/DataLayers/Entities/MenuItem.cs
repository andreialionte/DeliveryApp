using DeliveryApp.API.DataLayers.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryAppBackend.DataLayers.Entities
{
    public class MenuItem
    {
        [Key]
        public int MenuItemId { get; set; }
        public Restaurant? Restaurant { get; set; }
        [ForeignKey("Restaurant")]
        public int RestaurantId { get; set; }
        [MaxLength(30)]
        public string? Name { get; set; }
        [MaxLength(255)] // Corrected maximum length
        public string? Description { get; set; }
        [Range(2, 18)] // Corrected range
        public decimal Price { get; set; }
        public MenuItemCategory Category { get; set; }
        /*public string? ImageURL { get; set; }*/
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}