using StudentPaymentApp.Views;

namespace StudentPaymentApp;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute(nameof(AddAppointmentPage), typeof(AddAppointmentPage));
		Routing.RegisterRoute(nameof(SchedulePage), typeof(SchedulePage));
		Routing.RegisterRoute(nameof(AppointmentDetailsPage),
								typeof(AppointmentDetailsPage));
		Routing.RegisterRoute(nameof(EditAppointmentPage), typeof(EditAppointmentPage));
		Routing.RegisterRoute(nameof(AddStudentPage), typeof(AddStudentPage));
		Routing.RegisterRoute(nameof(EditStudentPage), typeof(EditStudentPage));
		Routing.RegisterRoute(nameof(StudentsPage), typeof(StudentsPage));

    }
}