using StudentPaymentApp.Model;
using StudentPaymentApp.Model.Services;
using StudentPaymentApp.ViewModel;

namespace StudentPaymentApp.Views;

public partial class EditStudentPage : ContentPage
{
	private readonly Student _student;
	private readonly StudentViewModel _viewModel;
    private readonly IPaymentService _service;
	public EditStudentPage(Student student, StudentViewModel viewModel, IPaymentService service)
	{
		InitializeComponent();
		_student = student;
		BindingContext = viewModel;
        _service = service;
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

        var payment = await _service.GetPaymentByIdAsync(_student.Payment.Id);

		_viewModel.ExtractProperties(_student, payment);

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