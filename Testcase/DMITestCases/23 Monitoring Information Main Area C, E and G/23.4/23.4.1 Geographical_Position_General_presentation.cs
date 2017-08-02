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
    /// 23.4.1 Geographical Position: General presentation
    /// TC-ID: 18.4.1
    /// 
    /// 
    /// 
    /// Tested Requirements:
    /// MMI_gen 9866; MMI_gen 9872; MMI_gen 9873 (partly: touchscreen); MMI_gen 9875; MMI_gen 9877;  MMI_gen 9878; MMI_gen 9879; MMI_gen 9874 (partly: toggled on); MMI_gen 2495 ( partly: maximum 3 digits of meter, maximum 4 digits of kilometer, kilometer_meter format); MMI_gen 9416; MMI_gen 656 (partly: transmit EVC-101, touch screen); MMI_gen 655; MMI_gen 2498; MMI_gen 1088 (partly: Bit #23);
    /// 
    /// Scenario:
    /// Perform SoM to SR mode, L1Pass BG1 at 100m:DMI changes from SR mode to FS mode.Packet 12:L_ENDSECTION = 3000mpacket 21:G_A = 0packet 27:V_STATIC = 150km/hPass BG2 at 200m:packet 79:NID_BG = 2M_POSITION = 1000000D_POSOFF = 0NID_BG = 3M_POSITION = 900D_POSOFF = 0Pass BG3 at 1000m (No packet information, use as reference location)Stop the train.Select Shunting modeExit Shunting mode and perform SoM to SR mode, L1Pass BG4 at 1700m:                 packet 79:NID_BG = 2M_POSITION = 1000000D_POSOFF = 0NID_BG = 3M_POSITION = 900D_POSOFF = 0Use the test script file to send an invalid value of EVC-5.Observer and verify Geographical Position indication at three locations:Balise#1 at 100 m to transition from SR to FS modeContinue driving train to reach balise#2 at 200 mContinue driving train to reach balise#3 at 1000mContinue driving train to reach balise#4 at 1700m and received packet EVC-5 from test script file
    /// 
    /// Used files:
    /// 18_4_1.tdg, 18_4_1.xml
    /// </summary>
    public class Geographical_Position_General_presentation : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is powered onCabin is activePerform a complete SoM to enter SR mode, ETCS level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in SH mode, Level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Drive the train forward with the permitted speed
            Expected Result: DMI displays in SR mode, level 1
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_SR_mode_level_1(this);


            /*
            Test Step 2
            Action: Pass BG1 with Pkt 12,21 and 27
            Expected Result: DMI displays in FS mode, level 1
            */
            // Call generic Action Method
            DmiActions.Pass_BG1_with_Pkt_12_21_and_27(this);
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_FS_mode_level_1(this);


            /*
            Test Step 3
            Action: Pass BG2 with pkt 79 Geographical position
            Expected Result: Verify the Geographical Position indicatorThe symbol ‘DR03’ is displayed in sub-area G12 as toggled off (the position is known by onbaord)DMI receives EVC-30 with bit No.23 of variable MMI_Q_REQUEST_ENABLE_64 = 1 (DMI displays that position is known by onboard)
            Test Step Comment: (1) MMI_gen 9866, MMI_gen 9872, MMI_gen 9875;(2) MMI_gen 9416 (partly: EVC-30. MMI_Q_REQUEST_ENABLE_64 = 1 = known onboard); MMI_gen 1088 (partly: Bit #23);
            */


            /*
            Test Step 4
            Action: Press on the ‘DR03’ symbol, on sub-area G12 to toggle on the Geographical Position function and verify the presentation on the screen
            Expected Result: Verify the Geographical Position indicatorThe sub-area G12 displays a grey background colour with black text colour showing numbers in the following format nnnn_ddd as shown in the figure belowwhere nnnn are the km digits, _ is a space character and ddd are the metersDMI displays the geographical position same as a value of variable MMI_M_ABSOLUTPOS of EVC-5.DMI sends EVC-101 with variable MMI_M_REQUEST = 8 (Figure 117, [MMI-ETCS-gen])
            Test Step Comment: (1) MMI_gen 9872, MMI_gen 9873 (partly: toggle on state for touchscreen); MMI_gen 9877;MMI_gen 9878;MMI_gen 2495 (partly: kilometer_meter format); MMI_gen 2498;       (2) MMI_gen 655, (3) MMI_gen 656 (partly: transmit EVC-101, touch screen)
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this,
                @"Press on the ‘DR03’ symbol, on sub-area G12 to toggle on the Geographical Position function and verify the presentation on the screen");


            /*
            Test Step 5
            Action: Press on the ‘DR03’ symbol on sub-area G12 to toggle off the Geographical Position function and verify the presentation on the screen
            Expected Result: (1) The grey background colour in previous step is replaced by symbol ‘DR03’ in sub-area G12
            Test Step Comment: (1) MMI_gen 9872, MMI_gen 9873 (partly: toggle off state for touchscreen); MMI_gen 9875;
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this,
                @"Press on the ‘DR03’ symbol on sub-area G12 to toggle off the Geographical Position function and verify the presentation on the screen");


            /*
            Test Step 6
            Action: Stop the train, the driver presses the symbol of Geographical Position at sub-area G12 again
            Expected Result: The Geographical Position is displayed with valid value of the train position. Verify that the Geographic Position is displayed the fractional part that consists of three digits. The integral part consists of four digits. A space character is inserted between the kilometre and the metre parts.The full sub-area G12 is displayed as grey background. The geographical position is displayed in black and located at centre of the G12 area
            Test Step Comment: (1) MMI_gen 2495 ( partly: maximum 3 digits of meter, maximum 4 digits of kilometer);    MMI_gen 9872;     (2) MMI_gen 9878;    (3) MMI_gen 9873 (partly: toggle on state for touchscreen);                                 
            */


            /*
            Test Step 7
            Action: Start driving the train forward
            Expected Result: Verify that the last status of geographical position is not changed. The full sub-area G12 is displayed as grey background. The geographical position is displayed in black and located at centre of the G12 area
            Test Step Comment: MMI_gen 9874 (partly: toggled on);   
            */


            /*
            Test Step 8
            Action: Press on the ‘DR03’ symbol on sub-area G12 to toggle off the Geographical Position function and verify the presentation on the screen
            Expected Result: The grey background colour in previous step is  replaced by symbol ‘DR03’ in sub-area G12
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this,
                @"Press on the ‘DR03’ symbol on sub-area G12 to toggle off the Geographical Position function and verify the presentation on the screen");


            /*
            Test Step 9
            Action: Pass BG3 with the new Geographical position
            Expected Result: The symbol ‘DR03’ remains in sub-area G12
            */


            /*
            Test Step 10
            Action: Press on the ‘DR03’ symbol, on sub-area G12 to toggle on the Geographical Position function and verify the presentation on the screen
            Expected Result: Verify the Geographical Position indicatorThe sub-area G12 displays a grey background colour with black text colour showing numbers in the following format nnnn_ddd as shown in the figure below
            Test Step Comment: MMI_gen 2495 ( partly: 2 digits of meter, at least 1 digit of kilometer);    
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this,
                @"Press on the ‘DR03’ symbol, on sub-area G12 to toggle on the Geographical Position function and verify the presentation on the screen");


            /*
            Test Step 11
            Action: Perform the following procedure,Stop the trainPress ‘Main’ button.Press and hold ‘Shunting’ button for 2 second or upper.Release the pressed area
            Expected Result: DMI displays in SH mode, level 1.Verify that the symbol of Geographical Position at sub-area G12 is not displayed. In sub-area G12, it is not displayed as a sensitive area for toggle on/off.DMI receives EVC-30 with bit No.23 of variable MMI_Q_REQUEST_ENABLE_64 = 0 or EVC-5 with variable MMI_M_ABSOLUTPOS < 0 (DMI displays that position is NOT known by onboard)
            Test Step Comment: (1) MMI_gen 9879;   (2) MMI_gen 9416 (partly: not known by onboard, EVC-30)
            */


            /*
            Test Step 12
            Action: Perform the following procedure,Press ‘Main’ button.Press and hold ‘Exit Shunting’ button for 2 second or upper.Release the pressed area.Perform SoM in SR mode, Level 1.Drive the train forward
            Expected Result: DMI displays in SR mode, Level 1
            */
            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_in_SR_mode_Level_1(this);


            /*
            Test Step 13
            Action: Pass BG4 with the new Geographical position
            Expected Result: The symbol ‘DR03’ displays in sub-area G12
            */
            // Call generic Check Results Method
            DmiExpectedResults.The_symbol_DR03_displays_in_sub_area_G12(this);


            /*
            Test Step 14
            Action: Perform the following procedure,Stop the train.Use the test script file 18_4_1.xml to send EVC-5 with MMI_M_ABSOLUTPOS = 8388609Press at sub-area G12
            Expected Result: Verify the following information,Verify that the symbol of Geographical Position at sub-area G12 is not displayed. In sub-area G12, it is not displayed as a sensitive area for toggle on/off.Use the log file to confirm that DMI did not send out packet EVC-101 EVC-101 with variable MMI_M_REQUEST = 8
            Test Step Comment: (1) MMI_gen 9416 (partly: not known by onboard, EVC-5); MMI_gen 9879 (partly: not display symbol DR03);   (2) MMI_gen 9879 (partly: G12 not be sensitive);   
            */


            /*
            Test Step 15
            Action: End of test
            Expected Result: 
            */


            return GlobalTestResult;
        }
    }
}