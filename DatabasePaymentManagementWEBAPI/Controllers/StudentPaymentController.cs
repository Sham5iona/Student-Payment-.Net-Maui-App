using DatabasePaymentManagementWEBAPI.Data;
using DatabasePaymentManagementWEBAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabasePaymentManagementWEBAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentPaymentController : ControllerBase
    {
        private readonly PaymentDbContext _dbContext;

        public StudentPaymentController(PaymentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("getStudents")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var students = await _dbContext.Students.ToListAsync();
            return Ok(students);
        }

        [HttpGet("getPayments")]
        public async Task<ActionResult<IEnumerable<Payment>>> GetPayments()
        {
            var payments = await _dbContext.Payments.Include(p => p.Student).ToListAsync();
            return Ok(payments);
        }

        [HttpPut("editStudent")]
        public async Task<IActionResult> EditStudent([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest();
            }

            await _dbContext.SaveChangesAsync();
            return Ok(student);
        }

        [HttpPut("editPayment")]
        public async Task<IActionResult> EditPayment([FromBody] Payment payment)
        {
            if (payment == null)
            {
                return BadRequest();
            }

            await _dbContext.SaveChangesAsync();
            return Ok(payment);
        }

        [HttpDelete("deleteStudent")]
        public async Task<IActionResult> DeleteStudent([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest();
            }

            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
            return Ok(student);
        }

        [HttpDelete("deletePayment")]
        public async Task<IActionResult> DeletePayment([FromBody] Payment payment)
        {
            if (payment == null)
            {
                return BadRequest();
            }

            _dbContext.Payments.Remove(payment);
            await _dbContext.SaveChangesAsync();
            return Ok(payment);
        }

        [HttpPost("addStudent")]
        public async Task<IActionResult> AddStudent([FromBody] Student student)
        {
            if (student == null)
            {
                return BadRequest();
            }

            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
            return Ok(student);
        }

        [HttpPost("addPayment")]
        public async Task<IActionResult> AddPayment([FromBody] Payment payment)
        {
            if (payment == null)
            {
                return BadRequest();
            }

            await _dbContext.Payments.AddAsync(payment);
            await _dbContext.SaveChangesAsync();
            return Ok(payment);
        }
    }
}
