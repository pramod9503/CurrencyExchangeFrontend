using CurrencyExchangeAdministrator.Abstracts;

namespace CurrencyExchangeAdministrator.Pages;

public partial class EditRatePage : ContentPage
{
    IEditRatePageViewModel? _viewModel;
 
    public IEditRatePageViewModel ViewModel 
    { 
        get => _viewModel!;
        set 
        {
            _viewModel = value;
            OnPropertyChanged();
        } 
    }

    public EditRatePage(IEditRatePageViewModel viewModel)
    {
        InitializeComponent();
        this.ViewModel = viewModel;
        BindingContext = this.ViewModel;
    }

    protected override bool OnBackButtonPressed()
    {
        Dispatcher.Dispatch(async () => 
        {
            await ViewModel.NavigateBack();
        });
        return true;        
    }    
}