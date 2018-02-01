using BT_CSB_Tools;
using Testcase.DMITestCases;

namespace Testcase
{
    class Program
    {
        static void Main(string[] args)
        {

            TestcaseRunner.AddTestcase(typeof(TC_ID_2_6_Safety_related_Data_Entry));
            //TestcaseRunner.AddTestcase(typeof(TestcaseBase));
            TestcaseRunner.RunTestcases(args);
        }
    }
}