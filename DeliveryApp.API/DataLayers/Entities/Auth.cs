using System.ComponentModel.DataAnnotations;

namespace DeliveryApp.API.DataLayers.Entities
{
    public class Auth
    {
        [Key]
        public int AuthId { get; set; }
        [MaxLength(100)]
        [EmailAddress]
        public string? Email { get; set; }
        [MaxLength]
        public byte[]? PasswordSalt { get; set; }
        [MaxLength]
        public byte[]? PasswordHash { get; set; }

    }
}
