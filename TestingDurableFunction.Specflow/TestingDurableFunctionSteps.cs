using TechTalk.SpecFlow;

namespace TestingDurableFunction.Specflow
{
    [Binding]
    public class TestingDurableFunctionSteps
    {
        [Given(@"These employees in the legacy application")]
        public void GivenTheseEmployeesInTheLegacyApplication(Table table)
        {
            ScenarioContext.Current.Pending();
        }
        
        [When(@"The migration process is launched")]
        public void WhenTheMigrationProcessIsLaunched()
        {
            ScenarioContext.Current.Pending();
        }
        
        [Then(@"These employees are saved into the new application")]
        public void ThenTheseEmployeesAreSavedIntoTheNewApplication(Table table)
        {
            ScenarioContext.Current.Pending();
        }
    }
}
