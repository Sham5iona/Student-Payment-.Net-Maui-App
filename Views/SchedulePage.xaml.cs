using StudentPaymentApp.Model;
using StudentPaymentApp.Model.Services;
using StudentPaymentApp.ViewModel;
using Syncfusion.Maui.Scheduler;
using System.Diagnostics;

namespace StudentPaymentApp.Views
{
    public partial class SchedulePage : ContentPage
    {
        private readonly SchedulerViewModel _scheduler;
        private readonly IAppointmentService _appointmentService;
        private readonly AppointmentViewModel _viewModel;
        public SchedulePage(SchedulerViewModel scheduler, IAppointmentService appointmentService,
                            AppointmentViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = scheduler;
            Scheduler.AppointmentTextStyle.FontSize = 20;
            _scheduler = scheduler;
            _appointmentService = appointmentService;
            _viewModel = viewModel;
        }

        private async void OnAddImageTapped(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AddAppointmentPage));
        }


        private async void ShowAppointmentPage(object sender, SchedulerTappedEventArgs e)
        {
            if(e.Element == SchedulerElement.Appointment)
            {
                var appointment = e.Appointments.FirstOrDefault();
                if(appointment != null)
                {
                    await Navigation.PushAsync(new AppointmentDetailsPage(appointment, _appointmentService, _viewModel));
                }
            }
        }

       

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _scheduler.LoadAppointmentsAsync(); // Load appointments asynchronously
        }


    }
}
