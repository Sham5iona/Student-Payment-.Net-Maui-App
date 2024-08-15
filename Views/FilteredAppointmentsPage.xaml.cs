using StudentPaymentApp.Model.Services;
using StudentPaymentApp.ViewModel;

namespace StudentPaymentApp.Views;

public partial class FilteredAppointmentsPage : ContentPage
{
	private readonly string _searchText;
	private readonly AppointmentViewModel _viewModel;
	private readonly IAppointmentService _service;
	public FilteredAppointmentsPage(object searchText,
									AppointmentViewModel viewModel,
									IAppointmentService service)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel;
		_service = service;
		_searchText = searchText.ToString();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
		await _viewModel.LoadFilteredAppointmentsAsync(_searchText);
    }
	private async void GoBack(object sender, EventArgs args)
	{
		await Shell.Current.GoToAsync("..");
	}
}