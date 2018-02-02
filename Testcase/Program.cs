using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BT_CSB_Tools;
using Testcase.DMITestCases;

namespace Testcase
{
    class Program
    {
        static void Main(string[] args)
        {
            if (false)
            {
                // Create a list of TestCases to ignore
                var ignorelist = new List<string>
                {
                    "TestcaseBase"
                };

                // Get all classes under Testcase.Scenarios
                var typelist = GetTypesInNamespace(Assembly.GetExecutingAssembly(), "Testcase.DMITestCases");

                // Filter typelist through ignorelist and add them to Testcaserunner
                foreach (var type in typelist.Where(t => !ignorelist.Contains(t.Name)))
                    TestcaseRunner.AddTestcase(type);

                TestcaseRunner.RunTestcases(args);
            }
            else
            {
                TestcaseRunner.AddTestcase(typeof(TC_ID_6_1_Acknowledgements));
                //TestcaseRunner.AddTestcase(typeof(TestcaseBase));
                TestcaseRunner.RunTestcases(args);
            }
        }

        private static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return
                assembly.GetTypes()
                    .Where(t => string.Equals(t.Namespace, nameSpace, StringComparison.Ordinal))
                    .ToArray();
        }
    }
}