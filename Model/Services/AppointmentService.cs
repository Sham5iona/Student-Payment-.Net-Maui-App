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

        public async Task<IEnumerable<Appointment>> GetAppointmentsAsync()
        {
            return await _dbContext.GetAppointmentsAsync();
        }

        public async Task DeleteAppointmentAsync(Appointment appointment)
        {
            await _dbContext.DeleteAppointmentAsync(appointment);
        }

        public Appointment ConfigureSchedulerAppoitment(
                            SchedulerAppointment scheduler_appointment)
        {
            var appointment = new Appointment
            {
                Subject = scheduler_appointment.Subject,
                Description = scheduler_appointment.Notes,
                Location = scheduler_appointment.Location,
                StartDate = scheduler_appointment.StartTime,
                EndDate = scheduler_appointment.EndTime
            };
            return appointment;
        }
        public Appointment ConfigureAppointment(SchedulerAppointment scheduler_appointment)
        {
            var appointment = new Appointment
            {
                Subject = scheduler_appointment.Subject,
                Description = scheduler_appointment.Notes,
                Location = scheduler_appointment.Location,
                StartDate = scheduler_appointment.StartTime,
                EndDate = scheduler_appointment.EndTime
            };

            return appointment;
        }

        public async Task<Appointment> EditAppointmentAsync(Appointment appointment)
        {
            await _dbContext.EditAppointmentAsync(appointment);
            return appointment;
        }

        public async Task<int> GetAppointmentIdByDate(Appointment appointment)
        {
            return await _dbContext.GetAppointmentIdAsync(appointment);
        }
    }
}