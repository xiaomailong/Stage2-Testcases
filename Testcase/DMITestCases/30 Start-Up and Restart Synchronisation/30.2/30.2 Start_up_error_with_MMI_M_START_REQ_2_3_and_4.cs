using System;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// 30.2 Start-up error with MMI_M_START_REQ = 2, 3 and 4
    /// TC-ID: 25.2
    /// 
    /// To verify that the DMI is working properly while DMI start-up is error.
    /// 
    /// Tested Requirements:
    /// MMI_gen 236;
    /// 
    /// Scenario:
    /// Use test scripts to simulate mistakes during DMI start-up and verify that DMI displays each text message correctly.
    /// 
    /// Used files:
    /// 25_2_a.xml25_2_b.xml25_2_c.xml
    /// </summary>
    public class TC_ID_25_2_Start_up_error_with_MMI_M_START_REQ_2_3_and_4 : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 26420;
            // Testcase entrypoint

            MakeTestStepHeader(1, UniqueIdentifier++,
                "Use 25_2_a.xml script to simulate the [MMI_START_ATP(EVC-0).MMI_M_START_REQ] = 2",
                "(1)    DMI displays the message “MMI type not supported” instead of “starting up” in area E5");
            /*
            Test Step 1
            Action: Use 25_2_a.xml script to simulate the [MMI_START_ATP(EVC-0).MMI_M_START_REQ] = 2
            Expected Result: (1)    DMI displays the message “MMI type not supported” instead of “starting up” in area E5
            Test Step Comment: (1) MMI_gen 236 (partly: MMI_M_START_REQ = 2)
            */
            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.VersionInfo;
            EVC0_MMIStartATP.Send();

            DmiActions.Re_establish_communication_EVC_DMI(this);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Starting up’ in area E5.");

            XML_25_2(msgType.typea);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays removes the message ‘Starting up’ and displays ‘MMI type not supported’ in area E5.");

            MakeTestStepHeader(2, UniqueIdentifier++,
                "Restart OTE and ATP again until the message “starting up” is displayed in area E5", "");

            /*
            Test Step 2
            Action: Restart OTE and ATP again until the message “starting up” is displayed in area E5
            Expected Result: 
            */
            DmiActions.ShowInstruction(this, "Power down the system, wait 10s, then power up the system again");

            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.VersionInfo;
            EVC0_MMIStartATP.Send();

            DmiActions.Re_establish_communication_EVC_DMI(this);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Starting up’ in area E5.");

            MakeTestStepHeader(3, UniqueIdentifier++,
                "Use 25_2_b.xml script to simulate the [MMI_START_ATP(EVC-0).MMI_M_START_REQ] = 3",
                "(1)    DMI displays the message “Incompatible IF versions” instead of “starting up” in area E5");
            /*
            Test Step 3
            Action: Use 25_2_b.xml script to simulate the [MMI_START_ATP(EVC-0).MMI_M_START_REQ] = 3
            Expected Result: (1)    DMI displays the message “Incompatible IF versions” instead of “starting up” in area E5
            Test Step Comment: (1) MMI_gen 236 (partly: MMI_M_START_REQ = 3)
            */
            XML_25_2(msgType.typeb);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays removes the message ‘Starting up’ and displays ‘Incompatible IF versions’ in area E5.");


            MakeTestStepHeader(4, UniqueIdentifier++,
                "Restart OTE and ATP again until the message “starting up” is displayed in area E5", "");
            /*
            Test Step 4
            Action: Restart OTE and ATP again until the message “starting up” is displayed in area E5
            Expected Result: 
            */
            DmiActions.ShowInstruction(this, "Power down the system, wait 10s, then power up the system again");

            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.VersionInfo;
            EVC0_MMIStartATP.Send();

            DmiActions.Re_establish_communication_EVC_DMI(this);
            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays the message ‘Starting up’ in area E5.");

            MakeTestStepHeader(5, UniqueIdentifier++,
                "Use 25_2_c.xml script to simulate the [MMI_START_ATP(EVC-0).MMI_M_START_REQ] = 4",
                "(1)    DMI displays the message “Incompatible SW versions” instead of “starting up” in area E5");
            /*
            Test Step 5
            Action: Use 25_2_c.xml script to simulate the [MMI_START_ATP(EVC-0).MMI_M_START_REQ] = 4
            Expected Result: (1)    DMI displays the message “Incompatible SW versions” instead of “starting up” in area E5
            Test Step Comment: (1) MMI_gen 236 (partly: MMI_M_START_REQ = 4)
            */
            XML_25_2(msgType.typec);

            WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                "1. DMI displays removes the message ‘Starting up’ and displays ‘Incompatible SW versions’ in area E5.");

            TraceHeader("End of test");

            /*
            Test Step 6
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }


        #region Send_XML_25_2_DMI_Test_Specification

        private enum msgType
        {
            typea,
            typeb,
            typec
        }

        private void XML_25_2(msgType packetSelector)
        {
            // XML file suggests sending a 0 and then waiting 5ms before sending important EVC0 signal
            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.VersionInfo;
            EVC0_MMIStartATP.Send();
            Wait_Realtime(5);

            switch (packetSelector)
            {
                case msgType.typea:
                    EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.MMITypeNotSupported;
                    break;
                case msgType.typeb:
                    EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.IncompatibleIFVersions;
                    break;
                case msgType.typec:
                    EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.IncompatibleSWVersions;
                    break;
            }

            EVC0_MMIStartATP.Send();
        }

        #endregion
    }
}