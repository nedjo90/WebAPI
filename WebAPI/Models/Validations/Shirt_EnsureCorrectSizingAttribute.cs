using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models.Validations;

public class Shirt_EnsureCorrectSizingAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        Shirt? shirt = validationContext.ObjectInstance as Shirt;
        if (shirt != null && !string.IsNullOrWhiteSpace( shirt.Gender))
        {
            if (shirt.Gender.Equals("men", StringComparison.OrdinalIgnoreCase) && shirt.Size < 8)
            {
                return new ValidationResult("For men's shirt, the size has to be greater or equal to 8");
            }
            if (shirt.Gender.Equals("women", StringComparison.OrdinalIgnoreCase) && shirt.Size < 6)
            {
                return new ValidationResult("For women's shirt, the size has to be greater or equal to 6");
            }
        }
        return ValidationResult.Success;
    }
}