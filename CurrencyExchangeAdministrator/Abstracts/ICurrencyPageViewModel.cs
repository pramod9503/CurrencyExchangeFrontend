using System.Windows.Input;
using CurrencyExchangeAdministrator.Models;

namespace CurrencyExchangeAdministrator.Abstracts
{
    public interface ICurrencyPageViewModel
    {
        Currency Currency { get; set; }        

        NoticeBoardModel BoardModel { get; set; }

        ICommand BackCommand { get; }

        ICommand RefreshCommand {  get; }

        ICommand NavEditRateCommand { get; }

        ICommand NavEditCurrencyCommand { get; }

        Task NavigateBackAsync();
    }
}
