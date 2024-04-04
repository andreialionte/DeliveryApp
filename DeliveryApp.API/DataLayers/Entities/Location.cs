using System.ComponentModel.DataAnnotations;

namespace DeliveryApp.API.DataLayers.Entities
{
    public class Location
    {
        [Range(18, 2)]
        public decimal? Latitude { get; set; }
        [Range(18, 2)]
        public decimal? Longitude { get; set; }
    }
}
