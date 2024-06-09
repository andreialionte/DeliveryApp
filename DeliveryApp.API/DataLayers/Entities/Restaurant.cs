using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DeliveryAppBackend.DataLayers.Entities
{
    public class Restaurant
    {
        [Key]
        public int RestaurantId { get; set; }
        [MaxLength(50)]
        public string? Name { get; set; }
        /*        [MaxLength]
                public string? Description { get; set; }*/
        [MaxLength]
        public string? Address { get; set; }
        [MaxLength(100)]
        [EmailAddress]
        public string? Email { get; set; }
        [MaxLength(20)]
        public string? PhoneNumber { get; set; }
        /*        [Precision(18, 2)]
                public decimal DeliveryFee { get; set; }*/
        [MaxLength]
        public string? OperatingHours { get; set; }
        [MaxLength]
        public string? RestaurantPhotoUrl { get; set; } //main background photo
                                                        // fiecare orderitem trebuie sa aibe o poze si un restaurant tot o poza 
        [JsonIgnore]
        public ICollection<MenuItem>? MenuItems { get; set; }
        public ICollection<Order>? Orders { get; set; }

    }
}
