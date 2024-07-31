using System.ComponentModel.DataAnnotations;

namespace NetCoreProyectExample.Validations
{
    public class BookValidation : ValidationAttribute
    {
        public int Minimum { get; set; } = 2024;
        public string DefaultErrorMessage{ get; set; } = "Minimun required : ";
        public BookValidation() {
           
        }
        public BookValidation(int year)
        {
            Minimum = year;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value != null) { 
            DateTime date = (DateTime) value;

                if (date.Year <= Minimum)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult(string.Format(ErrorMessage??DefaultErrorMessage,Minimum));
                }
            }
            else
            {
                return null;
            }
        }
    }
}
