using Microsoft.Identity.Client;

namespace PrintingHouse.Domain.Models
{
    public class Publisher
    {
        public int publisherId { get; set; }
        public string publisherName { get; set; }
        
        public List<Product> Products{ get; set; }
    }
}
