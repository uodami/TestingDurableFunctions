using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestingDurableFunction
{
    public interface IEmployeeRepository
    {
        Task SaveEmployeeAsync(Employee employee);
        Task<List<Employee>> GetEmployeesAsync();
    }
    
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly List<Employee> _employees;

        public EmployeeRepository()
        {
            _employees = new List<Employee>();
        }
        
        public async Task SaveEmployeeAsync(Employee employee)
        {
            _employees.Add(employee);
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return _employees;
        }
    }
}
