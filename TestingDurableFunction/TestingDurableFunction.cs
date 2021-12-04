using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace TestingDurableFunction
{
    public class TestingDurableFunction
    {
        private readonly IEmployeeLegacyClient _employeeLegacyClient;
        private readonly IEmployeeRepository _employeeRepository;

        public TestingDurableFunction()
        {
            _employeeLegacyClient = new EmployeesLegacyClient();
            _employeeRepository = new EmployeeRepository();
        }

        [FunctionName("MigrateEmployees")]
        public async Task MigrateEmployees(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var employees = await context.CallActivityAsync<List<EmployeeLegacyResponse>>(
                nameof(MigrateEmployees_GetEmployees),
                null
            );

            foreach (var employee in employees)
            {
                await context.CallActivityAsync(
                    nameof(MigrateEmployees_SaveEmployee),
                    employee
                );
            }
        }

        [FunctionName("MigrateEmployees_GetEmployees")]
        public async Task<List<EmployeeLegacyResponse>> MigrateEmployees_GetEmployees([ActivityTrigger] string name)
        {
            return await _employeeLegacyClient.GetEmployeesAsync();
        }

        [FunctionName("MigrateEmployees_SaveEmployee")]
        public async Task MigrateEmployees_SaveEmployee([ActivityTrigger] EmployeeLegacyResponse employee)
        {
            var newEmployeeData = new Employee(
                employee.Id,
                employee.FirstName,
                employee.LastName,
                employee.Email,
                employee.PhoneNumber,
                $"{employee.FirstName} {employee.LastName}"
            );

            await _employeeRepository.SaveEmployeeAsync(newEmployeeData);
        }

        [FunctionName("MigrateEmployees_HttpStart")]
        public async Task<HttpResponseMessage> MigrateEmployees_HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            var instanceId = await starter.StartNewAsync(nameof(MigrateEmployees), null);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }
    }
}