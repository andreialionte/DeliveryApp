namespace DeliveryApp.API.DTOs
{
    public class Login
    {
        public string? Email { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public byte[]? PasswordHash { get; set; }
    }
}
