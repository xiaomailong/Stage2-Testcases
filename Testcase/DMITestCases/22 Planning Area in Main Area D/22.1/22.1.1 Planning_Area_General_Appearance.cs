using System;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 22.1.1 Planning Area: General Appearance
    /// TC-ID: 17.1.1
    /// 
    /// This test case verifies the presentation of planning area in area D when DMI changes to FS mode.
    /// The planning area shall display the planning information and all objects to driver.
    /// The presentation of the planning area shall comply with [ERA-ERTMS] standard and [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 3063 (partly: FS mode);
    /// MMI_gen 7101 (partly: default configuration);
    /// MMI_gen 7102 (partly: default configuration); MMI_gen 9937;
    /// 
    /// Scenario:
    /// Activate Cabin A. Perform SoM in SR mode, Level 1. Drive train forward pass BG1 at 100 m.
    /// Then, verify that PA is not displayed in the SR mode.
    /// BG1: Packet 21 and 27. Drive train forward pass BG2 at 150 m.
    /// Then, verify that all objects in MMI_gen 9937 are displayed BG2: Packet 12 and 68
    /// 
    /// Used files:
    /// 17_1_1.tdg
    /// </summary>
    public class TC_ID_17_1_1_Planning_Area : TestcaseBase
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
            // Call generic Action Method
            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.GoToIdle;
            EVC0_MMIStartATP.Send();
            DmiActions.Activate_Cabin_1(this);
            DmiActions.Set_Driver_ID(this, "1234");
            // Call generic Check Results Method
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Is the Driver ID window displayed.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Perform SoM to SR mode, level 1");
            TraceReport("Expected Result");
            TraceInfo("DMI displays in SR mode, level 1");
            /*
            Test Step 2
            Action: Perform SoM to SR mode, level 1
            Expected Result: DMI displays in SR mode, level 1
            */
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;
            DmiActions.Finished_SoM_Default_Window(this);

            // Call generic Check Results Method
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Does the DMI show Staff Responsible Mode.");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Drive the train forward passing BG1");
            TraceReport("Expected Result");
            TraceInfo(
                "DMI remain displays in SR mode, level 1. Verify that the Planning area is not displayed in main area D");
            /*
            Test Step 3
            Action: Drive the train forward passing BG1
            Expected Result: DMI remain displays in SR mode, level 1. Verify that the Planning area is not displayed in main area D
            Test Step Comment: MMI_gen 7101 (partly: default  configuration);              
            */
            // Call generic Action Method
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Does the DMI show Staff Responsible Mode." + Environment.NewLine +
                                "2. Confirm that the planning area IS NOT displayed.");

            TraceHeader("Test Step 4");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("Drive the train forward passing BG2");
            TraceReport("Expected Result");
            TraceInfo("DMI changes from SR mode to FS mode.");
            /*
            Test Step 4
            Action: Drive the train forward passing BG2
            Expected Result: DMI changes from SR mode to FS mode.
                            The Planning area is displayed the planning information in main area D.
                            The planning area is displayed the information following:
                                Distance scale
                                Order and announcement of track condition
                                Gradient profile
                                Speed profile discontinuities
                                PASPIndication marker
                                Hide and show planning information
                                Zoom function (see the example in ‘Comment’ column)
            Test Step Comment: (1) MMI_gen 3063 (partly: FS mode); MMI_gen 7102 (partly: default  configuration);
                                (2) MMI_gen 9937;   
            */
            // Force train into Full Supervision Mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. Does the DMI show Full Supervision Mode." + Environment.NewLine +
                                "2. Confirm that the planning area IS displayed.");

            TraceHeader("Test Step 5");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo("End of test");
            
            /*
            Test Step 5
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}