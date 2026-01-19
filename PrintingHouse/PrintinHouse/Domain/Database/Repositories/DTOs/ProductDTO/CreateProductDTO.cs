using System.Text.Json.Serialization;
using PrintingHouse.Domain.Models;

namespace PrintingHouse.Domain.Database.Repositories.DTOs.ProductDTO
{
    public class CreateProductDTO
    {
        public string ProductName { get; set; }
        public string Desctription { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProductType ProductType { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageUrl { get; set; }
        public string ISBN { get; set; }
        public int publisherId { get; set; }
    }
}
