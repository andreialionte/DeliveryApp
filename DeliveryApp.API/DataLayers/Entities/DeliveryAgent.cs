using DeliveryApp.API.DataLayers.Entities;
using DeliveryApp.API.DataLayers.Entities.Enums;

namespace DeliveryAppBackend.DataLayers.Entities
{
    public class DeliveryAgent
    {
        public int DeliveryAgentId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DeliveryAgentStatus? Status { get; set; }
        public Location? Location { get; set; }


    }
}
