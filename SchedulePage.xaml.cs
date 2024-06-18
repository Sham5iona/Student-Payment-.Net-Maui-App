using StudentPaymentApp.ViewModel;
using Syncfusion.Maui.Scheduler;

namespace StudentPaymentApp.Views
{
    public partial class SchedulePage : ContentPage
    {
        private readonly SchedulerViewModel _scheduler;

        public SchedulePage(SchedulerViewModel scheduler)
        {
            InitializeComponent();
            BindingContext = scheduler;
            Scheduler.AppointmentTextStyle.FontSize = 20;
            _scheduler = scheduler;
        }

        private async void ShowAppointmentPage(object sender, SchedulerTappedEventArgs e)
        {
            if (e.Element == SchedulerElement.Appointment)
            {
                await App.Current.MainPage.DisplayAlert("Message", "Appointment Tapped", "OK");
            }
        }

        private async void AddAppointment(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(AddAppointmentPage));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _scheduler.LoadAppointmentsAsync(); // Load appointments asynchronously
        }


    }
}
