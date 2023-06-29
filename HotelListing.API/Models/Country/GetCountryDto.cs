using System.ComponentModel.DataAnnotations;

namespace HotelListing.API.Models.Country
{
    public class GetCountryDto : BaseCountryDto
    {
        public int Id { get; set; }
    }
}
