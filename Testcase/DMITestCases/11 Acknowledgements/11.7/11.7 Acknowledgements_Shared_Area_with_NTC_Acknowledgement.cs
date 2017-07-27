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
    /// 11.7 Acknowledgements: Shared Area with NTC-Acknowledgement
    /// TC-ID: 6.7
    /// 
    /// This test case verifies that:-- Sound Sinfo is not played once if new incoming NTC-acknowledgement is entered in the list.- NTC-acknowledgement is placed in the list if it shares area with ETCS- acknowledgement.
    /// 
    /// Tested Requirements:
    /// MMI_gen 4483 (partly: NTC);
    /// 
    /// Scenario:
    /// 1.Perform SoM to Level STM-ATB in SN mode.
    /// 2.Send test script file 6_7.xml to the DMI.
    /// 3.Press sub-area (E5+E6+E7+E8+E9) for acknowledgement and observe appearance of text message on the DMI.
    /// 4.Press sub-area (E5+E6+E7+E8+E9) for acknowledgement and observe appearance of text message on the DMI.
    /// 5.Press sub-area (E5+E6+E7+E8+E9) for acknowledgement and observe appearance of text message on the DMI.
    /// 6.Press sub-area (E5+E6+E7+E8+E9) for acknowledgement and observe appearance of text message on the DMI.
    /// 
    /// Used files:
    /// 6_7.xml
    /// </summary>
    public class Acknowledgements_Shared_Area_with_NTC_Acknowledgement : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // 1. System is power on.2. Configuration with Level STM-ATB (STM ID = 1).3. Set language to English.
            
            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SN mode, Level STM-ATB.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            
            /*
            Test Step 1
            Action: Perform SoM to Level STM-ATB in SN mode
            Expected Result: ETCS OB enters Level STM-ATB, SN mode
            */
            
            
            /*
            Test Step 2
            Action: Use test script file 6_7.xml to send STM text message and ETCS text message to the DMI via STM-38 and EVC-8, respectively with:-Message 1: STM-38- MMI_STM_Q_ACK = 1- MMI_STM_X_TEXT = 2Message 2: EVC-8- MMI_Q_TEXT_CLASS = 0- MMI_Q_TEXT_CRITERIA = 1- MMI_Q_TEXT = 268Message 3: STM-38- MMI_STM_Q_ACK = 1- MMI_STM_X_TEXT = 4Message 4: EVC-8- MMI_Q_TEXT_CLASS = 0- MMI_Q_TEXT_CRITERIA = 1- MMI_Q_TEXT = 305
            Expected Result: Verify the following information:After DMI receives test script- Text message ‘2 - Brakes are not operated’ is displayed in sub-area E5 with yellow flashing frame surrounded area (E5+E6+E7+E8+E9).- Sound Sinfo is not played.5 seconds later- Text message ‘Communication error’ is displayed in sub-area E5 with yellow flashing frame surrounded area (E5+E6+E7+E8+E9).- Sound Sinfo is played once.5 seconds later- Text message ‘4 - Brake feedback fault’ is displayed in sub-area E5 with yellow flashing frame surrounded area (E5+E6+E7+E8+E9).- Sound Sinfo is not played.5 seconds later- Text message ‘Train divided’ is displayed in sub-area E5 with yellow flashing frame surrounded area (E5+E6+E7+E8+E9).- Sound Sinfo is played once
            Test Step Comment: MMI_gen 4483 (partly: NTC);
            */
            
            
            /*
            Test Step 3
            Action: Press sub-area (E5+E6+E7+E8+E9) for acknowledgement
            Expected Result: Verify the following information:- Text message ‘Train divided’ is disappeared.- Text message ‘4 - Brake feedback fault’ is reappeared in sub-area E5 with yellow flashing frame surrounded area (E5+E6+E7+E8+E9)
            Test Step Comment: MMI_gen 4483 (partly: NTC);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press sub-area (E5+E6+E7+E8+E9) for acknowledgement");
            
            
            /*
            Test Step 4
            Action: Press sub-area (E5+E6+E7+E8+E9) for acknowledgement
            Expected Result: Verify the following information:- Text message ‘4 - Brake feedback fault’ is disappeared.- Text message ‘Communication error’ is reappeared in sub-area E5 with yellow flashing frame surrounded area (E5+E6+E7+E8+E9)
            Test Step Comment: MMI_gen 4483 (partly: NTC);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press sub-area (E5+E6+E7+E8+E9) for acknowledgement");
            
            
            /*
            Test Step 5
            Action: Press sub-area (E5+E6+E7+E8+E9) for acknowledgement
            Expected Result: Verify the following information:- Text message ‘Communication error’ is disappeared.- Text message ‘2 - Brakes are not operated’ is reappeared in sub-area E5 with yellow flashing frame surrounded area (E5+E6+E7+E8+E9)
            Test Step Comment: MMI_gen 4483 (partly: NTC);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press sub-area (E5+E6+E7+E8+E9) for acknowledgement");
            
            
            /*
            Test Step 6
            Action: Press sub-area (E5+E6+E7+E8+E9) for acknowledgement
            Expected Result: - Text message ‘2 - Brakes are not operated’ is disappeared
            Test Step Comment: This test step is to clear expected result of the previous test step.
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(@"Press sub-area (E5+E6+E7+E8+E9) for acknowledgement");
            
            
            /*
            Test Step 7
            Action: End of test
            Expected Result: 
            */
            

            return GlobalTestResult;
        }
    }
}
