using StudentPaymentApp.Model;
using StudentPaymentApp.Model.Services;
using StudentPaymentApp.ViewModel;
using Syncfusion.Maui.Scheduler;
using System.Diagnostics;
using System.Threading.Tasks;

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
            SearchField.Text = string.Empty;
            _scheduler.LoadAppointments();
            _scheduler.CheckFinishedAppointmentsAsync();
        }
        private async void ShowAddAppointmentWithFixedDate(object sender, SchedulerDoubleTappedEventArgs e)
        {
            if(e.Element == SchedulerElement.Appointment)
            {
                return;
            }
            else if(e.Element == SchedulerElement.SchedulerCell)
            {
                  _viewModel.StartDate = e.Date.Value;

                  _viewModel.StartTime = e.Date.Value.TimeOfDay;

                  _viewModel.EndDate = e.Date.Value.AddHours(1.5);

                  _viewModel.EndTime = e.Date.Value.TimeOfDay
                                      .Add(TimeSpan.FromHours(1.5));

                  await Navigation.PushAsync(new AddAppointmentPage(_viewModel));
                
            }
                
        }

        private async void RedirectToFilteredResults(object sender, TappedEventArgs args)
        {
            await Navigation.PushAsync(new FilteredAppointmentsPage(SearchField.Text,
                                           _viewModel, _appointmentService));
        }
        
    }
}
