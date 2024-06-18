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

    }
}