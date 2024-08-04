using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudentPaymentApp.Model;
using StudentPaymentApp.Model.Services;

namespace StudentPaymentApp.ViewModel
{
    public partial class StudentViewModel : ObservableObject
    {
        private readonly IStudentService _service;
        private static int _id;
        
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private int age;

        [ObservableProperty]
        private string location;

        [ObservableProperty]
        private string parentName;

        [ObservableProperty]
        private bool isActive;

        public StudentViewModel()
        {

        }

        public StudentViewModel(IStudentService service)
        {
            _service = service;
        }

        public void InitializeProperties()
        {
            // Initialize properties with default values
            Name = string.Empty;
            Age = 0;
            ParentName = string.Empty;
            IsActive = false;
            Location = string.Empty;
        }

        public void ExtractProperties(Student student)
        {
            if (student == null) return;

            _id = student.Id;
            Name = student.Name;
            Age = student.Age;
            Location = student.Location;
            ParentName = student.ParentName;
            IsActive = student.IsActive;
        }

        [RelayCommand]
        private async Task AddAsync()
        {

            if (string.IsNullOrWhiteSpace(Name) || Age == 0 ||
                string.IsNullOrWhiteSpace(Location))
            {
                MessagingCenter.Send(this, "Invalid parameters");
                return;
            }

            var students = await _service.GetStudentsAsync();
            var exists = students.FirstOrDefault(s => s.Name == Name && s.Age == Age
                                                 && s.Location == Location &&
                                                 s.ParentName == ParentName &&
                                                 s.IsActive == IsActive);

            if (exists is not null && exists.Id != _id)
            {
                MessagingCenter.Send(this, "Existing error");
                return;
            }

            var student = new Student
            {
                Name = Name,
                Age = Age,
                Location = Location,
                ParentName = ParentName,
                IsActive = IsActive,
                LastModification = DateTime.Now
            };

            await _service.AddStudentAsync(student);

            // Clear properties and reload appointments
            InitializeProperties();

            await Shell.Current.GoToAsync(".."); //navigate to previous page
        }

        [RelayCommand]
        public async Task EditAsync()
        {
            if (string.IsNullOrWhiteSpace(Name) || Age == 0 || string.IsNullOrWhiteSpace(Location))
            {
                MessagingCenter.Send(this, "Invalid parameters");
                return;
            }

            var students = await _service.GetStudentsAsync();
            var exists = students.FirstOrDefault(s => s.Name == Name && s.Age == Age && s.Location == Location &&
                                                 s.ParentName == ParentName && s.IsActive == IsActive);

            if (exists is not null && exists.Id != _id)
            {
                MessagingCenter.Send(this, "Existing error");
                return;
            }

            var student = new Student
            {
                Id = _id,
                Name = Name,
                Age = Age,
                Location = Location,
                ParentName = ParentName,
                IsActive = IsActive,
                LastModification = DateTime.Now
            };

            await _service.EditStudentAsync(student);

            InitializeProperties();

            await Shell.Current.GoToAsync("..");
        }
        
    }
}
