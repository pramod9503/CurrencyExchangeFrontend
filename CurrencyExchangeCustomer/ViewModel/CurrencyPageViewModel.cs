using System.Text.Json;
using System.Windows.Input;
using CurrencyExchangeCustomer.Models;
using CurrencyExchangeCustomer.Abstracts;
using CurrencyExchangeCustomer.Infrastructure;

namespace CurrencyExchangeCustomer.ViewModels
{       
    public class CurrencyPageViewModel : PropertyChangeNotifier, ICurrencyPageViewModel
    {
        Currency _currency = new();        
        public NoticeBoardModel BoardModel { get; set; } = new();        

        public Currency Currency
        {
            get => _currency;
            set
            {
                _currency = value;
                OnPropertyChanged();
            }
        }        

        public CurrencyPageViewModel() {}

        public ICommand BackCommand => new Command(async () =>
        {            
             await NavigateBackAsync();            
        });

        public ICommand RefreshCommand => new Command(async () =>
        {
            try
            {
                if (BoardModel.IsBoardVisible) BoardModel.IsBoardVisible = false;
                Uri uri = new(Path.Combine(ServerUrls.Currencies, this.Currency.Id.ToString()), UriKind.Relative);
                HttpClient client = CustomerClient.HttpClient;
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string resp = await response.Content.ReadAsStringAsync();
                    Currency? cur = JsonSerializer.Deserialize<Currency>(resp, CustomerClient.JsonSerializerOptions);
                    if (cur != null)
                    {
                        this.Currency = cur;
                    }
                    else
                    {
                        BoardModel.BoardMessage = "Currency is null.";
                        BoardModel.IsBoardVisible = true;
                    }
                }
                else
                {
                    string resp = await response.Content.ReadAsStringAsync();
                    BoardModel.BoardMessage = resp;
                    BoardModel.IsBoardVisible = true;
                }
            }
            catch (Exception ex)
            {
                BoardModel.BoardMessage = ex.Message;
                BoardModel.IsBoardVisible = true;
            }
        });        

        public async Task NavigateBackAsync()
        {
            BoardModel.BoardMessage = string.Empty;
            BoardModel.IsBoardVisible = false;            
            await Shell.Current.GoToAsync("..");
        }
    }
}
