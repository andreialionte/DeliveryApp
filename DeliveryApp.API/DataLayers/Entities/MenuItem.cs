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
        [ForeignKey("RestaurantId")]
        public int RestaurantId { get; set; }
        [MaxLength(30)]
        public string? Name { get; set; }
        [MaxLength]
        public string? Description { get; set; }
        [Range(18, 2)]
        public decimal Price { get; set; }
        public MenuItemCategory Category { get; set; }
        /*public string? ImageURL { get; set; }*/
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
