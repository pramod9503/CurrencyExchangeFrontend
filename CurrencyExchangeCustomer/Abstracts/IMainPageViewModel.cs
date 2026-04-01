using System.Windows.Input;
using System.Collections.ObjectModel;
using CurrencyExchangeCustomer.Models;

namespace CurrencyExchangeCustomer.Abstracts
{
    public interface IMainPageViewModel
    {
        NoticeBoardModel BoardModel { get; set; }

        LiveStatusModel LiveStatus { get; set; }        

        string Status { get; set; }

        ObservableCollection<Currency> Currencies { get; set; }

        ICommand CurrencyTappedCommand { get; }

        ICommand GetDataCommand { get; }

        public Task LoadCurrencies();        
    }
}
