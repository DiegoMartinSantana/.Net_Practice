using System.ComponentModel.DataAnnotations;
using System.Reflection;
namespace NetCoreProyectExample.Validations
{
    public class DateRangeValidator : ValidationAttribute   
    {
        private string? OtherPropName;
        public DateRangeValidator() { }
        public DateRangeValidator(string value) { 
        OtherPropName = value;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is not null)
            {
                DateTime to_date =Convert.ToDateTime(value);
                //obtengo la propiedad, EL NOMBRE LO RECIBO EN EL CONSTURCTOR  
                PropertyInfo? otherProperty = validationContext.ObjectType.GetProperty(OtherPropName);
                //obtengo el valor
              DateTime from_date=  Convert.ToDateTime(otherProperty.GetValue(validationContext.ObjectInstance));
                //This is a Reflexion
                //NOW I HAVE THE TWO VALUES.. ONLY LESS EVALUATING
                if (from_date > to_date)
                {
                    return new ValidationResult(ErrorMessage, new string[] { OtherPropName, validationContext.MemberName });
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
