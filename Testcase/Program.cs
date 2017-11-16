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
            TestcaseRunner.AddTestcase(typeof(TC_12_7_3_Train_Speed));
            TestcaseRunner.RunTestcases(args);
        }
    }
}