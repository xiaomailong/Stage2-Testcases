﻿using System;
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
            TestcaseRunner.AddTestcase(typeof(TC_ID_6_3_Acknowledgements));
            TestcaseRunner.RunTestcases(args);
        }
    }
}