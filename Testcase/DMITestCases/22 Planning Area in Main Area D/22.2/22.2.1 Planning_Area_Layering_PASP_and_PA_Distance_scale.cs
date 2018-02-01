using System;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.2.1 Planning Area-Layering: PASP and PA Distance scale
    /// TC-ID: 17.2.1
    /// 
    /// This test case verifies order of each objects in Planning area which ascending order from background to foreground refer to chapter 7.3.2 of requirement specification.
    /// 
    /// Tested Requirements:
    /// MMI_gen 7108;
    /// 
    /// Scenario:
    /// Activate Cabin A.Perform SoM to SR mode, Level 1.Drive the train forward pass BG
    /// 1.Then, verify that DMI displays layer order of PA’s objects correctly.BG1: packet 12, 21,27 and 68
    /// 
    /// Used files:
    /// 17_2_1.tdg
    /// </summary>
    public class TC_ID_17_2_1_Planning_Area : TestcaseBase
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
            // DMI displays in FS mode, level 1.

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
            TraceInfo("Activate cabin A");
            TraceReport("Expected Result");
            TraceInfo("DMI displays Driver ID window");
            /*
            Test Step 1
            Action: Activate cabin A
            Expected Result: DMI displays Driver ID window
            */
            DmiActions.Activate_Cabin_1(this);

            EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "1234";
            EVC14_MMICurrentDriverID.Send();

            DmiExpectedResults.Driver_ID_window_displayed(this);

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform SoM in SR mode, Level 1");
            TraceReport("Expected Result");
            TraceInfo("DMI displays in SR mode, level 1");
            /*
            Test Step 2
            Action: Perform SoM in SR mode, Level 1
            Expected Result: DMI displays in SR mode, level 1
            */
            // Tested exhaustively elsewhere: force SoM
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;
            DmiActions.Finished_SoM_Default_Window(this);

            DmiExpectedResults.SR_Mode_displayed(this);


            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Driver drives the train passing BG1");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI changes from SR mode to FS mode.Verify the order (background to fore ground) for each objects in PA as follows,PASPPA Distance ScaleIndication MarkerPA Track Condition, Gradient profile and Speed DiscontinuitiesHide/Show and Zoom PA buttons.Note: The object which have a lower order (i.e. PASP) cannot overlap the higher order object");
            /*
            Test Step 3
            Action: Driver drives the train passing BG1
            Expected Result: DMI changes from SR mode to FS mode.Verify the order (background to fore ground) for each objects in PA as follows,PASPPA Distance ScaleIndication MarkerPA Track Condition, Gradient profile and Speed DiscontinuitiesHide/Show and Zoom PA buttons.Note: The object which have a lower order (i.e. PASP) cannot overlap the higher order object
            Test Step Comment: MMI_gen 7108;
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            WaitForVerification(
                "Check that the following objects are displayed in order (from background to foreground):" +
                Environment.NewLine + Environment.NewLine +
                "1. PASP." + Environment.NewLine +
                "2. PA Distance Scale." + Environment.NewLine +
                "3. Indication marker." + Environment.NewLine +
                "4. PA Track Condition, Gradient Profile and Speed Discontinuities." + Environment.NewLine +
                "5. Hide/Show and Zoom PA buttons." + Environment.NewLine +
                "6. An object in the background of another object does not overlap it." + Environment.NewLine);
            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            
            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}