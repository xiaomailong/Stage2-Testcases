#region usings

using System;
using System.Diagnostics;
using CL345;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.Telegrams.DMItoEVC;
using Testcase.DMITestCases;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using BT_CSB_Tools.SignalPoolGenerator.Signals;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal;
using BT_RTSIMClient;

#endregion

namespace Testcase
{
    public class TestcaseBase : SignalPool
    {
        public int UniqueIdentifier;

        public string CurrentTestStepIdentifier;
        public Dictionary<string, bool> TestStepResults = new Dictionary<string, bool>();

        /// <summary>
        /// When sending Message Data with ack, wait this long for the ack before timing out
        /// </summary>
        public const int WaitForAckTimeout = 3000;

        /// <summary>
        /// The TCMS version being tested
        /// </summary>
        public Version TCMSVersion = new Version(6, 2, 0 ,0);

        public override void PreExecution()
        {
            // Pre-test configuration.

            // Subscribe to logging events
            Logger.LoggingEvent += Logger_LoggingEvent;

            // Record the version tested
            TraceInfo("TCMS Version " + TCMSVersion);

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
            EVC13_MMIDataView.Initialise(this);
            EVC14_MMICurrentDriverID.Initialise(this);
            EVC16_CurrentTrainNumber.Initialise(this);
            EVC18_MMISetVBC.Initialise(this);
            EVC19_MMIRemoveVBC.Initialise(this);
            EVC20_MMISelectLevel.Initialise(this);
            EVC22_MMICurrentRBC.Initialise(this);
            EVC23_MMILssma.Initialise(this);
            EVC24_MMISystemInfo.Initialise(this);
            EVC25_MMISpecificSTMDERequest.Initialise(this);
            EVC26_MMISpecificSTMDWValues.Initialise(this);
            EVC27_MMISpecificSTMTestRequest.Initialise(this);
            EVC28_MMIEchoedSetVBCData.Initialise(this);
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
            EVC109_MMISetTimeMMI.Initialise(this);
            EVC110_MMIConfirmedTrainData.Initialise(this);
            EVC111_MMIDriverMessageAck.Initialise(this);
            EVC112_MMINewRbcData.Initialise(this);
            EVC116_MMINewTrainNumber.Initialise(this);
            EVC118_MMINewSetVbc.Initialise(this);
            EVC119_MMINewRemoveVbc.Initialise(this);
            EVC121_MMINewLevel.Initialise(this);
            EVC122_MMINewLanguage.Initialise(this);
            EVC123_MMISpecificSTMDataToSTM.Initialise(this);
            EVC128_MMIConfirmedSetVBC.Initialise(this);
            EVC129_MMIConfirmedRemoveVBC.Initialise(this);
            EVC140_MMINewMaintenanceData.Initialise(this);
            EVC141_MMIConfirmedMaintenanceData.Initialise(this);
            EVC150_MMINewBrakePercentage.Initialise(this);
            EVC151_MMIConfirmedBrakePercentage.Initialise(this);
            EVC152_MMIDriverAction.Initialise(this);
        }

        /// <summary>
        /// Common startup steps
        /// </summary>
        public virtual void StartUp()
        {
            DmiActions.Deactivate_Cabin(this);
            DmiActions.Start_ATP();
            DmiActions.Activate_Cabin_1(this);
        }

        /// <summary>
        /// Collect test results and map them to teststeps
        /// </summary>
        /// <param name="glim"></param>
        private void Logger_LoggingEvent(BT_CSB_Tools.Logging.GuiLoggerAppender.MVVM.Model.GuiLogItemModel glim)
        {
            // not interested in Set Value or TraceInfo or Wait commands
            if (glim.Command == "Set" || glim.IsInfoItem || glim.Command == "Wait")
                return;

            if (glim.IsHeaderItem)
            {
                var regex = new Regex(@"^TP-\d*");

                if (regex.IsMatch(glim.Comment))
                {
                    CurrentTestStepIdentifier = regex.Match(glim.Comment).Value;
                    TestStepResults.Add(CurrentTestStepIdentifier, true);
                }
            }
            else if (glim.Result == "Failed")
            {
                TestStepResults[CurrentTestStepIdentifier] = false;
            }
        }

        public override void PostExecution()
        {
            // Post-test cleanup.
            DmiActions.Send_SB_Mode(this);
            DmiActions.Deactivate_Cabin(this);

            // Generate report over the teststeps
            TraceReport(string.Join(Environment.NewLine, TestStepResults.Select(pair => pair.Key + " " + pair.Value)));
        }

        /// <summary>
        /// Testcase entry point, as this is the base, this method should be overriden and never called
        /// </summary>
        /// <returns></returns>
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // As this method should never be called, throw an exception
            //throw new InvalidOperationException("The TestcaseEntryPoint method on TestcaseBase should never be called, it should be overriden.");
            return GlobalTestResult;
        }

        public override void RunDebugger()
        {
            Debugger.Launch();
        }

        /// <summary>
        /// Creates a test step header
        /// </summary>
        /// <param name="step">The test step, always starts at 1</param>
        /// <param name="ident">The unique identifier to link to Doors</param>
        /// <param name="action">The test step action</param>
        /// <param name="result">The test step expected result</param>
        public void MakeTestStepHeader(int step, int ident, string action, string result)
        {
            TraceHeader("Test Step " + step);
            TraceHeader("TP-" + ident);
            TraceReport("Action");
            TraceInfo(action);
            if (!string.IsNullOrEmpty(result))
            {
                TraceReport("Expected Result");
                TraceInfo(result);
            }
        }

        public void WaitForAck(BaseSignal smdStat)
        {
            bool waitForSignal = this.WaitForSignal(smdStat, 3, WaitForAckTimeout);
            if (!waitForSignal)
                TraceError("Ack for MD was not received!");
            smdStat.Value = 0;
        }
    }
}