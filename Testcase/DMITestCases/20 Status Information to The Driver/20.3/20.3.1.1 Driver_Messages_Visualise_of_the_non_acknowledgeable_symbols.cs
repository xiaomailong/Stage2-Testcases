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
    /// 20.3.1.1 Driver Messages: Visualise of the non-acknowledgeable symbols
    /// TC-ID: 15.3.1.1
    /// 
    /// This test case verifies the display information of each symbol refer to received packet information EVC-8 which the variable is marked with '#3' in [GenVSIS]. The location of symbols are complied with [ERA-ERTMS].
    /// 
    /// Tested Requirements:
    /// MMI_gen 7022 (partly: exclude radio connection symbols); MMI_gen 3005 (partly: exclude radio connection symbols); MMI_gen 144 (partly: Symbols); MMI_gen 1699 (partly: non-acknowledgement, symbol);
    /// 
    /// Scenario:
    /// At the default window, use the test script file to send Driver message to DMI. Then, verify the display of normal non-acknowledgement symbol.Use the test script file to send Driver message to DMI. Then, verify the display of normal non-acknowledgement symbol.Note: 
    /// 1.Each step of test script file in executed continuously, Tester need to confirm expected result within specific time (5 second).
    /// 2.For the symbol of Radio connection will be verified in another test case instead.
    /// 
    /// Used files:
    /// 15_3_1_1_a.xml, 15_3_1_1_b.xml, 15_3_1_1_c.xml
    /// </summary>
    public class Driver_Messages_Visualise_of_the_non_acknowledgeable_symbols : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power onSoM is performed until Level 1 is selected and confirmedMain window is closed
            
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
            Action: Use the test script file 15_3_1_1_a.xml to send EVC-8 with,MMI_Q_TEXT = 260MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 1
            Expected Result: Verify the following information,(1)    DMI displays ST01 symbol in sub-area C9 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 3005 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol);
            */
            
            
            /*
            Test Step 2
            Action: (Continue from step 1)Send EVC-8 with,MMI_Q_TEXT = 286MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 2
            Expected Result: Verify the following information,(1)    DMI displays ST06 symbol in sub-area C6 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 3005 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol);
            */
            
            
            /*
            Test Step 3
            Action: (Continue from step 2)Send EVC-8 with,MMI_Q_TEXT = 298MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 3
            Expected Result: Verify the following information,(1)    DMI displays DR02 symbol in sub-area D without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 3005 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol);
            */
            
            
            /*
            Test Step 4
            Action: (Continue from step 3)Send EVC-8 with,MMI_Q_TEXT = 710MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays train divided symbol in sub-area C1 without yellow flashing frame.Note: The information of this symbol is not provided by [ERA-ERTMS] and [GenVSIS], it’s used the same reference from DMI-DOS
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 3005 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol);
            */
            
            
            /*
            Test Step 5
            Action: (Continue from step 4)Send EVC-8 with,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 0MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays LE06 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */
            
            
            /*
            Test Step 6
            Action: (Continue from step 5)Send EVC-8 with,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 1MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays LE10 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */
            
            
            /*
            Test Step 7
            Action: (Continue from step 6)Send EVC-8 with,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 2MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays LE12 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */
            
            
            /*
            Test Step 8
            Action: (Continue from step 7)Send EVC-8 with,MMI_Q_TEXT = 276MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 3MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays LE14 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */
            
            
            /*
            Test Step 9
            Action: (Continue from step 8)Send EVC-8 with,MMI_Q_TEXT = 259MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays MO08 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */
            
            
            /*
            Test Step 10
            Action: (Continue from step 9)Send EVC-8 with,MMI_Q_TEXT = 262MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays MO15 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */
            
            
            /*
            Test Step 11
            Action: (Continue from step 10)Send EVC-8 with,MMI_Q_TEXT = 263MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays MO10 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */
            
            
            /*
            Test Step 12
            Action: (Continue from step 11)Send EVC-8 with,MMI_Q_TEXT = 264MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays MO17 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */
            
            
            /*
            Test Step 13
            Action: (Continue from step 12)Send EVC-8 with,MMI_Q_TEXT = 265MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays MO02 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */
            
            
            /*
            Test Step 14
            Action: (Continue from step 13)Send EVC-8 with,MMI_Q_TEXT = 266MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays MO05 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */
            
            
            /*
            Test Step 15
            Action: (Continue from step 14)Send EVC-8 with,MMI_Q_TEXT = 709MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays MO22 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */
            
            
            /*
            Test Step 16
            Action: (Continue from step 15)Send EVC-8 with,MMI_Q_TEXT = 257MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 0MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays LE07 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */
            
            
            /*
            Test Step 17
            Action: (Continue from step 16)Send EVC-8 with,MMI_Q_TEXT = 257MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 1MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays LE11 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */
            
            
            /*
            Test Step 18
            Action: (Continue from step 17)Send EVC-8 with,MMI_Q_TEXT = 257MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 2MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays LE13 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */
            
            
            /*
            Test Step 19
            Action: (Continue from step 18)Send EVC-8 with,MMI_Q_TEXT = 257MMI_Q_TEXT_CRITERIA = 3MMI_N_TEXT = 1MMI_X_TEXT = 3MMI_I_TEXT = 4
            Expected Result: Verify the following information,(1)    DMI displays LE15 symbol in sub-area C1 without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol); MMI_gen 147 (partly: symbol, remove the same index, MMI_gen 144 (partly: symbol, the same index); MMI_gen 3005 (partly: exclude radio connection symbols); 
            */
            
            
            /*
            Test Step 20
            Action: Press ‘Main’ button.Then, use the test script file 15_3_1_1_b.xml to Send EVC-8 with,MMI_Q_TEXT = 716MMI_Q_TEXT_CRITERIA = 3MMI_I_TEXT = 5
            Expected Result: Verify the following information,(1)    DMI displays ST05 symbol in the window title area without yellow flashing frame
            Test Step Comment: (1) MMI_gen 7022 (partly: exclude radio connection symbols);  MMI_gen 3005 (partly: exclude radio connection symbols);  MMI_gen 1699 (partly: non-acknowledgement, symbol);
            */
            
            
            /*
            Test Step 21
            Action: Use the test script file 15_3_1_1_c.xml to send EVC-8 with,MMI_Q_TEXT_CRITERIA = 4MMI_I_TEXT = 5
            Expected Result: Verify the following information,(1)    The symbol ST05 is removed from window title area
            Test Step Comment: (1) MMI_gen 144 (partly: Symbols, removed by Q_TEXT_CRITERIA);
            */
            
            
            /*
            Test Step 22
            Action: End of test
            Expected Result: 
            */
            

            return GlobalTestResult;
        }
    }
}
