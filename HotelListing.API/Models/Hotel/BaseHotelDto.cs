using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.Hotel
{
    public class BaseHotelDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Range(0.0, 5.0)]
        public double? Rating { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int CountryId { get; set; }
    }
}
