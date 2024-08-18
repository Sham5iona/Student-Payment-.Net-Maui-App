using StudentPaymentApp.ViewModel;

namespace StudentPaymentApp.Views;

public partial class ForecastPopUpPage : ContentPage
{
    private readonly ForecastPopUpViewModel _viewModel;
	public ForecastPopUpPage()
	{
		InitializeComponent();
        _viewModel = new ForecastPopUpViewModel();
        BindingContext = _viewModel;
        _viewModel.ShowForecastInformationAsync();
	}

    private async void OnCloseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

}