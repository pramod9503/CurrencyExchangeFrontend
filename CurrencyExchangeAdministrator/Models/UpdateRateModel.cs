using System.ComponentModel.DataAnnotations;

namespace CurrencyExchangeAdministrator.Models
{
    public class UpdateRateModel
    {
        public UpdateRateModel() { }

        [Required]
        public int Id { get; set; }

        [Required]
        public decimal Rate { get; set; }
    }
}
