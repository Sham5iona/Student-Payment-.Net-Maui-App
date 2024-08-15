using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudentPaymentApp.Model;
using StudentPaymentApp.Model.Services;

namespace StudentPaymentApp.ViewModel
{
    public partial class StudentViewModel : ObservableObject
    {
        private readonly IStudentService _service;
        private readonly IPaymentService _paymentService;
        private static int _id;
        private static int _paymentId;
        
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private int age;

        [ObservableProperty]
        private string location;

        [ObservableProperty]
        private string parentName;

        [ObservableProperty]
        private decimal givenAmount;

        [ObservableProperty]
        private bool isActive;

        public StudentViewModel()
        {

        }

        public StudentViewModel(IStudentService service, IPaymentService paymentService)
        {
            _service = service;
            _paymentService = paymentService;

        }

        public void InitializeProperties()
        {
            // Initialize properties with default values
            Name = string.Empty;
            Age = 0;
            ParentName = string.Empty;
            IsActive = false;
            Location = string.Empty;
            GivenAmount = 0;
        }

        public void ExtractProperties(Student student, Payment payment)
        {
            if (student == null) return;

            _id = student.Id;
            _paymentId = payment.Id;
            Name = student.Name;
            Age = student.Age;
            Location = student.Location;
            ParentName = student.ParentName;
            GivenAmount = student.Payment.GivenAmount;
            IsActive = student.IsActive;
        }

        [RelayCommand]
        private async Task AddAsync()
        {

            if (string.IsNullOrWhiteSpace(Name) || Age == 0 ||
                string.IsNullOrWhiteSpace(Location) || GivenAmount < 0)
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
                        
            var created_student = await _service.AddStudentAsync(student);

            var payment = new Payment
            {
                GivenAmount = GivenAmount,
                LastAmount = GivenAmount,
                LastModification = DateTime.Now,
                StudentId = created_student.Id
            };

            await _paymentService.AddPaymentAsync(payment);

            // Clear properties and reload appointments
            InitializeProperties();

            await Shell.Current.GoToAsync(".."); //navigate to previous page
        }

        [RelayCommand]
        public async Task EditAsync()
        {
            if (string.IsNullOrWhiteSpace(Name) || Age == 0 || string.IsNullOrWhiteSpace(Location)
                || GivenAmount < 0)
            {
                MessagingCenter.Send(this, "Invalid parameters");
                return;
            }

            var students = await _service.GetStudentsAsync();
            var exists = students.FirstOrDefault(s => s.Name == Name && s.Age == Age && s.Location == Location &&
                                                 s.ParentName == ParentName && s.IsActive == IsActive && s.Id != _id);

            if (exists is not null && exists.Id != _id)
            {
                MessagingCenter.Send(this, "Existing error");
                return;
            }

            var current_student = await _service.GetStudentPaymentByStudentIdAsync(_id);

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

            var payment = new Payment
            {
                Id = _paymentId,

                // Update GivenAmount based on the current payment and the new payment
                GivenAmount = current_student.Payment.LastAmount + Math.Abs(GivenAmount),

                // Calculate LastAmount
                LastAmount = current_student.Payment.LastAmount + Math.Abs(GivenAmount),

                LastModification = DateTime.Now,
                StudentId = _id
            };


            await _paymentService.UpdatePaymentAsync(payment);

            InitializeProperties();

            await Shell.Current.GoToAsync("..");
        }
        
    }
}
