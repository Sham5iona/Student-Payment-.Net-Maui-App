using SQLite;
using StudentPaymentApp.Model;

namespace StudentPaymentApp.Data
{
    public class StudentPaymentDbContext
    {
        private const string DB_NAME = "StudentPaymentDb.db3";
        private SQLiteAsyncConnection _connection;

        public StudentPaymentDbContext()
        {
            Init();
        }
        private async void Init()
        {
            _connection = new SQLiteAsyncConnection(
                              Path.Combine(FileSystem.AppDataDirectory, DB_NAME));
            if (_connection == null)
            {
                return;
            }

            await _connection.CreateTableAsync<Student>();
            await _connection.CreateTableAsync<Payment>();
            await _connection.CreateTableAsync<Appointment>();
        }

        public async Task<Student> AddStudentAsync(Student student)
        {
            await _connection.InsertAsync(student);
            return student;

        }
        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {

            var students = await _connection.Table<Student>().ToListAsync();

            var payments = await _connection.Table<Payment>().ToListAsync();

            if (payments.Count > 0)
            {
                IList<Student> student_payments = new List<Student>();

                foreach (var student in students)
                {
                    foreach (var payment in payments)
                    {
                        var student_payment = payments.FirstOrDefault(
                                p => p.StudentId == student.Id);

                        student.Payment = student_payment;

                    }

                    student_payments.Add(student);
                }

                return student_payments;

            }

            return students;

            
        }

        public async Task EditStudentAsync(Student student)
        {
            await _connection.UpdateAsync(student);
        }

        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            return await _connection.Table<Payment>().FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        {
            return await _connection.Table<Student>().FirstOrDefaultAsync(s => s.Id == id);
        }
        
        public async Task<Student> GetStudentPaymentByStudentIdAsync(int studentId)
        {
            var student = await GetStudentByIdAsync(studentId);

            var payment = await _connection.Table<Payment>()
                          .FirstOrDefaultAsync(p => p.StudentId == studentId);

            student.Payment = payment;

            return student;
        }

        public async Task DeleteStudentAsync(Student student)
        {
            await _connection.DeleteAsync(student);
        }

        public async Task AddPaymentAsync(Payment payment)
        {
            await _connection.InsertAsync(payment);
        }

        public async Task UpdatePaymentAsync(Payment payment)
        {
            await _connection.UpdateAsync(payment);
        }

        public async Task<IEnumerable<Appointment>> FilterAppointmentsBySearchTextAsync(string searchText)
        {
            var filteredAppointments = await _connection.Table<Appointment>()
                                     .Where(a => 
                                     a.Subject.ToLower().Contains(searchText.ToLower()))
                                     .ToListAsync();

            return filteredAppointments;
        }

        public async Task<IEnumerable<Student>> SortAscAsync()
        {
            var students = await _connection.Table<Student>().ToListAsync();

            var payments = await _connection.Table<Payment>().ToListAsync();

            if (payments.Count > 0)
            {
                IList<Student> student_payments = new List<Student>();

                foreach (var student in students)
                {
                    foreach (var payment in payments)
                    {
                        var student_payment = payments.FirstOrDefault(
                                p => p.StudentId == student.Id);

                        student.Payment = student_payment;

                    }

                    student_payments.Add(student);
                }


                return student_payments.OrderBy(sp => sp.Name);

            }

            return students.OrderBy(s => s.Name);
        }
        public async Task<IEnumerable<Student>> SortDescAsync()
        {
            var students = await _connection.Table<Student>().ToListAsync();

            var payments = await _connection.Table<Payment>().ToListAsync();

            if (payments.Count > 0)
            {
                IList<Student> student_payments = new List<Student>();

                foreach (var student in students)
                {
                    foreach (var payment in payments)
                    {
                        var student_payment = payments.FirstOrDefault(
                                p => p.StudentId == student.Id);

                        student.Payment = student_payment;

                    }

                    student_payments.Add(student);
                }

                return student_payments.OrderByDescending(sp => sp.Name); ;

            }

            return students;
        }
        public async Task<IEnumerable<Student>> FilterAsync(string search_string)
        {
            var students = await _connection.Table<Student>().ToListAsync();

            var payments = await _connection.Table<Payment>().ToListAsync();

            if (payments.Count > 0)
            {
                IList<Student> student_payments = new List<Student>();

                foreach (var student in students)
                {
                    foreach (var payment in payments)
                    {
                        var student_payment = payments.FirstOrDefault(
                                p => p.StudentId == student.Id);

                        student.Payment = student_payment;

                    }

                    student_payments.Add(student);
                }

                return student_payments.Where(sp => sp.Name.ToLower()
                           .Contains(search_string.ToLower()));

            }

            return students;
            
        }
        public async Task<Appointment> AddAppointmentAsync(Appointment appointment)
        {
            await _connection.InsertAsync(appointment);
            return appointment;
        }
        public async Task<IEnumerable<Appointment>> GetAppointmentsAsync()
        {
            return await _connection.Table<Appointment>().ToListAsync();

        }
        public async Task<int> AppointmentsCountAsync()
        {
            return await _connection.Table<Appointment>().CountAsync();
        }
        public async Task DeleteAppointmentAsync(Appointment appointment)
        {
            await _connection.Table<Appointment>()
                    .DeleteAsync(a => a.StartDate == appointment.StartDate &&
                    a.EndDate == appointment.EndDate);
        }
        public async Task<Appointment> EditAppointmentAsync(Appointment appointment)
        {
            var existingAppointment = await _connection.Table<Appointment>()
                                .Where(a => a.StartDate == appointment.StartDate)
                                .FirstOrDefaultAsync();

            await _connection.UpdateAsync(appointment);

            return appointment;
        }

        public async Task<int> GetAppointmentIdAsync(Appointment appointment)
        {
            var current_appointment = await _connection.Table<Appointment>()
                .FirstOrDefaultAsync(a => a.StartDate == appointment.StartDate &&
                a.EndDate == appointment.EndDate && a.Subject == appointment.Subject
                && a.Description == appointment.Description &&
                a.Location == appointment.Location);

            return current_appointment.Id;

        }
        public async Task<int> StudentsCountAsync()
        {
            return await _connection.Table<Student>().CountAsync();
        }
        public async Task<IEnumerable<Appointment>> GetNotFinishedYetAppointmentsAsync()
        {
            return await _connection.Table<Appointment>()
                    .Where(a => a.EndDate > DateTime.Now)
                    .ToListAsync();
        }
        public async Task<IEnumerable<Student>> GetStudentsWithLastAmountUnderOrEqual20Async()
        {
            var students = await GetStudentsAsync();

            return students.Where(s => s.Payment.LastAmount <= 20)
                   .ToList();
        }
    }
}