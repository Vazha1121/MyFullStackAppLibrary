using PrintingHouse.Domain.Models;
using PrintingHouse.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PrintingHouse.Domain.Database.Repositories.DTOs.AuthorDTO
{
    public class CreateAuthorDTO
    {
        [Key]
        [JsonPropertyName("authorId")]
        public int authorId { get; set; }

        [Required, MinLength(2), MaxLength(50)]
        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [Required, MinLength(2), MaxLength(50)]
        [JsonPropertyName("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("gender")]
        public Gender Gender { get; set; }

        [Required, RegularExpression(@"^\d{11}$")]
        [JsonPropertyName("personalNumber")]
        public string PersonalNumber { get; set; }

        [Required, MinAge(18)]
        [JsonPropertyName("birthDate")]
        public DateTime BirthDate { get; set; }

        [JsonPropertyName("countryId")]
        public int CountryId { get; set; }

        [JsonPropertyName("cityId")]
        public int CityId { get; set; }
        [MinLength(9)]

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [EmailAddress]
        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
