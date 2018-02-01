using BT_CSB_Tools;
using Testcase.DMITestCases;

namespace Testcase
{
    class Program
    {
        static void Main(string[] args)
        {
            TestcaseRunner.AddTestcase(typeof(TC_13_1_8_Brake));
            //TestcaseRunner.AddTestcase(typeof(TestcaseBase));
            TestcaseRunner.RunTestcases(args);
        }
    }
}