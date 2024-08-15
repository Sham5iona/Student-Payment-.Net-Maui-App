

using StudentPaymentApp.Data;

namespace StudentPaymentApp.Model.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly StudentPaymentDbContext _dbContext;
        public AnalyticsService(StudentPaymentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetAppointmentsCountAsync()
        {
            return await _dbContext.AppointmentsCountAsync();
        }

        public async Task<IEnumerable<Appointment>> GetNotFinishedYetAppointmentsAsync()
        {
            return await _dbContext.GetNotFinishedYetAppointmentsAsync();
        }

        public async Task<int> GetStudentsCountAsync()
        {
            return await _dbContext.StudentsCountAsync();
        }

        public async Task<IEnumerable<Student>> GetStudentsWithLastAmountUnderOrEqual20Async()
        {
            return await _dbContext.GetStudentsWithLastAmountUnderOrEqual20Async();
        }
    }
}
