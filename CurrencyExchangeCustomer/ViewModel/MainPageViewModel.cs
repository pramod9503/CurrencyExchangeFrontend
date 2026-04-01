using System.Text.Json;
using System.Diagnostics;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CurrencyExchangeCustomer.Pages;
using CurrencyExchangeCustomer.Models;
using CurrencyExchangeCustomer.Abstracts;
using CurrencyExchangeCustomer.Infrastructure;

namespace CurrencyExchangeCustomer.ViewModels
{
    public class MainPageViewModel : PropertyChangeNotifier, IMainPageViewModel, IDisposable
    {
        string _status = string.Empty;
        IServiceProvider _serviceProvider;
        CurrenciesModel _currenciesModel = new();                

        public NoticeBoardModel BoardModel { get; set; } = new();

        public LiveStatusModel LiveStatus { get; set; } = new();

        public string Status 
        {
            get => App.CurrenciesModel.Status;
            set 
            {
                App.CurrenciesModel.Status = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Currency> Currencies 
        {
            get => App.CurrenciesModel.Currencies;
            set 
            {
                App.CurrenciesModel.Currencies = value;
                OnPropertyChanged();
            }
        }        

        public MainPageViewModel(IServiceProvider serviceProvider) 
        {
            _serviceProvider = serviceProvider;            
            LiveStatusService.ConnectionStatusChanged += LiveStatusService_ConnectionStatusChanged;
            CurrencyUpdaterService.CurrencyChanged += CurrencyUpdaterService_CurrencyChanged;                       
        }        

        private void LiveStatusService_ConnectionStatusChanged(object? sender, bool e)
        {
            if (e)
            {
                this.LiveStatus.ConnectionStatus = "Connected";
                this.LiveStatus.StatusColor = Colors.Green;
            }
            else
            {
                this.LiveStatus.ConnectionStatus = "Disconnected";
                this.LiveStatus.StatusColor = Colors.Red;
            }
        }

        private void CurrencyUpdaterService_CurrencyChanged(object? sender, CurrencyMessageModel e)
        {
            switch (e.Operation)
            {
                case CurrencyOperationEnum.CurrencyAdded:
                    BoardModel.BoardMessage = $"New currency {e.Currency?.CurrencyName} of {e.Currency?.Country}" +
                        $" added with rate \u20b9 {e.Currency?.Rate}.";
                    break;
                case CurrencyOperationEnum.RateUpdate:
                    BoardModel.BoardMessage = $"Rate of {e.Currency?.CurrencyName} of {e.Currency?.Country}" +
                        $" changed to \u20b9 {e.Currency?.Rate}.";
                    break;
                case CurrencyOperationEnum.CurrencyUpdate:
                    BoardModel.BoardMessage = $"Currency {e.Currency?.CurrencyName} of {e.Currency?.Country}" +
                        $" with rate \u20b9 {e.Currency?.Rate} has been updated.";
                    break;
                case CurrencyOperationEnum.CurrencySoftDelete:
                    BoardModel.BoardMessage = $"Currency {e.Currency?.CurrencyName} of {e.Currency?.Country}" +
                        $" with rate \u20b9 {e.Currency?.Rate} has been deleted.";
                    break;
            }

            BoardModel.IsBoardVisible = true;
        }

        public ICommand CurrencyTappedCommand => new Command(async (args) =>
        {
            Currency currency = (Currency)args;            
            CurrencyPage? curPage = _serviceProvider.GetService<CurrencyPage>();
            ICurrencyPageViewModel? pageModel = _serviceProvider.GetService<ICurrencyPageViewModel>();            
            if (pageModel == null) return;
            pageModel.Currency = currency;
            await Shell.Current.GoToAsync(nameof(CurrencyPage));
        });

        public ICommand GetDataCommand => new Command(async () => 
        {
            await LoadCurrencies();
            if (this.LiveStatus.ConnectionStatus == "Disconnected")
            {
                RetryConnectModel.SetRetryRequest(true);
            }
        });

        public async Task LoadCurrencies()
        {
            try
            {
                Uri uri = new(ServerUrls.Currencies, UriKind.Relative);
                HttpClient client = CustomerClient.HttpClient;
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string resp = await response.Content.ReadAsStringAsync();
                    CurrenciesModel curModel = JsonSerializer.Deserialize<CurrenciesModel>(resp, CustomerClient.JsonSerializerOptions)!;                    
                    this.Status = curModel.Status;                    
                    this.Currencies.Clear();
                    foreach (var curruncy in curModel.Currencies)
                    {                        
                        this.Currencies.Add(curruncy);
                    }
                    BoardModel.BoardMessage = string.Empty;
                    BoardModel.IsBoardVisible = false;
                }
                else
                {
                    string resp = await response.Content.ReadAsStringAsync();
                    if (resp == string.Empty) resp = "Error: Something went wrong on the server.";
                    BoardModel.BoardMessage = resp;
                    BoardModel.IsBoardVisible = true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                BoardModel.BoardMessage = ex.Message;
                BoardModel.IsBoardVisible = true;
            }
        }

        public void Dispose()
        {
            LiveStatusService.ConnectionStatusChanged -= LiveStatusService_ConnectionStatusChanged;
            CurrencyUpdaterService.CurrencyChanged -= CurrencyUpdaterService_CurrencyChanged;
        }
    }
}
