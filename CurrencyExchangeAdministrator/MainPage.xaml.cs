using CurrencyExchangeAdministrator.Abstracts;

namespace CurrencyExchangeAdministrator
{
    public partial class MainPage : ContentPage 
    {                        
        private bool isPageLoaded = false;
        private IMainPageViewModel? _viewModel;

        public IMainPageViewModel ViewModel 
        { 
            get => _viewModel!;
            set 
            {
                _viewModel = value;
                OnPropertyChanged();
            } 
        }

        public MainPage(IMainPageViewModel viewModel)
        {            
                InitializeComponent();
                this.ViewModel = viewModel;
                BindingContext = ViewModel;
                this.Loaded += MainPage_Loaded;                           
        }        

        private void MainPage_Loaded(object? sender, EventArgs e)
        {
            if (!isPageLoaded)
            {
                Dispatcher.Dispatch(async () =>
                {
                    await ViewModel.LoadCurrencies();
                    isPageLoaded = true;
                });
            }
        }
    }    
}
