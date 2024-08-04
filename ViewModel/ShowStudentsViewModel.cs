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

        public ObservableCollection<Student> Students { get; private set; }

        public ShowStudentsViewModel(IStudentService service)
        {
            _service = service;
            Students = new ObservableCollection<Student>();
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
                Students.Add(student);
            }

        }
    }
}
