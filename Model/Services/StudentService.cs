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

        public async Task AddStudentAsync(Student student)
        {
            await _dbContext.AddStudentAsync(student);
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
    }
}
