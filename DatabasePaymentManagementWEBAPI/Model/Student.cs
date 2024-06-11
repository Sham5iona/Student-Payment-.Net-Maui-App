namespace DatabasePaymentManagementWEBAPI.Model
{
    public class Student
    {
        private int _id;
        public int Id { get { return _id; } set { _id = value; } }

        private string _name;
        public string Name { get { return _name; } set { _name = value; } }

        private int _age;
        public int Age { get { return _age; } set { _age = value; } }

        private string _location;
        public string Location { get { return _location; } set { _location = value; } }

        private string? _parentName;
        public string? ParentName { get { return _parentName;} set { _parentName = value; } }

        private bool _isActive;
        public bool IsActive { get { return _isActive; } set { _isActive = value; } }

        public Payment Payment { get; set; }

        private DateTime _lastModification;
        public DateTime LastModification { get { return _lastModification; } 
                                           set { _lastModification = DateTime.Now; } }

        public Student(string name, int age, string location, string? parentName,
                        bool isActive)
        {
            this.Name = name;
            this.Age = age;
            this.Location = location;
            this.ParentName = parentName;
            this.IsActive = isActive;
        }

        public Student() //Empty Constructor for EF Core
        {
            
        }
    }
}
