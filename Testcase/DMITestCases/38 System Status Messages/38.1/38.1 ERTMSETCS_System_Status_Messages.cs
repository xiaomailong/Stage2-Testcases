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

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 38.1 ERTMS/ETCS System Status Messages
    /// TC-ID: 35.1
    /// 
    /// This test case verifies the display of system status messages refer to recevied packet information EVC-8 which comply with [MMI-ETCS-gen], system status messages are displayed as text messages not to be acknowledged.
    /// 
    /// Tested Requirements:
    /// MMI_gen 9520 (partly: Table 76); MMI_gen 9522 (partly: Table 76);
    /// 
    /// Scenario:
    /// Drive the train forward. Then, verify the system status message refer to received packet information EVC-8 which compliedUse the test script file to send EVC-
    /// 8.Then, verifies the display information.
    /// 
    /// Used files:
    /// 35_1.xml
    /// </summary>
    public class ERTMSETCS_System_Status_Messages : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power onSoM is performed until Level 1 is confirmed and the ‘Main’ window is closed.
            
            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SB mode, level 1

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            
            /*
            Test Step 1
            Action: Force the train roll away by moving of speed with ‘Neutral’ direction
            Expected Result: Verify the following information,DMI displays system message “Runaway movement” in sub-area E5 without yellow flashing frame.Use the log file to confirm that DMI received the EVC-8 with [MMI_DRIVER_MESSAGE (EVC-8).MMI_Q_TEXT] = 269
            Test Step Comment: (1) MMI_gen 9520 (partly: Table 76);(2) MMI_gen 9522 (partly: Table 76);
            */
            
            
            /*
            Test Step 2
            Action: Use the test script file 35_1.xml to send multiple packets EVC-8 with the following value,Common variableMMI_Q_TEXT_CLASS = 1MMI_Q_TEXT_CRITERIA =3The order of MMI_Q_TEXT value in each packetMMI_Q_TEXT = 267MMI_Q_TEXT = 560MMI_Q_TEXT = 268MMI_Q_TEXT = 274MMI_Q_TEXT = 275MMI_Q_TEXT = 290MMI_Q_TEXT = 292MMI_Q_TEXT = 296MMI_Q_TEXT = 310MMI_Q_TEXT = 299MMI_Q_TEXT = 273MMI_Q_TEXT = 300MMI_Q_TEXT = 315MMI_Q_TEXT = 606MMI_Q_TEXT = 316MMI_Q_TEXT = 280MMI_Q_TEXT = 320MMI_Q_TEXT = 572MMI_Q_TEXT = 701MMI_Q_TEXT = 702MMI_Q_TEXT = 703MMI_Q_TEXT = 569
            Expected Result: Verify the display message in sub-area E5-E9 are correct refer to following message respectively,Balise read errorTrackside malfunctionCommunication errorEntering FSEntering OSSH refusedSH request failedTrackside not compatibleTrain data changedTrain is rejectedUnauthorized passing of EOA / LOANo MA received at level transitionSR distance exceededSH stop orderSR stop orderEmergency stopRV distance exceededNo Track DescriptionRoute unsuitable – axle load categoryRoute unsuitable – loading gaugeRoute unsuitable – traction systemRadio network registration failedNote: Use the <Up> and <Down> button to scroll the list of message in sub-area E5-E9
            Test Step Comment: MMI_gen 9522 (partly: Table 76);
            */
            
            
            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */
            

            return GlobalTestResult;
        }
    }
}
