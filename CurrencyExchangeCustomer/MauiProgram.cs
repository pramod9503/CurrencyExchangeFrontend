using Microsoft.Extensions.Logging;
using CurrencyExchangeCustomer.Pages;
using CurrencyExchangeCustomer.Abstracts;
using CurrencyExchangeCustomer.ViewModels;

namespace CurrencyExchangeCustomer
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });
            builder.Services.AddSingleton<CurrencyPage>();
            builder.Services.AddSingleton<IMainPageViewModel, MainPageViewModel>();
            builder.Services.AddSingleton<ICurrencyPageViewModel, CurrencyPageViewModel>();
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
