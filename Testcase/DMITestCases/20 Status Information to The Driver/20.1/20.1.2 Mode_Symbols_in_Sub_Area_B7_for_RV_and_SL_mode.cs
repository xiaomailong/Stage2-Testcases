using System;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.1.2 Mode Symbols in Sub-Area B7 for RV and SL mode
    /// TC-ID: 15.1.2
    /// 
    /// This test case verifies a display of of reversing permitted symbol in area C6, ETCS-Object ETCS Mode symbol (acknowledgement for RV) in area C1 and ETCS Mode symbol (RV and SL) in area B7. The symbol of each ETCS Mode shall comply with [ERA] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7485; MMI_gen 1227 (partly:MO15); MMI_gen 11233 (partly: MO15); MMI_gen 11084 (partly: current ETCS mode); MMI_gen 110 (partly: MO14); MMI_gen 11278;
    /// 
    /// Scenario:
    /// Activate cabin.Complete the SoM in SR mode, Level 1.Force the train to FS mode.Force the train to RV mode, then stop the train. Then, verify a display of reversing permitted symbol.Change the train direction to reverse. Then, verify a display of acknowledgement for RV symbol.Acknowledge the acknowledgement for RV symbol. Then, verify a display of RV symbol.Force the train into SL mode. Then, verify a display of DMI.
    /// 
    /// Used files:
    /// 15_1_2.tdg 
    /// </summary>
    public class TC_15_1_2_ETCS_Mode_Symbols : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:
            // - Test system is powered on- Cabin is active- Complete the SoM in SR mode, level 1- Force the train into FS mode by moving the train forward passing BG1

            // Call the TestCaseBase PreExecution
            base.PreExecution();
        }

        public override void PostExecution()
        {
            // Post-conditions from TestSpec
            // DMI displays SL mode.

            // Call the TestCaseBase PostExecution
            base.PostExecution();
        }

        public override bool TestcaseEntryPoint()
        {
            // Testcase entrypoint

            #region Test Step 1

            /*
            Action: Drive the train forward passing BG2
            Expected Result: 
            */

            DmiActions.Complete_SoM_L1_SR(this);
            DmiActions.Send_FS_Mode(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays in FS mode, Level 1.");

            #endregion

            #region Test Step 2

            /*
            Action: Force the train into RV mode by stopping the train
            Expected Result: Verify the following information,
            Use the log file to confirm that DMI received the EVC-8 with [MMI_DRIVER_MESSAGE (EVC-8).MMI_Q_TEXT] = 286 in order to display a symbol ‘ST06’ in sub-area C6
            Test Step Comment: (1) MMI_gen 7485;
            */

            DmiActions.Stop_the_train(this);

            DmiActions.Send_RV_Permitted_Symbol(this);
            DmiExpectedResults.RV_Permitted_Symbol_displayed(this);

            #endregion

            #region Test Step 3

            /*
            Action: Change the train direction to reverse
            Expected Result: Verify the following information,The acknowledgement for Reversing symbol (MO15) is displayed in area C1.Use the log file to confirm that DMI received the EVC-8 with [MMI_DRIVER_MESSAGE (EVC-8).MMI_Q_TEXT] = 262 in order to display the acknowledgement for Reversing symbol
            Test Step Comment: (1) MMI_gen 1227 (partly:MO15);                   (2) MMI_gen 11233 (partly: MO15);
            */

            DmiActions.ShowInstruction(this, "Switch MCS to REVERSE position.");
            DmiExpectedResults.RV_Mode_Ack_requested(this);

            #endregion

            #region Test Step 4

            /*
            Action: Press the symbol ‘MO15’ in area C1
            Expected Result: Verify the following information,The Reversing symbol (M014) is displayed in area B7.
            Use the log file to confirm that DMI received the EVC-7 with [MMI_ETCS_MISC_OUT_SIGNALS.OBU_TR_M_MODE] = 14 in order to display the Reversing symbol
            Test Step Comment: (1) MMI_gen 110 (partly:MO14);                   
                               (2) MMI_gen 11084      (partly: Displaying the current ETCS mode);
            */

            DmiActions.ShowInstruction(this, "Press DMI Sub Area C1");

            // Remove the ACK symbol
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            DmiActions.Send_RV_Mode(this);
            DmiExpectedResults.RV_Mode_displayed(this);

            #endregion

            #region  Test Step 5

            /*
            Action: Force the train into SL mode by the steps below:
            Deactivate the Cabin
            Force the simulation to ‘Sleeping’
            Expected Result: Verify the following information,
            Use the log file to confirm that DMI received the EVC-7 with [MMI_ETCS_MISC_OUT_SIGNALS.OBU_TR_M_MODE] = 5.
            No symbol display in area B7.
            All ETCS related information to the driver is removed and the text “Driver’s cab not active” is displayed in area E5
            Test Step Comment: (1) MMI_gen 11084 (partly: Displaying the current ETCS mode);        
                               (2) MMI_gen 11084 (partly: ETCS mode SL);                           
                               (3) MMI_gen 11278;
            */

            DmiActions.Deactivate_Cabin(this);

            DmiActions.ShowInstruction(this, "Force the simulation of \"Sleeping\".");
            DmiActions.Send_SL_Mode(this);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI does not display a symbol in sub-area B7." + Environment.NewLine +
                                "2. ETCS information for the driver is removed." + Environment.NewLine +
                                "3. DMI displays the message \"Driver's cab not active\" in sub-area E5");

            #endregion

            #region Test Step 6

            /*           
            Action: End of test
            Expected Result: 
            */

            #endregion

            return GlobalTestResult;
        }
    }
}