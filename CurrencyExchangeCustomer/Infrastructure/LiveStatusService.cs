namespace CurrencyExchangeCustomer.Infrastructure
{
    public class LiveStatusService
    {
        private static bool isConnected = false;

        public static bool IsConnected 
        {
            get => isConnected;
            set 
            {
                isConnected = value;
                ConnectionStatusChanged?.Invoke(null, isConnected);
            }
        }

        public static void ChangeConnected(bool status) => IsConnected = status; 

        public static event EventHandler<bool>? ConnectionStatusChanged;
    }
}
