using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestingDurableFunction
{
    public interface IEmployeesLegacyClient
    {
        Task<List<EmployeeLegacyResponse>> GetEmployeesAsync();
    }

    public class EmployeesesLegacyClient : IEmployeesLegacyClient
    {
        public Task<List<EmployeeLegacyResponse>> GetEmployeesAsync()
        {
            return Task.FromResult(new List<EmployeeLegacyResponse>());
        }
    }
}
