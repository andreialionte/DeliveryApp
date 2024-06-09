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
        public DeliveryAgent? DeliveryAgent { get; set; }
        [ForeignKey("DeliveryAgentId")]
        public int DeliveryAgentId { get; set; }
        public DateTime OrderDate { get; set; }
        [MaxLength]
        public string? DeliveryAddress { get; set; }
        [Range(18, 2)]
        public decimal TotalAmount { get; set; }

        public ICollection<OrderItem>? OrderItems { get; set; }

        //here we need to add delivery agent and order to be add in db after payment was made

    }
}
