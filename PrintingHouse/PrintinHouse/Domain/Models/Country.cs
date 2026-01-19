using System.ComponentModel.DataAnnotations;

namespace PrintingHouse.Domain.Models
{
    public class Country
    {
        [Key]
        public int countryId {  get; set; }
        public string countryName { get; set; }
        public ICollection<City> City { get; set; }
        public List<Author> Authors { get; set; }
    }
}
