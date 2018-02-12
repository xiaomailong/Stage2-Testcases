using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 38.1 ERTMS/ETCS System Status Messages
    /// TC-ID: 35.1
    /// 
    /// This test case verifies the display of system status messages refer to recevied packet information EVC-8 which comply with [MMI-ETCS-gen], system status messages are displayed as text messages not to be acknowledged.
    /// 
    /// Tested Requirements:
    /// MMI_gen 9520 (partly: Table 76); MMI_gen 9522 (partly: Table 76);
    /// 
    /// Scenario:
    /// Drive the train forward. Then, verify the system status message refer to received packet information EVC-8 which compliedUse the test script file to send EVC-
    /// 8.Then, verifies the display information.
    /// 
    /// Used files:
    /// 35_1.xml
    /// </summary>
    public class TC_ID_35_1_ERTMSETCS_System_Status_Messages : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 26782;
            // Testcase entrypoint

            StartUp();
            // System is power onSoM is performed until Level 1 is confirmed and the ‘Main’ window is closed.
            DmiActions.Complete_SoM_L1_SB(this);

            MakeTestStepHeader(1, UniqueIdentifier++,
                "Force the train roll away by moving of speed with ‘Neutral’ direction",
                "Verify the following information,DMI displays system message “Runaway movement” in sub-area E5 without yellow flashing frame.Use the log file to confirm that DMI received the EVC-8 with [MMI_DRIVER_MESSAGE (EVC-8).MMI_Q_TEXT] = 269");
            /*
            Test Step 1
            Action: Force the train roll away by moving of speed with ‘Neutral’ direction
            Expected Result: Verify the following information,DMI displays system message “Runaway movement” in sub-area E5 without yellow flashing frame.Use the log file to confirm that DMI received the EVC-8 with [MMI_DRIVER_MESSAGE (EVC-8).MMI_Q_TEXT] = 269
            Test Step Comment: (1) MMI_gen 9520 (partly: Table 76);(2) MMI_gen 9522 (partly: Table 76);
            */
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 269;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Runaway movement’ in sub-area E5 without a yellow flashing frame.");

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Use the test script file 35_1.xml to send multiple packets EVC-8 with the following value,Common variableMMI_Q_TEXT_CLASS = 1MMI_Q_TEXT_CRITERIA =3The order of MMI_Q_TEXT value in each packetMMI_Q_TEXT = 267MMI_Q_TEXT = 560MMI_Q_TEXT = 268MMI_Q_TEXT = 274MMI_Q_TEXT = 275MMI_Q_TEXT = 290MMI_Q_TEXT = 292MMI_Q_TEXT = 296MMI_Q_TEXT = 310MMI_Q_TEXT = 299MMI_Q_TEXT = 273MMI_Q_TEXT = 300MMI_Q_TEXT = 315MMI_Q_TEXT = 606MMI_Q_TEXT = 316MMI_Q_TEXT = 280MMI_Q_TEXT = 320MMI_Q_TEXT = 572MMI_Q_TEXT = 701MMI_Q_TEXT = 702MMI_Q_TEXT = 703MMI_Q_TEXT = 569",
                "Verify the display message in sub-area E5-E9 are correct refer to following message respectively,Balise read errorTrackside malfunctionCommunication errorEntering FSEntering OSSH refusedSH request failedTrackside not compatibleTrain data changedTrain is rejectedUnauthorized passing of EOA / LOANo MA received at level transitionSR distance exceededSH stop orderSR stop orderEmergency stopRV distance exceededNo Track DescriptionRoute unsuitable – axle load categoryRoute unsuitable – loading gaugeRoute unsuitable – traction systemRadio network registration failedNote: Use the <Up> and <Down> button to scroll the list of message in sub-area E5-E9");
            /*
            Test Step 2
            Action: Use the test script file 35_1.xml to send multiple packets EVC-8 with the following value,Common variableMMI_Q_TEXT_CLASS = 1MMI_Q_TEXT_CRITERIA =3The order of MMI_Q_TEXT value in each packetMMI_Q_TEXT = 267MMI_Q_TEXT = 560MMI_Q_TEXT = 268MMI_Q_TEXT = 274MMI_Q_TEXT = 275MMI_Q_TEXT = 290MMI_Q_TEXT = 292MMI_Q_TEXT = 296MMI_Q_TEXT = 310MMI_Q_TEXT = 299MMI_Q_TEXT = 273MMI_Q_TEXT = 300MMI_Q_TEXT = 315MMI_Q_TEXT = 606MMI_Q_TEXT = 316MMI_Q_TEXT = 280MMI_Q_TEXT = 320MMI_Q_TEXT = 572MMI_Q_TEXT = 701MMI_Q_TEXT = 702MMI_Q_TEXT = 703MMI_Q_TEXT = 569
            Expected Result: Verify the display message in sub-area E5-E9 are correct refer to following message respectively,Balise read errorTrackside malfunctionCommunication errorEntering FSEntering OSSH refusedSH request failedTrackside not compatibleTrain data changedTrain is rejectedUnauthorized passing of EOA / LOANo MA received at level transitionSR distance exceededSH stop orderSR stop orderEmergency stopRV distance exceededNo Track DescriptionRoute unsuitable – axle load categoryRoute unsuitable – loading gaugeRoute unsuitable – traction systemRadio network registration failedNote: Use the <Up> and <Down> button to scroll the list of message in sub-area E5-E9
            Test Step Comment: MMI_gen 9522 (partly: Table 76);
            */

            #region Send_XML_35_1_DMI_Test_Specification

            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 267;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Balise read error’ in sub-area E5-E9 without a yellow flashing frame.");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 2;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 560;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Trackside malfunction’ in sub-area E5-E9 without a yellow flashing frame.");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 3;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 268;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Communication error’ in sub-area E5-E9 without a yellow flashing frame.");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 4;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 274;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Entering FS’ in sub-area E5-E9 without a yellow flashing frame.");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 5;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 275;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Entering OS’ in sub-area E5-E9 without a yellow flashing frame.");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 6;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 290;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘SH refused’ in sub-area E5-E9 without a yellow flashing frame.");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 7;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 292;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘SH request failed’ in sub-area E5-E9 without a yellow flashing frame.");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 8;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 296;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Trackside not compatible’ in sub-area E5-E9 without a yellow flashing frame.");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 9;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 310;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Train data changed’ in sub-area E5-E9 without a yellow flashing frame.");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 10;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 299;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Train is rejected’ in sub-area E5-E9 without a yellow flashing frame.");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 11;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 273;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Unauthorized passing of EOA / LOA’ in sub-area E5-E9 without a yellow flashing frame.");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 12;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 300;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘No MA received at level transition’ in sub-area E5-E9 without a yellow flashing frame.");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 13;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 315;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘SR distance exceeded’ in sub-area E5-E9 without a yellow flashing frame.");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 14;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 606;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘SH stop order’ in sub-area E5-E9 without a yellow flashing frame.");
            EVC8_MMIDriverMessage.MMI_I_TEXT = 15;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 316;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘SR stop order’ in sub-area E5-E9 without a yellow flashing frame.");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 16;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 280;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Emergency stop’ in sub-area E5-E9 without a yellow flashing frame.");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 17;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 320;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘RV distance exceeded’ in sub-area E5-E9 without a yellow flashing frame.");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 18;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 572;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘No Track Description’ in sub-area E5-E9 without a yellow flashing frame.");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 19;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 701;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Route unsuitable – axle load category’ in sub-area E5-E9 without a yellow flashing frame.");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 20;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 702;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Route unsuitable – loading gauge’ in sub-area E5-E9 without a yellow flashing frame.");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 21;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 703;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Route unsuitable – traction system’ in sub-area E5-E9 without a yellow flashing frame.");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 22;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 569;
            EVC8_MMIDriverMessage.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘	Radio network registration failed’ in sub-area E5-E9 without a yellow flashing frame.");

            #endregion

            TraceHeader("End of test");

            /*
            Test Step 3
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}