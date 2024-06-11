using DatabasePaymentManagementWEBAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace DatabasePaymentManagementWEBAPI.Data
{
    public class PaymentDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Payment> Payments { get; set; }

        public PaymentDbContext(DbContextOptions options) : base(options) { }

        public PaymentDbContext()
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Student>().HasOne(s => s.Payment).WithOne(p => p.Student)
                                          .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
