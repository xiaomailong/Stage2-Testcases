using System;
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 23.3 Reversing Allowance: Sub-Area C6
    /// TC-ID: 18.3
    /// 
    /// This test case verifies the display of ‘Reversing Allowance’ on DMI.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7485; MMI_gen 11470 (partly: Bit # 5);
    /// 
    /// Scenario:
    /// 1.Activate Cabin A and perform SoM to SR mode Level 1.
    /// 2.Drive train forward    Pass BG1 at 100m:    DMI changes from SR mode to FS mode.         Packet 12: L_ENDSECTION = 3000 m         packet 21: G_A = 0         packet 27: V_STATIC =  160 km/h     Pass BG2 at 200m:         packet 138: D_STARTREVERSE = 0 m                             L_REVERSEAREA = 200 m         Packet 139: D_REVERSE = 200 m                             V_REVERSE = 20 km/h
    /// 3.Stop the train. Then, verify the display information.
    /// 4.Change train direction to ‘Reverse’. Then, verify the display information.
    /// 
    /// Used files:
    /// 18_3.tdg
    /// </summary>
    public class TC_ID_18_3_Reversing_Allowance_Sub_Area_C6 : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();

            // Test system is powered on.Activate Cabin A.SoM is completed in SR mode, Level 1.
            DmiActions.Complete_SoM_L1_SR(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in RV mode, level 1.

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
            TraceInfo("Drive the train forward passing BG1");
            TraceReport("Expected Result");
            TraceInfo("DMI changes from SR mode to FS mode, Level 1");
            /*
            Test Step 1
            Action: Drive the train forward passing BG1
            Expected Result: DMI changes from SR mode to FS mode, Level 1
            */
            // Put in slight pause so the mode change can be observed.
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 10;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 0;

            Wait_Realtime(3000);
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 100;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 3000;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI changes from SR mode to FS mode,  Level 1");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Drive the train forward passing BG2.Then, stop the train");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,Use the log file to confirm that DMI receives packet information [MMI_DRIVER_MESSAGE (EVC-8)] with variable [MMI_DRIVER_MESSAGE (EVC-8)].MMI_Q_TEXT = 286The symbol ST06 is displayed in sub-area C6");
            /*
            Test Step 2
            Action: Drive the train forward passing BG2.Then, stop the train
            Expected Result: Verify the following information,Use the log file to confirm that DMI receives packet information [MMI_DRIVER_MESSAGE (EVC-8)] with variable [MMI_DRIVER_MESSAGE (EVC-8)].MMI_Q_TEXT = 286The symbol ST06 is displayed in sub-area C6
            Test Step Comment: (1) MMI_gen 7485 (partly: received packet EVC-8);(2) MMI_gen 7485        (partly: ST06);
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 200;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 286;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ‘Reversing permitted’ symbol, ST06, in sub-area C6");
            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Change the train direction to ‘Reverse’");
            TraceReport("Expected Result");
            TraceInfo("An acknowledgement of ‘Reversing’ mode is displayed in sub-area C1");
            /*
            Test Step 3
            Action: Change the train direction to ‘Reverse’
            Expected Result: An acknowledgement of ‘Reversing’ mode is displayed in sub-area C1
            */

            DmiActions.ShowInstruction(this, "Change the train direction to reverse");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 262;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays symbol M015, in sub-area C1");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Acknowledges ‘Reversing’ mode by pressing at area C1");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI displays in RV mode.The symbol MO14 is displayed in sub-area B7.Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 5 (Ack of Reversing mode)");
            /*
            Test Step 4
            Action: Acknowledges ‘Reversing’ mode by pressing at area C1
            Expected Result: DMI displays in RV mode.The symbol MO14 is displayed in sub-area B7.Use the log file to confirm that DMI sends out packet [MMI_DRIVER_ACTION (EVC-152)] with the value of variable MMI_M_DRIVER_ACTION refer to sequence below,a)   MMI_M_DRIVER_ACTION = 5 (Ack of Reversing mode)
            Test Step Comment: MMI_gen 11470 (partly: Bit # 5);
            */
            DmiActions.ShowInstruction(this, @"Acknowledge ‘Reversing’ mode by pressing in area C1");

            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION =
                EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.ReversingModeAck;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Reversing;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in RV mode" + Environment.NewLine + Environment.NewLine +
                                "2. DMI displays symbol M014 in sub-area B7");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            
            /*
            Test Step 5
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}