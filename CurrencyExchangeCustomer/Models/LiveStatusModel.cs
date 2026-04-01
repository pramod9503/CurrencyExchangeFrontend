
namespace CurrencyExchangeCustomer.Models
{
    public class LiveStatusModel : PropertyChangeNotifier
    {
        private Color _statusColor = Colors.Red;
        private string _connectionStatus = string.Empty;

        public string ConnectionStatus
        {
            get => _connectionStatus;
            set
            {
                _connectionStatus = value;
                OnPropertyChanged();
            }
        }

        public Color StatusColor
        {
            get => _statusColor;
            set
            {
                _statusColor = value;
                OnPropertyChanged();
            }
        }
    }
}
