using DeliveryApp.API.DataLayers.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace DeliveryApp.API.DataLayers.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [MaxLength(20)]
        public string? FirstName { get; set; }
        [MaxLength(20)]
        public string? LastName { get; set; }
        [MaxLength(100)]
        [EmailAddress]
        public string? Email { get; set; }
        public UserRole? Role { get; set; }
        [MaxLength]
        public string? Address { get; set; }
        [MaxLength(10)]
        public string? PostalCode { get; set; }
        [MaxLength(20)]
        public string? City { get; set; }
        [MaxLength(20)]
        public string? Region { get; set; }
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }

    }
}
