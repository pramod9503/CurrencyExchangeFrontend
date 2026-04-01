using System.Text.Json.Serialization;

namespace CurrencyExchangeCustomer.Models
{
    public class Currency : PropertyChangeNotifier
    {
        decimal _rate = 0M;
        string _source = string.Empty;
        string _country = string.Empty;
        string _currencyName = string.Empty;               

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

        public bool IsDeleted { get; set; }

        public string Source
        {
            get => _source;
            set
            {
                _source = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public Currency CurrencyRef
        {
            get => this;
        }
    }    
}
