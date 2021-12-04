using System.Threading.Tasks;

namespace TestingDurableFunction
{
    public interface IEmployeeRepository
    {
        Task SaveEmployeeAsync(Employee employee);
    }
    public class EmployeeRepository : IEmployeeRepository
    {
        public Task SaveEmployeeAsync(Employee employee)
        {
            return Task.CompletedTask;
        }
    }
}
