using StudentPaymentApp.Model;
using StudentPaymentApp.Model.Services;
using StudentPaymentApp.ViewModel;
using Syncfusion.Maui.Scheduler;

namespace StudentPaymentApp.Views;

public partial class AppointmentDetailsPage : ContentPage
{
	private readonly IAppointmentService _service;
	private readonly AppointmentViewModel _viewModel;
	private SchedulerAppointment _appointment { get; set; }
	public AppointmentDetailsPage(object appointment, IAppointmentService service, AppointmentViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = appointment;
		_service = service;
		_viewModel = viewModel;
		_appointment = (SchedulerAppointment)appointment;
	}

    public AppointmentDetailsPage()
    {
		InitializeComponent();
    }

	private async void Delete(object sender, EventArgs e)
	{
		var appointment = _service.ConfigureAppointment(_appointment);

		bool isConfirmed = await DisplayAlert("Delete an appointment", "Are you sure ?", "OK", "Cancel");

		if (isConfirmed)
		{
			await _service.DeleteAppointmentAsync(appointment);
			await Shell.Current.GoToAsync("../");
		}

	}

	private async void Edit(object sender, EventArgs e)
	{
        var appointment = _service.ConfigureAppointment(_appointment);

		appointment.Id = await _service.GetAppointmentIdByDate(appointment);

		await Navigation.PushAsync(new EditAppointmentPage(_viewModel, appointment));

    }

    private async void GoBack(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync(".."); //navigate to previous page
    }
}