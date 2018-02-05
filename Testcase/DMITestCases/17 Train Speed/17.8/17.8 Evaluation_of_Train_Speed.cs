using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.8 Evaluation of Train Speed
    /// TC-ID: 12.8
    /// 
    /// This test case verifies a display information of specified objects (e.g. Speed pointer, Speed digital, CSG, CSG-Extension, all hooks, Ditance to target bar and digital) refer to received packet EVC-1.
    /// 
    /// Tested Requirements:
    /// MMI_gen 1268; MMI_gen 1275; MMI_gen 1086; MMI_gen 1277;
    /// 
    /// Scenario:
    /// Perform the test scenarios below, and verify the display of all speed objects which are corresponded to the received packet EVC-1 with each scenario.    
    /// 1.Start ATP.    
    /// 2.Activate cabin and perform SoM in SR mode, level 
    /// 1.
    /// 3.Drive the train forward pass BG1 at position 100m.    
    /// 4.Use the test script file to send a special value of EVC-1.
    /// 
    /// Used files:
    /// 12_8.tdg, 12_8_a.xml
    /// </summary>
    public class TC_12_8_Train_Speed : TestcaseBase
    {

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint

            StartUp();

            MakeTestStepHeader(1, UniqueIdentifier++, "Start ATP without cabin activation",
                "Verify the following information,(1)    Use the log file to confirm that DMI receives packet EVC-1 with variable MMI_V_TRAIN = -1.(2)   The following objects are not displayed on the DMI,Speed PointerSpeed DigitalCSGCSG-ExtensionAll hooksTarget Distance Bar");
            /*
            Test Step 1
            Action: Start ATP without cabin activation
            Expected Result: Verify the following information,(1)    Use the log file to confirm that DMI receives packet EVC-1 with variable MMI_V_TRAIN = -1.(2)   The following objects are not displayed on the DMI,Speed PointerSpeed DigitalCSGCSG-ExtensionAll hooksTarget Distance Bar
            Test Step Comment: (1) MMI_gen 1086 (partly: received MMI_V_TRAIN equal -1); MMI_gen 1268 (partly: received MMI_V_TRAIN equal -1); MMI_gen 1275 (partly: received invalid MMI_V_TRAIN);(2) MMI_gen 1086 (partly: when MMI_V_TRAIN equal -1);  MMI_gen 1268 (partly: when MMI_DYNAMIC not elder than 600ms and MMI_V_TRAIN equal -1); MMI_gen 1275 (partly: when MMI_V_TRAIN is invalid); 
            */
            EVC1_MMIDynamic.MMI_V_TRAIN = -1;

            WaitForVerification("Check that the following objects are not displayed on the DMI:" + Environment.NewLine +
                                Environment.NewLine +
                                "1. Speed Pointer." + Environment.NewLine +
                                "2. Speed Digital" + Environment.NewLine +
                                "3. CSG" + Environment.NewLine +
                                "4. CSG - Extension" + Environment.NewLine +
                                "5. All hooks" + Environment.NewLine +
                                "6. Target Distance Bar");

            MakeTestStepHeader(2, UniqueIdentifier++, "Activate cabin A and perform SoM in SR mode, Level 1",
                "Verify the following information,(1)   Use the log file to confirm that DMI receives packet EVC-1 with variable MMI_V_TRAIN = 0.(2)    The Speed pointer, Speed digital, CSG, CSG-Extension, all hooks, Target Distance Bar and Target Distance Digital are diplayed and correspond to the  received packet EVC-1");
            /*
            Test Step 2
            Action: Activate cabin A and perform SoM in SR mode, Level 1
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI receives packet EVC-1 with variable MMI_V_TRAIN = 0.(2)    The Speed pointer, Speed digital, CSG, CSG-Extension, all hooks, Target Distance Bar and Target Distance Digital are diplayed and correspond to the  received packet EVC-1
            Test Step Comment: (1) MMI_gen 1086 (partly: negative case - received MMI_V_TRAIN not equal -1); MMI_gen 1268 (partly: received MMI_V_TRAIN greater than -1); MMI_gen 1275 (partly: negative case - received valid MMI_V_TRAIN);(2) MMI_gen 1086 (partly: negative case - when MMI_V_TRAIN not equal -1); MMI_gen 1268 (partly: when MMI_DYNAMIC not elder than 600ms and MMI_V_TRAIN greater than -1); MMI_gen 1275 (partly: negative case - when MMI_V_TRAIN is valid);
            */
            // Set train running number, cab 1 active, and other defaults
            DmiActions.Activate_Cabin_1(this);

            // Set driver ID
            DmiActions.Set_Driver_ID(this, "1234");

            // Set to level 1 and SR mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;

            // Enable standard buttons including Start, and display Default window.
            DmiActions.Finished_SoM_Default_Window(this);

            EVC1_MMIDynamic.MMI_V_TRAIN = 0;
            EVC1_MMIDynamic.MMI_V_TARGET = 200;
            EVC1_MMIDynamic.MMI_V_RELEASE = 250;
            EVC1_MMIDynamic.MMI_V_PERMITTED = 300;

            WaitForVerification("Check that the following objects are displayed on the DMI with speed = 0:" +
                                Environment.NewLine + Environment.NewLine +
                                "1. The Speed pointer" + Environment.NewLine +
                                "2. Speed digital" + Environment.NewLine +
                                "3. CSG" + Environment.NewLine +
                                "4. CSG-Extension" + Environment.NewLine +
                                "5. All hooks" + Environment.NewLine +
                                "6. Target Distance Bar" + Environment.NewLine +
                                "7. Digital Target Distance");

            MakeTestStepHeader(3, UniqueIdentifier++, "Drive the train forward pass BG1 with speed = 25 km/h",
                "Verify the following information,(1)   Use the log file to confirm that DMI received packet EVC-1 with variable MMI_V_TRAIN = 694.(2)    The Speed pointer and Speed digital are diplayed consist with received packet EVC-1.(3)   The Speed Pointer and Speed Digital on DMI screen are correspond with the current train speed");
            /*
            Test Step 3
            Action: Drive the train forward pass BG1 with speed = 25 km/h
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received packet EVC-1 with variable MMI_V_TRAIN = 694.(2)    The Speed pointer and Speed digital are diplayed consist with received packet EVC-1.(3)   The Speed Pointer and Speed Digital on DMI screen are correspond with the current train speed
            Test Step Comment: (1) MMI_gen 1086 (partly: negative case - received MMI_V_TRAIN not equal -1); MMI_gen 1268 (partly: received MMI_V_TRAIN greater than -1); MMI_gen 1275 (partly: negative case - received valid MMI_V_TRAIN);(2) MMI_gen 1086 (partly: negative case - when MMI_V_TRAIN not equal -1); MMI_gen 1268 (partly: when MMI_DYNAMIC not elder than 600ms and MMI_V_TRAIN greater than -1); MMI_gen 1275 (partly: negative case - when MMI_V_TRAIN is valid);(3) MMI_gen 1277;
            */
            EVC1_MMIDynamic.MMI_V_TRAIN = 694;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Speed pointer and Speed digital are displayed." + Environment.NewLine +
                                "2. The Speed Pointer and Digital Speed on DMI screen show speed at 25 km/h.");

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Use the test script file 12_8_a.xml to send EVC-1 with, MMI_V_TRAIN = -2",
                "Verify the following information,(1)   The following objects are not display on DMI,Speed PointerSpeed DigitalCSGCSG-ExtensionAll hooksTarget Distance BarTarget Distance Digital");
            /*
            Test Step 4
            Action: Use the test script file 12_8_a.xml to send EVC-1 with, MMI_V_TRAIN = -2
            Expected Result: Verify the following information,(1)   The following objects are not display on DMI,Speed PointerSpeed DigitalCSGCSG-ExtensionAll hooksTarget Distance BarTarget Distance Digital
            Test Step Comment: (1) MMI_gen 1086 (partly: negative case - when MMI_V_TRAIN not equal -1); MMI_gen 1268 (partly: negative case - when MMI_DYNAMIC not elder than 600ms and MMI_V_TRAIN not greater than and equal -1); MMI_gen 1275;
            */
            XML_12_8();

            WaitForVerification("Check that the following objects are not displayed" + Environment.NewLine +
                                Environment.NewLine +
                                "1. The Speed pointer" + Environment.NewLine +
                                "2. Speed digital" + Environment.NewLine +
                                "3. CSG" + Environment.NewLine +
                                "4. CSG-Extension" + Environment.NewLine +
                                "5. Any hooks" + Environment.NewLine +
                                "6. Target Distance Bar" + Environment.NewLine +
                                "7. Digital Target Distance");

            MakeTestStepHeader(5, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 5
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_12_8_DMI_Test_Specification

        private void XML_12_8()
        {
            EVC1_MMIDynamic.MMI_M_SLIDE = 1;
            EVC1_MMIDynamic.MMI_M_SLIP = 1;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Ceiling_Speed_Monitoring; // 0
            EVC1_MMIDynamic.MMI_A_TRAIN = 0;
            EVC1_MMIDynamic.MMI_V_TRAIN = -1; // value in xml file is out of range so send this
            EVC1_MMIDynamic.MMI_V_TARGET = 1111;
            EVC1_MMIDynamic.MMI_V_PERMITTED = 1111;
            EVC1_MMIDynamic.MMI_V_RELEASE = 555;
            EVC1_MMIDynamic.MMI_O_BRAKETARGET = 0;
            EVC1_MMIDynamic.MMI_O_IML = 0;
            EVC1_MMIDynamic.MMI_V_INTERVENTION = 0;

            //SITR.ETCS1.Dynamic.EVC01Validity1.Value = 0x0;
            //SITR.ETCS1.Dynamic.EVC01Validity2.Value = 0x0;
        }

        #endregion
    }
}