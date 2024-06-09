namespace DeliveryApp.API.DTOs
{
    public class MenuItemDTO
    {
        public int RestaurantId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
//ca sa adaugam la ce restaurant apartine
