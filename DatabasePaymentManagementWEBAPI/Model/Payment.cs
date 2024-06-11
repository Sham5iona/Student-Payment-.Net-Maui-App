namespace DatabasePaymentManagementWEBAPI.Model
{
    public class Payment
    {
        private int _id;
        public int Id { get { return _id; } set { _id = value; } }

        private int _student_id;
        public int StudentId { get { return _student_id; } set { _student_id = value; } }
        public Student Student { get; set; }

        private decimal _givenAmount;
        public decimal GivenAmount { get { return _givenAmount; } 
                                     set { _givenAmount = value; } }

        private decimal _lastAmount;
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

        public Payment() //Empty constructor for EFCore
        {
            
        }
    }
}
