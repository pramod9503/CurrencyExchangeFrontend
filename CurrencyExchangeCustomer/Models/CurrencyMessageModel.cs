using CurrencyExchangeCustomer.Infrastructure;

namespace CurrencyExchangeCustomer.Models
{    
    public class CurrencyMessageModel
    {
        public Currency? Currency { get; set; }

        public CurrencyOperationEnum Operation { get; set; }
    }
}
