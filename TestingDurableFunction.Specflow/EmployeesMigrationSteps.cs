using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Moq;
using TechTalk.SpecFlow;

namespace TestingDurableFunction.Specflow
{
    [Binding]
    public class EmployeesMigrationSteps
    {
        private ScenarioContext _scenarioContext;

        public EmployeesMigrationSteps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        
        [Given(@"These employees in the legacy application")]
        public void GivenTheseEmployeesInTheLegacyApplication(List<EmployeeLegacyResponse> expectedResponse)
        {
            var apiMock = new Mock<IEmployeesLegacyClient>();
            apiMock
                .Setup(m => m.GetEmployeesAsync())
                .ReturnsAsync(expectedResponse);

            _scenarioContext["ApiMock"] = apiMock;
        }
        
        [When(@"The migration process is launched")]
        public async Task WhenTheMigrationProcessIsLaunched()
        {
            _scenarioContext["EmployeeRepository"] = new EmployeeRepository();
            
            var function = new TestingDurableFunction(
                ((Mock<IEmployeesLegacyClient>) _scenarioContext["ApiMock"]).Object,
                (IEmployeeRepository) _scenarioContext["EmployeeRepository"]
            );
            
            var durableOrchestrationContextMock = new Mock<IDurableOrchestrationContext>();

            async Task<List<EmployeeLegacyResponse>> GetEmployeesDelegate(string name, object input) =>
                await function.MigrateEmployees_GetEmployees(name);

            async Task SaveEmployeeDelegate(string name, EmployeeLegacyResponse employee) =>
                await function.MigrateEmployees_SaveEmployee(employee);

            durableOrchestrationContextMock
                .Setup(m => m.CallActivityAsync<List<EmployeeLegacyResponse>>(nameof(TestingDurableFunction.MigrateEmployees_GetEmployees), null))
                .Returns((Func<string, object, Task<List<EmployeeLegacyResponse>>>)GetEmployeesDelegate);
            
            durableOrchestrationContextMock
                .Setup(m => m.CallActivityAsync(nameof(TestingDurableFunction.MigrateEmployees_SaveEmployee), It.IsAny<EmployeeLegacyResponse>()))
                .Returns((Func<string, EmployeeLegacyResponse, Task>)SaveEmployeeDelegate);
            
            await function.MigrateEmployees(durableOrchestrationContextMock.Object);
        }
        
        [Then(@"These employees are saved into the new application")]
        public async Task ThenTheseEmployeesAreSavedIntoTheNewApplication(List<Employee> expectedEmployees)
        {
            var employeeRepository = (EmployeeRepository) _scenarioContext["EmployeeRepository"];
            var savedEmployees = await employeeRepository.GetEmployeesAsync();
            savedEmployees.Should().BeEquivalentTo(expectedEmployees);
        }
    }
}
