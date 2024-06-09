namespace DeliveryApp.API.DTOs;

public class OrderDTO
{
    public DateTime OrderDate { get; set; }
    public string? DeliveryAddress { get; set; }
    public decimal TotalAmount { get; set; }

}