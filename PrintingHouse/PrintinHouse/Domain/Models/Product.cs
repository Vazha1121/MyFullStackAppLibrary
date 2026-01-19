using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PrintingHouse.Domain.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required, MinLength(1), MaxLength(50)]
        public string ProductName { get; set; }

        [Required, MinLength(0), MaxLength(500)]
        public string Desctription { get; set; }

        
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ProductType ProductType { get; set; }

        [Required, RegularExpression(@"^\\d{13}$")]
        public string ISBN { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public string ImageUrl { get; set; }
        public int publisherId { get; set; }
        public Publisher Publisher { get; set; }

        public ICollection<AuthorBook> AuthorBooks { get; set; } = new List<AuthorBook>();


    }
}
