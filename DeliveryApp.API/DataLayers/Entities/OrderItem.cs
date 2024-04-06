using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryAppBackend.DataLayers.Entities
{
    public class OrderItem
    {
        [Key]
        public int OrderItemId { get; set; }
        public Order? Order { get; set; }
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public MenuItem? MenuItem { get; set; }
        [ForeignKey("MenuItemId")]
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        [Range(18, 2)]
        public decimal TotalPrice { get; set; }

    }
}

