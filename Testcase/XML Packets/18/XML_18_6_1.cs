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
    /// Values of 18.6.1.xml file
    /// </summary>
    static class XML_18_6_1
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            // Step 1
            EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 0;

            TrackCondition trackCondition = new TrackCondition
            {
                MMI_O_TRACKCOND_ANNOUNCE = 0,
                MMI_O_TRACKCOND_START = 0,
                MMI_O_TRACKCOND_END = 0,
                MMI_NID_TRACKCOND = 0,
                MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Invalid,
                MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea,
                MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction,
                MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction
            };

            List<TrackCondition> trackConditionList = new List<TrackCondition> { trackCondition };

            EVC32_MMITrackConditions.TrackConditions = trackConditionList;

            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI displays no symbol in sub-areas B3-B5.");

            // Step 2
            trackCondition.MMI_NID_TRACKCOND = 1;
            trackCondition.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.Spare8;
            trackCondition.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI displays no symbol in sub-areas B3-B5.");
            
            // Step 3
            trackCondition.MMI_NID_TRACKCOND = 2;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            trackCondition.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;

            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI displays symbol TC02 in sub-area B3 without a flashing yellow frame.");

            // Step 4
            EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 1;

            trackCondition.MMI_NID_TRACKCOND = 3;
            trackCondition.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI displays symbol TC03 in sub-area B4 with a flashing yellow frame." + Environment.NewLine +
                                      "2. The ‘Sinfo’ sound is played.");
        
            // Step 5
            trackCondition.MMI_NID_TRACKCOND = 4;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.InsideArea_Active;
            trackCondition.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;

            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI displays symbol TC01 in sub-area B5.");

            // Step 6
            trackCondition.MMI_NID_TRACKCOND = 5;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.LeaveArea;
            trackCondition.MMI_Q_TRACKCOND_ACTION_START = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;
            trackCondition.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithoutDriverAction;

            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI does not change sub-areas B3-B5 (all areas are already displaying symbols.");
            
            // Step 7
            trackCondition.MMI_NID_TRACKCOND = 3;
            trackCondition.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Non_Stopping_Area;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.RemoveTC;
            trackCondition.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbol TC03 in sub-area B4." + Environment.NewLine +
                                      "2. DMI removes symbol TC01 from sub-area B5 and displays it in sub-area B4" + Environment.NewLine +
                                      "2. DMI displays symbol TC04 in sub-area B5");

            // Step 8
            trackCondition.MMI_NID_TRACKCOND = 6;
            trackCondition.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.AnnounceArea;
            trackCondition.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI does not change sub-areas B3-B5 (all areas are already displaying symbols.");

            // Step 9
            EVC32_MMITrackConditions.MMI_Q_TRACKCOND_UPDATE = 0;

            trackCondition.MMI_NID_TRACKCOND = 0;
            trackCondition.MMI_M_TRACKCOND_TYPE = Variables.MMI_M_TRACKCOND_TYPE.Pantograph;
            trackCondition.MMI_Q_TRACKCOND_STEP = Variables.MMI_Q_TRACKCOND_STEP.LeaveArea;
            trackCondition.MMI_Q_TRACKCOND_ACTION_END = Variables.MMI_Q_TRACKCOND_ACTION.WithDriverAction;

            EVC32_MMITrackConditions.Send();

            _pool.WaitForVerification("Check the following (TC-03 is not displayed because stored track conditions are deleted):" + Environment.NewLine + Environment.NewLine +
                                      "1. DMI removes symbols TC02, TC01 and TC04 from sub-areas B3-B5." + Environment.NewLine +
                                      "2. DMI displays symbol TC05 in sub-area B3");

        }
    }
}