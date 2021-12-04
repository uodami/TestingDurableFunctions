namespace TestingDurableFunction
{
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }

        public Employee()
        {
            
        }
        
        public Employee(
            int id, 
            string firstName, 
            string lastName, 
            string email, 
            string phoneNumber, 
            string fullName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            FullName = fullName;
        }
    }
}
