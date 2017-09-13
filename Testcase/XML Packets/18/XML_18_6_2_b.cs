#region usings

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
using Testcase.DMITestCases;

#endregion

namespace Testcase.XML
{
    /// <summary>
    /// Values of 18.6.2.b.xml file
    /// </summary>
    static class XML_18_6_2_b
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            // Step 12
            EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 1;

            TrackCondition trackCondition = new TrackCondition
            {
                MMI_O_TRACKCOND_ANNOUNCE = 0,
                MMI_O_TRACKCOND_START = 0,
                MMI_O_TRACKCOND_END = 0,
                MMI_NID_TRACKCOND = 0,
                MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Non_Stopping_Area,
                MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC,
                MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction,
                MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
            };
            
            EVC32_MMITrackConditions.TrackConditions = new List<TrackCondition> { trackCondition };

            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC02 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC01 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC04 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC06 in sub-area B5.");

            // Step 13
            trackCondition.MMI_NID_TRACKCOND = 1;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC01 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC04 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC06 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC06 in sub-area B5.");


            // Step 14
            trackCondition.MMI_NID_TRACKCOND = 2;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC04 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC06 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC06 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC08 in sub-area B5.");

            // Step 15
            trackCondition.MMI_NID_TRACKCOND = 3;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC06 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC06 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC08 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC010 in sub-area B5.");
            
            // Step 16
            trackCondition.MMI_NID_TRACKCOND = 4;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC06 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC08 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC010 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC012 in sub-area B5.");

            // Step 17
            trackCondition.MMI_NID_TRACKCOND = 5;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC08 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC10 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC12 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC13 in sub-area B5.");

            // Step 18
            trackCondition.MMI_NID_TRACKCOND = 6;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC10 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC12 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC13 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC13 in sub-area B5.");

            // Step 19
            trackCondition.MMI_NID_TRACKCOND = 7;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC12 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC13 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC13 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC15 in sub-area B5.");

            // Step 20
            trackCondition.MMI_NID_TRACKCOND = 8;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC13 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC13 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC15 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC15 in sub-area B5.");

            // Step 21
            trackCondition.MMI_NID_TRACKCOND = 9;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC13 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC15 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC15 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC17 in sub-area B5.");

            // Step 22
            trackCondition.MMI_NID_TRACKCOND = 10;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC15 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC15 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC17 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC17 in sub-area B5.");

            // Step 23
            trackCondition.MMI_NID_TRACKCOND = 11;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC15 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC17 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC17 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC19 in sub-area B5.");
            
            // Step 24
            trackCondition.MMI_NID_TRACKCOND = 12;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC15 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC17 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC17 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC19 in sub-area B5.");

            // Step 25
            trackCondition.MMI_NID_TRACKCOND = 13;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC17 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC19 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC19 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC20 in sub-area B5.");
            
            // Step 26
            trackCondition.MMI_NID_TRACKCOND = 14;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC19 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC19 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC20 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC23 in sub-area B5.");
            
            // Step 27
            trackCondition.MMI_NID_TRACKCOND = 15;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC19 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC20 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC23 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC23 in sub-area B5.");
            
            // Step 28
            trackCondition.MMI_NID_TRACKCOND = 16;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC20 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC23 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC23 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC25 in sub-area B5.");
            
            // Step 29
            trackCondition.MMI_NID_TRACKCOND = 17;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC23 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC23 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC25 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC25 in sub-area B5.");
            
            // Step 30
            trackCondition.MMI_NID_TRACKCOND = 18;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC23 from sub-area B3" + Environment.NewLine +
                                      "2. DMI displays symbol TC25 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC25 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC27 in sub-area B5.");

            // Step 31
            trackCondition.MMI_NID_TRACKCOND = 19;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC25 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC25 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC27 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC27 in sub-area B5.");
            
            // Step 32
            trackCondition.MMI_NID_TRACKCOND = 20;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC25 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC27 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC27 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC29 in sub-area B5.");

            // Step 33
            trackCondition.MMI_NID_TRACKCOND = 21;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC27 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC27 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC29 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC29 in sub-area B5.");
            
            // Step 34
            trackCondition.MMI_NID_TRACKCOND = 22;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC27 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC29 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC29 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC31 in sub-area B5.");

            // Step 35
            trackCondition.MMI_NID_TRACKCOND = 23;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC29 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC29 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC31 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC31 in sub-area B5.");
            
            // Step 36
            trackCondition.MMI_NID_TRACKCOND = 24;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC29 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC31 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC31 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC33 in sub-area B5.");
            
            // Step 37
            trackCondition.MMI_NID_TRACKCOND = 25;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC31 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC31 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC33 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC33 in sub-area B5.");
            
            // Step 38
            trackCondition.MMI_NID_TRACKCOND = 26;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC31 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC33 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC33 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays symbol TC03 in sub-area B5.");
            
            // Step 39
            trackCondition.MMI_NID_TRACKCOND = 27;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC33 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC33 in sub-area B3." + Environment.NewLine +
                                      "3. DMI displays symbol TC03 in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays no symbol in sub-area B5.");

            // Step 40
            trackCondition.MMI_NID_TRACKCOND = 28;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC33 from sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC03 in sub-area B3 with a yellow flashing frame." + Environment.NewLine +
                                      "3. DMI displays no symbol in sub-area B4." + Environment.NewLine +
                                      "4. DMI displays no symbol in sub-area B5.");

            // Step 41
            trackCondition.MMI_NID_TRACKCOND = 29;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC36 from sub-area C1");
            
        }
    }
}