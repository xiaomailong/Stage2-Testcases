#region usings

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Testcase.Telegrams.EVCtoDMI;

#endregion

namespace Testcase
{
    public class TestcaseBase : SignalPool
    {
        public override void PreExecution()
        {
            // Pre-test configuration.

            // Initialise instance of all telegrams
            TraceInfo("Initialise Default Values");
            EVC0_MMIStartATP.Initialise(this);
            EVC1_MMIDynamic.Initialise(this);
            EVC2_MMIStatus.Initialise(this);
            EVC3_MMISetTimeATP.Initialise(this);
            EVC5_MMIGeoPosition.Initialise(this);
            EVC6_MMICurrentTrainData.Initialise(this);
            EVC7_MMIEtcsMiscOutSignals.Initialise(this);
            EVC8_MMIDriverMessage.Initialise(this);
            EVC14_MMICurrentDriverID.Initialise(this);
            EVC16_CurrentTrainNumber.Initialise(this);
            EVC22_MMICurrentRBC.Initialise(this);
            EVC30_MMIRequestEnable.Initialise(this);
            EVC34_MMISystemVersion.Initialise(this);

            // Initialises all EVC packets that contain dynamic arrays
            SITR.SMDCtrl.ETCS1.SelectLevel.Value = 0x8;
            SITR.SMDCtrl.ETCS1.SetVbc.Value = 0x8;
            SITR.SMDCtrl.ETCS1.RemoveVbc.Value = 0x8;
            SITR.SMDCtrl.ETCS1.TrackDescription.Value = 0x8;
            SITR.SMDCtrl.ETCS1.CurrentRbcData.Value = 0x8;
            SITR.SMDCtrl.ETCS1.EchoedTrainData.Value = 0x8;
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
            throw new InvalidOperationException(
                "The TestcaseEntryPoint method on TestcaseBase should never be called, it should be overriden");
        }

        public override void RunDebugger()
        {
            Debugger.Launch();
        }
    }
}