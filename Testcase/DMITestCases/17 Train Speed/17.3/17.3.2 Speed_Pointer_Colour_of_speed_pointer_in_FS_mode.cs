using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BT_Tools;
using BT_CSB_Tools;
using BT_CSB_Tools.Logging;
using BT_CSB_Tools.Utils.Xml;
using BT_CSB_Tools.SignalPoolGenerator.Signals;
using BT_CSB_Tools.SignalPoolGenerator.Signals.MwtSignal;
using BT_CSB_Tools.SignalPoolGenerator.Signals.MwtSignal.Misc;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using CL345;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.XML;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 17.3.2 Speed Pointer: Colour of speed pointer in FS mode
    /// TC-ID: 12.3.2
    /// 
    /// This test case verifies the colour of speed pointer which display refer to received packet EVC-1 while the train is running
    /// in each supervision status and speed monitoring for FS mode and verifies the sound S2 which played when received
    /// MMI_M_WARNING = 6.
    /// 
    /// Tested Requirements:
    /// MMI_gen 6299 (partly: FS mode); MMI_gen 11921 (partly: MMI_M_WARNING = 6);
    /// 
    /// Scenario:
    /// 1. Drive the train forward pass BG1 at position 100 m. Verify the display of speed pointer refer to received packet EVC-1.
    ///     BG1: Packet 12, 21 and 27 (Entering FS)
    /// 2. Continue to drive the train forward with the specified speed.
    ///     Verify the display of speed pointer refer to received packet EVC-1.
    /// 
    /// Used files:
    /// 12_3_2.tdg, 12_3_2_a.xml, 12_3_2_b.xml, 12_3_2_c.xml, 12_3_2_d.xml, 12_3_2_e.xml, 12_3_2_f.xml
    /// </summary>
    public class TC_12_3_2_Train_Speed : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // Test system is power on.Cabin is activated.SoM is performed in SR mode, Level 1.

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            DmiActions.Complete_SoM_L1_FS(this);
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays in FS mode, level 1
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1.");

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Drive the train forward with speed = 40 km/h pass BG1
            Expected Result:
            DMI displays in FS mode, level 1
            The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: OBU_TR_M_MODE, MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, FS mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, FS mode in CSM supervision);
            */
            
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 40;
            EVC1_MMIDynamic.MMI_V_INTERVENTION_KMH = 45;     
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1." + Environment.NewLine + 
                                "2. Is the speed pointer displaying 40 km/h?" + Environment.NewLine +
                                "3. Is the speed pointer grey?");

            /*
            Test Step 2
            Action: Increase the train speed to 41 km/h
            Expected Result: The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, FS mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, FS mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Overspeed_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 41;
           
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer displaying 41 km/h?" + Environment.NewLine +
                                "2. Is the speed pointer orange?");
            /*
            Test Step 3
            Action: Increase the train speed to 45 km/h.
            Expected Result: MI_M_WARNING = 4 (Status = WaS, Supervision = CSM) while the value of MMI_V_TRAIN = 1250 (45 km/h) which greater than MMI_V_PERMITTED but lower than MMI_V_INTERVENTION(2)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, FS mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, FS mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Warning_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 45;
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer displaying 45 km/h?" + Environment.NewLine +
                                "2. Is the speed pointer orange?");

            /*
            Test Step 4
            Action: Increase the train speed to 46 km/h
            Expected Result: The train speed is force to decrease because of emergency brake is applied by ETCS onboard.Verify the following information,Before train speed is decreased(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN = 1278 (46 km/h) which greater than MMI_V_INTERVENTION(2)   The speed pointer display in red colourAfter train speed is decreased(3)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 12 (Status = IntS, Supervision = CSM) while the value of MMI_V_TRAIN is lower than MMI_V_INTERVENTION(4)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, FS mode in CSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, FS mode in CSM supervision);(3) MMI_gen 6299 (partly: MMI_M_WARNING, FS mode in CSM supervision);(4) MMI_gen 6299 (partly: colour of speed pointer, FS mode in CSM supervision);
            */
            // Call generic Action Method
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 46;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer red?");

            DmiActions.Apply_Brakes(this);

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Ceiling_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer grey?");

            /*
            Test Step 5
            Action: Continue to drive the train forward with speed = 30 km/h.Then, stop the train
            Expected Result: Verify the following information,While the train is moving(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 11 (Status = NoS, Supervision = TSM) while the value of MMI_V_TRAIN is greater than MMI_V_TARGET(2)    The speed pointer display in white colourWhen the train is stopped(3)    Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 11 (Status = NoS, Supervision = TSM) while the value of MMI_V_TRAIN is lower than or same as MMI_V_TARGET(4)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to target speed MMI_V_TARGET, FS mode in TSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, FS mode in TSM supervision);(3) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to target speed MMI_V_TARGET, FS mode in TSM supervision);(4) MMI_gen 6299 (partly: colour of speed pointer, FS mode in TSM supervision);
            */
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 25;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 30;
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                               "1. Is the speed pointer white?");

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Normal_Status_Target_Speed_Monitoring;
          
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                               "1. Is the speed pointer grey?");
            /*
            Test Step 6
            Action: Continue the drive the train forward with speed = 30 km/h
            Expected Result: The permitted speed is decreased continuously, driver can observe at the circular speed gauge.Verify the following information,While the train speed < permitted speed(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 1 (Status = IndS, Supervision = TSM) while the value of MMI_V_TRAIN is lower than MMI_V_PERMITTED and MMI_V_TRAIN is greater than MMI_V_TARGET(2)    The speed pointer display in yellow colourWhile the train speed > permitted speed (3)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 9 (Status = OvS and IndS, Supervision = TSM) while the value of MMI_V_TRAIN is greater than MMI_V_PERMITTEDMMI_M_WARNING = 5 (Status = WaS and IndS, Supervision = TSM) while the value of MMI_V_TRAIN is greater than MMI_V_PERMITTED(4)   The speed pointer display in orange colourWhile the train speed is force to decrease by emergency brake applied from ETCS onboard(5)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 13 (Status = IntS and IndS, Supervision = TSM) while the value of MMI_V_TRAIN is greater than MMI_V_INTERVENTION(4)   The speed pointer display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to permitted speed MMI_V_PERMITTED, FS mode in TSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, FS mode in TSM supervision);(3) MMI_gen 6299 (partly: MMI_M_WARNING, FS mode in TSM supervision);(4) MMI_gen 6299 (partly: colour of speed pointer, FS mode in TSM supervision);(5) MMI_gen 6299 (partly: MMI_M_WARNING, FS mode in TSM supervision);(6) MMI_gen 6299 (partly: colour of speed pointer, FS mode in TSM supervision);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 30;
            EVC1_MMIDynamic.MMI_V_TARGET_KMH = 25;
        
            // ?? Send
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                               "1. Is the speed pointer yellow?");

            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Overspeed_Status_Indication_Status_Target_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_PERMITTED_KMH = 25;
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer orange?");
          
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Warning_Status_Indication_Status_Target_Speed_Monitoring;
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer orange?");

            DmiActions.Apply_Brakes(this);

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 30;
            EVC1_MMIDynamic.MMI_V_INTERVENTION_KMH = 20;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Indication_Status_Target_Speed_Monitoring;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer red?");

            /*
            Test Step 7
            Action: Drive the train with speed = 5 km/h
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 3 (Status = IndS, Supervision = RSM) while the value of MMI_V_TRAIN is lower than or same as MMI_V_RELEASE(2)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to release speed MMI_V_RELEASE, FS mode in RSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, FS mode in RSM supervision);
            */
            // Call generic Action Method
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 5;
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Indication_Status_Release_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_RELEASE = 5;
            
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer yellow?");

            /*
            Test Step 8
            Action: Drive the train with speed = 6 km/h
            Expected Result: Verify the following information,(1)   Use the log file to confirm that DMI received the packet information EVC-1 with the following condition,MMI_M_WARNING = 15 (Status = IntS and IndS, Supervision = RSM) while the value of MMI_V_TRAIN is greater than MMI_V_RELEASE(2)   The speed pointer display in yellow colour
            Test Step Comment: (1) MMI_gen 6299 (partly: MMI_M_WARNING, train speed in relation to release speed MMI_V_RELEASE, FS mode in RSM supervision);(2) MMI_gen 6299 (partly: colour of speed pointer, FS mode in CSM supervision);
            */
            EVC1_MMIDynamic.MMI_M_WARNING = MMI_M_WARNING.Intervention_Status_Indication_Status_Release_Speed_Monitoring;
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 6;
            EVC1_MMIDynamic.MMI_V_INTERVENTION_KMH = 20;

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer yellow?");

            /*
            Test Step 9
            Action: Stop the train.Then, use the test script file 12_3_2_a.xml to send the following packets,EVC-1MMI_M_WARNING = 2MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 972EVC-7OBU_TR_M_MODE = 0
            Expected Result: DMI displays in FS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, FS mode in PIM supervision);
            */
            XML_12_3_2_a.Send(this);            

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode" + Environment.NewLine +
                                "2. Is the speed pointer grey?");

            /*
            Test Step 10
            Action: Use the test script file 12_3_2_b.xml to send the following packets,EVC-1MMI_M_WARNING = 2MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1111EVC-7OBU_TR_M_MODE = 0
            Expected Result: DMI displays in FS mode, level 1.Verify the following information,(1)   The speed pointer display in white colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, FS mode in PIM supervision);
            */
            XML_12_3_2_b.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer white?");

            /*
            Test Step 11
            Action: Use the test script file 12_3_2_c.xml to send the following packets,EVC-1MMI_M_WARNING = 10MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1139EVC-7OBU_TR_M_MODE = 0
            Expected Result: DMI displays in FS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, FS mode in PIM supervision);
            */
            XML_12_3_2_c.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer orange?");
            /*
            Test Step 12
            Action: Use the test script file 12_3_2_d.xml to send the following packets,EVC-1MMI_M_WARNING = 6MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1250EVC-7OBU_TR_M_MODE = 0
            Expected Result: DMI displays in FS mode, level 1.Verify the following information,(1)   The speed pointer display in orange colour(2)   Sound S2 is played while the Warning Status is active
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, FS mode in PIM supervision);(2) MMI_gen 11921 (partly: MMI_M_WARNING = 6);
            */
            XML_12_3_2_d.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer orange?" + 
                                "2. Is sound S2 played while the warning status is active?");

            /*
            Test Step 13
            Action: Use the test script file 12_3_2_e.xml to send the following packets,EVC-1MMI_M_WARNING = 14MMI_V_PERMITTED = 1111MMI_V_TARGET = 1083MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 1277EVC-7OBU_TR_M_MODE = 0
            Expected Result: DMI displays in FS mode, level 1.Verify the following information,(1)   The speed pointer display in red colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, FS mode in PIM supervision);
            */
            XML_12_3_2_e.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the speed pointer red?");

            /*
            Test Step 14
            Action: Use the test script file 12_3_2_f.xml to send the following packets,EVC-1MMI_M_WARNING = 13MMI_V_PERMITTED = 1111MMI_V_TARGET = 0MMI_V_INTERVENTION = 1250MMI_V_TRAIN = 0EVC-7OBU_TR_M_MODE = 0
            Expected Result: DMI displays in FS mode, level 1.Verify the following information,(1)   The speed pointer display in grey colour
            Test Step Comment: (1) MMI_gen 6299 (partly: colour of speed pointer, FS mode in TSM supervision);
            */
            XML_12_3_2_f.Send(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, level 1." + Environment.NewLine +
                                "2. Is the speed pointer grey?");
            
            /*
            Test Step 15
            Action: End of test
            Expected Result: 
            */
            
            return GlobalTestResult;
        }
    }
}