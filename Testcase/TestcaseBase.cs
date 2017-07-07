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
    public class TestcaseBase : SignalPool
    {
        public override void PreExecution()
        {
            // Pre-test configuration.

            // Set up safe words
            SITR.CCUS.ETCSTrTelegram1.Tr1SSW1.Value = 0x8000;
            SITR.CCUS.ETCSTrTelegram1.Tr1SSW2.Value = 0x8000;
            SITR.CCUS.ETCSTrTelegram1.Tr1SSW3.Value = 0x8000;
        }

        public override void PostExecution()
        {
			// Post-test cleanup.
        }

        /// <summary>
        /// Testcase entry point, as this is the base, this method should be overriden and never called
        /// </summary>
        /// <returns></returns>
        public override bool TestcaseEntryPoint()
        {
			// As this method should never be called, throw an exception
            throw new InvalidOperationException("The TestcaseEntryPoint method on TestcaseBase should never be called, it should be overriden");
        }

		public override void RunDebugger()
        {
            Debugger.Launch();
        }
    }
}
