using System;
using BT_CSB_Tools;

namespace Testcase.DMITestCases
{
    /// <summary>
    /// Performance of ETCS-DMI: Data handling
    /// TC-ID: 1.9
    /// Doors unique ID: TP-35771
    /// This test case verifies the performance of ETCS-DMI data handling before the data processing in ETCS-DMI. The function designs comply with the conditions in [MMI-ETCS-gen]. The data range and interface comply with the data information in [VSIS_gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 89; 
    /// 
    /// Scenario:
    /// Connect RCI and start RCI logging
    /// Activate the cabin.
    /// Verify and calculate the time responses of the following events:
    /// a. Perform SoM until the ‘Staff Responsible’ mode, level 2, is confirmed.
    ///         b. Send data of ~200 bytes (EVC-8) 
    /// 
    /// Used files:
    /// 1_9.utt, 1_9_a.xml
    /// </summary>
    public class TC_ID_1_9_Performance_of_ETCS_DMI_Data_handling : TestcaseBase
    {
        public override void PreExecution()
        {
            /* Pre-conditions from TestSpec
            	1. The test environment is powered on.
            	2. The RCI client is connected to ETCS-DMI with the concerned ETCS-DMI IP address via port 15001 (Raw connection).
            	3. The RCI is commanded to start logging the following data:
            	- The incoming data message from the MVB port to GPP component.
            	- The outgoing data message from the GPP component to MVB port.
            	- The concerned data in the GPP component.
            	4. The cabin is activated.
            */

            TraceInfo("Pre-condition: " + "1. The test environment is powered on." + Environment.NewLine +
                      "2. The RCI client is connected to ETCS-DMI with the concerned ETCS-DMI IP address via port 15001 (Raw connection)." +
                      Environment.NewLine + "3. The RCI is commanded to start logging the following data:" +
                      Environment.NewLine + "- The incoming data message from the MVB port to GPP component." +
                      Environment.NewLine + "- The outgoing data message from the GPP component to MVB port." +
                      Environment.NewLine + "- The concerned data in the GPP component." + Environment.NewLine +
                      "4. The cabin is activated.");

            base.PreExecution();
        }

        public override void PostExecution()
        {
            /* Post-conditions from TestSpec
            	DMI displays in SR mode, level 2.
            */

            TraceInfo("Post-condition: " + "DMI displays in SR mode, level 2.");

            base.PostExecution();
        }

        // TODO this entire testcase needs to be rewritten to work on the TCMS DMI
        public override bool TestcaseEntryPoint()
        {
            throw new TestcaseException("This testcase only works on RCS DMI, needs to be rewritten");
            /*
            Test Step 1
            Action:
            	Perform SoM in SR mode, Level 2
            Expected Result:
            	RCI logs the concerned activities as specified in the precondition
            */
            MakeTestStepHeader(1, 35790, "Perform SoM in SR mode, Level 2",
                "RCI logs the concerned activities as specified in the precondition");

            /*
            Test Step 2
            Action:
            	Observe the timestamps in RCI log and calculate the data throughput
            Expected Result:
            	(1) Use the RCI log to confirm the (average) response time differentiation of every incoming or outgoing EVC data (extracted EVC packets) with the size of 50 Bytes in GPP component and MVB port (tEVCinGPP – tinMVB or tEVCoutGPP - toutMVB)is less than 250 ms
            Test Step Comment:
            	(1) MMI_gen 89 (partly: throughput);
            */
            MakeTestStepHeader(2, 35791, "Observe the timestamps in RCI log and calculate the data throughput",
                "(1) Use the RCI log to confirm the (average) response time differentiation of every incoming or outgoing EVC data (extracted EVC packets) with the size of 50 Bytes in GPP component and MVB port (tEVCinGPP – tinMVB or tEVCoutGPP - toutMVB)is less than 250 ms");

            /*
            Test Step 3
            Action:
            	Send the data of EVC-8 with size of 200 Bytes by 1_9_a.xml
            Expected Result:
            	The big size of data can be transferred to ETCS-DMI screen and the text message of “ABC … BC17” displayed in area E5 – E9.
            	Note: Each group of the text message is identified with number 2 – 17, except the first group
            Test Step Comment:
            	(1) MMI_gen 89 (partly: extra in one shot);
            */
            MakeTestStepHeader(3, 35792, "Send the data of EVC-8 with size of 200 Bytes by 1_9_a.xml",
                "The big size of data can be transferred to ETCS-DMI screen and the text message of “ABC … BC17” displayed in area E5 – E9." +
                Environment.NewLine + "" + Environment.NewLine +
                "Note: Each group of the text message is identified with number 2 – 17, except the first group");

            /* End Of Test */
            TraceHeader("End Of Test");


            return GlobalTestResult;
        }
    }
}

