using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudentPaymentApp.Model;
using StudentPaymentApp.Model.Services;
using StudentPaymentApp.Views;
using System.Collections.ObjectModel;


namespace StudentPaymentApp.ViewModel
{
    public partial class ShowStudentsViewModel : ObservableObject
    {
        private readonly IStudentService _service;
        private readonly IPaymentService _paymentService;

        public ObservableCollection<Student> Students { get; private set; }

        public ShowStudentsViewModel(IStudentService service, IPaymentService paymentService)
        {
            _service = service;
            Students = new ObservableCollection<Student>();
            _paymentService = paymentService;
        }
        public ShowStudentsViewModel()
        {
            
        }
        public async void LoadStudents()
        {
            var students = await _service.GetStudentsAsync();

            Students.Clear();

            foreach (var student in students)
            {
                student.Payment.FormattedAmount = $"{student.Payment.LastAmount}" +
                                        $" / {student.Payment.GivenAmount}";

                Students.Add(student);

            }
        }

        [RelayCommand]
        private async Task DeleteAsync(int id)
        {
            var student = await _service.GetStudentByIdAsync(id);

            bool isConfirmed = await Application.Current.MainPage
                            .DisplayAlert("Delete a student", "Are you sure", "Ok",
                            "Cancel");

            if (isConfirmed)
            {
                await _service.DeleteStudentAsync(student);
                await Shell.Current.GoToAsync(nameof(StudentsPage));
            }

        }

        [RelayCommand]
        private async Task SortAscAsync()
        {
            var students = await _service.SortAscAsync();

            Students.Clear();

            foreach (var student in students)
            {
                student.Payment.FormattedAmount = $"{student.Payment.LastAmount}" +
                                        $" / {student.Payment.GivenAmount}";
                Students.Add(student);
            }

        }

        [RelayCommand]
        private async Task SortDescAsync()
        {
            var students = await _service.SortDescAsync();

            Students.Clear();

            foreach (var student in students)
            {
                student.Payment.FormattedAmount = $"{student.Payment.LastAmount}" +
                                        $" / {student.Payment.GivenAmount}";

                Students.Add(student);
            }

        }

        public async void CheckStudentsWithLowLeftAmountAsync()
        {
            var students = await _service.GetStudentsAsync();

            foreach(var student in students)
            {
                if (student.Payment.LastAmount <= 20)
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Student notification",
                        $"NOTE: {student.Name} has {student.Payment.LastAmount} / {student.Payment.GivenAmount}" +
                        $" payment amount which may expire shortly! ", "OK");
                }
            }
        }
    }
}
