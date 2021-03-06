using System;
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 18.1.6 Distance to Target Digital in RV mode with special value
    /// TC-ID: 13.1.6
    /// 
    /// This test case verifies the presentation of the distance to target digital in RV mode when received the special value.
    /// 
    /// Tested Requirements:
    /// MMI_gen 6777; 
    /// 
    /// Scenario:
    /// 1. Drive the train forward pass BG1 at position 50 m.  BG1: packet 12, 21 and 27 (Entering FS)
    /// 2. Drive the train forward pass BG2 at position 200 m. BG2 packet138: D_STARTREVERSE 100, L_REVERSEAREA 400 packet 139: D_REVERSE 32767, V_REVERSE 30      
    /// 3. Stop the train at position 700 m.
    /// 4. Select and confirm reversing mode.
    /// 5. Drive the train backward and verify the display of distance to target on DMI.
    /// 
    /// Used files:
    /// 13_1_6.tdg
    /// </summary>
    public class TC_ID_13_1_6_Brake : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 21934;
            // Testcase entrypoint
            StartUp();

            MakeTestStepHeader(1, UniqueIdentifier++,
                "Drive the train forward passing BG1 with speed = 40 km/h until entering FS mode", "");

            /*
            Test Step 1
            Action: Drive the train forward passing BG1 with speed = 40 km/h until entering FS mode
            Expected Result: 
            */
            DmiActions.Complete_SoM_L1_FS(this);

            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 300000; // 3 km

            MakeTestStepHeader(2, UniqueIdentifier++, "Continue drive the train forward passing BG2", "");

            /*
            Test Step 2
            Action: Continue drive the train forward passing BG2
            Expected Result: 
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 20000;

            MakeTestStepHeader(3, UniqueIdentifier++, "The train is in reversing area", "");

            /*
            Test Step 3
            Action: The train is in reversing area
            Expected Result: 
            */

            MakeTestStepHeader(4, UniqueIdentifier++, "Stop the train",
                "The train is standstill.Driver is informed that reversing is possible");
            /*
            Test Step 4
            Action: Stop the train
            Expected Result: The train is standstill. Driver is informed that reversing is possible
            */
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.AuxiliaryInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 286; // Reversing possible
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI indicates speed = 0 km/h" + Environment.NewLine +
                                "2. DMI displays message that reversing is possible and displays symbol ST06 in sub-area C6");

            MakeTestStepHeader(5, UniqueIdentifier++,
                "Change the direction of train to reverse. Then select and confirm RV mode",
                "DMI displays in RV mode, level 1. Verify the following information," +
                "(1) Use the log file to confirm that DMI received packet EVC-1 with variable MMI_O_BRAKETARGET = 2147483647" +
                "(2) The symbol infinity '∞' is displayed for distance to target digital in sub-area A2." +
                "(3) The symbol is be horizontally and vertically centered in Sub-Area A2");
            /*
            Test Step 5
            Action: Change the direction of train to reverse. Then select and confirm RV mode
            Expected Result: DMI displays in RV mode, level 1.Verify the following information,
            (1)    Use the log file to confirm that DMI received packet EVC-1 with variable MMI_O_BRAKETARGET = 2147483647
            (2)    The symbol infinity '∞' is displayed for distance to target digital in sub-area A2.
            (3)    The symbol is be horizontally and vertically centered in Sub-Area A2
            Test Step Comment:
            (1) MMI_gen 6777  (partly: receive MMI_O_BRAKETARGET equal special value);
            (2) MMI_gen 6777 (partly: when MMI_O_BRAKETARGET  equal special value);
            (3) MMI_gen 6777 (partly: horizontally and vertically centered);
            */
            EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 262;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.Send();

            DmiActions.ShowInstruction(this, "Confirm RV mode.");

            EVC152_MMIDriverAction.Check_MMI_M_DRIVER_ACTION =
                EVC152_MMIDriverAction.MMI_M_DRIVER_ACTION.ReversingModeAck;

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Reversing;

            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 2147483647;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in RV mode, level 1." + Environment.NewLine +
                                "2. The infinity symbol ‘∞’ is displayed for digital distance to target in sub-area A2, horizontally and vertically centered.");

            TraceHeader("End of test");

            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}