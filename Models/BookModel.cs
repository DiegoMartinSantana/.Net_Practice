using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using NetCoreProyectExample.Validations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NetCoreProyectExample.Models
{
    //if i want more personality validations and not recycable use IVALIDATION
    public class BookModel : IValidatableObject
    {
        //[FromQuery]
        [Required]
        [Range(1, int.MaxValue)]
        public int? BookId { get; set; }

        [Required(ErrorMessage = "not empty or null")]

        public string? Author { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string? Description { get; set; }

        [BindNever]
        [BookValidation(2026, ErrorMessage = "test message, {0}")]
        public DateTime? CreatedDate { get; set; }
        public override string ToString()
        {
            return $"Book Id : {BookId}, Author : {Author}, Description : {Description}";
        }

        //method to validate
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //literally here write the validations!!
            yield return new ValidationResult("pepe", new[] { nameof(Author) });

            //if i take a lot of errors use YIELD RETURN in each
        }

        [BindNever]

        public DateTime? FromDate { get; set; }

        //le paso from date
        [DateRangeValidator("FromDate", ErrorMessage = "'From Date' shoul be older than or equal to 'To Date'")]
        [BindNever]

        public DateTime? ToDate { get; set; }


    }
}
