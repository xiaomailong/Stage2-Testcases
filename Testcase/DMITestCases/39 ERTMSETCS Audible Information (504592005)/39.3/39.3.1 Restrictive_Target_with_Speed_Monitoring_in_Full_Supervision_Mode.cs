using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 39.3.1 Restrictive Target with Speed Monitoring in Full Supervision Mode
    /// TC-ID: 36.3.1
    /// 
    /// This test case verifies that sound ‘S info’ is played once when restrictive target becomes applicable in FS mode.
    /// 
    /// Tested Requirements:
    /// MMI_gen 12070; MMI_gen 12043 (partly: speed monitoring in FS mode); MMI_gen 9516 (partly: restrictive target changed); MMI_gen 12025 (partly: restrictive target changed);
    /// 
    /// Scenario:
    /// 1.Perform SoM to Level 1 in SR mode.
    /// 2.Drive the train forward with constant speed at 40 km/h.
    /// 3.Train runs pass BG1 at position 100 m. that contained pkt 12, pkt 21 and pkt 27 to enter FS mode.
    /// 4.Stop the train.
    /// 
    /// Used files:
    /// 36_3_1.tdg
    /// </summary>
    public class TC_ID_36_3_1_Restrictive_Target_with_Speed_Monitoring_in_Full_Supervision_Mode : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // System is power on.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, Level 1.

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
            TraceInfo("Perform SoM to Level 1 in SR mode");
            TraceReport("Expected Result");
            TraceInfo("ETCS OB enters SR mode in Level 1");
            /*
            Test Step 1
            Action: Perform SoM to Level 1 in SR mode
            Expected Result: ETCS OB enters SR mode in Level 1
            */
            // Steps tested elsewhere: force SoM
            DmiActions.Complete_SoM_L1_SR(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Drive the train forward with constant speed at 40 km/h");
            TraceReport("Expected Result");
            TraceInfo("The train can drive forward and all brakes are not applied");
            /*
            Test Step 2
            Action: Drive the train forward with constant speed at 40 km/h
            Expected Result: The train can drive forward and all brakes are not applied
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display the ‘Emergency brake’ symbol, ST01, in sub-area C9.");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Train runs pass BG1");
            TraceReport("Expected Result");
            TraceInfo("ETCS OB enters FS mode in Level 1");
            /*
            Test Step 3
            Action: Train runs pass BG1
            Expected Result: ETCS OB enters FS mode in Level 1
            Test Step Comment: Note Sound ‘Sinfo’ is played once because new text message is displayed, refers to MMI_gen 11455
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1." + Environment.NewLine +
                                "2. The ‘Sinfo’ sound is played once");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Continue to drive the train forward and wait until train enters PIM supervision");
            TraceReport("Expected Result");
            TraceInfo(
                "(1) Sound Sinfo is played once when train enters PIM in FS mode with verification as follows:-DMI Appearance- The DMI displays white colour on CSG from Vtarget to Vperm.Log FileUse log file to verify that the DMI receives EVC-1 with variable [MMI_M_WARNING = 2].(2) Use log file to verify that restrictive target becomes applicable when DMI enters PIM in FS mode by verification below:-- In packet EVC-1 that variable ‘MMI_M_WARNING’ is changed to 2, variable ‘MMI_O_BRAKETARGET’ and ‘MMI_V_TARGET’ are changed value from -1 to any value");
            /*
            Test Step 4
            Action: Continue to drive the train forward and wait until train enters PIM supervision
            Expected Result: (1) Sound Sinfo is played once when train enters PIM in FS mode with verification as follows:-DMI Appearance- The DMI displays white colour on CSG from Vtarget to Vperm.Log FileUse log file to verify that the DMI receives EVC-1 with variable [MMI_M_WARNING = 2].(2) Use log file to verify that restrictive target becomes applicable when DMI enters PIM in FS mode by verification below:-- In packet EVC-1 that variable ‘MMI_M_WARNING’ is changed to 2, variable ‘MMI_O_BRAKETARGET’ and ‘MMI_V_TARGET’ are changed value from -1 to any value
            Test Step Comment: (1) MMI_gen 12070; MMI_gen 12043 (partly: speed monitoring in FS mode); MMI_gen 9516 (partly: restrictive target changed); MMI_gen 12025 (partly: restrictive target changed);(2) MMI_gen 12043 (partly: restrictive target becomes applicable by speed monitoring);Note DMI appearance refers to Table 33: Conditions for display and colour of the CSG in [MMI-ETCS-gen]. 
            */
            EVC1_MMIDynamic.MMI_V_PERMITTED = 100;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 50;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 200000;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 15000;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_PreIndication_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI plays the ‘Sinfo’ sound once." + Environment.NewLine +
                                "2. The permitted speed hook is displayed on the CSG from 60 to 70 km/h in orange." +
                                Environment.NewLine +
                                "3. The CSG is coloured white from the target speed (50 km/h) up to the permitted speed (100 km/h)" +
                                Environment.NewLine +
                                "4. The speed pointer displays 40 km/h and is coloured white.");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Continue to drive the train forward and wait until train enters TSM");
            TraceReport("Expected Result");
            TraceInfo(
                "(1) Sound Sinfo is not played when train enters TSM in FS mode with verification as follows:-- Use log file to verify that the DMI receives EVC-1 with variable [MMI_M_WARNING = 11].(2) Use log file to verify that restrictive target doesn’t exist when DMI enters TSM in FS mode by verification below:-- In packet EVC-1 that variable ‘MMI_M_WARNING’ is changed to 11, variable ‘MMI_O_BRAKETARGET’ and ‘MMI_V_TARGET’ are not changed value");
            /*
            Test Step 5
            Action: Continue to drive the train forward and wait until train enters TSM
            Expected Result: (1) Sound Sinfo is not played when train enters TSM in FS mode with verification as follows:-- Use log file to verify that the DMI receives EVC-1 with variable [MMI_M_WARNING = 11].(2) Use log file to verify that restrictive target doesn’t exist when DMI enters TSM in FS mode by verification below:-- In packet EVC-1 that variable ‘MMI_M_WARNING’ is changed to 11, variable ‘MMI_O_BRAKETARGET’ and ‘MMI_V_TARGET’ are not changed value
            Test Step Comment: (1) MMI_gen 12070 (partly: negative test, no more restrictive target exists);(2) MMI_gen 12043 (partly: negative test no more restrictive target exists);Note DMI appearance refers to Table 33: Conditions for display and colour of the CSG in [MMI-ETCS-gen].
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Target_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Sinfo’ sound is not played (again).");

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Continue to drive the train forward and wait until train enters RSM");
            TraceReport("Expected Result");
            TraceInfo(
                "(1) Sound Sinfo is not played when train enters RSM in FS mode with verification as follows:-- Use log file to verify that the DMI receives EVC-1 with variable [MMI_M_WARNING = 3].(2) Use log file to verify that restrictive target doesn’t exist when DMI enters RSM by verification below:-- In packet EVC-1 that variable ‘MMI_M_WARNING’ is changed to 3, variable ‘MMI_O_BRAKETARGET’ and ‘MMI_V_TARGET’ are not changed value");
            /*
            Test Step 6
            Action: Continue to drive the train forward and wait until train enters RSM
            Expected Result: (1) Sound Sinfo is not played when train enters RSM in FS mode with verification as follows:-- Use log file to verify that the DMI receives EVC-1 with variable [MMI_M_WARNING = 3].(2) Use log file to verify that restrictive target doesn’t exist when DMI enters RSM by verification below:-- In packet EVC-1 that variable ‘MMI_M_WARNING’ is changed to 3, variable ‘MMI_O_BRAKETARGET’ and ‘MMI_V_TARGET’ are not changed value
            Test Step Comment: (1) MMI_gen 12070 (partly: negative test, no more restrictive target exists);(2) MMI_gen 12043 (partly: negative test, no more restrictive target exists);Note DMI appearance refers to Table 33: Conditions for display and colour of the CSG in [MMI-ETCS-gen]. 
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Release_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The ‘Sinfo’ sound is not played (again).");

            TraceHeader("Test Step 7");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Stop the train");
            TraceReport("Expected Result");
            TraceInfo("The train is at standstill");
            /*
            Test Step 7
            Action: Stop the train
            Expected Result: The train is at standstill
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed pointer displays 0 km/h.");

            TraceHeader("Test Step 8");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            
            /*
            Test Step 8
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}