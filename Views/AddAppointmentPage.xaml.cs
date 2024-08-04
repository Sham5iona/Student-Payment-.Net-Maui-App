using StudentPaymentApp.ViewModel;

namespace StudentPaymentApp.Views;

// MAKE VALIDATION FOR EVERYTHING BUT MOSTLY TO PREVENT MAKING APPOINTMENTS ON
// ALREADY TAKEN DATES

public partial class AddAppointmentPage : ContentPage
{
    private readonly AppointmentViewModel _viewModel;
    public AddAppointmentPage(AppointmentViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;

        //Display an error message to the view by subscribing to the AppointmentViewModel Messaging Center
        MessagingCenter.Subscribe<AppointmentViewModel>(this, "Invalid input data!", async (sender) =>
        {
            await DisplayAlert("Validation Error", "Please check your input data.", "OK");
        });

        MessagingCenter.Subscribe<AppointmentViewModel>(this, "Appointment already exists!", async (sender) =>
        {
            await DisplayAlert("Error", "The appointment already exists!", "OK");
        });
    }
    public AddAppointmentPage()
    {
        
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _viewModel.InitializeProperties();
        // Scroll to the top of the ScrollView with a slight delay
        await Task.Delay(100); // Adjust the delay as necessary to be able to
        //show the top of the page everytime
        await ScrollView.ScrollToAsync(0, 0, false);
    }
    private async void GoBack(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync(".."); //navigate to previous page
    }

}