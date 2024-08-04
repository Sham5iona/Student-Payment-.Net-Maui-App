using StudentPaymentApp.Model;
using StudentPaymentApp.ViewModel;

namespace StudentPaymentApp.Views;

public partial class EditStudentPage : ContentPage
{
	private readonly Student _student;
	private readonly StudentViewModel _viewModel;
	public EditStudentPage(Student student, StudentViewModel viewModel)
	{
		InitializeComponent();
		_student = student;
		BindingContext = viewModel;
		_viewModel = viewModel;

        //Display an error message to the view by subscribing to the
        //StudentViewModel Messaging Center
        MessagingCenter.Subscribe<StudentViewModel>(this, "Invalid parameters",
            async (sender) =>
        {
            await DisplayAlert("Validation Error", "Please check your input data.", "OK");
        });

        MessagingCenter.Subscribe<StudentViewModel>(this, "Existing error",
            async (sender) =>
        {
            await DisplayAlert("Error", "The student already exists!", "OK");
        });
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
		_viewModel.ExtractProperties(_student);
        // Scroll to the top of the ScrollView with a slight delay
        await Task.Delay(100); // Adjust the delay as necessary to be able to
        //show the top of the page everytime
        await ScrollView.ScrollToAsync(0, 0, false);
    }

	private async void GoBack(object sender, EventArgs args)
	{
		await Shell.Current.GoToAsync("..");
	}

}