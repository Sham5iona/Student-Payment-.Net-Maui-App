using StudentPaymentApp.ViewModel;

namespace StudentPaymentApp.Views;

public partial class AnalyticsPage : ContentPage
{
	private readonly AnalyticsViewModel _viewModel;
	public AnalyticsPage(AnalyticsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel;
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        // Scroll to the top of the ScrollView with a slight delay
        
        _viewModel.LoadFilteredAppointmentsAsync();
		_viewModel.LoadFilteredStudentsAsync();
        await Task.Delay(100); // Adjust the delay as necessary to be able to
        //show the top of the page everytime
        await ScrollView.ScrollToAsync(0, 0, false);
        await ScrollView2.ScrollToAsync(0, 0, false);
    }
}