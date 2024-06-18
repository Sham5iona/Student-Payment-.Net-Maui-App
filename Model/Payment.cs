using SQLite;
using SQLiteNetExtensions.Attributes;
using StudentPaymentApp.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace StudentPaymentApp.Model
{
    [SQLite.Table("Payments")]
    public class Payment
    {
        private int _id;
        [PrimaryKey, AutoIncrement, SQLite.NotNull, SQLite.Column("Id")]
        public int Id { get { return _id; } set { _id = value; } }

        private int _student_id;
        [SQLiteNetExtensions.Attributes.ForeignKey(typeof(Student))]
        public int StudentId { get { return _student_id; } set { _student_id = value; } }
        public Student Student { get; set; }

        private decimal _givenAmount;
        [AllowNull, SQLite.Column("GivenAmount")]
        public decimal GivenAmount { get { return _givenAmount; } 
                                     set { _givenAmount = value; } }

        private decimal _lastAmount;
        [SQLite.NotNull, SQLite.Column("LastAmount")]
        public decimal LastAmount { get { return _lastAmount; } 
                                    set { _lastAmount = value; } }

        private DateTime _lastModification;
        public DateTime LastModification
        {
            get { return _lastModification; }
            set { _lastModification = DateTime.Now; }
        }

        public Payment(int studentId, decimal givenAmount, decimal lastAmount)
        {
            this.StudentId = studentId;
            this.GivenAmount = givenAmount;
            this.LastAmount = lastAmount;
        }
        public Payment()
        {
            
        }
    }
}
