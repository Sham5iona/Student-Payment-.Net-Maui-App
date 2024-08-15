using StudentPaymentApp.Data;

namespace StudentPaymentApp.Model.Services
{
    public class StudentService : IStudentService
    {
        private readonly StudentPaymentDbContext _dbContext;
        public StudentService(StudentPaymentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
             var created_student = await _dbContext.AddStudentAsync(student);
             return created_student;
        }

        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            return await _dbContext.GetStudentsAsync();
        }

        public async Task EditStudentAsync(Student student)
        {
            await _dbContext.EditStudentAsync(student);
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _dbContext.GetStudentByIdAsync(id);
        }

        public async Task DeleteStudentAsync(Student student)
        {
            await _dbContext.DeleteStudentAsync(student);
        }

        public async Task<IEnumerable<Student>> SortAscAsync()
        {
            return await _dbContext.SortAscAsync();
        }

        public async Task<IEnumerable<Student>> SortDescAsync()
        {
            return await _dbContext.SortDescAsync();
        }

        public async Task<IEnumerable<Student>> FilterAsync(string search_string)
        {
            return await _dbContext.FilterAsync(search_string);
        }

        public async Task<Student> GetStudentPaymentByStudentIdAsync(int studentId)
        {
            return await _dbContext.GetStudentPaymentByStudentIdAsync(studentId);
        }
    }
}
