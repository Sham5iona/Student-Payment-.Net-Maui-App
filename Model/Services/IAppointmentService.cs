using Syncfusion.Maui.Scheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPaymentApp.Model.Services
{
    public interface IAppointmentService
    {
        public Task<ObservableCollection<SchedulerAppointment>> ShowAppointmentsAsync();
        public Task<Appointment> AddAppointmentAsync(Appointment appointment);
        public SchedulerAppointment ConfigureSchedulerAppointment(Appointment appointment);

    }
}
