using System;
using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.5.4 PA Gradient Profile:  Invalid Information Ignoring
    /// TC-ID: 17.5.4
    /// 
    /// This test case verifies an information updating of PA Gradient Profile and Speed Profile refer to information from received packet EVC-4 which will be ignored when the value in packet is invalid.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7260; MMI_gen 7261; MMI_gen 7257 (partly: Special value);
    /// 
    /// Scenario:
    /// Activate cabin A. Perform SoM to SR mode, level 1.Drive the train forward pass BG1 at position 100.BG1 giving pkt12, pkt 21 and pkt27.Q_GDIR = 1, G_A = 1Stop the train.Use the test script file to send packet information EVC-
    /// 4.Then, verify that DMI ignores the invalid EVC-4 packet correctly.Note: Each step of test script file in executed continuously, Tester need to verify expected result within specific time (5 second).
    /// 
    /// Used files:
    /// 17_5_4.tdg, 17_5_4_a.xml, 17_5_4_b.xml, 17_5_4_c.xml, 17_5_4_d.xml, 17_5_4_e.xml
    /// </summary>
    public class TC_ID_17_5_4_PA_Gradient_Profile_Invalid_Information_Ignoring : TestcaseBase
    {
        static List<TrackDescription> DescriptionsList;

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 23777;
            // Testcase entrypoint

            MakeTestStepHeader(1, UniqueIdentifier++, "Activate cabin A. Then  perform SoM to SR mode, level 1",
                "DMI displays SR mode, level 1");
            /*
            Test Step 1
            Action: Activate cabin A. Then  perform SoM to SR mode, level 1
            Expected Result: DMI displays SR mode, level 1
            */

            StartUp();
            DmiActions.Complete_SoM_L1_SR(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in SR mode, Level 1.");

            MakeTestStepHeader(2, UniqueIdentifier++, "Drive the train forward pass BG1.Then, stop the train",
                "DMI changes from SR to FS mode. The Planning Area is displayed in area D with PA Gradient Profile value = 1 and PA Gradient Profile colour is grey");
            /*
            Test Step 2
            Action: Drive the train forward pass BG1.Then, stop the train
            Expected Result: DMI changes from SR to FS mode. The Planning Area is displayed in area D with PA Gradient Profile value = 1 and PA Gradient Profile colour is grey
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 10000;

            DescriptionsList = new List<TrackDescription>()
            {
                new TrackDescription {MMI_G_GRADIENT = 2, MMI_O_GRADIENT = 25000}
            };

            EVC4_MMITrackDescription.TrackDescriptions = DescriptionsList;
            EVC4_MMITrackDescription.MMI_G_GRADIENT_CURR = 2;
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1." + Environment.NewLine +
                                "2. The Planning Area is displayed in area D." + Environment.NewLine +
                                "3. A Gradient Profile is displayed in grey, with a value (Uphill) of 2.");

            // Steps 3 to 6 are in XML_17_5_4_a()
            MakeTestStepHeader(3, UniqueIdentifier++,
                "Use the test script file 17_5_4_a.xml to send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 254MMI_N_GRADIENT = 0",
                "Verify that the value of PA Gradient Profile is change to 254");
            /*
            Test Step 3
            Action: Use the test script file 17_5_4_a.xml to send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 254MMI_N_GRADIENT = 0
            Expected Result: Verify that the value of PA Gradient Profile is change to 254
            */
            XML_17_5_4_a();

            MakeTestStepHeader(4, UniqueIdentifier++,
                "Send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 255MMI_N_GRADIENT = 0",
                "Verify that the value of PA Gradient Profile is not change, still display as 254");
            /*
            Test Step 4
            Action: Send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 255MMI_N_GRADIENT = 0
            Expected Result: Verify that the value of PA Gradient Profile is not change, still display as 254
            Test Step Comment: MMI_gen 7260 (partly: 4th bullet);
            */

            MakeTestStepHeader(5, UniqueIdentifier++,
                "Send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = -254MMI_N_GRADIENT = 0",
                "Verify that the value of PA Gradient Profile is change to 254 and PA Gradient Profile colour is change to dark-grey colour");
            /*
            Test Step 5
            Action: Send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = -254MMI_N_GRADIENT = 0
            Expected Result: Verify that the value of PA Gradient Profile is change to 254 and PA Gradient Profile colour is change to dark-grey colour
            */

            MakeTestStepHeader(6, UniqueIdentifier++,
                "Send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = -255MMI_N_GRADIENT = 0",
                "Verify that PA Gradient Profile is removed from area D");
            /*
            Test Step 6
            Action: Send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = -255MMI_N_GRADIENT = 0
            Expected Result: Verify that PA Gradient Profile is removed from area D
            Test Step Comment: MMI_gen 7257 (partly: Special value);      
            */

            // Steps 7 to 8 are in XML_17_5_4_b 
            MakeTestStepHeader(7, UniqueIdentifier++,
                "Use the test script file 17_5_4_b.xml to send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 10MMI_N_GRADIENT = 0",
                "The Planning Area is displayed in area D with PA Gradient Profile value = 10 and PA Gradient Profile colour is grey");
            /*
            Test Step 7
            Action: Use the test script file 17_5_4_b.xml to send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 10MMI_N_GRADIENT = 0
            Expected Result: The Planning Area is displayed in area D with PA Gradient Profile value = 10 and PA Gradient Profile colour is grey
            */
            XML_17_5_4_b();

            MakeTestStepHeader(8, UniqueIdentifier++,
                "Send EVC-4 with,MMI_V_MRSP_CURR = 11112MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 20MMI_N_GRADIENT = 0",
                "Verify that the value of PA Gradient Profile is not change, still display as 10");
            /*
            Test Step 8
            Action: Send EVC-4 with,MMI_V_MRSP_CURR = 11112MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 20MMI_N_GRADIENT = 0
            Expected Result: Verify that the value of PA Gradient Profile is not change, still display as 10
            Test Step Comment: MMI_gen 7260 (partly: 3rd  bullet);
            */

            // Steps 8 to 9 are in XML_17_5_4_c
            MakeTestStepHeader(9, UniqueIdentifier++,
                "Use the test script file 17_5_4_c.xml to send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 22MMI_N_GRADIENT = 1MMI_G_GRADIENT = 11MMI_O_GRADIENT_2 = 15259MMI_O_GRADIENT_1 = 16176",
                "The 2 PA Gradient profiles (value = 11 and 22) are displayed in area D5");
            /*
            Test Step 9
            Action: Use the test script file 17_5_4_c.xml to send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 22MMI_N_GRADIENT = 1MMI_G_GRADIENT = 11MMI_O_GRADIENT_2 = 15259MMI_O_GRADIENT_1 = 16176
            Expected Result: The 2 PA Gradient profiles (value = 11 and 22) are displayed in area D5
            */
            XML_17_5_4_c();

            MakeTestStepHeader(10, UniqueIdentifier++,
                "Send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 10MMI_N_GRADIENT = 32MMI_G_GRADIENT = 1MMI_O_GRADIENT_2 = 15259MMI_O_GRADIENT_1 = 16176",
                "Verify that the value of PA Gradient Profile is not change, still display PA Gradient Profiles value = 11 and 22");
            /*
            Test Step 10
            Action: Send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 10MMI_N_GRADIENT = 32MMI_G_GRADIENT = 1MMI_O_GRADIENT_2 = 15259MMI_O_GRADIENT_1 = 16176
            Expected Result: Verify that the value of PA Gradient Profile is not change, still display PA Gradient Profiles value = 11 and 22
            Test Step Comment: MMI_gen 7260 (partly: 1st   bullet);                      
            */

            // Steps 11 to 13 are in XML_17_5_4_d
            MakeTestStepHeader(11, UniqueIdentifier++,
                "Use the test script file 17_5_4_d.xml to send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 20MMI_N_GRADIENT = 1MMI_G_GRADIENT = 1MMI_O_GRADIENT_2 = 15258MMI_O_GRADIENT_1 = 51712",
                "Verify that the value of PA Gradient Profile is not change, still display PA Gradient Profiles value = 11 and 22");
            /*
            Test Step 11
            Action: Use the test script file 17_5_4_d.xml to send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 20MMI_N_GRADIENT = 1MMI_G_GRADIENT = 1MMI_O_GRADIENT_2 = 15258MMI_O_GRADIENT_1 = 51712
            Expected Result: Verify that the value of PA Gradient Profile is not change, still display PA Gradient Profiles value = 11 and 22
            Test Step Comment: MMI_gen 7261 (partly: 3rd bullet);
            */
            XML_17_5_4_d();

            MakeTestStepHeader(12, UniqueIdentifier++,
                "Send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 20MMI_N_GRADIENT = 1MMI_G_GRADIENT = 255MMI_O_GRADIENT_2 = 15259MMI_O_GRADIENT_1 = 16176",
                "Verify that the value of PA Gradient Profile is not change, still display PA Gradient Profiles value = 11 and 22");
            /*
            Test Step 12
            Action: Send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 20MMI_N_GRADIENT = 1MMI_G_GRADIENT = 255MMI_O_GRADIENT_2 = 15259MMI_O_GRADIENT_1 = 16176
            Expected Result: Verify that the value of PA Gradient Profile is not change, still display PA Gradient Profiles value = 11 and 22
            Test Step Comment: MMI_gen 7261 (partly: 1st  bullet);
            */

            MakeTestStepHeader(13, UniqueIdentifier++,
                "Send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 20MMI_N_GRADIENT = 1MMI_G_GRADIENT = 2MMI_O_GRADIENT_2 = 32768MMI_O_GRADIENT_1 = 0",
                "Verify that the value of PA Gradient Profile is not change, still display PA Gradient Profiles value = 11 and 22");
            /*
            Test Step 13
            Action: Send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 0MMI_G_GRADIENT_CURR = 20MMI_N_GRADIENT = 1MMI_G_GRADIENT = 2MMI_O_GRADIENT_2 = 32768MMI_O_GRADIENT_1 = 0
            Expected Result: Verify that the value of PA Gradient Profile is not change, still display PA Gradient Profiles value = 11 and 22
            Test Step Comment: MMI_gen 7261 (partly: 2nd   bullet);
            */

            // Steps 14 to 15 are in XML_17_5_4_e
            MakeTestStepHeader(14, UniqueIdentifier++,
                "Use the test script file 17_5_4_e.xml to send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 1MMI_V_MRSP = 3000MMI_O_MRSP_2 = 15259MMI_O_MRSP_1 = 16176MMI_G_GRADIENT_CURR = 20MMI_N_GRADIENT = 0",
                "Verify that only one PA Gradient value is display as value = 20 with speed profile updated as picture in comment");
            /*
            Test Step 14
            Action: Use the test script file 17_5_4_e.xml to send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 1MMI_V_MRSP = 3000MMI_O_MRSP_2 = 15259MMI_O_MRSP_1 = 16176MMI_G_GRADIENT_CURR = 20MMI_N_GRADIENT = 0
            Expected Result: Verify that only one PA Gradient value is display as value = 20 with speed profile updated as picture in comment
            */
            XML_17_5_4_e();

            MakeTestStepHeader(15, UniqueIdentifier++,
                "Send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 32MMI_V_MRSP = 3000MMI_O_MRSP_2 = 15259MMI_O_MRSP_1 = 16176MMI_G_GRADIENT_CURR = 2MMI_N_GRADIENT = 0",
                "Verify that PA Gradient Profile and speed profile is not update, result is still same as step 14");
            /*
            Test Step 15
            Action: Send EVC-4 with,MMI_V_MRSP_CURR = 11111MMI_N_MRSP = 32MMI_V_MRSP = 3000MMI_O_MRSP_2 = 15259MMI_O_MRSP_1 = 16176MMI_G_GRADIENT_CURR = 2MMI_N_GRADIENT = 0
            Expected Result: Verify that PA Gradient Profile and speed profile is not update, result is still same as step 14
            Test Step Comment: MMI_gen 7260 (partly: 2nd   bullet);    
            */

            TraceHeader("End of test");

            /*
            Test Step 16
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }

        #region Send_XML_17_5_4_DMI_Test_Specification

        private void XML_17_5_4_a()
        {
            DescriptionsList.Clear();

            // Step 3
            EVC4_MMITrackDescription.MMI_G_GRADIENT_CURR = 254;
            EVC4_MMITrackDescription.MMI_V_MRSP_CURR = 0;
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Gradient Profile value displayed is (+) 254.");

            // Step 4
            EVC4_MMITrackDescription.MMI_G_GRADIENT_CURR = 255;
            EVC4_MMITrackDescription.MMI_V_MRSP_CURR = 11111;
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Gradient Profile value displayed is still 254.");

            // Step 5
            EVC4_MMITrackDescription.MMI_G_GRADIENT_CURR = -254;
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Gradient Profile value displayed is (-) 254 in Dark-grey .");

            // Step 6
            EVC4_MMITrackDescription.MMI_G_GRADIENT_CURR = 255;
            EVC4_MMITrackDescription.MMI_V_MRSP_CURR = 11111;
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Gradient Profile is removed from area D.");
        }

        private void XML_17_5_4_b()
        {
            // Step 7
            EVC4_MMITrackDescription.MMI_G_GRADIENT_CURR = 10;
            EVC4_MMITrackDescription.MMI_V_MRSP_CURR = 11111;
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.  Planning Area is displayed in area D." + Environment.NewLine +
                                "2. A Gradient Profile is displayed in grey, with a value (Uphill) of 10.");

            // Step 8
            EVC4_MMITrackDescription.MMI_G_GRADIENT_CURR = 20;
            EVC4_MMITrackDescription.MMI_V_MRSP_CURR = 11112;
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Gradient Profile value displayed is still 10.");
        }

        private void XML_17_5_4_c()
        {
            // Step 9
            DescriptionsList.Clear();
            DescriptionsList.Add(new TrackDescription {MMI_G_GRADIENT = 22, MMI_O_GRADIENT = 16176});
            DescriptionsList.Add(new TrackDescription {MMI_G_GRADIENT = 11, MMI_O_GRADIENT = 15259});

            EVC4_MMITrackDescription.MMI_G_GRADIENT_CURR = 22;
            EVC4_MMITrackDescription.MMI_V_MRSP_CURR = 11111;
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1.  Planning Area is displayed in area D." + Environment.NewLine +
                                "2. Two Gradient Profiles are displayed in grey, with values (Uphill) of 11 and 22.");

            // Step 10
            // 32 descriptions: invalid list
            for (int dl = 0; dl < 30; dl++)
            {
                DescriptionsList.Add(new TrackDescription {MMI_G_GRADIENT = 22, MMI_O_GRADIENT = 15259});
            }

            EVC4_MMITrackDescription.MMI_G_GRADIENT_CURR = 10;
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Gradient Profile values displayed are still 11 and 22.");
        }

        // Incomplete: spec and xml files area at variance with DMI_RS_ETCS and VSIS documents
        private void XML_17_5_4_d()
        {
            // Step 11
            TrackDescription descriptionElement = DescriptionsList[0];
            descriptionElement.MMI_G_GRADIENT = 20;
            descriptionElement.MMI_O_GRADIENT = 15258;
            descriptionElement = DescriptionsList[1];
            descriptionElement.MMI_G_GRADIENT = 1;
            descriptionElement.MMI_O_GRADIENT = 51712;

            EVC4_MMITrackDescription.MMI_G_GRADIENT_CURR = 20;
            EVC4_MMITrackDescription.MMI_V_MRSP_CURR = 11111;
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Gradient Profile values displayed are still 11 and 22.");

            // Step 12
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Gradient Profile values displayed are still 11 and 22.");

            // Step 13
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. The Gradient Profile values displayed are still 11 and 22.");
        }

        // Incomplete?: spec and xml files area at variance with DMI_RS_ETCS and VSIS documents
        private void XML_17_5_4_e()
        {
            // Step 14
            DescriptionsList.Clear();
            // 
            DescriptionsList.Add(new TrackDescription {MMI_V_MRSP = 3000, MMI_O_MRSP = 16176});

            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. One Gradient Profile is displayed in grey, with value (Uphill) of 20.");

            // Step 15
            // Add another 31 speed discontinuities => invalid set
            for (int dl = 0; dl < 31; dl++)
            {
                DescriptionsList.Add(new TrackDescription {MMI_V_MRSP = 3000, MMI_O_MRSP = 16176});
            }

            EVC4_MMITrackDescription.MMI_G_GRADIENT_CURR = 2;
            EVC4_MMITrackDescription.Send();

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. One Gradient Profile is still displayed in grey, with value (Uphill) of 20.");
        }

        #endregion
    }
}