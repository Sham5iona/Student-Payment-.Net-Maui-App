using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;


namespace StudentPaymentApp.Model.Services
{
    public interface IAppointmentService
    {
        public Task<ObservableCollection<SchedulerAppointment>> ShowAppointmentsAsync();
        public Task<IEnumerable<Appointment>> GetAppointmentsAsync();
        public Task<Appointment> AddAppointmentAsync(Appointment appointment);
        public SchedulerAppointment ConfigureSchedulerAppointment(Appointment appointment);
        public Task DeleteAppointmentAsync(Appointment appointment);
        public Appointment ConfigureAppointment(
                            SchedulerAppointment scheduler_appointment);
        public Task<Appointment> EditAppointmentAsync(Appointment appointment);

        public Task<int> GetAppointmentIdByDate(Appointment appointment);
    }
}