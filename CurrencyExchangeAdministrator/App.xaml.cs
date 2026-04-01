using CurrencyExchangeAdministrator.Models;

namespace CurrencyExchangeAdministrator
{
    public partial class App : Application
    {
        public static CurrenciesModel CurrenciesModel { get; set; } = new();

        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}