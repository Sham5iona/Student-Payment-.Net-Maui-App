using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Diagnostics.CodeAnalysis;

namespace StudentPaymentApp.Model
{
    [SQLite.Table("Students")]
    public class Student
    {
        private int _id;
        [PrimaryKey, AutoIncrement, SQLite.NotNull, SQLite.Column("Id")]
        public int Id { get { return _id; } set { _id = value; } }

        private string _name;
        [SQLite.NotNull, SQLite.Column("Name")]
        public string Name { get { return _name; } set { _name = value; } }

        private int _age;
        [SQLite.NotNull, SQLite.Column("Age")]
        public int Age { get { return _age; } set { _age = value; } }

        private string _location;
        [SQLite.NotNull, SQLite.Column("Location")]
        public string Location { get { return _location; } set { _location = value; } }

        private string? _parentName;
        [AllowNull, SQLite.Column("ParentName")]
        public string? ParentName { get { return _parentName; } set { _parentName = value; } }

        private bool _isActive;
        [SQLite.Column("IsActive")]
        public bool IsActive { get { return _isActive; } set { _isActive = value; } }

        [OneToOne(CascadeOperations = CascadeOperation.All)]
        public Payment Payment { get; set; }

        private DateTime _lastModification;
        public DateTime LastModification
        {
            get { return _lastModification; }
            set { _lastModification = DateTime.Now; }
        }

        public Student(string name, int age, string location, string? parentName,
                        bool isActive)
        {
            this.Name = name;
            this.Age = age;
            this.Location = location;
            this.ParentName = parentName;
            this.IsActive = isActive;
        }
        public Student()
        {

        }
    }
}
