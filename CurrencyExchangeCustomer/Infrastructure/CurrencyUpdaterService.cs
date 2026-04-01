using CurrencyExchangeCustomer.Models;

namespace CurrencyExchangeCustomer.Infrastructure
{
    public static class CurrencyUpdaterService
    {
        private static CurrencyMessageModel? _currencyMessage;

        public static CurrencyMessageModel CurrencyMessageUpdate 
        {  
            get => _currencyMessage!;
            private set 
            {
                _currencyMessage = value;                
                CurrencyChanged?.Invoke(null, _currencyMessage);
                
            } 
        }

        public static void SetUpdate(CurrencyMessageModel currencyMessage) => CurrencyMessageUpdate = currencyMessage;        

        public static event EventHandler<CurrencyMessageModel>? CurrencyChanged;
    }
}
