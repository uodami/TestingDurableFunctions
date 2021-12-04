using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace TestingDurableFunction.Specflow
{
    [Binding]
    public class StepArgumentTransformations
    {
        [StepArgumentTransformation]
        public List<EmployeeLegacyResponse> TransformToEmployeeLegacyResponse(Table table)
        {
            return table.CreateSet<EmployeeLegacyResponse>().ToList();
        }
        
        [StepArgumentTransformation]
        public List<Employee> TransformToEmployee(Table table)
        {
            return table.CreateSet<Employee>().ToList();
        }
    }
}