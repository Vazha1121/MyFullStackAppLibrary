using System.ComponentModel.DataAnnotations;

namespace PrintingHouse.Validation
{
    public class MinAgeAttribute : ValidationAttribute
    {
        private readonly int _age;
        public MinAgeAttribute(int age)
        {
            _age = age;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Birth date required");

                var date = (DateTime)value;

                if (date > DateTime.Today.AddYears(-_age))
                {
                    return new ValidationResult($"Minimum age is {_age}");
                }

            }
            return ValidationResult.Success;
        }
    }
}
