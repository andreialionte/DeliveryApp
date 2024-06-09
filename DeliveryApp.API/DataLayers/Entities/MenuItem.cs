using DeliveryAppBackend.DataLayers.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class MenuItem
{
    [Key]
    public int MenuItemId { get; set; }

    public int RestaurantId { get; set; }

    [ForeignKey("RestaurantId")]
    public Restaurant? Restaurant { get; set; }

    [MaxLength(30)]
    public string? Name { get; set; }

    [MaxLength(255)]
    public string? Description { get; set; }

    [Range(2, 18)]
    public decimal Price { get; set; }

    [MaxLength]
    public string? MenuItemPhotoUrl { get; set; }

    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }

    public ICollection<OrderItem>? OrderItems { get; set; }
}
