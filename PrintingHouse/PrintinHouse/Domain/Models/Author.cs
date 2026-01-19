using System.ComponentModel.DataAnnotations;
using PrintingHouse.Validation;

namespace PrintingHouse.Domain.Models
{
    public class Author
    {
        [Key]
        public int authorId { get; set; }

        [Required, MinLength(2), MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MinLength(2), MaxLength(50)]
        public string LastName { get; set; }

        public Gender Gender { get; set; }

        [Required, RegularExpression(@"^\d{11}$")]
        public string PersonalNumber {  get; set; }

        [Required, MinAge(18)]
        public DateTime BirthDate { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        [MinLength(9)]
        public string PhoneNumber { get; set; }

        [EmailAddress]

        public string Email {  get; set; }

        public ICollection<AuthorBook> AuthorBooks { get; set; } = new List<AuthorBook>();


    }
}
