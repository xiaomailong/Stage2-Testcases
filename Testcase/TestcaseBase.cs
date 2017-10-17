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
using Testcase.Telegrams.DMItoEVC;

#endregion

namespace Testcase
{
    public class TestcaseBase : SignalPool
    {
        public override void PreExecution()
        {
            // Pre-test configuration.

            // Initialise instance of all telegrams
            TraceInfo("Initialise default telegram values.");

            // EVC->DMI
            EVC0_MMIStartATP.Initialise(this);
            EVC1_MMIDynamic.Initialise(this);
            EVC2_MMIStatus.Initialise(this);
            EVC3_MMISetTimeATP.Initialise(this);
            EVC4_MMITrackDescription.Initialise(this);
            EVC5_MMIGeoPosition.Initialise(this);
            EVC6_MMICurrentTrainData.Initialise(this);
            EVC7_MMIEtcsMiscOutSignals.Initialise(this);
            EVC8_MMIDriverMessage.Initialise(this);
            EVC10_MMIEchoedTrainData.Initialise(this);
            EVC11_MMICurrentSRRules.Initialise(this);
            EVC14_MMICurrentDriverID.Initialise(this);
            EVC16_CurrentTrainNumber.Initialise(this);
            EVC18_MMISetVBC.Initialise(this);
            EVC19_MMIRemoveVBC.Initialise(this);
            EVC20_MMISelectLevel.Initialise(this);
            EVC22_MMICurrentRBC.Initialise(this);
            EVC23_MMILssma.Initialise(this);
            EVC25_MMISpecificSTMDERequest.Initialise(this);
            EVC29_MMIEchoedRemoveVBCData.Initialise(this);
            EVC30_MMIRequestEnable.Initialise(this);
            EVC31_MMINTCDeSelect.Initialise(this);
            EVC32_MMITrackConditions.Initialise(this);
            EVC33_MMIAdditionalOrder.Initialise(this);
            EVC34_MMISystemVersion.Initialise(this);

            EVC40_MMICurrentMaintenanceData.Initialise(this);
            EVC41_MMIEchoedMaintenanceData.Initialise(this);

            EVC50_MMICurrentBrakePercentage.Initialise(this);
            EVC51_MMIEchoedBrakePercentage.Initialise(this);

            // DMI->EVC
            EVC100_MMIStartMmi.Initialise(this);
            EVC101_MMIDriverRequest.Initialise(this);
            EVC102_MMIStatusReport.Initialise(this);
            EVC104_MMINewDriverData.Initialise(this);
            EVC106_MMINewSrRules.Initialise(this);
            EVC107_MMINewTrainData.Initialise(this);
            //EVC109_MMISetTimeMMI.Initialise(this);
            EVC110_MMIConfimedTrainData.Initialise(this);
            EVC111_MMIDriverMessageAck.Initialise(this);
            EVC112_MMINewRbcData.Initialise(this);
            EVC116_MMINewTrainNumber.Initialise(this);
            EVC118_MMINewSetVbc.Initialise(this);
            EVC121_MMINewLevel.Initialise(this);
            EVC123_MMISpecificSTMDataToSTM.Initialise(this);
            EVC128_MMIConfirmedSetVBC.Initialise(this);
            EVC129_MMIConfirmedRemoveVBC.Initialise(this);
            EVC140_MMINewMaintenanceData.Initialise(this);
            EVC152_MMIDriverAction.Initialise(this);
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
                "The TestcaseEntryPoint method on TestcaseBase should never be called, it should be overriden.");
        }

        public override void RunDebugger()
        {
            Debugger.Launch();
        }
    }
}