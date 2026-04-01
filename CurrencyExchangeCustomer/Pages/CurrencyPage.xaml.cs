using CurrencyExchangeCustomer.Abstracts;

namespace CurrencyExchangeCustomer.Pages;

public partial class CurrencyPage : ContentPage 
{
    ICurrencyPageViewModel? _viewModel;

    public ICurrencyPageViewModel ViewModel 
    { 
        get => _viewModel!;
        set 
        {
            _viewModel = value;
            OnPropertyChanged();
        } 
    }    

    public CurrencyPage(ICurrencyPageViewModel viewModel) 
    {
        InitializeComponent();
        this.ViewModel = viewModel;
        BindingContext = this.ViewModel;
    }

    protected override bool OnBackButtonPressed()
    {
        Dispatcher.Dispatch(async () =>
        {            
            await ViewModel.NavigateBackAsync();
        });
        return true;
    }    
}