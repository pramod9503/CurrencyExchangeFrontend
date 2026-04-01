using CurrencyExchangeAdministrator.Infrastructure;

namespace CurrencyExchangeAdministrator.Models
{    
    public class CurrencyMessageModel
    {
        public Currency? Currency { get; set; }

        public CurrencyOperationEnum Operation { get; set; }
    }
}
