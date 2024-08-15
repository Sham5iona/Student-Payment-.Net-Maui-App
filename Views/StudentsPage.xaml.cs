using StudentPaymentApp.Model.Services;
using StudentPaymentApp.ViewModel;

namespace StudentPaymentApp.Views;

public partial class StudentsPage : ContentPage
{
	private readonly ShowStudentsViewModel _viewModel;
	private readonly StudentViewModel _studentViewModel;
	private readonly IStudentService _service;
	private readonly IPaymentService _paymentService;
	public StudentsPage(ShowStudentsViewModel viewModel, IStudentService service,
                        StudentViewModel studentViewModel, IPaymentService paymentService)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
        _studentViewModel = studentViewModel;
        _service = service;
        _paymentService = paymentService;

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
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        _viewModel.CheckStudentsWithLowLeftAmountAsync();
    }
    private async void EditAsync(object sender, TappedEventArgs args)
	{

        var Id = (int)args.Parameter;
        
        var student = await _service.GetStudentPaymentByStudentIdAsync(Id);
        
        await Navigation.PushAsync(new EditStudentPage(student,
        						_studentViewModel, _paymentService));

    }

    private async void FilterAsync(object sender, EventArgs args)
	{
		var students = await _service.FilterAsync(SearchField.Text);

		_viewModel.Students.Clear();

		foreach (var student in students)
		{
            student.Payment.FormattedAmount = $"{student.Payment.LastAmount}" +
                                        $" / {student.Payment.GivenAmount}";
            _viewModel.Students.Add(student);
		}
	}

}