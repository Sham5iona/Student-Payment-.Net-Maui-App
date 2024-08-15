using CommunityToolkit.Mvvm.ComponentModel;
using StudentPaymentApp.Model.Services;
using StudentPaymentApp.Views;
using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;


namespace StudentPaymentApp.ViewModel
{
    public partial class SchedulerViewModel : ObservableObject
    {
        private readonly IAppointmentService _service;
        private readonly IStudentService _studentService;
        private readonly IPaymentService _paymentService;
        public ObservableCollection<SchedulerAppointment> Events { get; set; }

        public SchedulerViewModel(IAppointmentService service, IStudentService studentService,
                                  IPaymentService paymentService)
        {

            _service = service;
            Events = new ObservableCollection<SchedulerAppointment>();
            _studentService = studentService;
            _paymentService = paymentService;
        }
        public SchedulerViewModel()
        {
        }

        public async void LoadAppointments()
        {

            var appointments = await _service.ShowAppointmentsAsync();

            if (Events.Any())
            {
                Events.Clear(); // Clear existing appointments
            }

            foreach (var appointment in appointments)
            {
                Events.Add(appointment); // Add retrieved appointments
            }
        }
        public async void CheckFinishedAppointmentsAsync()
        {
            var appointments = await _service.GetAppointmentsAsync();
            var students = await _studentService.GetStudentsAsync();

            foreach (var appointment in appointments)
            {
                if (!appointment.IsFinished.Value)
                {
                    var student = students.FirstOrDefault(s => s.Name == appointment.Subject);

                    if (student != null && appointment.EndDate <= DateTime.Now)
                    {
                        await Task.Delay(100);

                        bool isFinished = await Application.Current.MainPage.DisplayAlert(
                            $"{appointment.Subject} Appointment",
                            $"Is the appointment {appointment.Subject} ->" +
                            $" {appointment.Description} already finished ?",
                            "Yes", "No");


                        if (isFinished)
                        {

                            var studentPayment = await _studentService.GetStudentPaymentByStudentIdAsync(student.Id);

                            studentPayment.Payment.LastAmount -= 10;

                            await _paymentService.UpdatePaymentAsync(studentPayment.Payment);

                            appointment.IsFinished = true;

                            await _service.EditAppointmentAsync(appointment);

                            await Shell.Current.GoToAsync(nameof(SchedulePage));
                        }

                    }
                }
            }
        }

    }
}
