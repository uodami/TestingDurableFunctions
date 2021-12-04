using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestingDurableFunction
{
    public interface IEmployeeLegacyClient
    {
        Task<List<EmployeeLegacyResponse>> GetEmployeesAsync();
    }

    public class EmployeesLegacyClient : IEmployeeLegacyClient
    {
        public Task<List<EmployeeLegacyResponse>> GetEmployeesAsync()
        {
            return Task.FromResult(new List<EmployeeLegacyResponse>());
        }
    }
}
