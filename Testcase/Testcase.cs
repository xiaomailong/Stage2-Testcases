#region usings

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BT_Tools;
using BT_CSB_Tools;
using BT_CSB_Tools.Logging;
using BT_CSB_Tools.Utils.Xml;
using BT_CSB_Tools.SignalPoolGenerator.Signals;
using BT_CSB_Tools.SignalPoolGenerator.Signals.MwtSignal;
using BT_CSB_Tools.SignalPoolGenerator.Signals.MwtSignal.Misc;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using CL345;

#endregion

namespace Testcase
{
    public class Testcase : SignalPool
    {
        public override void PreExecution()
        {
			// Pre-test configuration.
        }

        public override void PostExecution()
        {
			// Post-test cleanup.
        }

        public override bool TestcaseEntryPoint()
        {
			// Test case entry point.
			return GlobalTestResult;
		}

		public override void RunDebugger()
        {
            Debugger.Launch();
        }
    }
}
