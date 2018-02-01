using BT_CSB_Tools;
using Testcase.DMITestCases;

namespace Testcase
{
    class Program
    {
        static void Main(string[] args)
        {

            //TestcaseRunner.AddTestcase(typeof(TC_ID_5_8_Screen_Layout_Windows));
            TestcaseRunner.AddTestcase(typeof(TestcaseBase));
            TestcaseRunner.RunTestcases(args);
        }
    }
}