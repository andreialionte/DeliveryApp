using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryAppBackend.DataLayers.Entities
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        public Order? Order { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public MenuItem? MenuItem { get; set; }
        [ForeignKey("MenuItem")]
        public int? MenuItemId { get; set; }
        public int Quantity { get; set; }
        [Range(2, 18)]
        public decimal TotalPrice { get; set; }
    }

}

