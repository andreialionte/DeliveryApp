using DeliveryApp.API.DataLayers.Entities;
using DeliveryApp.API.DataLayers.Entities.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeliveryAppBackend.DataLayers.Entities
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public User? User { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        [Range(18, 2)]
        public decimal Amount { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public PaymentStatus PaymentStatus { get; set; }


    }
}
