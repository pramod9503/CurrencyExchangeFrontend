using CurrencyExchangeAdministrator.Abstracts;

namespace CurrencyExchangeAdministrator.Pages;

public partial class CurrencyPage : ContentPage
{    
	private ICurrencyPageViewModel? _viewModel;    

    public ICurrencyPageViewModel ViewModel
    {
        get => _viewModel!;
		set 
		{
			_viewModel = value;
			OnPropertyChanged(nameof(ViewModel));
		}
    }

    public CurrencyPage(ICurrencyPageViewModel pageViewModel) 
	{
        InitializeComponent();
		ViewModel = pageViewModel;
        BindingContext = ViewModel;        
    }	

	protected override bool OnBackButtonPressed()
    {
		Dispatcher.Dispatch(async ()=> 
		{			            
			await ViewModel.NavigateBackAsync();
        });
		return true;		        
    }    
}