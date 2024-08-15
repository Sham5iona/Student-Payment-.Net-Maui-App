using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPaymentApp.Model.Services
{
    public interface IPaymentService
    {
        public Task AddPaymentAsync(Payment payment);
        public Task<Payment> GetPaymentByIdAsync(int id);
        public Task UpdatePaymentAsync(Payment payment);

    }
}
