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

#endregion

namespace Testcase.XML
{
    /// <summary>
    /// Values of 22.6.3.2.3.2.a.xml file
    /// </summary>
    static class XML_22_6_3_2_3_2_a
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            EVC40_MMICurrentMaintenanceData.MMI_Q_MD_DATASET = Variables.MMI_Q_MD_DATASET.WheelDiameter;
            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1 = Variables.MMI_M_SDU_WHEEL_SIZE.TechnicalRangeCheckFailed;
            EVC40_MMICurrentMaintenanceData.MMI_M_SDU_WHEEL_SIZE_1 = Variables.MMI_M_SDU_WHEEL_SIZE.TechnicalRangeCheckFailed;
            EVC40_MMICurrentMaintenanceData.MMI_M_WHEEL_SIZE_ERR = Variables.MMI_M_WHEEL_SIZE_ERR.TechnicalRangeCheckFailed;

            EVC40_MMICurrentMaintenanceData.Send();

        }
    }
}