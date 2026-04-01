using System.Text.Json.Serialization;
using CurrencyExchangeAdministrator.Abstracts;

namespace CurrencyExchangeAdministrator.Models
{    
    public class Currency : CurrencyBase
    {        
        string _source = string.Empty;        

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
