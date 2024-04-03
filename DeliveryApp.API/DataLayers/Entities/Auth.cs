namespace DeliveryApp.API.DataLayers.Entities
{
    public class Auth
    {
        public string? Email { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public byte[]? PasswordHash { get; set; }

    }
}
