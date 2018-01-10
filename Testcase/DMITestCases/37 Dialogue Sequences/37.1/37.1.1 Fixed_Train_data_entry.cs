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
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 37.1.1 Fixed Train data entry
    /// TC-ID: 34.1.1
    /// 
    /// This test case verifies the display information of window covering the total grid array for Fixed Train data windows when driver enter ‘valid’ value.Moreover, this test case also confirm result of value(s) stored onboard replacing information which occur only when driver press the ‘Yes’ button at Train data validation window.
    /// 
    /// Tested Requirements:
    /// MMI_gen 8863 (partly:Exception); MMI_gen 8865 (partly:Exception 1);
    /// 
    /// Scenario:
    /// Activate Cabin A.Enter and confirm Driver ID.Select and confirm Level 1.Open Train data window.Select another new train type button without confirmation.Close and re-open Train data window. Then, verify that the train type is not changed to the new selected train type. Select and confirm another new train type button.Select ‘Yes’ button at Train data entry window.Select and confirm ‘No’ button at Train data validation window.Open Train data window Then, verify that the train type is not changed to the new selected train typeSelect and confirm another new train type button.Select ‘Yes’ button at Train data entry window.Select and confirm ‘Yes’ button at Train data validation window.Open Train data window Then, verify that the train type is changed to the new selected train type.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_34_1_1_Dialogue_Sequences : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Open defaultValues_default.xml in OTE  then set all value of parameter "TR_OBU_TrainType" to 1 (Now is 2) then you will get the fixed train data entry System is power on

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
            TraceInfo("This test case requires a specific configuration - " +
                      "See Precondition requirements. If this is not done manually, the test may fail!");
            /*
            Test Step 1
            Action: Activate Cabin AEnter Driver ID and perform brake testSelect and confirm Level 1
            Expected Result: DMI displays Main window
            */
            DmiActions.Complete_SoM_L1_SB(this);

            DmiActions.ShowInstruction(this, "Press the ‘Main’ button");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main; // Main window
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.TrainData;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window.");
            /*
            Test Step 2
            Action: Press ‘Train data’ button
            Expected Result: DMI displays Train data window
            */
            DmiActions.ShowInstruction(this, @"Press ‘Train data’ button");

            DmiActions.Send_EVC6_MMICurrentTrainData_FixedDataEntry(this,
                new[] {"FLU", "RLU", "Rescue"},
                1);

            DmiExpectedResults.Train_data_window_displayed(this);

            /*
            Test Step 3
            Action: Select dedicated keyboard button which have different label from an input field without confirmation
            Expected Result: The value of input field is changed refer to pressed button
            */
            DmiActions.ShowInstruction(this,
                "Press the <type 2> key on the dedicated keyboard without confirming the value");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The data input field displays the value pressed.");

            /*
            Test Step 4
            Action: Select Close button.Then, select Train data button
            Expected Result: DMI displays Train data window.Verify that the train type is not changed to the pressed button from step 3
            Test Step Comment: MMI_gen 8865 (partly:Negative testing for exception 1);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button, then press Train data button");

            DmiActions.Send_EVC6_MMICurrentTrainData_FixedDataEntry(this,
                new[] {"FLU", "RLU", "Rescue"},
                1);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Train data window." + Environment.NewLine +
                                "2. The train type does not change to the type of the button pressed in Step 3.");

            /*
            Test Step 5
            Action: Select and confirm dedicated keyboard button which have different label from an input field. Then, press ‘Yes’ button
            Expected Result: DMI displays Train data validation window
            */
            DmiActions.ShowInstruction(this,
                @"Press the <type 2> key on the dedicated keyboard, then press ‘Yes’ button");

            DmiActions.Send_EVC10_MMIEchoedTrainData_FixedDataEntry(this, Variables.paramEvc6FixedTrainsetCaptions);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Train data validation window.");

            /*
            Test Step 6
            Action: Select and confirm ‘No’ button
            Expected Result: DMI displays Main window
            */
            DmiActions.ShowInstruction(this, @"Press and confirm the ‘No’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Main window.");

            /*
            Test Step 7
            Action: Select Train data button
            Expected Result: DMI displays Train data window.Verify that the train type is not changed to the pressed button from step 5
            Test Step Comment: MMI_gen 8865 (partly:Exception 1);
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Train data’ button");


            DmiActions.Send_EVC6_MMICurrentTrainData_FixedDataEntry(this,
                new[] {"FLU", "RLU", "Rescue"},
                1);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Train data window." + Environment.NewLine +
                                "2. Train type is ‘type 1’ and does not change to ‘type 2’ (confirmed in Step 5.");

            /*
            Test Step 8
            Action: Repeat Step 5.Then, select and confirm ‘Yes’ button.
            Expected Result: DMI displays Main window.
            Test Step Comment:
            */
            DmiActions.ShowInstruction(this,
                @"Press the ‘type 2’ key on the dedicated keyboard, then press ‘Yes’ button");

            DmiActions.Send_EVC10_MMIEchoedTrainData_FixedDataEntry(this, Variables.paramEvc6FixedTrainsetCaptions);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Train data validation window.");

            DmiActions.ShowInstruction(this, @"Press and confirm the ‘Yes’ button");

            /*
            Test Step 9
            Action: Select Train data button
            Expected Result: DMI displays Train data window.Verify that the train type is change to pressed button from step 8.
            Test Step Comment:MMI_gen 8863 (partly:Exception);
            */
            DmiActions.ShowInstruction(this, @"Press ‘Train data’ button");

            DmiActions.Send_EVC6_MMICurrentTrainData_FixedDataEntry(this,
                new[] {"FLU", "RLU", "Rescue"},
                2);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays Train data window." + Environment.NewLine +
                                "2. Train type changes to ‘type 2’ (confirmed in Step 8).");

            /*
            Test Step 10
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}