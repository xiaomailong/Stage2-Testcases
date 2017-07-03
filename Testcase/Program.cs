using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BT_CSB_Tools;

namespace Testcase
{
    class Program
    {
        static void Main(string[] args)
        {
            TestcaseRunner.AddTestcase(typeof(Testcase));
            TestcaseRunner.RunTestcases(args);
        }
    }
}
