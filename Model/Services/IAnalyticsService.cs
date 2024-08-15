
namespace StudentPaymentApp.Model.Services
{
    public interface IAnalyticsService
    {
        public Task<int> GetStudentsCountAsync();
        public Task<int> GetAppointmentsCountAsync();
        public Task<IEnumerable<Student>> GetStudentsWithLastAmountUnderOrEqual20Async();
        public Task<IEnumerable<Appointment>> GetNotFinishedYetAppointmentsAsync();
    }
}
