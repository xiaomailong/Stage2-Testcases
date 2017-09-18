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
    /// Values of 18.6.2.a.xml file
    /// </summary>
    static class XML_18_6_2_a
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            // Step 1
            EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 0;

            TrackCondition trackCondition0 = new TrackCondition
            {
                MMI_O_TRACKCOND_ANNOUNCE = 0,
                MMI_O_TRACKCOND_START = 0,
                MMI_O_TRACKCOND_END = 0,
                MMI_NID_TRACKCOND = 0,
                MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph,
                MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction,
                MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
            };
            TrackCondition trackCondition1 = new TrackCondition
            {
                MMI_O_TRACKCOND_ANNOUNCE = 0,
                MMI_O_TRACKCOND_START = 0,
                MMI_O_TRACKCOND_END = 0,
                MMI_NID_TRACKCOND = 1,
                MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph,
                MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active,
                MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction,
                MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
            };
            TrackCondition trackCondition2 = new TrackCondition
            {
                MMI_O_TRACKCOND_ANNOUNCE = 0,
                MMI_O_TRACKCOND_START = 0,
                MMI_O_TRACKCOND_END = 0,
                MMI_NID_TRACKCOND = 2,
                MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph,
                MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.LeaveArea,
                MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction,
                MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction
            };

            List<TrackCondition> trackConditionList = new List<TrackCondition> { trackCondition0, trackCondition1, trackCondition2 };

            EVC32_MMITrackConditions.TrackConditions = trackConditionList;

            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI displays symbol TC02 in sub-area B3." + Environment.NewLine +
                                      "2. DMI displays symbol TC01 in sub-area B4." + Environment.NewLine +
                                      "3. DMI displays symbol TC04 in sub-area B5.");

            // Step 2
            EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 1;
            
            trackCondition0.MMI_NID_TRACKCOND = 3;
            trackCondition0.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Main_power_switch_Neutral_Section;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            trackCondition0.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
            trackCondition0.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
        
            trackCondition1.MMI_NID_TRACKCOND = 4;
            trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Main_power_switch_Neutral_Section;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
        
            trackCondition1.MMI_NID_TRACKCOND = 5;
            trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Main_power_switch_Neutral_Section;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.LeaveArea;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;

            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI still displays symbol TC02 in sub-area B3." + Environment.NewLine +
                                      "2. DMI still displays symbol TC01 in sub-area B4." + Environment.NewLine +
                                      "3. DMI still displays symbol TC04 in sub-area B5.");

            // Step 3
            trackCondition0.MMI_NID_TRACKCOND = 6;

            trackCondition0.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Non_Stopping_Area;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            trackCondition0.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
            trackCondition0.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            trackCondition1.MMI_NID_TRACKCOND = 7;
            trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Radio_hole;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            trackCondition1.MMI_NID_TRACKCOND = 8;
            trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Magnetic_Shoe_Brakes;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            
            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI still displays symbol TC02 in sub-area B3." + Environment.NewLine +
                                      "2. DMI still displays symbol TC01 in sub-area B4." + Environment.NewLine +
                                      "3. DMI still displays symbol TC04 in sub-area B5.");
            
            // Step 4            
            trackCondition0.MMI_NID_TRACKCOND = 9;

            trackCondition0.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Magnetic_Shoe_Brakes;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
            trackCondition0.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            trackCondition0.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            trackCondition1.MMI_NID_TRACKCOND = 10;
            trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Eddy_Current_Brakes;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            trackCondition1.MMI_NID_TRACKCOND = 11;
            trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Eddy_Current_Brakes;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI still displays symbol TC02 in sub-area B3." + Environment.NewLine +
                                      "2. DMI still displays symbol TC01 in sub-area B4." + Environment.NewLine +
                                      "3. DMI still displays symbol TC04 in sub-area B5.");

            // Step 5           
            trackCondition0.MMI_NID_TRACKCOND = 12;

            trackCondition0.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Regenerative_Brakes;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            trackCondition0.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
            trackCondition0.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            trackCondition1.MMI_NID_TRACKCOND = 13;
            trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Regenerative_Brakes;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            trackCondition1.MMI_NID_TRACKCOND = 14;
            trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Air_tightness;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI still displays symbol TC02 in sub-area B3." + Environment.NewLine +
                                      "2. DMI still displays symbol TC01 in sub-area B4." + Environment.NewLine +
                                      "3. DMI still displays symbol TC04 in sub-area B5.");

            // Step 6
            trackCondition0.MMI_NID_TRACKCOND = 15;

            trackCondition0.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Air_tightness;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
            trackCondition0.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            trackCondition0.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            trackCondition1.MMI_NID_TRACKCOND = 16;
            trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Air_tightness;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.LeaveArea;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;

            trackCondition1.MMI_NID_TRACKCOND = 17;
            trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Change_traction_not_fitted;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI still displays symbol TC02 in sub-area B3." + Environment.NewLine +
                                      "2. DMI still displays symbol TC01 in sub-area B4." + Environment.NewLine +
                                      "3. DMI still displays symbol TC04 in sub-area B5.");

            // Step 7
            trackCondition0.MMI_NID_TRACKCOND = 18;

            trackCondition0.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Change_traction_not_fitted;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
            trackCondition0.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            trackCondition0.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            trackCondition1.MMI_NID_TRACKCOND = 19;
            trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Change_traction_AC_25_kV_50_Hz;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            trackCondition1.MMI_NID_TRACKCOND = 20;
            trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Change_traction_AC_25_kV_50_Hz;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI still displays symbol TC02 in sub-area B3." + Environment.NewLine +
                                      "2. DMI still displays symbol TC01 in sub-area B4." + Environment.NewLine +
                                      "3. DMI still displays symbol TC04 in sub-area B5.");

            // Step 8
            trackCondition0.MMI_NID_TRACKCOND = 21;

            trackCondition0.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Change_traction_AC_15_kV_16_7_Hz;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            trackCondition0.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
            trackCondition0.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            trackCondition1.MMI_NID_TRACKCOND = 22;
            trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Change_traction_AC_15_kV_16_7_Hz;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            trackCondition1.MMI_NID_TRACKCOND = 23;
            trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Change_traction_DC_3_kV;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI still displays symbol TC02 in sub-area B3." + Environment.NewLine +
                                      "2. DMI still displays symbol TC01 in sub-area B4." + Environment.NewLine +
                                      "3. DMI still displays symbol TC04 in sub-area B5.");

            // Step 9
            trackCondition0.MMI_NID_TRACKCOND = 24;

            trackCondition0.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Change_traction_DC_3_kV;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
            trackCondition0.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            trackCondition0.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            trackCondition1.MMI_NID_TRACKCOND = 25;
            trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Change_traction_DC_1_5_kV;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            trackCondition1.MMI_NID_TRACKCOND = 26;
            trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Change_traction_DC_1_5_kV;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI still displays symbol TC02 in sub-area B3." + Environment.NewLine +
                                      "2. DMI still displays symbol TC01 in sub-area B4." + Environment.NewLine +
                                      "3. DMI still displays symbol TC04 in sub-area B5.");

            // Step 10
            trackCondition0.MMI_NID_TRACKCOND = 27;
            trackCondition0.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Change_traction_DC_600_750;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            trackCondition0.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;
            trackCondition0.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            trackCondition1.MMI_NID_TRACKCOND = 28;
            trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Change_traction_DC_600_750;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            trackCondition1.MMI_NID_TRACKCOND = 29;
            trackCondition1.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Tunnel_Stopping_Area;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            trackCondition1.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI still displays symbol TC02 in sub-area B3." + Environment.NewLine +
                                      "2. DMI still displays symbol TC01 in sub-area B4." + Environment.NewLine +
                                      "3. DMI still displays symbol TC04 in sub-area B5." + Environment.NewLine +
                                      "4. DMI displays symbol TC36 in sub-area C2.");

            // Step 11   
            trackCondition0.MMI_NID_TRACKCOND = 30;
            trackCondition0.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph;
            trackCondition0.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            trackCondition0.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            trackCondition0.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            trackConditionList.Remove(trackCondition1);
            trackConditionList.Remove(trackCondition2);

            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI still displays symbol TC02 in sub-area B3." + Environment.NewLine +
                                      "2. DMI still displays symbol TC01 in sub-area B4." + Environment.NewLine +
                                      "3. DMI still displays symbol TC04 in sub-area B5.");

        }
    }
}