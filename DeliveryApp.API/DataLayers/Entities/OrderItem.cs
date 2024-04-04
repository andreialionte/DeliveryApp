using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryAppBackend.DataLayers.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public Order? Order { get; set; }
        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public MenuItem? MenuItem { get; set; }
        [ForeignKey("MenuItemId")]
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }

    }
}

