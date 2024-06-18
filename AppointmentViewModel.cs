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
        private readonly SchedulerViewModel _scheduler;

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
        public AppointmentViewModel(IAppointmentService service,
                                    SchedulerViewModel scheduler)
        {
            _service = service;
            _scheduler = scheduler;
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

        [RelayCommand]
        private async Task Add()
        {
            if (string.IsNullOrWhiteSpace(Subject)) return; // Ensure subject is not empty

            var startDateTime = StartDate.Date + StartTime;
            var endDateTime = EndDate.Date + EndTime;

            var appointment = new Appointment
            {
                Subject = Subject,
                StartDate = startDateTime,
                EndDate = endDateTime,
                Description = Description,
                Location = Location,
            };

            var addedAppointment = await _service.AddAppointmentAsync(appointment);

            // Clear properties and reload appointments
            InitializeProperties();

            await Shell.Current.GoToAsync(".."); //navigate to previous page
        }


    }
}
