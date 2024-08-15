
namespace StudentPaymentApp.Model.Services
{
    public interface IStudentService
    {
        public Task<Student> AddStudentAsync(Student student);
        public Task<IEnumerable<Student>> GetStudentsAsync();
        public Task EditStudentAsync(Student student);
        public Task<Student> GetStudentByIdAsync(int id);
        public Task<Student> GetStudentPaymentByStudentIdAsync(int studentId);
        public Task DeleteStudentAsync(Student student);
        public Task<IEnumerable<Student>> SortAscAsync();
        public Task<IEnumerable<Student>> SortDescAsync();
        public Task<IEnumerable<Student>> FilterAsync(string search_string);
    }
}
