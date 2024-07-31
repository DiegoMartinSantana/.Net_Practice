using Microsoft.AspNetCore.Mvc.ModelBinding;
using NetCoreProyectExample.Validations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetCoreProyectExample.Models
{
    public class OrderModel : IValidatableObject
    {
        public OrderModel()
        {

            OrderNo = new Random().Next(1, 99999);
        }

        [BindNever]
        public int? OrderNo { get; set; }


        [Required]
       
        public DateTime? OrderDate
        {
            get; set;
        }

        [Required(ErrorMessage = "Invoce Price dont be null {0}")]
        [DisplayName("Invoce Price")]
        public double InvoicePrice { get; set; }
        [Required]
        public List<ProductModel>? Products { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(OrderDate < Convert.ToDateTime("2000-01-01"))
            {
                yield return new ValidationResult("Not allowed orders less than 2000", new[] { nameof(OrderDate) });

            }
            if (OrderDate > DateTime.Now.AddDays(7))
            {
                yield return new ValidationResult("Not allowed orders with a more than 7 days of delay", new[] { nameof(OrderDate) });
            }

            //validate the invoce price equals the sum of all the products
            double Invoce = 0;
            foreach (var item in Products)
            {
                Invoce += item.Price * item.Quantity;

            }
            if (!Equals(Invoce, InvoicePrice))
            {
                yield return new ValidationResult("the invoce price needs equals to the amount of the products.", new[] { nameof(InvoicePrice) });
            }


        }

    }

}