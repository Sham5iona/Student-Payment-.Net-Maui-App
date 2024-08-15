using CommunityToolkit.Mvvm.ComponentModel;
using StudentPaymentApp.Model;
using StudentPaymentApp.Model.Services;
using System.Collections.ObjectModel;

namespace StudentPaymentApp.ViewModel
{
    public partial class AnalyticsViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Appointment> appointments;

        [ObservableProperty]
        private ObservableCollection<Student> students;

        private readonly IAnalyticsService _service;

        public AnalyticsViewModel(IAnalyticsService service)
        {
            appointments = new ObservableCollection<Appointment>();
            students = new ObservableCollection<Student>();
            _service = service;
        }
        public AnalyticsViewModel()
        {
            
        }
        public async void LoadFilteredAppointmentsAsync()
        {
            var appointments = await _service.GetNotFinishedYetAppointmentsAsync();

            Appointments.Clear();

            foreach(var appointment in appointments)
            {
                Appointments.Add(appointment);
            }
        }
        
        public async void LoadFilteredStudentsAsync()
        {
            var students = await _service.GetStudentsWithLastAmountUnderOrEqual20Async();

            Students.Clear();

            foreach(var student in students)
            {
                Students.Add(student);
            }
        }
    }
}
