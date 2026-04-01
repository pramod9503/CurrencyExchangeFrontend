
namespace CurrencyExchangeCustomer.Models
{
    public static class RetryConnectModel
    {
        private static bool _retryRequested = false;

        public static bool RetryRequested 
        { 
            get => _retryRequested;
            set 
            {
                _retryRequested = value;
                RetryConnectRequested?.Invoke(null, _retryRequested);
            } 
        }

        public static void SetRetryRequest(bool requestRetry) => RetryRequested = requestRetry;

        public static event EventHandler<bool>? RetryConnectRequested;
    }
}
