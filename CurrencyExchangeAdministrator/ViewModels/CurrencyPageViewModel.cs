using System.Text.Json;
using System.Windows.Input;
using CurrencyExchangeAdministrator.Pages;
using CurrencyExchangeAdministrator.Models;
using CurrencyExchangeAdministrator.Abstracts;
using CurrencyExchangeAdministrator.Infrastructure;

namespace CurrencyExchangeAdministrator.ViewModels
{    
    [QueryProperty(nameof(EditPageResponse), "EditPageResponse")]
    public class CurrencyPageViewModel : PropertyChangeNotifier, ICurrencyPageViewModel
    {
        Currency _currency = new();
        IServiceProvider _serviceProvider;
        public NoticeBoardModel BoardModel { get; set; } = new();

        public EditPageResponse? EditPageResponse { get; set; } = new();

        public Currency Currency
        {
            get => _currency;
            set
            {
                _currency = value;
                OnPropertyChanged();
            }
        }        

        public CurrencyPageViewModel(IServiceProvider serviceProvider) 
        {
            _serviceProvider = serviceProvider;
        }

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
                HttpClient client = AdministratorClient.HttpClient;
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string resp = await response.Content.ReadAsStringAsync();
                    Currency? cur = JsonSerializer.Deserialize<Currency>(resp, AdministratorClient.JsonSerializerOptions);
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

        public ICommand NavEditRateCommand => new Command(async () =>
        {            
            EditRatePage? ratePage = _serviceProvider.GetService<EditRatePage>();
            IEditRatePageViewModel? rateViewModel = _serviceProvider.GetService<IEditRatePageViewModel>();
            if (ratePage == null) return;
            if (rateViewModel == null) return;
            rateViewModel.Currency = this.Currency;
            ratePage.Unloaded += RatePage_Unloaded;            
            await Shell.Current.GoToAsync(nameof(EditRatePage));
        });

        private void RatePage_Unloaded(object? sender, EventArgs e)
        {
            if (this.EditPageResponse == null) return;
            EditRatePage? ratePage = sender as EditRatePage;
            if (ratePage == null) return;            
            if (this.EditPageResponse.IsRateUpdated)
            {
                this.Currency = this.EditPageResponse.UpdatedCurrency; 
                this.EditPageResponse.UpdatedCurrency = new();
                this.EditPageResponse.IsRateUpdated = false;
                BoardModel.BoardMessage = "Currency rate successfully updated.";
                BoardModel.IsBoardVisible = true;
            }
            ratePage.Unloaded -= RatePage_Unloaded;
        }

        public ICommand NavEditCurrencyCommand => new Command(async () =>
        {            
            await Shell.Current.DisplayAlert("Information", "This feature is not implemented yet.", "Ok");
        });

        public async Task NavigateBackAsync()
        {
            BoardModel.BoardMessage = string.Empty;
            BoardModel.IsBoardVisible = false;            
            await Shell.Current.GoToAsync("..");
        }
    }
}
