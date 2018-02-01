using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 39.3.2 Restrictive Target with Movement Authority Changed in Full Supervision Mode
    /// TC-ID: 36.3.2
    /// 
    /// This test case verifies that sound ‘S info’ is played once when more restrictive target exits in FS mode with variable [EVC-1.MMI_O_BRAKETARGET] or [EVC-1.MMI_V_TARGET] is changed to lower value.
    /// 
    /// Tested Requirements:
    /// MMI_gen 12043 (partly: MA changed in FS mode);
    /// 
    /// Scenario:
    /// 1.Perform SoM to Level 1 in SR mode.
    /// 2.Drive the train forward with constant speed at 20 km/h.
    /// 3.Train runs pass BG1 at position 100 m. that contained pkt 12, pkt 21 and pkt 27 to enter FS mode.
    /// 4.Train runs pass BG2 at position 200 m. that contained pkt 12, pkt 21 and pkt 27 with reduction of target speed.
    /// 5.Stop the train.
    /// 
    /// Used files:
    /// 36_3_2.tdg
    /// </summary>
    public class TC_ID_36_3_2_Restrictive_Target_with_Movement_Authority_Changed_in_Full_Supervision_Mode : TestcaseBase
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
            TraceInfo("Drive the train forward with constant speed at 20 km/h");
            TraceReport("Expected Result");
            TraceInfo("The train can drive forward and all brakes are not applied");
            /*
            Test Step 2
            Action: Drive the train forward with constant speed at 20 km/h
            Expected Result: The train can drive forward and all brakes are not applied
            */
            EVC1_MMIDynamic.MMI_V_TARGET = -1;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = -1;

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 20;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display the ‘Emergency brake’ symbol, ST01, in sub-area C9.");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Train runs pass BG1");
            TraceReport("Expected Result");
            TraceInfo(
                "Sound Sinfo is played continuously after train runs pass BG1 with verification as follows:-Log FileUse log file to verify that after train pass BG1, train enters FS mode and restrictive target exists all the time when the DMI receives the following:-- EVC-7 with variable [MMI_OBU_TR_M_Mode = 0].- EVC-1 with variable ‘MMI_V_TARGET’ is changed from -1 to 1666 once.- EVC-1 with variable ‘MMI_O_BRAKETARGET’ is changed from -1 to any value and keep changing to lower value all the time");
            /*
            Test Step 3
            Action: Train runs pass BG1
            Expected Result: Sound Sinfo is played continuously after train runs pass BG1 with verification as follows:-Log FileUse log file to verify that after train pass BG1, train enters FS mode and restrictive target exists all the time when the DMI receives the following:-- EVC-7 with variable [MMI_OBU_TR_M_Mode = 0].- EVC-1 with variable ‘MMI_V_TARGET’ is changed from -1 to 1666 once.- EVC-1 with variable ‘MMI_O_BRAKETARGET’ is changed from -1 to any value and keep changing to lower value all the time
            Test Step Comment: MMI_gen 12043 (partly: restrictive target becomes applicable by MA changed)
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 60;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 100000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1." + Environment.NewLine +
                                "2. The ‘Sinfo’ sound is played continously");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Train runs pass BG2");
            TraceReport("Expected Result");
            TraceInfo(
                "Sound Sinfo is still played continuously after train runs pass BG2 with verification as follows:-Log FileUse log file to verify that after train pass BG2, train is still in FS mode and restrictive target still exists all the time when the DMI receives the following:-- EVC-7 with variable [MMI_OBU_TR_M_Mode = 0].- EVC-1 with variable ‘MMI_V_TARGET’ is changed from 1666 to 416 once.- EVC-1 with variable ‘MMI_O_BRAKETARGET’ is changed to lower value and keep changing to lower value all the time");
            /*
            Test Step 4
            Action: Train runs pass BG2
            Expected Result: Sound Sinfo is still played continuously after train runs pass BG2 with verification as follows:-Log FileUse log file to verify that after train pass BG2, train is still in FS mode and restrictive target still exists all the time when the DMI receives the following:-- EVC-7 with variable [MMI_OBU_TR_M_Mode = 0].- EVC-1 with variable ‘MMI_V_TARGET’ is changed from 1666 to 416 once.- EVC-1 with variable ‘MMI_O_BRAKETARGET’ is changed to lower value and keep changing to lower value all the time
            Test Step Comment: MMI_gen 12043 (partly: more restrictive target exists by MA changed)Note When variable [EVC-1. MMI_V_TARGET] is changed from 1666 to 416, variable [EVC-1. MMI_O_BRAKETARGET] is changed to upper value. So at that time sound Sinfo is played because of the changing of variable [EVC-1. MMI_V_TARGET]. 
            */
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 15;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 50000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1." + Environment.NewLine +
                                "2. The ‘Sinfo’ sound is still played continously");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Stop the train");
            TraceReport("Expected Result");
            TraceInfo("The train is at standstill");
            /*
            Test Step 5
            Action: Stop the train
            Expected Result: The train is at standstill
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed pointer displays 0 km/h.");

            TraceHeader("Test Step 6");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            TraceReport("Expected Result");
            TraceInfo("");
            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}