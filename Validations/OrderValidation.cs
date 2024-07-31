using System.ComponentModel.DataAnnotations;

namespace NetCoreProyectExample.Validations
{
    public class OrderValidation :ValidationAttribute   
    { 
        public DateTime DateAccept { get; set; }
        public string? DefaultErrorMessage { get; set; } = "Cant accept a value more less than ";
        public OrderValidation()
        {
            
        }
        public OrderValidation(DateTime dateAccept)
        {
            DateAccept = dateAccept;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            if (value is not null)
            {
                //get the prop
                DateTime DateInsert = Convert.ToDateTime(value);
                if(DateInsert< DateAccept)
                {
                    return new ValidationResult(string.Format(ErrorMessage ?? DefaultErrorMessage, DateAccept));
                }
                return ValidationResult.Success;

                
            }
            else
            {
                return null;
            }


        }
    }
}
