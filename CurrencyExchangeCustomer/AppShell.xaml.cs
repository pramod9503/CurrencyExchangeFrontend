using System.Diagnostics;
using CurrencyExchangeCustomer.Pages;
using CurrencyExchangeCustomer.Models;
using Microsoft.AspNetCore.SignalR.Client;
using CurrencyExchangeCustomer.Infrastructure;

namespace CurrencyExchangeCustomer
{
    public partial class AppShell : Shell, IDisposable
    {
        HubConnection connection;

        public AppShell()
        {
            InitializeComponent();
            BindingContext = this;
            Routing.RegisterRoute(nameof(CurrencyPage), typeof(CurrencyPage));
            connection = new HubConnectionBuilder()
                .WithUrl($"{CustomerClient.SignalRUrl}Currency/NotifyUpdate").Build();
            RetryConnectModel.RetryConnectRequested += RetryConnectModel_RetryConnectRequested;
            connection.Closed += async (error) =>
            {
                try
                {
                    await Task.Delay(new Random().Next(0, 5) * 1000);
                    await connection.StartAsync();
                    LiveStatusService.ChangeConnected(true);
                }
                catch
                {
                    LiveStatusService.ChangeConnected(false);
                    Debug.Print("Couldn't connect to the SignalR Server");
                }
            };

            this.Loaded += AppShell_Loaded;
        }

        private async void RetryConnectModel_RetryConnectRequested(object? sender, bool e)
        {
            try
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
                LiveStatusService.ChangeConnected(true);
            }
            catch
            {
                LiveStatusService.ChangeConnected(false);
                Debug.Print("Couldn't connect to the SignalR Server");
            }
        }

        private async void AppShell_Loaded(object? sender, EventArgs e)
        {
            connection.On<CurrencyMessageModel>("ReceiveUpdate", async (message) =>
            {
                await Dispatcher.DispatchAsync(() =>
                {
                    CurrencyMessageModel? messageModel = (CurrencyMessageModel)message;
                    if (messageModel == null) return;
                    switch (messageModel.Operation)
                    {
                        case CurrencyOperationEnum.CurrencyAdded:
                            if (messageModel.Currency == null) return;
                            App.CurrenciesModel.Currencies.Add(messageModel.Currency);
                            break;
                        case CurrencyOperationEnum.RateUpdate:
                            Currency? rupCurrency = App.CurrenciesModel.Currencies.Where(x => x.Id == messageModel?.Currency?.Id).SingleOrDefault();
                            if (rupCurrency == null) return;
                            rupCurrency.Rate = messageModel.Currency?.Rate ?? 0M;
                            break;
                        case CurrencyOperationEnum.CurrencyUpdate:
                            Currency? upCurrency = App.CurrenciesModel.Currencies.Where(x => x.Id == messageModel?.Currency?.Id).SingleOrDefault();
                            if (upCurrency == null) return;
                            upCurrency.CurrencyName = messageModel.Currency?.CurrencyName ?? string.Empty;
                            upCurrency.Country = messageModel.Currency?.Country ?? string.Empty;
                            upCurrency.Source = messageModel.Currency?.Source ?? string.Empty;
                            upCurrency.Rate = messageModel.Currency?.Rate ?? 0M;
                            break;
                        case CurrencyOperationEnum.CurrencySoftDelete:
                            Currency? dCurrency = App.CurrenciesModel.Currencies.Where(x => x.Id == messageModel?.Currency?.Id).SingleOrDefault();
                            if (dCurrency == null) return;
                            App.CurrenciesModel.Currencies.Remove(dCurrency);
                            break;
                    }
                    CurrencyUpdaterService.SetUpdate(messageModel);
                });
            });

            try
            {
                await connection.StartAsync();
                Debug.Print("Connection started.");
                LiveStatusService.ChangeConnected(true);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                LiveStatusService.ChangeConnected(false);
            }
        }

        public void Dispose()
        {
            RetryConnectModel.RetryConnectRequested -= RetryConnectModel_RetryConnectRequested;
            this.Loaded -= AppShell_Loaded;
        }
    }
}
