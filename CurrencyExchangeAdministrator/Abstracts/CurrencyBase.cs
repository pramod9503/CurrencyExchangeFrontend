using CurrencyExchangeAdministrator.Models;

namespace CurrencyExchangeAdministrator.Abstracts
{
    public abstract class CurrencyBase : PropertyChangeNotifier
    {
        string _country = string.Empty;
        string _currencyName = string.Empty;
        decimal _rate = 0M;

        public int Id { get; set; }

        public string Country
        {
            get => _country;
            set
            {
                _country = value;
                OnPropertyChanged();
            }
        }

        public string CurrencyName
        {
            get => _currencyName;
            set
            {
                _currencyName = value;
                OnPropertyChanged();
            }
        }

        public decimal Rate
        {
            get => _rate;
            set
            {
                _rate = value;
                OnPropertyChanged();
            }
        }
    }
}
