using StudentPaymentApp.Model.Services;
using StudentPaymentApp.ViewModel;

namespace StudentPaymentApp.Views;

public partial class StudentsPage : ContentPage
{
	private readonly ShowStudentsViewModel _viewModel;
	private readonly StudentViewModel _studentViewModel;
	private readonly IStudentService _service;
	public StudentsPage(ShowStudentsViewModel viewModel, IStudentService service, StudentViewModel studentViewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = viewModel;
		_studentViewModel = studentViewModel;
		_service = service;
	}

	private async void AddAsync(object sender, EventArgs args)
	{
		await Shell.Current.GoToAsync(nameof(AddStudentPage));
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
		_viewModel.LoadStudents();
    }

	private async void EditAsync(object sender, TappedEventArgs args)
	{

		var id = (int)args.Parameter;

		var student = await _service.GetStudentByIdAsync(id);

		await Navigation.PushAsync(new EditStudentPage(student, _studentViewModel));
	}

}