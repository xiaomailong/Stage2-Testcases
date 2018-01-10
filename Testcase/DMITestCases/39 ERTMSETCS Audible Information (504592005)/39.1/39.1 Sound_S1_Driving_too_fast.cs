using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 39.1 Sound S1 - Driving too fast
    /// TC-ID: 36.1
    /// 
    /// This test case verifies that sound ‘S1_toofast.wav’ is played once when current train speed is exceeded permitted speed in CSM and PIM supervision.
    /// 
    /// Tested Requirements:
    /// MMI_gen 12070; MMI_gen 12043 (partly: speed monitoring in FS mode); MMI_gen 9516 (partly: restrictive target changed); MMI_gen 12025 (partly: restrictive target changed);
    /// 
    /// Scenario:
    /// 1. Perform SoM to Level 1 in SR mode.
    /// 2. Drive the train forward with speed at 40 km/h.
    /// 3. Train runs pass BG1 at position 100 m.that contained pkt 12, pkt 21 and pkt 27 to enter FS mode.
    /// 4. Accelerate the train with max acceleration (100% throttle) above permitted speed.
    /// 5. Stop the train.
    /// 6. Use XML script to send EVC-1 to the DMI.
    /// 7. Deactivate cabin A and power off the system.
    /// 8. Power on the system and perform SoM to Level 1 in SR mode.
    /// 9. Drive the train forward with speed at 40 km/h.
    /// 10. Train runs pass BG1 at position 100 m.that contained pkt 12, pkt 21 and pkt 27 to enter FS mode.
    /// 11. Accelerate the train with max acceleration (100% throttle) until speed at 95 km/h.
    /// 12. Wait until train enters PIM supervision and then increase train speed above permitted speed.
    /// 13. Stop the train.
    /// 14. Use XML script to send EVC-1 to the DMI.
    /// 
    /// Used files:
    /// 36_1.tdg, 36_1_a.xml and 36_1_b.xml
    /// </summary>
    public class TC_ID_36_1_Sound_S1_Driving_too_fast : TestcaseBase
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
            // Testcase entrypoint

            /*
            Test Step 1
            Action: Perform SoM to Level 1 in SR mode
            Expected Result: ETCS OB enters SR mode in Level 1
            */
            // tested elsewhere force SoM
            DmiActions.Complete_SoM_L1_SR(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            /*
            Test Step 2
            Action: Drive the train forward with constant speed at 40 km/h
            Expected Result: The train can drive forward and all brakes are not applied
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display the ‘Emergency brake’ symbol, ST01, in sub-area C9.");

            /*
            Test Step 3
            Action: Train runs pass BG1
            Expected Result: ETCS OB enters FS mode in Level 1            
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1.");

            /*
            Test Step 4
            Action: Accelerate the train with max acceleration (100% throttle) above permitted speed. 
            Expected Result: (1) Sound ‘S1_toofast.wav’ is played once when over-speed status in CSM supervision is active  
            Test Step Comment: (1) MMI_gen 12029 (partly: MMI_M_WARNING = 8 with TDG file)(2) MMI_gen 12060 (partly: MMI_M_WARNING = 8 with TDG file)1Note Sound file is stored in DMI_ERTMS_BL3 product in database path:/proj/ccmbkk3/mmi_v.
            */
            EVC1_MMIDynamic.MMI_V_PERMITTED = 60;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 70;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 70;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Overspeed_Status_Ceiling_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI plays the ‘S1_toofast.wav’ sound once." + Environment.NewLine +
                                "2. The permitted speed hook is displayed on the CSG from 60 to 70 km/h in orange." +
                                Environment.NewLine +
                                "3. The CSG is coloured Dark-grey up to the speed hook" + Environment.NewLine +
                                "4. The speed pointer displays 70 km/h and is coloured orange.");

            /*
            Test Step 5
            Action: Stop the train.
            Expected Result: The train is at standstill.
            Test Step Comment:
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed pointer displays 0 km/h.");

            /*
            Test Step 6
            Action: Use test script 36_1_a.xml to send dynamic information via EVC-1 with: MMI_M_WARNING = 8 MMI_V_TRAIN = 2880 MMI_V_PERMITTED = 2777 MMI_V_INTERVENTION = 2929
            Expected Result: Sound ‘S1_toofast.wav’ is played once.
            Test Step Comment: MMI_gen 12060 (partly: MMI_M_WARNING = 8 with XML script)
            */
            XM_36_1(msgType.typeA);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI plays the ‘S1_toofast.wav’ sound once.");
            /*
             * 
            Test Step 7
            Action: Deactivate cabin A and power off the system.
            Expected Result: System is power off and DMI displays ‘No contact with ATP’.
            */
            DmiActions.Deactivate_Cabin(this);

            DmiActions.ShowInstruction(this, "Power off the system");

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘No contact with ATP’");

            /*
            Test Step 8
            Action: Power on the system and perform SoM to Level 1 in SR mode.
            Expected Result: ETCS OB enters SR mode in Level 1.
            */
            DmiActions.ShowInstruction(this,
                "Wait for at least 10s after powering off the system, then power on the system");

            DmiActions.Complete_SoM_L1_SR(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1");

            /*
            Test Step 9
            Action: Drive the train forward with speed at 40 km/h.
            Expected Result: The train can drive forward and all brakes are not applied.
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display the ‘Emergency brake’ symbol, ST01, in sub-area C9.");

            /*
            Test Step 10
            Action: Train runs pass BG1.
            Expected Result: ETCS OB enters FS mode in Level 1.
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1");

            /*
            Test Step 11
            Action: Accelerate the train with max acceleration (100% throttle) until speed at 95 km/h.Wait until train enters PIM supervision and then increase train speed above permitted speed.
            Expected Result: (1) Sound ‘S1_toofast.wav’ is played once when over-speed status in PIM supervision is active as figure below.(2) Use log file to verify that train speed is exceeded permitted supervision limit in PIM when DMI receives EVC-1 with variable [MMI_M_WARNING = 10].
            Test Step Comment: 1) MMI_gen 12029 (partly: MMI_M_WARNING = 10 with TDG file)(2) MMI_gen 12060 (partly: MMI_M_WARNING = 10 with TDG file)
            */
            EVC1_MMIDynamic.MMI_V_PERMITTED = 100;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 80;
            EVC1_MMIDynamic.MMI_V_INTERVENTION_KMH = 102;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 95;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Overspeed_Status_PreIndication_Monitoring;

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 102;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI plays the ‘S1_toofast.wav’ sound once." + Environment.NewLine +
                                "2. The permitted speed hook is displayed on the CSG from 100 to 102 km/h in orange." +
                                Environment.NewLine +
                                "3. The CSG is coloured white up to the speed hook" + Environment.NewLine +
                                "4. The speed pointer displays 102 km/h and is coloured orange.");

            /*
            Test Step 12
            Action: Stop the train.
            Expected Result: The train is at standstill.
            */
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The speed pointer displays 0 km/h.");

            /*
            Test Step 13
            Action: Use test script 36_1_b.xml to send dynamic information via EVC-1 with: MMI_M_WARNING = 10 MMI_V_TRAIN = 2806 MMI_V_PERMITTED = 2687 MMI_V_INTERVENTION = 2882
            Expected Result: Sound ‘S1_toofast.wav’ is played once.
            Test Step Comment: MMI_gen 12060 (partly: MMI_M_WARNING = 10 with XML script)
            */
            XM_36_1(msgType.typeB);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI plays the ‘S1_toofast.wav’ sound once.");

            /*
            Test Step 14
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_36_1_DMI_Test_Specification

        private enum msgType : byte
        {
            typeA,
            typeB
        }

        private void XM_36_1(msgType packetSelector)
        {
            EVC1_MMIDynamic.MMI_A_TRAIN = 100;
            EVC1_MMIDynamic.MMI_V_RELEASE = 1;
            EVC1_MMIDynamic.MMI_M_SLIP = 0;
            EVC1_MMIDynamic.MMI_M_SLIDE = 0;

            switch (packetSelector)
            {
                case msgType.typeA:
                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Overspeed_Status_Ceiling_Speed_Monitoring;
                    EVC1_MMIDynamic.MMI_V_TRAIN = 2800;
                    EVC1_MMIDynamic.MMI_V_TARGET = 1;
                    EVC1_MMIDynamic.MMI_V_PERMITTED = 2777;
                    EVC1_MMIDynamic.MMI_O_BRAKETARGET = 1;
                    EVC1_MMIDynamic.MMI_O_IML = 1000153908;
                    EVC1_MMIDynamic.MMI_V_INTERVENTION = 2929;
                    break;
                case msgType.typeB:
                    EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Overspeed_Status_Ceiling_Speed_Monitoring;
                    EVC1_MMIDynamic.MMI_V_TRAIN = 2806;
                    EVC1_MMIDynamic.MMI_V_TARGET = 0;
                    EVC1_MMIDynamic.MMI_V_PERMITTED = 2687;
                    EVC1_MMIDynamic.MMI_O_BRAKETARGET = 1000310017;
                    EVC1_MMIDynamic.MMI_O_IML = 1000152624;
                    EVC1_MMIDynamic.MMI_V_INTERVENTION = 2882;
                    break;
            }
        }

        #endregion
    }
}