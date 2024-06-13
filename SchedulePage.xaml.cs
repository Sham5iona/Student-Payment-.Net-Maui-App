namespace StudentPaymentApp.Views;

public partial class SchedulePage : ContentPage
{
	public SchedulePage()
	{

		InitializeComponent();

		if(Scheduler.DaysView != null)
			Scheduler.AppointmentTextStyle.FontSize = 20;
		
	}

	private void ShowAppointmentPage(object sender, EventArgs e)
	{
		//access SchedulerPage properties by Scheduler field


	}

}