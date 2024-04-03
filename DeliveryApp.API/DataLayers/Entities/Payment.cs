using DeliveryApp.API.DataLayers.Entities;
using DeliveryApp.API.DataLayers.Entities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryAppBackend.DataLayers.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public User? User { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public PaymentStatus PaymentStatus { get; set; }


    }
}
