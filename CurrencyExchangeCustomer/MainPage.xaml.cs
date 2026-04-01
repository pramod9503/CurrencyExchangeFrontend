using CurrencyExchangeCustomer.Abstracts;

namespace CurrencyExchangeCustomer
{
    public partial class MainPage : ContentPage, IDisposable
    {
        IMainPageViewModel? _viewModel;        
        private bool isPageLoaded = false;

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
            BindingContext = this.ViewModel;
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

        public void Dispose()
        {
            this.Loaded -= MainPage_Loaded;
        }
    }

}
