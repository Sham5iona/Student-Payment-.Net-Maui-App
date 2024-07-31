using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudentPaymentApp.Model;
using StudentPaymentApp.Model.Services;
using StudentPaymentApp.Views;
using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;

namespace StudentPaymentApp.ViewModel
{
    public partial class SchedulerViewModel : ObservableObject
    {
        private readonly IAppointmentService _service;
        public ObservableCollection<SchedulerAppointment> Events { get; set; }

        public SchedulerViewModel(IAppointmentService service)
        {

            _service = service;
            Events = new ObservableCollection<SchedulerAppointment>();
        }
        public SchedulerViewModel()
        {
        }

        public async void LoadAppointmentsAsync()
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

    }
}
