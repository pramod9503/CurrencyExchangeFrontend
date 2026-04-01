using Microsoft.Extensions.Logging;
using CurrencyExchangeAdministrator.Pages;
using CurrencyExchangeAdministrator.Abstracts;
using CurrencyExchangeAdministrator.ViewModels;

namespace CurrencyExchangeAdministrator
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
            builder.Services.AddSingleton<EditRatePage>();
            builder.Services.AddSingleton<IMainPageViewModel, MainPageViewModel>();
            builder.Services.AddSingleton<ICurrencyPageViewModel, CurrencyPageViewModel>();
            builder.Services.AddSingleton<IEditRatePageViewModel, EditRatePageViewModel>();
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
