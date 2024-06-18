using StudentPaymentApp.Data;
using Syncfusion.Maui.Scheduler;
using System.Collections.ObjectModel;


namespace StudentPaymentApp.Model.Services
{
        public class AppointmentService : IAppointmentService
        {
        private readonly StudentPaymentDbContext _dbContext;
        public AppointmentService(StudentPaymentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Appointment> AddAppointmentAsync(Appointment appointment)
        {
            await _dbContext.AddAppointmentAsync(appointment);
            return appointment;

        }

    public async Task<ObservableCollection<SchedulerAppointment>> ShowAppointmentsAsync()
    {
            var appointments = await _dbContext.GetAppointmentsAsync();

            var schedulerAppointments = new ObservableCollection<SchedulerAppointment>();
            
            foreach (var appointment in appointments)
            {
                var schedulerAppointment = new SchedulerAppointment
                {
                    Subject = appointment.Subject,
                    StartTime = appointment.StartDate,
                    EndTime = appointment.EndDate,
                    Notes = appointment.Description,
                    Location = appointment.Location,
                    Background = Colors.Blue
                };

                schedulerAppointments.Add(schedulerAppointment);
            }

            return schedulerAppointments;
     }
        public SchedulerAppointment ConfigureSchedulerAppointment(
                                    Appointment appointment)
        {
            var schedulerAppointment = new SchedulerAppointment
            {
                Subject = appointment.Subject,
                StartTime = appointment.StartDate,
                EndTime = appointment.EndDate,
                Notes = appointment.Description,
                Location = appointment.Location,
                Background = Colors.Blue

            };

            return schedulerAppointment;
        }
  }
}
