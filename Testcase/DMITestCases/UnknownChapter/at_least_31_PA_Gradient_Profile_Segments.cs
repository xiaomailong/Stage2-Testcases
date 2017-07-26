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

namespace Testcase.DMITestCases
{
    /// <summary>
    /// Handle at least 31 PA Gradient Profile Segments
    /// TC-ID: 17.12
    /// 
    /// This test case verifies that the DMI can handle at least 31 PA Gradient Profile segments. Also verify that the PA Gradient Profile is continuously updated the value and the gradient received from EVC-4 is applied at the correct position. This function shall comply with [MMI-ETCS-gen].
    /// 
    /// Tested Requirements:
    /// MMI_gen 7256;
    /// 
    /// Scenario:
    /// Activate Cabin A.Perform SoM in SR mode, Level 1.Drive train forward pass BG1 at 100mBG1: Packet 12, 21 and 27D_GRADIENT = 0, Q_GDIR=1, G_A=2N_ITER = 16D_GRADIENT = 200, Q_GDIR=1, G_A =4D_GRADIENT = 50, Q_GDIR=1, G_A =6D_GRADIENT = 150, Q_GDIR=1, G_A =8D_GRADIENT = 50, Q_GDIR=1, G_A =10D_GRADIENT = 100, Q_GDIR=1, G_A =12D_GRADIENT = 50, Q_GDIR=1, G_A =14D_GRADIENT = 50, Q_GDIR=1, G_A =16D_GRADIENT = 100, Q_GDIR=1, G_A =18D_GRADIENT = 50, Q_GDIR=1, G_A =20D_GRADIENT = 50, Q_GDIR=1, G_A =22D_GRADIENT = 150, Q_GDIR=1, G_A =24D_GRADIENT = 100, Q_GDIR=1, G_A =26D_GRADIENT = 50, Q_GDIR=1, G_A =28D_GRADIENT = 100, Q_GDIR=1, G_A =30D_GRADIENT = 50, Q_GDIR=1, G_A =32D_GRADIENT = 80, Q_GDIR=1, G_A =34Drive train forward pass BG2 at 200mBG2: Packet 12, 21 and 27D_GRADIENT=1400, Q_GDIR=0, G_A=1N_ITER = 16D_GRADIENT = 100, Q_GDIR=0, G_A =2D_GRADIENT = 200, Q_GDIR=0, G_A =4D_GRADIENT = 150, Q_GDIR=0, G_A =6D_GRADIENT = 120, Q_GDIR=0, G_A =8D_GRADIENT = 200, Q_GDIR=0, G_A =10D_GRADIENT = 50, Q_GDIR=0, G_A =12D_GRADIENT = 100, Q_GDIR=0, G_A =14D_GRADIENT = 150, Q_GDIR=0, G_A =16D_GRADIENT = 100, Q_GDIR=0, G_A =18D_GRADIENT = 300, Q_GDIR=0, G_A =20D_GRADIENT = 80, Q_GDIR=0, G_A =22D_GRADIENT = 200, Q_GDIR=0, G_A =24D_GRADIENT = 150, Q_GDIR=0, G_A =26D_GRADIENT = 100, Q_GDIR=0, G_A =28D_GRADIENT = 200, Q_GDIR=0, G_A =30D_GRADIENT = 100, Q_GDIR=0, G_A =32
    /// 
    /// Used files:
    /// 17_12.tdg
    /// </summary>
    public class at_least_31_PA_Gradient_Profile_Segments : TestcaseBase
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
            // Testcase entrypoint


            /*
            Test Step 1
            Action: Activate cabin A.
            Expected Result: DMI displays the default window. The Driver ID window is displayed.
            */

            /*
            Test Step 2
            Action: Driver performs SoM to SR mode, level 1.
            Expected Result: DMI is displayed in SR mode, level 1.
            */

            /*
            Test Step 3
            Action: Drive the train forward pass BG1.
            Expected Result: DMI changes from SR mode to FS mode.The planning area is displayed the PA Gradient Profile. DMI is able to handle more than 16 PA Gradient Profile segments. (see the figure in ‘Comment’ column)Note: PA Gradient Profile value are 2, 4, 6, …. 34. In grey segment bars
            */

            /*
            Test Step 4
            Action: Drive the train forward pass BG2.
            Expected Result: An information of PA Gradient Profile is updated.The planning area is displayed the PA Gradient Profile. DMI is able to handle more than 31 PA Gradient Profile segments.Note: PA Gradient Profile value are 1, 2, 4, 6, …. 32. In both of grey colour segment bars and dark-grey colour segment bars.Press ‘Scale Up’  button and it will increase the distance scale to [0…1000].
            Test Step Comment: MMI_gen 7256;
            */

            /*
            Test Step 5
            Action: End of Test.
            Expected Result: 
            */

            return GlobalTestResult;
        }
    }
}