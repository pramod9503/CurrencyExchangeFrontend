using System.Windows.Input;
using CurrencyExchangeCustomer.Models;

namespace CurrencyExchangeCustomer.Abstracts
{
    public interface ICurrencyPageViewModel
    {
        Currency Currency { get; set; }        

        NoticeBoardModel BoardModel { get; set; }

        ICommand BackCommand { get; }

        ICommand RefreshCommand {  get; }

        Task NavigateBackAsync();
    }
}
