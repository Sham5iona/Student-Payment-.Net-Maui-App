using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;


namespace StudentPaymentApp.Model.Services
{
    public interface IAppointmentService
    {
        public Task<ObservableCollection<SchedulerAppointment>> ShowAppointmentsAsync();
        public Task<Appointment> AddAppointmentAsync(Appointment appointment);
        public SchedulerAppointment ConfigureSchedulerAppointment(Appointment appointment);

    }
}