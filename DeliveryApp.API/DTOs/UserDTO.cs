using DeliveryApp.API.DataLayers.Entities.Enums;

namespace DeliveryApp.API.DTOs
{
    public class UserDTO
    {
        /*public int UserId { get; set; }*/
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public UserRole? Role { get; set; }
        public string? Address { get; set; }
        /*        public string? PostalCode { get; set; }*/
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
