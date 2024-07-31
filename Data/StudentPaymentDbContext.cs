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
            if (_connection != null)
            {
                return;
            }

            await _connection.CreateTableAsync<Student>();
            await _connection.CreateTableAsync<Appointment>();
        }

        public async Task AddStudentAsync(Student student)
        {
            await _connection.InsertAsync(student);

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
        public async Task<int> Count()
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
                .FirstOrDefaultAsync(a => a.StartDate == appointment.StartDate && a.EndDate == appointment.EndDate);

            return current_appointment.Id;

        }
    }
}