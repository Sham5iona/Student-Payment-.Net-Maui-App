using Syncfusion.Maui.Scheduler;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPaymentApp.ViewModel
{
    public class SchedulerViewModel
    {
        public ObservableCollection<SchedulerAppointment> Events { get; set; }

        public SchedulerViewModel()
        {
            Events = new ObservableCollection<SchedulerAppointment>
            {
                new SchedulerAppointment
                {
                     StartTime = new DateTime(2024, 6, 5, 7, 0, 0),
                     EndTime = new DateTime(2024, 6, 5, 14, 30, 0),
                     Subject="Hunting",
                     Background=Colors.Blue,
                     Location="Шуманица"
                }
            };

        }
    }
}
