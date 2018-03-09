using System;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// Updated to DMI Test Spec 4.4 by JS at 2018-02-16
    /// 
    /// 17.3.1 Speed Pointer: Sub-Area B1
    /// TC-ID: 12.3
    /// 
    /// This test case verifies the presentation of speed pointer that displays in sub-area B1.
    /// The dimensions and colours of speed pointer shall comply with [ERA] standard.
    /// 
    /// Tested Requirements:
    /// MMI_gen 5965; MMI_gen 5968; arn_043#4087;
    /// 
    /// Scenario:
    /// Drive the train forward. Then, verify the display of speed pointer.
    /// 
    /// Used files:
    /// N/A
    /// </summary>
    public class TC_12_3_Train_Speed : TestcaseBase
    {
        public override bool TestcaseEntryPoint()
        {
            // This identifier shall match the identity of the first testcasestep of the testcase in Doors
            UniqueIdentifier = 21380;
            // Testcase entrypoint
            StartUp();

            TraceInfo("This is a repeat of TC_12_1_Display_of_Speed_Pointer_and_Speed_Digital." +
                      "Please see results of this test case.");

            GlobalTestResult = true;

            MakeTestStepHeader(1, UniqueIdentifier++, "Drive the train forward, speed up to 25 km/h",
                "Verify the following information,The speed pointer is displayed in sub-area B1. The speed pointer consists of a needle and a circular part centred in sub-area B1. Both parts are displayed in same colour. The dimension of the speed pointer is presented");
            /*
            Test Step 1
            Action: Drive the train forward, speed up to 25 km/h
            Expected Result: Verify the following information,The speed pointer is displayed in sub-area B1. The speed pointer consists of a needle and a circular part centred in sub-area B1. Both parts are displayed in same colour. The dimension of the speed pointer is presented
            Test Step Comment: (1) MMI_gen 5965;   (2) MMI_gen 5968;   
            */

            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 25;

            WaitForVerification("1. Is the speed pointer displayed in sub-area B1?" + Environment.NewLine +
                                "2. (2)	The speed pointer consists of a needle and a circular part centred in sub-area B1. Both parts are displayed in same colour. The dimension of the speed pointer is presented.");

            MakeTestStepHeader(2, UniqueIdentifier++, "Stop the train", "The speed pointer is indicated to zero km/h");
            /*
            Test Step 2
            Action: Stop the train
            Expected Result: The speed pointer is indicated to zero km/h
            */
            

            MakeTestStepHeader(3, UniqueIdentifier++, "Simulate loss-communication between ETCS onboard and DMI", "DMI displays the  message “ATP Down Alarm” with sound alarm.\r\nSpeed pointer and Speed digital are disappear\r\n");
            /*
            Test Step 3
            Action: Simulate loss-communication between ETCS onboard and DMI
            Expected Result: DMI displays the  message “ATP Down Alarm” with sound alarm.
            Speed pointer and Speed digital are disappear.
            Test Step Comment: arn_043#4087;
            */
            DmiActions.Force_Loss_Communication(this);

            WaitForVerification(
                "DMI displays the message “ATP Down Alarm” with sound alarm.\r\nSpeed pointer and Speed digital dissapears.\r\n");

            DmiActions.Restablish_Communication(this);

            TraceHeader("End of test");
            /*
            Test Step 4
            Action: End of test
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}