using System.ComponentModel.DataAnnotations;

namespace NetCoreProyectExample.Models
{
    public class AccountBankModel

    {

        [Required]
        public int AccountNumber { get; set; }
        [Required]
        public string? AccountHolderName { get; set; }
        [Required]
        public Decimal Balance { get; set; }

    }
}
