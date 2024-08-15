using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudentPaymentApp.Model;
using StudentPaymentApp.Model.Services;
using StudentPaymentApp.Views;
using System.Collections.ObjectModel;

namespace StudentPaymentApp.ViewModel
{
    public partial class AppointmentViewModel : ObservableObject
    {
        private readonly IAppointmentService _service;
        private readonly IStudentService _studentService;
        private readonly IPaymentService _paymentService;

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

        [ObservableProperty]
        private string selectedType;

        [ObservableProperty]
        private bool? isFinished;

        [ObservableProperty]
        private ObservableCollection<string> appointmentTypes;

        [ObservableProperty]
        private ObservableCollection<Appointment> appointments;

        public AppointmentViewModel(IAppointmentService service,
                                    IStudentService studentService,
                                    IPaymentService paymentService)
        {
            _service = service;
            _studentService = studentService;
            _paymentService = paymentService;
            Appointments = new ObservableCollection<Appointment>();
            InitializeAppointmentTypes();
            InitializeProperties();
        }

        public AppointmentViewModel()
        {
            
        }
        private void InitializeAppointmentTypes()
        {
            AppointmentTypes = new ObservableCollection<string>
            {
                "Once", "Everyday", "Working days", "Every week", "Every month"
            };
        }

        public async Task LoadFilteredAppointmentsAsync(string searchText)
        {
            var filteredAppointments = await _service.FilterAppointmentsBySearchTextAsync(searchText);

            Appointments.Clear();

            foreach(var appointment in filteredAppointments)
            {
                Appointments.Add(appointment);
            }
        }

        public void InitializeProperties()
        {
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
            IsFinished = appointment.IsFinished;
        }

        private async Task<bool> ValidateAppointmentAsync(DateTime startDateTime, DateTime endDateTime)
        {
            if (string.IsNullOrWhiteSpace(Subject) ||
                startDateTime >= endDateTime ||
                string.IsNullOrWhiteSpace(Location) ||
                string.IsNullOrWhiteSpace(Description))
            {
                MessagingCenter.Send(this, "Invalid input data!");
                return false;
            }

            var appointments = await _service.GetAppointmentsAsync();
            bool exists = appointments.Any(a => (a.StartDate == startDateTime || a.EndDate == endDateTime)
                          && a.Id != _id);

            if (exists)
            {
                MessagingCenter.Send(this, "Appointment already exists!");
                return false;
            }

            return true;
        }

        private async Task ScheduleAppointmentsAsync(DateTime startDateTime, DateTime endDateTime, Func<DateTime, DateTime> incrementDate)
        {
            var deadline = new DateTime(endDateTime.Year + 1, endDateTime.Month, endDateTime.Day);

            for (DateTime currentDate = startDateTime.Date; currentDate <= deadline; currentDate = incrementDate(currentDate))
            {
                var automatedAppointment = new Appointment
                {
                    Subject = Subject,
                    StartDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day,
                                              startDateTime.Hour, startDateTime.Minute, startDateTime.Second),
                    EndDate = new DateTime(currentDate.Year, currentDate.Month, currentDate.Day,
                                            endDateTime.Hour, endDateTime.Minute, endDateTime.Second),
                    Description = Description,
                    Location = Location,
                    IsFinished = false
                };

                await _service.AddAppointmentAsync(automatedAppointment);
            }
        }

        [RelayCommand]
        private async Task AddAsync()
        {
            var startDateTime = StartDate.Date + StartTime;
            var endDateTime = EndDate.Date + EndTime;

            if (!await ValidateAppointmentAsync(startDateTime, endDateTime)) return;

            switch (SelectedType)
            {
                case "Once":
                    await _service.AddAppointmentAsync(new Appointment
                    {
                        Subject = Subject,
                        StartDate = startDateTime,
                        EndDate = endDateTime,
                        Description = Description,
                        Location = Location,
                        IsFinished = false,
                        LastModification = DateTime.Now
                    });
                    break;

                case "Everyday":
                    await ScheduleAppointmentsAsync(startDateTime, endDateTime, date => date.AddDays(1));
                    break;

                case "Every week":
                    await ScheduleAppointmentsAsync(startDateTime, endDateTime, date => date.AddDays(7));
                    break;

                case "Every month":
                    await ScheduleAppointmentsAsync(startDateTime, endDateTime, date => date.AddMonths(1));
                    break;

                case "Working days":
                    await ScheduleAppointmentsAsync(startDateTime, endDateTime, date => date.AddDays(1));
                    break;

                default:
                    MessagingCenter.Send(this, "Unknown appointment type!");
                    return;
            }

            InitializeProperties();
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task EditAsync()
        {
            var startDateTime = StartDate.Date + StartTime;
            var endDateTime = EndDate.Date + EndTime;

            if (!await ValidateAppointmentAsync(startDateTime, endDateTime)) return;

            var appointments = await _service.GetAppointmentsAsync();
            var currentAppointment = appointments.FirstOrDefault(a => a.Id == _id);

            // Store original values
            var originalSubject = currentAppointment.Subject;
            var originalStartDate = currentAppointment.StartDate;
            var originalEndDate = currentAppointment.EndDate;
            var originalDescription = currentAppointment.Description;
            var originalLocation = currentAppointment.Location;
            var originalIsFinished = currentAppointment.IsFinished;

            // Check if any property has changed
            bool isChanged =
                originalSubject != Subject ||
                originalStartDate != startDateTime ||
                originalEndDate != endDateTime ||
                originalDescription != Description ||
                originalLocation != Location ||
                originalIsFinished != IsFinished;

            // If nothing has changed, navigate back without making any updates
            if (!isChanged)
            {
                await Shell.Current.GoToAsync("..");
                return;
            }

            // Proceed with updating the appointment
            var appointment = new Appointment
            {
                Id = _id,
                Subject = Subject,
                StartDate = startDateTime,
                EndDate = endDateTime,
                Description = Description,
                Location = Location,
                IsFinished = IsFinished,
                LastModification = DateTime.Now
            };

            await _service.EditAppointmentAsync(appointment);

            // Handle logic for marking an appointment as finished
            if (!originalIsFinished.Value && IsFinished.Value)
            {
                var students = await _studentService.GetStudentsAsync();
                var student = students.FirstOrDefault(a => a.Name == Subject);
                var studentPayment = await _studentService.GetStudentPaymentByStudentIdAsync(student.Id);

                studentPayment.Payment.LastAmount -= 10;
                await _paymentService.UpdatePaymentAsync(studentPayment.Payment);
            }

            InitializeProperties();
            await Shell.Current.GoToAsync(nameof(SchedulePage));
        }

    }
}
