using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudentPaymentApp.Model;
using StudentPaymentApp.Model.Services;
using StudentPaymentApp.Views;

namespace StudentPaymentApp.ViewModel
{
    public partial class AppointmentViewModel : ObservableObject
    {
        private readonly IAppointmentService _service;
        private static int _id;

        [ObservableProperty]
        private string subject;

        [ObservableProperty]
        private DateTime startDate;

        [ObservableProperty]
        private TimeSpan startTime;

        [ObservableProperty]
        private DateTime endDate;

        [ObservableProperty]
        private TimeSpan endTime;

        [ObservableProperty]
        private string description;

        [ObservableProperty]
        private string location;

        public AppointmentViewModel()
        {
            
        }

        // Combine the constructors to ensure IAppointmentService is always initialized
        public AppointmentViewModel(IAppointmentService service)
        {
            _service = service;

            InitializeProperties();
        }

        public void InitializeProperties()
        {
            // Initialize properties with default values
            StartDate = DateTime.Now;
            EndDate = DateTime.Now;
            StartTime = DateTime.Now.TimeOfDay;
            EndTime = DateTime.Now.TimeOfDay;
            Subject = string.Empty;
            Location = string.Empty;
            Description = string.Empty;
        }

        public void ExtractProperties(Appointment appointment)
        {

            if (appointment == null) 
                throw new ArgumentNullException(nameof(appointment));

            _id = appointment.Id;
            StartDate = appointment.StartDate;
            EndDate = appointment.EndDate;
            StartTime = appointment.StartDate.TimeOfDay;
            EndTime = appointment.EndDate.TimeOfDay;
            Subject = appointment.Subject;
            Location = appointment.Location;
            Description = appointment.Description;
        }

        [RelayCommand]
        private async Task AddAsync()
        {

            var startDateTime = StartDate.Date + StartTime;
            var endDateTime = EndDate.Date + EndTime;
            DateTime now = DateTime.Now;

            // Ensure input data is all valid
            if (string.IsNullOrWhiteSpace(Subject) || startDateTime < now.Date || endDateTime <= now ||
            DateTime.Compare(startDateTime, endDateTime) >= 0 ||
            string.IsNullOrWhiteSpace(Location) || string.IsNullOrWhiteSpace(Description))
            {
                //Send a message request by Messaging Center to be shown on the view that is subscribed to this view model
                MessagingCenter.Send(this, "Invalid input data!");

                return;

            }

            var appointments = await _service.GetAppointmentsAsync();
            
            var exists = appointments.Any(a =>
                a.StartDate == startDateTime || a.EndDate == endDateTime);

            if (exists)
            {
                MessagingCenter.Send(this, "Appointment already exists!");
                return;
            }

            var appointment = new Appointment
            {
                Subject = Subject,
                StartDate = startDateTime,
                EndDate = endDateTime,
                Description = Description,
                Location = Location,
            };

            await _service.AddAppointmentAsync(appointment);

            // Clear properties and reload appointments
            InitializeProperties();

            await Shell.Current.GoToAsync(".."); //navigate to previous page
        }

        [RelayCommand]
        private async Task EditAsync()
        {
            var startDateTime = StartDate.Date + StartTime;
            var endDateTime = EndDate.Date + EndTime;
            DateTime now = DateTime.Now;

            // Ensure input data is all valid
            if (string.IsNullOrWhiteSpace(Subject) || startDateTime < now.Date || endDateTime <= now.Date ||
            DateTime.Compare(startDateTime, endDateTime) >= 0 ||
            string.IsNullOrWhiteSpace(Location) || string.IsNullOrWhiteSpace(Description))
            {
                //Send a message request by Messaging Center to be shown on the view that is subscribed to this view model
                MessagingCenter.Send(this, "Invalid input data!");

                return;

            }

            var appointments = await _service.GetAppointmentsAsync();

            var exists = appointments.Any(a =>
                a.StartDate == startDateTime || a.EndDate == endDateTime);

            if (exists)
            {
                MessagingCenter.Send(this, "Appointment already exists!");
                return;
            }

            var appointment = new Appointment
            {
                Id = _id,
                Subject = Subject,
                StartDate = startDateTime,
                EndDate = endDateTime,
                Description = Description,
                Location = Location
            };

            await _service.EditAppointmentAsync(appointment);

            // Clear properties and reload appointments
            InitializeProperties();

            await Shell.Current.GoToAsync(nameof(SchedulePage)); //navigate to previous page
        }

    }
}
