using System.ComponentModel.DataAnnotations;

namespace PrintingHouse.Domain.Models
{
    public class City
    {
        [Key]
        public int cityId { get; set; }
        public string cityName { get; set; }

        public Country Country { get; set; }
    }
}
