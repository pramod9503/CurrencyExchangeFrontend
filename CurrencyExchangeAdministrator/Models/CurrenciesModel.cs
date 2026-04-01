using System.Collections.ObjectModel;

namespace CurrencyExchangeAdministrator.Models
{
    public class CurrenciesModel : PropertyChangeNotifier
    {
        string _status = string.Empty;
        ObservableCollection<Currency> _currencies = new();

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Currency> Currencies
        {
            get => _currencies;
            set
            {
                _currencies = value;
                OnPropertyChanged();
            }
        }
    }
}
