using DeliveryApp.API.DataLayers.Entities.Enums;

namespace DeliveryApp.API.DTOs
{
    public class DeliveryAgentDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DeliveryAgentStatus? Status { get; set; }
    }
}
