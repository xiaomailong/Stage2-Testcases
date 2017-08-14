using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BT_CSB_Tools;
using CL345;
using Testcase.DMITestCases;

namespace Testcase
{
    class Program
    {
        static void Main(string[] args)
        {
            TestcaseRunner.AddTestcase(typeof(TC_15_5_1_Adhesion_Factor));
            TestcaseRunner.RunTestcases(args);
        }
    }
}