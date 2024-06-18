using SQLite;

namespace StudentPaymentApp.Model
{
    [SQLite.Table("Appointments")]
    public class Appointment
    {
        private int _id;
        [PrimaryKey, AutoIncrement, NotNull, SQLite.Column("Id")]
        public int Id { get { return _id; } set { _id = value; } }

        private string _subject;
        [NotNull, SQLite.Column("Subject")]
        public string Subject { get { return _subject; } set { _subject = value; } }

        private DateTime _startDate;
        [NotNull, SQLite.Column("StartDate")]
        public DateTime StartDate { get { return _startDate; } set { _startDate = value; } }

        private DateTime _endDate;
        [NotNull, SQLite.Column("EndDate")]
        public DateTime EndDate { get { return _endDate; } set { _endDate = value; } }

        private string _description;
        [NotNull, SQLite.Column("Description")]
        public string Description { get { return _description; } set { _description = value; } }

        public string _location;
        [NotNull, SQLite.Column("")]
        public string Location { get { return _location; } set { _location = value; } }

        public Appointment(string subject, DateTime startDate, DateTime endDate,
                           string description, string location)
        {
            this.Subject = subject;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Description = description;
            this.Location = location;
        }

        public Appointment() //empty constructor for EF Core
        {
            
        }
    }
}
