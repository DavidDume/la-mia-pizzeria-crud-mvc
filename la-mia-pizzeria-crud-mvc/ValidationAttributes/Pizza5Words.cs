using System.ComponentModel.DataAnnotations;

namespace la_mia_pizzeria_crud_mvc.ValidationAttributes
{
    public class Pizza5Words : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is string)
            {
                string inputValue = (string)value;

                if (inputValue == null || inputValue.Split(' ').Length <= 5)
                {
                    return new ValidationResult("Inserisci almeno 5 parole!");
                }

                return ValidationResult.Success;
            }

            return new ValidationResult("Inserisci un'altro formato");

        }
    }
}
