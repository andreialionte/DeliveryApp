﻿namespace DeliveryApp.API.DTOs
{
    public class RestaurantDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public decimal DeliveryFee { get; set; }
        public string? OperatingHours { get; set; }
    }
}
