using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.6.2 Basic Speed Hook(s): Speed hooks are outside Speed Dial’s range
    /// TC-ID: 12.6.2
    /// 
    /// This test case verifies the appearance of the Basic Speed Hook(s) in OS mode (for supervision is not CSM) when permitted speed and target speed are outside the range of speed dial, the Basic Speed Hook(s) are not display.
    /// 
    /// Tested Requirements:
    /// MMI_gen 6331;
    /// 
    /// Scenario:
    /// 1.Drive the train forward pass BG1 at position 100m. Then, verify the display information of all basic speed hooks refer to received packet information EVC1 and EVC-7.BG1: packet 12, 21, 27 and 80 (Entering OS)
    /// 2.Use the test script file to send EVC-1 with permitted speed and target speed is outside range of speed dial. Then, verify that Basic Speed Hook(s) are not display.
    /// 
    /// Used files:
    /// 12_6_2.tdg, 12_6_2_a.xml, 12_6_2_b.xml
    /// </summary>
    public class TC_12_6_2_Train_Speed : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 21707;
            // Testcase entrypoint
            StartUp();

            DmiActions.Complete_SoM_L1_SR(this);

            MakeTestStepHeader(1, UniqueIdentifier++,
                "Driver drives the train forward passing BG1.Then, stop the train and acknowledge OS mode by pressing area C1",
                "DMI displays in OS mode, Level 1. Verify the following information," +
                "(1) Use the log file to confirm that DMI received packet information EVC-1 with following variables, MMI_V_PERMITTED = 4166 (150 km/h) MMI_V_TARGET = 4027 (145 km/h)" +
                "(2) All basic speed hooks are not displays in sub-area B2");
            /*
            Test Step 1
            Action: Driver drives the train forward passing BG1.Then, stop the train and acknowledge OS mode by pressing area C1
            Expected Result: DMI displays in OS mode, Level 1.Verify the following information,
            (1) Use the log file to confirm that DMI received packet information EVC-1 with following variables, MMI_V_PERMITTED = 4166 (150 km/h) MMI_V_TARGET = 4027 (145 km/h)
            (2) All basic speed hooks are not displays in sub-area B2
            Test Step Comment: (1) MMI_gen 6331 (partly: outside the Speed Dial’s maximum speed);(2) MMI_gen 6331 (partly: not to be shown);
            */

            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 300;  // Outside the DMI speed dial range
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 250;     // Outside the DMI speed dial range
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 259;
            EVC8_MMIDriverMessage.Send();

            DmiActions.ShowInstruction(this, "Acknowledge OS mode by pressing in area C1");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.OnSight;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in OS mode, Level 1." + Environment.NewLine +
                                "2. No basic speed hooks are displayed in sub-area B2.");

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Use the test script file 12_6_2_a.xml to send EVC-1 with, MMI_V_TARGET = 65535 MMI_V_PERMITTED = 0",
                "Verify the following information," +
                "(1) There is only white basic speed hook displays at 0 km/h");
            /*
            Test Step 2
            Action: Use the test script file 12_6_2_a.xml to send EVC-1 with,MMI_V_TARGET = 65535MMI_V_PERMITTED = 0
            Expected Result: Verify the following information,(1)   There is only white basic speed hook displays at 0 km/h
            Test Step Comment: (1) MMI_gen 6331 (partly: Target speed is outside the speed range determined by zero and the Speed Dial's maximum Speed, not to be shown);
            */
            // These tests use speed values [65535] outside the range of short
            XML_12_6_2(msgType.typea);

            WaitForVerification(
                "Acknowledgement of OS mode is requested. Press button to accept and then check the following:" +
                Environment.NewLine + Environment.NewLine +
                "1. Only a white basic speed hook is displayed at 0 km/h.");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Use the test script file 12_6_2_b.xml to send EVC-1 with, MMI_V_TARGET = 0 MMI_V_PERMITTED = 65535",
                "Verify the following information,(1)   There is only medium grey basic speed hook displays at 0 km/h");
            /*
            Test Step 3
            Action: Use the test script file 12_6_2_b.xml to send EVC-1 with,MMI_V_TARGET = 0MMI_V_PERMITTED = 65535
            Expected Result: Verify the following information,(1)   There is only medium grey basic speed hook displays at 0 km/h
            Test Step Comment: (1) MMI_gen 6331 (partly: Permitted speed is outside the speed range determined by zero and the Speed Dial's maximum Speed, not to be shown);
            */
            XML_12_6_2(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Only a medium-grey basic speed hook is displays at 0 km/h.");

            TraceHeader("End of test");

            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_12_6_2_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb
        }

        private void XML_12_6_2(msgType type)
        {
            EVC1_MMIDynamic.MMI_M_SLIDE = 0;
            EVC1_MMIDynamic.MMI_M_SLIP = 0;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring; // 1
            EVC1_MMIDynamic.MMI_A_TRAIN = 0;
            EVC1_MMIDynamic.MMI_V_TRAIN = 100;
            EVC1_MMIDynamic.MMI_V_RELEASE = 555;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 10002000;
            EVC1_MMIDynamic.MMI_O_IML = 0;
            EVC1_MMIDynamic.MMI_V_INTERVENTION = 0;

            EVC1_MMIDynamic.SetValidityBits(false);

            switch (type)
            {
                case msgType.typea:
                    // unsigned short value in xml is 65535 => -1 short : breaks in EVC1..Send(); using high value instead
                    EVC1_MMIDynamic.MMI_V_TARGET = 11112;
                    EVC1_MMIDynamic.MMI_V_PERMITTED = 0;
                    break;

                case msgType.typeb:
                    // unsigned short value in xml is 65535 => -1 short : breaks in EVC1..Send(); using high value instead 
                    EVC1_MMIDynamic.MMI_V_PERMITTED = 11112;
                    EVC1_MMIDynamic.MMI_V_TARGET = 0;
                    break;
            }
        }

        #endregion
    }
}