using BT_CSB_Tools;
using Testcase.DMITestCases;

namespace Testcase
{
    class Program
    {
        static void Main(string[] args)
        {

            TestcaseRunner.AddTestcase(typeof(TC_ID_6_1_Acknowledgements));
            //TestcaseRunner.AddTestcase(typeof(TestcaseBase));
            TestcaseRunner.RunTestcases(args);
        }
    }
}