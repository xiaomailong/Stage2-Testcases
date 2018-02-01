using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 27.1.1 Sub-Level Window: General appearances
    /// TC-ID: 22.1.1
    /// 
    /// This test case verifies the selection and display of the Sub-level window on the ‘Default’ window which comply with [ERA-ERTMS] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 2959; MMI_gen 167; MMI_gen 11925; MMI_gen 2956; MMI_gen 11924; MMI_gen 4381; MMI_gen 4382; MMI_gen 9512; MMI_gen 968; MMI_gen 12137; MMI_gen 2955; MMI_gen 2957;
    /// 
    /// Scenario:
    /// Power on DMI without ATP to verify the ‘Settings’ button when DMI has no connection with ATP.Power on ATP together with DMI without cabin activation to verify the ‘Settings’ button when the cabin is not active.Activate the cabin and complete SoM to mode SB, level 1.Close the ‘Main’ window to verify the display of all buttons on the default window by the following actions:Press and hold the buttonSlide out from the buttonSlide back to the buttonRelease the buttonUpdate all languages on DMI to verify the label on all button in different languagePower off ATP to verify the ‘Settings’ button when ATP is down without function Fallback.Activate function Fallback to verify the ‘Settings button when ATP is down with function Fallback.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_ID_22_1_1_Sub_Level_Window : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // N/A

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays is SB, level 1.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint

            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Power on DMI (without ATP)");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,The ‘Main’, ‘Override’, Data View’ and ‘Special’ buttons are invisible.The ‘Settings’ button is visible");
            /*
            Test Step 1
            Action: Power on DMI (without ATP)
            Expected Result: Verify the following information,The ‘Main’, ‘Override’, Data View’ and ‘Special’ buttons are invisible.The ‘Settings’ button is visible
            Test Step Comment: (1) MMI_gen 2959       (partly: state inactive, ‘Main’, ‘Override’, ‘Data View’ and ‘Special’);(2) MMI_gen 2959       (partly: enabled ‘Settings’, state inactive);
            */
            DmiActions.ShowInstruction(this, "Power on DMI (without ATP)");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display the ‘Main’ button." + Environment.NewLine +
                                "2. DMI does not display the ‘Override’ button." + Environment.NewLine +
                                "3. DMI does not display the ‘Data View’ button." + Environment.NewLine +
                                "4. DMI displays the ‘Settings’ button.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Then, power on ATP (without cabin activation)");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,The ‘Main’, ‘Override’, Data View’ and ‘Special’ buttons are invisible.The ‘Settings’ button is visible");
            /*
            Test Step 2
            Action: Then, power on ATP (without cabin activation)
            Expected Result: Verify the following information,The ‘Main’, ‘Override’, Data View’ and ‘Special’ buttons are invisible.The ‘Settings’ button is visible
            Test Step Comment: (1) MMI_gen 2959       (partly: state inactive, ‘Main’, ‘Override’, ‘Data View’ and ‘Special’);(2) MMI_gen 2959       (partly: enabled ‘Settings’, state inactive);
            */
            DmiActions.Start_ATP();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display the ‘Main’ button." + Environment.NewLine +
                                "2. DMI does not display the ‘Override’ button." + Environment.NewLine +
                                "3. DMI does not display the ‘Data View’ button." + Environment.NewLine +
                                "4. DMI displays the ‘Settings’ button.");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Perform the following procedure,Activate Cabin AEnter Driver ID and perform brake testSelect and confirm Level 1");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Main window");
            /*
            Test Step 3
            Action: Perform the following procedure,Activate Cabin AEnter Driver ID and perform brake testSelect and confirm Level 1
            Expected Result: DMI displays Main window
            */
            // Individual steps tested elsewhere...
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.Start | Variables.standardFlags;
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window.");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Close’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays Default window.Verifies the following points,The sub-level window is composed of 5 buttons displayed in area F1-F5.The following buttons are all enabled and labeled in English as follows:The ‘Main’ button in area F1The ‘Override’ button in area F2The ‘Data View’ button in area F3The ‘Special’ button in area F4The ‘Settings’ button displaying with symbol SE04 in area F5");
            /*
            Test Step 4
            Action: Press ‘Close’ button
            Expected Result: DMI displays Default window.Verifies the following points,The sub-level window is composed of 5 buttons displayed in area F1-F5.The following buttons are all enabled and labeled in English as follows:The ‘Main’ button in area F1The ‘Override’ button in area F2The ‘Data View’ button in area F3The ‘Special’ button in area F4The ‘Settings’ button displaying with symbol SE04 in area F5
            Test Step Comment: (1) MMI_gen 167;(2) MMI_gen 11925, MMI_gen 2956, MMI_gen 2959 (partly: state ‘Active’), MMI_gen 11924
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window." + Environment.NewLine +
                                "2. The sub-level window comprises 5 buttons displayed in sub-areas F1-F5." +
                                Environment.NewLine +
                                "3. The ‘Main’ button is displayed enabled in sub-area F1" + Environment.NewLine +
                                "4. The ‘Override’ button is displayed enabled in sub-area F2" + Environment.NewLine +
                                "5. The ‘Data view’ button is displayed enabled in sub-area F3" + Environment.NewLine +
                                "6. The ‘Special’ button is displayed enabled in sub-area F4" + Environment.NewLine +
                                "7. The ‘Settings’ button is displayed enabled with symbol SE04 in sub-area F5");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press and hold ‘Main’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following potins,The ‘Main’ button is shown as pressed state. The sound ‘Click’ played once");
            /*
            Test Step 5
            Action: Press and hold ‘Main’ button
            Expected Result: Verify the following potins,The ‘Main’ button is shown as pressed state. The sound ‘Click’ played once
            Test Step Comment: (1) MMI_gen 2955 (partly: ‘Main’, MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));(2) MMI_gen 2955 (partly: button ‘Main’), MMI_gen 4381 (partly: the sound for Up-Type button)); MMI_gen 9512; MMI_gen 968;
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Main’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Main’ button pressed." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide out of ‘Main’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI still displays the Default window.The ‘Main’ button becomes the ‘Enabled’ state without a sound");
            /*
            Test Step 6
            Action: Slide out of ‘Main’ button
            Expected Result: DMI still displays the Default window.The ‘Main’ button becomes the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 2955 (partly: button ‘Main’), MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Main’ button pressed, drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Main’ button enabled." + Environment.NewLine +
                                "2. No sound is played.");

            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide back into ‘Main’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI still displays the Default window.The Main button is shown as pressed state and no sound ‘Click’ is played");
            /*
            Test Step 7
            Action: Slide back into ‘Main’ button
            Expected Result: DMI still displays the Default window.The Main button is shown as pressed state and no sound ‘Click’ is played
            Test Step Comment: (1) MMI_gen 2955 (partly: button ‘Main’), MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound));
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Main’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Main’ button pressed." + Environment.NewLine +
                                "2. No sound is played.");

            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Release the ‘Main’ button");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,DMI displays Main window");
            /*
            Test Step 8
            Action: Release the ‘Main’ button
            Expected Result: Verify the following information,DMI displays Main window
            Test Step Comment: (1) MMI_gen 2955 (partly: ‘Main’, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)) , MMI_gen 12137 (partly: Sub-level window, main)
            */
            DmiActions.ShowInstruction(this, @"Release the ‘Main’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Main window.");

            TraceHeader("Test Step 9");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Close’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Default window");
            /*
            Test Step 9
            Action: Press ‘Close’ button
            Expected Result: DMI displays Default window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            TraceHeader("Test Step 10");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press and hold ‘Override’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following potins,The ‘Override’ button is shown as pressed state. The sound ‘Click’ played once");
            /*
            Test Step 10
            Action: Press and hold ‘Override’ button
            Expected Result: Verify the following potins,The ‘Override’ button is shown as pressed state. The sound ‘Click’ played once
            Test Step Comment: (1) MMI_gen 2955 (partly: ‘Override’, MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));(2) MMI_gen 2955 (partly: button ‘Override’), MMI_gen 4381 (partly: the sound for Up-Type button)); MMI_gen 9512; MMI_gen 968;
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Override’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Override’ button pressed." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            TraceHeader("Test Step 11");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide out of ‘Override’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI still displays the Default window.The ‘Override’ button becomes the ‘Enabled’ state without a sound");
            /*
            Test Step 11
            Action: Slide out of ‘Override’ button
            Expected Result: DMI still displays the Default window.The ‘Override’ button becomes the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 2955 (partly: button ‘Override’), MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Override’ button pressed, drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Override’ button enabled." + Environment.NewLine +
                                "2. No sound is played.");

            TraceHeader("Test Step 12");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide back into ‘Override’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI still displays the Default window.The Override button is shown as pressed state and no sound ‘Click’ is played");
            /*
            Test Step 12
            Action: Slide back into ‘Override’ button
            Expected Result: DMI still displays the Default window.The Override button is shown as pressed state and no sound ‘Click’ is played
            Test Step Comment: (1) MMI_gen 2955 (partly: button ‘Override’), MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound));
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Override’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Override’ button pressed." + Environment.NewLine +
                                "2. No sound is played.");

            TraceHeader("Test Step 13");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Release the ‘Override’ button");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,DMI displays Override window");
            /*
            Test Step 13
            Action: Release the ‘Override’ button
            Expected Result: Verify the following information,DMI displays Override window
            Test Step Comment: (1) MMI_gen 2955 (partly: ‘Override, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)) , MMI_gen 12137 (partly: Sub-level window, override)
            */
            DmiActions.ShowInstruction(this, @"Release the ‘Override’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Override window.");

            TraceHeader("Test Step 14");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Close’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Default window");
            /*
            Test Step 14
            Action: Press ‘Close’ button
            Expected Result: DMI displays Default window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            TraceHeader("Test Step 15");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press and hold ‘Data view’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following potins,The ‘Data View’ button is shown as pressed state. The sound ‘Click’ played once");
            /*
            Test Step 15
            Action: Press and hold ‘Data view’ button
            Expected Result: Verify the following potins,The ‘Data View’ button is shown as pressed state. The sound ‘Click’ played once
            Test Step Comment: (1) MMI_gen 2955 (partly: ‘Data view’, MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));(2) MMI_gen 2955 (partly: button ‘Data view’), MMI_gen 4381 (partly: the sound for Up-Type button)); MMI_gen 9512; MMI_gen 968;
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Data view’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Data view’ button pressed." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            TraceHeader("Test Step 16");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide out of ‘Data view’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI still displays the Default window.The ‘Data View’ button becomes the ‘Enabled’ state without a sound");
            /*
            Test Step 16
            Action: Slide out of ‘Data view’ button
            Expected Result: DMI still displays the Default window.The ‘Data View’ button becomes the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 2955 (partly: button ‘Data view’), MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Data view’ button pressed, drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Data view’ button enabled." + Environment.NewLine +
                                "2. No sound is played.");

            TraceHeader("Test Step 17");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide back into ‘Data view’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI still displays the Default window.The Data View button is shown as pressed state and no sound ‘Click’ is played");
            /*
            Test Step 17
            Action: Slide back into ‘Data view’ button
            Expected Result: DMI still displays the Default window.The Data View button is shown as pressed state and no sound ‘Click’ is played
            Test Step Comment: (1) MMI_gen 2955 (partly: button ‘Data view), MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound));
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Data view’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Data view’ button pressed." + Environment.NewLine +
                                "2. No sound is played.");

            TraceHeader("Test Step 18");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Release the ‘Data view’ button");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,DMI displays Data View window");
            /*
            Test Step 18
            Action: Release the ‘Data view’ button
            Expected Result: Verify the following information,DMI displays Data View window
            Test Step Comment: (1) MMI_gen 2955 (partly: ‘Data view’, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)) , MMI_gen 12137 (partly: Sub-level window, data view)
            */
            DmiActions.ShowInstruction(this, @"Release the ‘Data view’ button");
            EVC13_MMIDataView.MMI_X_DRIVER_ID = "";
            EVC13_MMIDataView.MMI_NID_OPERATION = 0xffffffff;
            EVC13_MMIDataView.MMI_M_DATA_ENABLE = (Variables.MMI_M_DATA_ENABLE) 0x0080; // 128
            EVC13_MMIDataView.MMI_L_TRAIN = 4096;
            EVC13_MMIDataView.MMI_V_MAXTRAIN = 601;
            EVC13_MMIDataView.MMI_M_BRAKE_PERC = 9;
            EVC13_MMIDataView.MMI_NID_KEY_AXLE_LOAD = Variables.MMI_NID_KEY.FG4; // 20
            EVC13_MMIDataView.MMI_NID_RADIO =
                0xffffffffffffffff; // 4294967295 (= 0xffffffff) hi, 4294967295 (= 0xffffffff) lo
            EVC13_MMIDataView.MMI_M_AIRTIGHT = 3;
            EVC13_MMIDataView.MMI_NID_KEY_LOAD_GAUGE = Variables.MMI_NID_KEY.CATE5;
            EVC13_MMIDataView.Trainset_Caption = "";
            EVC13_MMIDataView.Network_Caption = "";
            EVC13_MMIDataView.MMI_NID_KEY_TRAIN_CAT = Variables.MMI_NID_KEY.CATA; // 21
            EVC13_MMIDataView.Send();
            ;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Data view window.");

            TraceHeader("Test Step 19");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Close’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Default window");
            /*
            Test Step 19
            Action: Press ‘Close’ button
            Expected Result: DMI displays Default window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            TraceHeader("Test Step 20");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press and hold ‘Special’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following potins,The ‘Special’ button is shown as pressed state. The sound ‘Click’ played once");
            /*
            Test Step 20
            Action: Press and hold ‘Special’ button
            Expected Result: Verify the following potins,The ‘Special’ button is shown as pressed state. The sound ‘Click’ played once
            Test Step Comment: (1) MMI_gen 2955 (partly: ‘Special’, MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));(2) MMI_gen 2955 (partly: button ‘Special’), MMI_gen 4381 (partly: the sound for Up-Type button)); MMI_gen 9512; MMI_gen 968;
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Special’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Special’ button pressed." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            TraceHeader("Test Step 21");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide out of ‘Special’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI still displays the Default window.The ‘Special’ button becomes the ‘Enabled’ state without a sound");
            /*
            Test Step 21
            Action: Slide out of ‘Special’ button
            Expected Result: DMI still displays the Default window.The ‘Special’ button becomes the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 2955 (partly: button ‘Special’), MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Special’ button pressed, drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Special’ button enabled." + Environment.NewLine +
                                "2. No sound is played.");

            TraceHeader("Test Step 22");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide back into ‘Special’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI still displays the Default window.The Special button is shown as pressed state and no sound ‘Click’ is played");
            /*
            Test Step 22
            Action: Slide back into ‘Special’ button
            Expected Result: DMI still displays the Default window.The Special button is shown as pressed state and no sound ‘Click’ is played
            Test Step Comment: (1) MMI_gen 2955 (partly: button ‘Special’), MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound));
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Special’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Special’ button pressed." + Environment.NewLine +
                                "2. No sound is played.");

            TraceHeader("Test Step 23");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Release the ‘Special’ button");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,DMI displays Special window");
            /*
            Test Step 23
            Action: Release the ‘Special’ button
            Expected Result: Verify the following information,DMI displays Special window
            Test Step Comment: (1) MMI_gen 2955 (partly: ‘Special’, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)) , MMI_gen 12137 (partly: Sub-level window, special)
            */
            DmiActions.ShowInstruction(this, @"Release the ‘Special’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Special window.");

            TraceHeader("Test Step 24");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Close’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Default window");
            /*
            Test Step 24
            Action: Press ‘Close’ button
            Expected Result: DMI displays Default window
            */
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window.");

            TraceHeader("Test Step 25");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press and hold ‘Setting’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following potins,The ‘Settings’ button is shown as pressed state. The sound ‘Click’ played once");
            /*
            Test Step 25
            Action: Press and hold ‘Setting’ button
            Expected Result: Verify the following potins,The ‘Settings’ button is shown as pressed state. The sound ‘Click’ played once
            Test Step Comment: (1) MMI_gen 2955 (partly: ‘Setting’, MMI_gen 4381 (partly: change to state ‘Pressed’ as long as remain actuated));(2) MMI_gen 2955 (partly: button ‘Setting’), MMI_gen 4381 (partly: the sound for Up-Type button)); MMI_gen 9512; MMI_gen 968;
            */
            DmiActions.ShowInstruction(this, @"Press and hold the ‘Settings’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Settings’ button pressed." + Environment.NewLine +
                                "2. The ‘Click’ sound is played once.");

            TraceHeader("Test Step 26");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide out of ‘Setting’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI still displays the Default window.The ‘Settings’ button becomes the ‘Enabled’ state without a sound");
            /*
            Test Step 26
            Action: Slide out of ‘Setting’ button
            Expected Result: DMI still displays the Default window.The ‘Settings’ button becomes the ‘Enabled’ state without a sound
            Test Step Comment: (1) MMI_gen 2955 (partly: button ‘Setting’), MMI_gen 4382 (partly: state ‘Enabled’ when slide out with force applied, no sound));
            */
            DmiActions.ShowInstruction(this, @"Whilst keeping the ‘Settings’ button pressed, drag outside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the Default window." + Environment.NewLine +
                                "2. DMI displays the ‘Settings’ button enabled." + Environment.NewLine +
                                "3. No sound is played.");

            TraceHeader("Test Step 27");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Slide back into ‘Setting’ button");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI still displays the Default window.The Settings button is shown as pressed state and no sound ‘Click’ is played");
            /*
            Test Step 27
            Action: Slide back into ‘Setting’ button
            Expected Result: DMI still displays the Default window.The Settings button is shown as pressed state and no sound ‘Click’ is played
            Test Step Comment: (1) MMI_gen 2955 (partly: button ‘Setting’), MMI_gen 4382 (partly: state ‘Pressed’ when slide back, no sound));
            */
            DmiActions.ShowInstruction(this,
                @"Whilst keeping the ‘Settings’ button pressed, drag it back inside its area");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI still displays the Default window." + Environment.NewLine +
                                "2. DMI displays the ‘Settings’ button pressed." + Environment.NewLine +
                                "2. No sound is played.");

            TraceHeader("Test Step 28");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Release the ‘Setting’ button");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,DMI displays Settings window");
            /*
            Test Step 28
            Action: Release the ‘Setting’ button
            Expected Result: Verify the following information,DMI displays Settings window
            Test Step Comment: (1) MMI_gen 2955 (partly: ‘Setting’, MMI_gen 4381 (partly: exit state ‘Pressed’, execute function associated to the button)), MMI_gen 12137 (partly: Sub-level window, settings)
            */
            DmiActions.ShowInstruction(this, @"Release the ‘Settings’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window.");

            TraceHeader("Test Step 29");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Language’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Language window");
            /*
            Test Step 29
            Action: Press ‘Language’ button
            Expected Result: DMI displays Language window
            */
            // Enable the button
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Language;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, @"Press the ‘Language’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Language window.");

            TraceHeader("Test Step 30");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Select and confirm ‘Deutsch’ language");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Setting window with changed display refer to selected language");
            /*
            Test Step 30
            Action: Select and confirm ‘Deutsch’ language
            Expected Result: DMI displays Setting window with changed display refer to selected language
            */
            DmiActions.ShowInstruction(this, @"Select and confirm ‘Deutsch’ language");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window with texts displayed in German.");

            TraceHeader("Test Step 31");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Close’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI display Default window.Verify that symbol SE04 still applies for Deutsch");
            /*
            Test Step 31
            Action: Press ‘Close’ button
            Expected Result: DMI display Default window.Verify that symbol SE04 still applies for Deutsch
            Test Step Comment: (1) MMI_gen 2957 (partly: Deutsch);
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window with symbol SE04 in sub-area F5.");

            TraceHeader("Test Step 32");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Press ‘Setting’ button");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Setting window");
            /*
            Test Step 32
            Action: Press ‘Setting’ button
            Expected Result: DMI displays Setting window
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, @"Press the ‘Settings’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window");

            TraceHeader("Test Step 33");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Repeat action setp 30-31 for another language");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,Verify the label of are F5 is presented symbol SE04 independently from the currently active language");
            /*
            Test Step 33
            Action: Repeat action setp 30-31 for another language
            Expected Result: Verify the following information,Verify the label of are F5 is presented symbol SE04 independently from the currently active language
            Test Step Comment: (1) MMI_gen 2957;
            */
            // Repeat Step 30
            DmiActions.ShowInstruction(this, @"Select and confirm a language other than German");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Settings window with texts displayed in the language just selected.");

            // Repeat Step 31
            DmiActions.ShowInstruction(this, @"Press the ‘Close’ button");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the Default window with symbol SE04 in sub-area F5.");

            TraceHeader("Test Step 34");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Power off ATP (without Fallback)");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,The buttons ‘Main’, ‘Override’, Data View’ and ‘Special’ buttons are invisibled in any other state.The ‘Settings’ button is still visible");
            /*
            Test Step 34
            Action: Power off ATP (without Fallback)
            Expected Result: Verify the following information,The buttons ‘Main’, ‘Override’, Data View’ and ‘Special’ buttons are invisibled in any other state.The ‘Settings’ button is still visible
            Test Step Comment: (1) MMI_gen 2959 (partly: They are invisible in any other state, unavailable fallback);(2) MMI_gen 2959 (partly: ‘Settings’, inactive cabin, unavailable fallback)
            */
            // Call generic Check Results Method
            DmiActions.Simulate_communication_loss_EVC_DMI(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display the ‘Main’ button." + Environment.NewLine +
                                "2. DMI does not display the ‘Override’ button." + Environment.NewLine +
                                "3. DMI does not display the ‘Data View’ button." + Environment.NewLine +
                                "4. DMI still displays the ‘Settings’ button.");

            TraceHeader("Test Step 35");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Run the ‘Fallback’ function (Drive the train with speed Fallback)");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,The buttons ‘Main’, ‘Override’, Data View’ and ‘Special’ buttons are invisibled in any other state.The ‘Settings’ button is still visible");
            /*
            Test Step 35
            Action: Run the ‘Fallback’ function (Drive the train with speed Fallback)
            Expected Result: Verify the following information,The buttons ‘Main’, ‘Override’, Data View’ and ‘Special’ buttons are invisibled in any other state.The ‘Settings’ button is still visible
            Test Step Comment: (1) MMI_gen 2959 (partly: They are invisible in any other state, fallback);(2) MMI_gen 2959 (partly: ‘Settings’, inactive cabin, fallback)
            */
            DmiActions.ShowInstruction(this, @"Run the ‘Fallback’ function");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display the ‘Main’ button." + Environment.NewLine +
                                "2. DMI does not display the ‘Override’ button." + Environment.NewLine +
                                "3. DMI does not display the ‘Data View’ button." + Environment.NewLine +
                                "4. DMI still displays the ‘Settings’ button.");

            TraceHeader("Test Step 36");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            
            /*
            Test Step 36
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}