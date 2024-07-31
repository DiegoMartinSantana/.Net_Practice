using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace NetCoreProyectExample.Models
{
    public class ProductModel
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int ProductCode { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        public double Price { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }


    }
}
