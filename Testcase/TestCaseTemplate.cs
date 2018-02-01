#region usings

#endregion

namespace Testcase
{
    public class TestCaseTemplate : TestcaseBase
    {
        public override void PreExecution()
        {
            // Setup test

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Do post actions

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            return GlobalTestResult;
        }
    }
}