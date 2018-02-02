using System;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// 11.6 Acknowledgements: Negative acknowledgement option ignoring
    /// TC-ID: 6.6
    /// 
    /// This test case verifies that DMI is still display a symbol with acknowledgement even received packet EVC-8 with negative acknowledgement option.
    /// 
    /// Tested Requirements:
    /// MMI_gen 4504;
    /// 
    /// Scenario:
    /// 1.At the default window, Use the test script file to send Driver message to DMI. Then, verifies the display information.
    /// 2.Press an acknowledgement area. Then use the test script file to send Driver message to DMI and verifies the display information.
    /// 
    /// Used files:
    /// 6_6_a.xml, 6_6_b.xml, 6_6_c.xml
    /// </summary>
    public class TC_ID_6_6_Acknowledgements : TestcaseBase
    {
        public override void PreExecution()
        {
            // Pre-conditions from TestSpec:

            // Call the TestCaseBase PreExecution
            base.PreExecution();
            DmiActions.Complete_SoM_L1_SB(this);
            // System is power on.Cabin is activated.SoM is performed until Level 1 is selected and confirmed.Main window is closed.
        }

        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 0;
            // Testcase entrypoint

            TraceHeader("Test Step 1");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Use the test script file 6_6_a.xml to send EVC-8 with,MMI_Q_TEXT = 260MMI_Q_TEXT_CRITERIA = 2MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)    DMI displays ST01 symbol with yellow flashing frame in sub-area C9");
            /*
            Test Step 1
            Action: Use the test script file 6_6_a.xml to send EVC-8 with,MMI_Q_TEXT = 260MMI_Q_TEXT_CRITERIA = 2MMI_I_TEXT = 1
            Expected Result: Verify the following information,(1)    DMI displays ST01 symbol with yellow flashing frame in sub-area C9
            Test Step Comment: (1) MMI_gen 4504 (partly: symbols);
            */
            XML_6_6(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the ST01 symbol with a yellow flashing frame in sub-area C9.");

            TraceHeader("Test Step 2");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Press an acknowledgement on sub-area C9.Then, use the test script file 6_6_b.xml to send EVC-8 with,MMI_Q_TEXT = 298MMI_Q_TEXT_CRITERIA = 2MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo("Verify the following information,(1)    DMI displays DR02 symbol with yes button in area D");
            /*
            Test Step 2
            Action: Press an acknowledgement on sub-area C9.Then, use the test script file 6_6_b.xml to send EVC-8 with,MMI_Q_TEXT = 298MMI_Q_TEXT_CRITERIA = 2MMI_I_TEXT = 1
            Expected Result: Verify the following information,(1)    DMI displays DR02 symbol with yes button in area D
            Test Step Comment: (1) MMI_gen 4504 (partly: TAF);
            */
            DmiActions.ShowInstruction(this, "Acknowledge by pressing in sub-area C9");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            XML_6_6(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the DR02 symbol and a ‘Yes’ button (to the right) in area D.");

            TraceHeader("Test Step 3");
            TraceHeader("TP-" + UniqueIdentifier++);
            TraceReport("Action");
            TraceInfo(
                "Press ‘Yes’ button on area D.Then, use the test script file 6_6_c.xml to send EVC-8 with,MMI_Q_TEXT = 263MMI_Q_TEXT_CRITERIA = 2MMI_I_TEXT = 1");
            TraceReport("Expected Result");
            TraceInfo(
                "Verify the following information,(1)    DMI displays MO10 symbol with yellow flashing frame in sub-area C1");
            /*
            Test Step 3
            Action: Press ‘Yes’ button on area D.Then, use the test script file 6_6_c.xml to send EVC-8 with,MMI_Q_TEXT = 263MMI_Q_TEXT_CRITERIA = 2MMI_I_TEXT = 1
            Expected Result: Verify the following information,(1)    DMI displays MO10 symbol with yellow flashing frame in sub-area C1
            Test Step Comment: (1) MMI_gen 4504 (partly: symbols);
            */
            DmiActions.ShowInstruction(this, "Press the ‘Yes’ button (to the right) in area D.");

            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();

            XML_6_6(msgType.typec);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the MO10 symbol with a yellow flashing frame in sub-area C1.");

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

        #region Send_XML_6_6_DMI_Test_Specification

        enum msgType
        {
            typea,
            typeb,
            typec
        }

        private void XML_6_6(msgType type)
        {
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.PlainTextMessage = "";
            switch (type)
            {
                case msgType.typea:
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
                    break;
                case msgType.typeb:

                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 298;
                    break;
                case msgType.typec:
                    EVC8_MMIDriverMessage.MMI_Q_TEXT = 263;
                    break;
            }

            EVC8_MMIDriverMessage.Send();
        }

        #endregion
    }
}