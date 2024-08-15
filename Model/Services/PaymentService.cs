using StudentPaymentApp.Data;

namespace StudentPaymentApp.Model.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly StudentPaymentDbContext _dbContext;
        public PaymentService(StudentPaymentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            await _dbContext.AddPaymentAsync(payment);
        }

        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            return await _dbContext.GetPaymentByIdAsync(id);
        }
        public async Task UpdatePaymentAsync(Payment payment)
        {
            await _dbContext.UpdatePaymentAsync(payment);
        }
    }
}
