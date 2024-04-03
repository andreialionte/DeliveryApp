using DeliveryApp.API.DataLayers.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryAppBackend.DataLayers.Entities
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public User? User { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public Restaurant? Restaurant { get; set; }
        [ForeignKey("RestaurantId")]
        public int RestaurantId { get; set; }
        public DateTime OrderDate { get; set; }
        public string DeliveryAddress { get; set; }
        public decimal TotalAmount { get; set; }

    }
}
