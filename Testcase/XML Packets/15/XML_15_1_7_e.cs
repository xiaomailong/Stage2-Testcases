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
    /// Values of 15.1.7.a.xml file
    /// </summary>
    static class XML_15_1_7_e
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_EBTestInProgress = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_EB_Status = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_RadioStatus = 0;

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_STM_HS_ENABLED = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_STM_DA_ENABLED = 0;

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_BrakeTest_Status = 
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_BRAKETEST_STATUS.BrakeTestNotInProgress;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = 
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L0;

            // Value to check -	MMI_OBU_TR_M_MODE = 17 (“Not used”)
            _pool.SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrMMode.Value = 17;

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_ADHESION = 0;
            EVC7_MMIEtcsMiscOutSignals.OBU_TR_NID_STM_HS = 0;
            EVC7_MMIEtcsMiscOutSignals.OBU_TR_NID_STM_DA = 0;
            EVC7_MMIEtcsMiscOutSignals.BRAKE_TEST_TIMEOUT = 0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_O_TRAIN = 10000000;

            _pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity1.Value = 4096;
            _pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity2.Value = 1;
            _pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7SSW1.Value = 0;
            _pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7SSW2.Value = 0;
            _pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7SSW3.Value = 0;
            _pool.SITR.Client.Write("ETCS1_EtcsMiscOutSignals_SDT_UDV", 0);

            _pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
                                     "There is no symbol displayed on sub-area B7.");
        }
    }
}