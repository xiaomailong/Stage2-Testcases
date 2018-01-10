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
            return GlobalTestResult;
        }
    }
}