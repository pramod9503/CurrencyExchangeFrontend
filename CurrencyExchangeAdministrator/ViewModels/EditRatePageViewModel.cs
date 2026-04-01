using System.Text.Json;
using System.Windows.Input;
using CurrencyExchangeAdministrator.Models;
using CurrencyExchangeAdministrator.Abstracts;
using CurrencyExchangeAdministrator.Infrastructure;

namespace CurrencyExchangeAdministrator.ViewModels
{
    public class EditRatePageViewModel : PropertyChangeNotifier, IEditRatePageViewModel
    {
        Currency _currency = new();
        EditCurrencyModel _editRateModel = new();

        public Currency Currency
        {
            get => _currency;
            set
            {
                _currency = value;
                _editRateModel.Id = _currency.Id;
                _editRateModel.Country = _currency.Country;
                _editRateModel.CurrencyName = _currency.CurrencyName;
                _editRateModel.Rate = _currency.Rate;
            }
        }

        public EditCurrencyModel RateModel
        {
            get => _editRateModel;
            set
            {
                _editRateModel = value;
                OnPropertyChanged();
            }
        }

        public bool IsRateUpdated { get; set; }

        public ICommand BackCommand => new Command(async () =>
        {
            await NavigateBack();
        });

        public ICommand SubmitCommand => new Command(async () =>
        {            
            if (_editRateModel.Rate == 0M) 
            {
                await Shell.Current.DisplayAlert("Error", "Currency rate cannot be zero?", "Ok");
                return;
            }
            if (_editRateModel.Rate == this.Currency.Rate) 
            {
                await Shell.Current.DisplayAlert("Error", "No change in currency found?", "Ok");
                return;
            }
            bool result = await Shell.Current.DisplayAlert("Warning", "Are you sure to change the currency rate?", "Go ahead", "Cancel");
            if (result == false) return;
            UpdateRateModel rateModel = new()
            {
                Id = _editRateModel.Id,
                Rate = _editRateModel.Rate,
            };
            Uri uri = new(ServerUrls.UpdateRate, UriKind.Relative);
            HttpClient client = AdministratorClient.HttpClient;
            string json = JsonSerializer.Serialize<UpdateRateModel>(rateModel, AdministratorClient.JsonSerializerOptions);
            StringContent content = new(json, System.Text.Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await client.PutAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    string resp = await response.Content.ReadAsStringAsync();
                    Currency repCurrency = JsonSerializer.Deserialize<Currency>(resp, AdministratorClient.JsonSerializerOptions)!;
                    this.Currency = repCurrency;
                    this.IsRateUpdated = true;                    
                    await Shell.Current.GoToAsync("..", new Dictionary<string, object> {
                        {"EditPageResponse", new EditPageResponse{ 
                                UpdatedCurrency = Currency, 
                                IsRateUpdated = true } 
                        }
                    });
                }
                else
                {
                    string resp = await response.Content.ReadAsStringAsync();                    
                    await Shell.Current.DisplayAlert("Error", resp, "Ok");
                }
            }
            catch (Exception ex)
            {                
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
        });

        public async Task NavigateBack()
        {            
            await Shell.Current.GoToAsync("..");
        }
    }
}
