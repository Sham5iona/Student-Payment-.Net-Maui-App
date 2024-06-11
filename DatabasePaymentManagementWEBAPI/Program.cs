
using DatabasePaymentManagementWEBAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace DatabasePaymentManagementWEBAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var _connectionString = builder.Configuration
                                       .GetConnectionString("DefaultConnection");

            // Add services to the container.
            builder.Services.AddDbContext<PaymentDbContext>(options =>
                options.UseSqlServer(_connectionString)
             );

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
