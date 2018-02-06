using System;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.Telegrams.DMItoEVC;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 20.5.1 Adhesion factor: General appearance
    /// TC-ID: 15.5.1
    /// 
    /// This test case verifies the display of the adhesion factor indication on DMI when the factor is independently activated by
    /// driver and Trackside, when the condition complies with [MMI-ETCS-gen], [MMIIS] and [ERA-ERTMS] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7088; MMI_gen 111; MMI_gen 1688;
    /// 
    /// Scenario:
    /// Drive the train past BG0 at position 100 m: packet 3 Q_NVDRIVER_ADHES = 0 (to reset the value if it has been set)
    /// Drive the train past BG1 at position 250 m: packet 3 Q_NVDRIVER_ADHES = 1
    /// The adhesion factor indication is verified with the following events:
    ///     (Simple Case) Activate Slippery button at Adhesion window.
    ///     Driver simulates the communication loss between ETCS Onboard and DMI. Then re-establishes the communication.
    ///     Drive the train past BG2 at position 600 m: packet 71 D_ADHESION = 0, L_ADHESION = 200, M_ADHESION = 0 (Slippery)
    ///     Drive the train past length of reduced adhesion at position 800 m.
    /// 
    /// Used files:
    /// 15_5_1.tdg
    /// </summary>
    public class TC_15_5_1_Adhesion_Factor : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint
            StartUp();
            DmiActions.Complete_SoM_L1_SR(this);

            MakeTestStepHeader(1, UniqueIdentifier++,
                "Driver drives the train forward passing BG1. Then, press the ‘Special’ button.",
                "DMI still displays in SR mode. Verify that ‘Adhesion’ button is enabled");
            /*
            Test Step 1
            Action: Driver drives the train forward passing BG1. Then, press the ‘Special’ button.
            Expected Result: DMI still displays in SR mode. Verify that ‘Adhesion’ button is enabled
            */

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.Adhesion | Variables.standardFlags;
            EVC30_MMIRequestEnable.Send();

            DmiActions.ShowInstruction(this, "Select the Special button.");

            // Call generic Check Results Method
            DmiExpectedResults.DMI_displays_Special_window_with_enabled_Adhesion_button(this);

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Press ‘Adhesion’ button. Then, press ‘Slippery rail’ button and confirm an by pressing the input field.",
                "DMI displays symbol ST02 in sub-area A4");
            /*
            Test Step 2
            Action: Press ‘Adhesion’ button. Then, press ‘Slippery rail’ button and confirm an by pressing the input field.
            Expected Result: DMI displays symbol ST02 in sub-area A4
            Test Step Comment: (1) MMI_gen 7088 (partly: EVC-2, ‘Low Adhesion by Driver’)(2) MMI_gen 111;     
            */
            DmiActions.ShowInstruction(this, "Perform the following actions" + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press \"Adhesion\" button, followed by \"Slippery rail\" button." +
                                             Environment.NewLine +
                                             "2. Confirm \"Slippery rail\" by pressing the input field.");

            EVC101_MMIDriverRequest.CheckMRequestReleased =
                Variables.MMI_M_REQUEST.SetAdhesionCoefficientToSlipperyRail;

            EVC2_MMIStatus.MMI_M_ADHESION = 0x1;
            EVC2_MMIStatus.Send();

            DmiExpectedResults.Driver_symbol_displayed(this, "Adhesion factor slippery rail", "ST02", "A4", false);

            MakeTestStepHeader(3, UniqueIdentifier++, "Simulate the communication loss between ETCS Onboard and DMI",
                "Adhesion symbol ST02 is removed");
            /*
            Test Step 3
            Action: Simulate the communication loss between ETCS Onboard and DMI
            Expected Result: Adhesion symbol ST02 is removed
            Test Step Comment: (1) MMI_gen 1688;
            */
            // Call generic Action Method
            DmiActions.Simulate_communication_loss_EVC_DMI(this);
            this.WaitForVerification("Has the \"Slippery rail\" symbol disappeared from the DMI?");

            MakeTestStepHeader(4, UniqueIdentifier++, "Re-establish the communication between ETCS onboard and DMI",
                "Verify that the Adhesion symbol ST02 is resumed");
            /*
            Test Step 4
            Action: Re-establish the communication between ETCS onboard and DMI
            Expected Result: Verify that the Adhesion symbol ST02 is resumed
            */
            // Call generic Action Method
            DmiActions.Re_establish_communication_EVC_DMI(this);
            DmiExpectedResults.Driver_symbol_displayed(this, "Adhesion factor slippery rail", "ST02", "A4", false);

            MakeTestStepHeader(5, UniqueIdentifier++,
                "Press ‘Special’ button. Press ‘Adhesion’ button. Select and confirm ‘Non slippery rail’ button",
                "No adhesion factor indication is displayed.");
            /*
            Test Step 5
            Action: Press ‘Special’ button. Press ‘Adhesion’ button. Select and confirm ‘Non slippery rail’ button
            Expected Result: No adhesion factor indication is displayed.
            Test Step Comment: (1) MMI_gen 7088 (partly: No symbol displayed);    
            */
            // Call generic Action Method
            DmiActions.ShowInstruction(this, "Perform the following actions" + Environment.NewLine +
                                             Environment.NewLine +
                                             "1. Press \"Adhesion\" button, followed by \"Slippery rail\" button." +
                                             Environment.NewLine +
                                             "2. Confirm \"Non slippery rail\" by pressing the input field.");

            EVC2_MMIStatus.MMI_M_ADHESION = 0x00;
            EVC2_MMIStatus.Send();

            this.WaitForVerification("Has the \"Slippery rail\" symbol disappeared from the DMI?");

            MakeTestStepHeader(6, UniqueIdentifier++, "Drive the train forward passing BG2",
                "DMI displays symbol ST02 in sub-area A4");
            /*
            Test Step 6
            Action: Drive the train forward passing BG2
            Expected Result: DMI displays symbol ST02 in sub-area A4
            Test Step Comment: (1) MMI_gen 7088 (partly: EVC-2, ‘Low Adhesion from Trackside’);(2) MMI_gen 111;
            */

            EVC2_MMIStatus.MMI_M_ADHESION = 0x02;
            EVC2_MMIStatus.Send();

            // Call generic Check Results Method
            DmiExpectedResults.Driver_symbol_displayed(this, "Adhesion factor slippery rail", "ST02", "A4", false);

            MakeTestStepHeader(7, UniqueIdentifier++, "Drive the train forward",
                "No adhesion factor indication is displayed.");
            /*
            Test Step 7
            Action: Drive the train forward
            Expected Result: No adhesion factor indication is displayed.
            Test Step Comment: (1) MMI_gen 7088 (partly: No symbol displayed);    
            */
            EVC2_MMIStatus.MMI_M_ADHESION = 0x00;
            EVC2_MMIStatus.Send();

            this.WaitForVerification("Has the \"Slippery rail\" symbol disappeared from the DMI?");

            MakeTestStepHeader(8, UniqueIdentifier++, "Stop the train", "The Train is at standstill");
            /*
            Test Step 8
            Action: Stop the train
            Expected Result: The Train is at standstill
            */
            // Call generic Action Method
            DmiActions.Stop_the_train(this);

            MakeTestStepHeader(9, UniqueIdentifier++, "End of test", "");

            /*
            Test Step 9
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}