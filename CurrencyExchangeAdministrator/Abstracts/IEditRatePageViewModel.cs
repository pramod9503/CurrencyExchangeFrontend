using System.Windows.Input;
using CurrencyExchangeAdministrator.Models;

namespace CurrencyExchangeAdministrator.Abstracts
{
    public interface IEditRatePageViewModel
    {
        Currency Currency { get;set;}

        EditCurrencyModel RateModel { get; set;}

        bool IsRateUpdated { get; set; }

        ICommand BackCommand { get; }

        ICommand SubmitCommand { get; }

        Task NavigateBack();
    }
}