#region XML Files

/*
1_9_a.xml:
	<?xml version="1.0" encoding="utf-16"?>
	<!DOCTYPE Test SYSTEM "mmitest.dtd"[]>
	<Test>
	  <Testcase destination="Etcs2DMI10">
	    <TestcaseID>
	        1.9
	        </TestcaseID>
	    <TestcaseDescr>
	        Performance of ETCS-DMI: Data handling
	        </TestcaseDescr>
	    <STARTLOG filename="Results/test.log" />
	    <MESSAGE>
	      <MESSAGE_HEADER></MESSAGE_HEADER>
	      <PACKET name="MMI_DRIVER_MESSAGE" num="EVC-8">
	        <VAR name="FFFIS_NID_STM" len="8" value="254" />
	        <VAR name="FFFIS_L_LENGTH" len="8" value="35" />
	        <VAR name="FFFIS_NID_PACKET" len="8" value="213" />
	        <VAR name="FFFIS_L_PACKET" len="13" value="264" />
	        <VAR name="PADDING" len="3" value="0" />
	        <!--                                                    -->
	        <VAR name="MMI_M_PACKET" len="16" value="8" />
	        <VAR name="MMI_L_PACKET" len="16" value="240" />
	        <!--                                                    -->
	        <VAR name="MMI_Q_TEXT_CLASS" len="1" value="1" />
	        <VAR name="PADDING" len="3" value="0" />
	        <VAR name="MMI_Q_TEXT_CRITERIA" len="4" value="3" />
	        <VAR name="MMI_I_TEXT" len="8" value="1" />
	        <VAR name="MMI_Q_TEXT" len="16" value="256" />
	        <VARi name="MMI_N_TEXT" elem="1" len="16" value="165" />
	        <!--  1  len = 80                                                      -->
	        <VAR name="MMI_X_TEXT" len="8" value="65" />
	        <!-- A -->
	        <VAR name="MMI_X_TEXT" len="8" value="66" />
	        <!-- B -->
	        <VAR name="MMI_X_TEXT" len="8" value="67" />
	        <!-- C -->
	        <VAR name="MMI_X_TEXT" len="8" value="68" />
	        <!-- D -->
	        <VAR name="MMI_X_TEXT" len="8" value="69" />
	        <!-- E -->
	        <VAR name="MMI_X_TEXT" len="8" value="70" />
	        <!-- F -->
	        <VAR name="MMI_X_TEXT" len="8" value="71" />
	        <!-- G -->
	        <VAR name="MMI_X_TEXT" len="8" value="73" />
	        <!-- H -->
	        <VAR name="MMI_X_TEXT" len="8" value="74" />
	        <!-- I -->
	        <VAR name="MMI_X_TEXT" len="8" value="75" />
	        <!-- J -->
	        <!--  2  len = 80                                                      -->
	        <VAR name="MMI_X_TEXT" len="8" value="32" />
	        <!--  -->
	        <VAR name="MMI_X_TEXT" len="8" value="50" />
	        <!-- x -->
	        <VAR name="MMI_X_TEXT" len="8" value="67" />
	        <!-- C -->
	        <VAR name="MMI_X_TEXT" len="8" value="68" />
	        <!-- D -->
	        <VAR name="MMI_X_TEXT" len="8" value="69" />
	        <!-- E -->
	        <VAR name="MMI_X_TEXT" len="8" value="70" />
	        <!-- F -->
	        <VAR name="MMI_X_TEXT" len="8" value="71" />
	        <!-- G -->
	        <VAR name="MMI_X_TEXT" len="8" value="73" />
	        <!-- H -->
	        <VAR name="MMI_X_TEXT" len="8" value="74" />
	        <!-- I -->
	        <VAR name="MMI_X_TEXT" len="8" value="50" />
	        <!-- x -->
	        <!--  2  len = 80                                                      -->
	        <VAR name="MMI_X_TEXT" len="8" value="32" />
	        <!--  -->
	        <VAR name="MMI_X_TEXT" len="8" value="51" />
	        <!-- x -->
	        <VAR name="MMI_X_TEXT" len="8" value="67" />
	        <!-- C -->
	        <VAR name="MMI_X_TEXT" len="8" value="68" />
	        <!-- D -->
	        <VAR name="MMI_X_TEXT" len="8" value="69" />
	        <!-- E -->
	        <VAR name="MMI_X_TEXT" len="8" value="70" />
	        <!-- F -->
	        <VAR name="MMI_X_TEXT" len="8" value="71" />
	        <!-- G -->
	        <VAR name="MMI_X_TEXT" len="8" value="73" />
	        <!-- H -->
	        <VAR name="MMI_X_TEXT" len="8" value="74" />
	        <!-- I -->
	        <VAR name="MMI_X_TEXT" len="8" value="51" />
	        <!-- x -->
	        <!--  2  len = 80                                                      -->
	        <VAR name="MMI_X_TEXT" len="8" value="32" />
	        <!--  -->
	        <VAR name="MMI_X_TEXT" len="8" value="52" />
	        <!-- x -->
	        <VAR name="MMI_X_TEXT" len="8" value="67" />
	        <!-- C -->
	        <VAR name="MMI_X_TEXT" len="8" value="68" />
	        <!-- D -->
	        <VAR name="MMI_X_TEXT" len="8" value="69" />
	        <!-- E -->
	        <VAR name="MMI_X_TEXT" len="8" value="70" />
	        <!-- F -->
	        <VAR name="MMI_X_TEXT" len="8" value="71" />
	        <!-- G -->
	        <VAR name="MMI_X_TEXT" len="8" value="73" />
	        <!-- H -->
	        <VAR name="MMI_X_TEXT" len="8" value="74" />
	        <!-- I -->
	        <VAR name="MMI_X_TEXT" len="8" value="52" />
	        <!-- x -->
	        <!--  2  len = 80                                                      -->
	        <VAR name="MMI_X_TEXT" len="8" value="32" />
	        <!--  -->
	        <VAR name="MMI_X_TEXT" len="8" value="53" />
	        <!-- x -->
	        <VAR name="MMI_X_TEXT" len="8" value="67" />
	        <!-- C -->
	        <VAR name="MMI_X_TEXT" len="8" value="68" />
	        <!-- D -->
	        <VAR name="MMI_X_TEXT" len="8" value="69" />
	        <!-- E -->
	        <VAR name="MMI_X_TEXT" len="8" value="70" />
	        <!-- F -->
	        <VAR name="MMI_X_TEXT" len="8" value="71" />
	        <!-- G -->
	        <VAR name="MMI_X_TEXT" len="8" value="73" />
	        <!-- H -->
	        <VAR name="MMI_X_TEXT" len="8" value="74" />
	        <!-- I -->
	        <VAR name="MMI_X_TEXT" len="8" value="53" />
	        <!-- x -->
	        <!--  2  len = 80                                                      -->
	        <VAR name="MMI_X_TEXT" len="8" value="32" />
	        <!--  -->
	        <VAR name="MMI_X_TEXT" len="8" value="54" />
	        <!-- x -->
	        <VAR name="MMI_X_TEXT" len="8" value="67" />
	        <!-- C -->
	        <VAR name="MMI_X_TEXT" len="8" value="68" />
	        <!-- D -->
	        <VAR name="MMI_X_TEXT" len="8" value="69" />
	        <!-- E -->
	        <VAR name="MMI_X_TEXT" len="8" value="70" />
	        <!-- F -->
	        <VAR name="MMI_X_TEXT" len="8" value="71" />
	        <!-- G -->
	        <VAR name="MMI_X_TEXT" len="8" value="73" />
	        <!-- H -->
	        <VAR name="MMI_X_TEXT" len="8" value="74" />
	        <!-- I -->
	        <VAR name="MMI_X_TEXT" len="8" value="54" />
	        <!-- x -->
	        <!--  2  len = 80                                                      -->
	        <VAR name="MMI_X_TEXT" len="8" value="32" />
	        <!--  -->
	        <VAR name="MMI_X_TEXT" len="8" value="55" />
	        <!-- x -->
	        <VAR name="MMI_X_TEXT" len="8" value="67" />
	        <!-- C -->
	        <VAR name="MMI_X_TEXT" len="8" value="68" />
	        <!-- D -->
	        <VAR name="MMI_X_TEXT" len="8" value="69" />
	        <!-- E -->
	        <VAR name="MMI_X_TEXT" len="8" value="70" />
	        <!-- F -->
	        <VAR name="MMI_X_TEXT" len="8" value="71" />
	        <!-- G -->
	        <VAR name="MMI_X_TEXT" len="8" value="73" />
	        <!-- H -->
	        <VAR name="MMI_X_TEXT" len="8" value="74" />
	        <!-- I -->
	        <VAR name="MMI_X_TEXT" len="8" value="55" />
	        <!-- x -->
	        <!--  2  len = 80                                                      -->
	        <VAR name="MMI_X_TEXT" len="8" value="32" />
	        <!--  -->
	        <VAR name="MMI_X_TEXT" len="8" value="56" />
	        <!-- x -->
	        <VAR name="MMI_X_TEXT" len="8" value="67" />
	        <!-- C -->
	        <VAR name="MMI_X_TEXT" len="8" value="68" />
	        <!-- D -->
	        <VAR name="MMI_X_TEXT" len="8" value="69" />
	        <!-- E -->
	        <VAR name="MMI_X_TEXT" len="8" value="70" />
	        <!-- F -->
	        <VAR name="MMI_X_TEXT" len="8" value="71" />
	        <!-- G -->
	        <VAR name="MMI_X_TEXT" len="8" value="73" />
	        <!-- H -->
	        <VAR name="MMI_X_TEXT" len="8" value="74" />
	        <!-- I -->
	        <VAR name="MMI_X_TEXT" len="8" value="56" />
	        <!-- x -->
	        <!--  2  len = 80                                                      -->
	        <VAR name="MMI_X_TEXT" len="8" value="32" />
	        <!--  -->
	        <VAR name="MMI_X_TEXT" len="8" value="57" />
	        <!-- x -->
	        <VAR name="MMI_X_TEXT" len="8" value="67" />
	        <!-- C -->
	        <VAR name="MMI_X_TEXT" len="8" value="68" />
	        <!-- D -->
	        <VAR name="MMI_X_TEXT" len="8" value="69" />
	        <!-- E -->
	        <VAR name="MMI_X_TEXT" len="8" value="70" />
	        <!-- F -->
	        <VAR name="MMI_X_TEXT" len="8" value="71" />
	        <!-- G -->
	        <VAR name="MMI_X_TEXT" len="8" value="73" />
	        <!-- H -->
	        <VAR name="MMI_X_TEXT" len="8" value="74" />
	        <!-- I -->
	        <VAR name="MMI_X_TEXT" len="8" value="57" />
	        <!-- x -->
	        <!--  10  len = 80                                                      -->
	        <VAR name="MMI_X_TEXT" len="8" value="32" />
	        <!--  -->
	        <VAR name="MMI_X_TEXT" len="8" value="49" />
	        <!-- 1 -->
	        <VAR name="MMI_X_TEXT" len="8" value="48" />
	        <!-- 0 -->
	        <VAR name="MMI_X_TEXT" len="8" value="68" />
	        <!-- D -->
	        <VAR name="MMI_X_TEXT" len="8" value="69" />
	        <!-- E -->
	        <VAR name="MMI_X_TEXT" len="8" value="70" />
	        <!-- F -->
	        <VAR name="MMI_X_TEXT" len="8" value="71" />
	        <!-- G -->
	        <VAR name="MMI_X_TEXT" len="8" value="73" />
	        <!-- H -->
	        <VAR name="MMI_X_TEXT" len="8" value="49" />
	        <!-- 1 -->
	        <VAR name="MMI_X_TEXT" len="8" value="48" />
	        <!-- 0 -->
	        <!--  11  len = 80                                                      -->
	        <VAR name="MMI_X_TEXT" len="8" value="32" />
	        <!--  -->
	        <VAR name="MMI_X_TEXT" len="8" value="49" />
	        <!-- 1 -->
	        <VAR name="MMI_X_TEXT" len="8" value="49" />
	        <!-- 1 -->
	        <VAR name="MMI_X_TEXT" len="8" value="68" />
	        <!-- D -->
	        <VAR name="MMI_X_TEXT" len="8" value="69" />
	        <!-- E -->
	        <VAR name="MMI_X_TEXT" len="8" value="70" />
	        <!-- F -->
	        <VAR name="MMI_X_TEXT" len="8" value="71" />
	        <!-- G -->
	        <VAR name="MMI_X_TEXT" len="8" value="73" />
	        <!-- H -->
	        <VAR name="MMI_X_TEXT" len="8" value="49" />
	        <!-- 1 -->
	        <VAR name="MMI_X_TEXT" len="8" value="49" />
	        <!-- 1 -->
	        <!--  12  len = 80                                                      -->
	        <VAR name="MMI_X_TEXT" len="8" value="32" />
	        <!--  -->
	        <VAR name="MMI_X_TEXT" len="8" value="49" />
	        <!-- 1 -->
	        <VAR name="MMI_X_TEXT" len="8" value="50" />
	        <!-- 2 -->
	        <VAR name="MMI_X_TEXT" len="8" value="68" />
	        <!-- D -->
	        <VAR name="MMI_X_TEXT" len="8" value="69" />
	        <!-- E -->
	        <VAR name="MMI_X_TEXT" len="8" value="70" />
	        <!-- F -->
	        <VAR name="MMI_X_TEXT" len="8" value="71" />
	        <!-- G -->
	        <VAR name="MMI_X_TEXT" len="8" value="73" />
	        <!-- H -->
	        <VAR name="MMI_X_TEXT" len="8" value="49" />
	        <!-- 1 -->
	        <VAR name="MMI_X_TEXT" len="8" value="50" />
	        <!-- 2 -->
	        <!--  13  len = 80                                                      -->
	        <VAR name="MMI_X_TEXT" len="8" value="32" />
	        <!--  -->
	        <VAR name="MMI_X_TEXT" len="8" value="49" />
	        <!-- 1 -->
	        <VAR name="MMI_X_TEXT" len="8" value="51" />
	        <!-- 3 -->
	        <VAR name="MMI_X_TEXT" len="8" value="68" />
	        <!-- D -->
	        <VAR name="MMI_X_TEXT" len="8" value="69" />
	        <!-- E -->
	        <VAR name="MMI_X_TEXT" len="8" value="70" />
	        <!-- F -->
	        <VAR name="MMI_X_TEXT" len="8" value="71" />
	        <!-- G -->
	        <VAR name="MMI_X_TEXT" len="8" value="73" />
	        <!-- H -->
	        <VAR name="MMI_X_TEXT" len="8" value="49" />
	        <!-- 1 -->
	        <VAR name="MMI_X_TEXT" len="8" value="51" />
	        <!-- 3 -->
	        <!--  14  len = 80                                                      -->
	        <VAR name="MMI_X_TEXT" len="8" value="32" />
	        <!--  -->
	        <VAR name="MMI_X_TEXT" len="8" value="49" />
	        <!-- 1 -->
	        <VAR name="MMI_X_TEXT" len="8" value="52" />
	        <!-- 4 -->
	        <VAR name="MMI_X_TEXT" len="8" value="68" />
	        <!-- D -->
	        <VAR name="MMI_X_TEXT" len="8" value="69" />
	        <!-- E -->
	        <VAR name="MMI_X_TEXT" len="8" value="70" />
	        <!-- F -->
	        <VAR name="MMI_X_TEXT" len="8" value="71" />
	        <!-- G -->
	        <VAR name="MMI_X_TEXT" len="8" value="73" />
	        <!-- H -->
	        <VAR name="MMI_X_TEXT" len="8" value="49" />
	        <!-- 1 -->
	        <VAR name="MMI_X_TEXT" len="8" value="52" />
	        <!-- 4 -->
	        <!--  15  len = 80                                                      -->
	        <VAR name="MMI_X_TEXT" len="8" value="32" />
	        <!--  -->
	        <VAR name="MMI_X_TEXT" len="8" value="49" />
	        <!-- 1 -->
	        <VAR name="MMI_X_TEXT" len="8" value="53" />
	        <!-- 5 -->
	        <VAR name="MMI_X_TEXT" len="8" value="68" />
	        <!-- D -->
	        <VAR name="MMI_X_TEXT" len="8" value="69" />
	        <!-- E -->
	        <VAR name="MMI_X_TEXT" len="8" value="70" />
	        <!-- F -->
	        <VAR name="MMI_X_TEXT" len="8" value="71" />
	        <!-- G -->
	        <VAR name="MMI_X_TEXT" len="8" value="73" />
	        <!-- H -->
	        <VAR name="MMI_X_TEXT" len="8" value="49" />
	        <!-- 1 -->
	        <VAR name="MMI_X_TEXT" len="8" value="53" />
	        <!-- 5 -->
	        <!--  16  len = 80                                                      -->
	        <VAR name="MMI_X_TEXT" len="8" value="32" />
	        <!--  -->
	        <VAR name="MMI_X_TEXT" len="8" value="49" />
	        <!-- 1 -->
	        <VAR name="MMI_X_TEXT" len="8" value="54" />
	        <!-- 6 -->
	        <VAR name="MMI_X_TEXT" len="8" value="68" />
	        <!-- D -->
	        <VAR name="MMI_X_TEXT" len="8" value="69" />
	        <!-- E -->
	        <VAR name="MMI_X_TEXT" len="8" value="70" />
	        <!-- F -->
	        <VAR name="MMI_X_TEXT" len="8" value="71" />
	        <!-- G -->
	        <VAR name="MMI_X_TEXT" len="8" value="73" />
	        <!-- H -->
	        <VAR name="MMI_X_TEXT" len="8" value="49" />
	        <!-- 1 -->
	        <VAR name="MMI_X_TEXT" len="8" value="54" />
	        <!-- 6 -->
	        <!--  17  len = 40                                                      -->
	        <VAR name="MMI_X_TEXT" len="8" value="32" />
	        <!--  -->
	        <VAR name="MMI_X_TEXT" len="8" value="66" />
	        <!-- B -->
	        <VAR name="MMI_X_TEXT" len="8" value="67" />
	        <!-- C -->
	        <VAR name="MMI_X_TEXT" len="8" value="49" />
	        <!-- 1 -->
	        <VAR name="MMI_X_TEXT" len="8" value="55" />
	        <!-- 7 -->
	      </PACKET>
	    </MESSAGE>
	    <STOPLOG />
	  </Testcase>
	</Test>

*/

#endregion