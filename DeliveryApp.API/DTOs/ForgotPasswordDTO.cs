using System.ComponentModel.DataAnnotations;

namespace DeliveryApp.API.DTOs
{
    public class ForgotPasswordDTO
    {
        [EmailAddress]
        public string? Email { get; set; }
    }
}
