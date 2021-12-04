namespace TestingDurableFunction
{
    public class Employee
    {
        public int Id { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Email { get; }
        public string PhoneNumber { get; }
        public string FullName { get; }

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
