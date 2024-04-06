using DeliveryApp.API.DataLayers.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace DeliveryAppBackend.DataLayers.Entities
{
    public class DeliveryAgent
    {
        [Key]
        public int DeliveryAgentId { get; set; }
        [MaxLength(20)]
        public string? FirstName { get; set; }
        [MaxLength(20)]
        public string? LastName { get; set; }
        [MaxLength(100)]
        [EmailAddress]
        public string? Email { get; set; }
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
        public DeliveryAgentStatus? Status { get; set; }
        /*        public Location? Location { get; set; }*/


    }
}
