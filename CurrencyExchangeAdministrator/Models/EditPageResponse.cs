
namespace CurrencyExchangeAdministrator.Models
{
    public class EditPageResponse
    {
        public bool IsRateUpdated { get; set; }

        public Currency UpdatedCurrency { get; set; } = new();
    }
}
