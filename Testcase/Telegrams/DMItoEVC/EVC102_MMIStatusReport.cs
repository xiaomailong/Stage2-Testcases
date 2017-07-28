using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using CL345;

namespace Testcase.Telegrams
{
    /// <summary>
    /// This packet shall be sent when the driver requests for an action from the ATP, 
    /// typically by pressing a button..
    /// </summary>
    static class EVC102_MMIStatusReport
    {
        private static SignalPool _pool;
        private static bool _bResult;
        private static string _modeRead;

        public static EVC102modeReadback Evc102modeReadback { get; set; }

        public static void ReadMode()
        {
            switch (Evc102modeReadback)
            {
                case EVC102modeReadback.FullSupervision:
                    _modeRead = "FS - Full Supervision";
                    _bResult = _pool.SITR.CCUO.ETCS1StatusReport.MmiMModeReadback.Equals(255);
                    break;
                case EVC102modeReadback.OnSight:
                    _modeRead = "OS - On Sight";
                    _bResult = _pool.SITR.CCUO.ETCS1StatusReport.MmiMModeReadback.Equals(254);
                    break;
                case EVC102modeReadback.StaffResponsible:
                    _modeRead = "SR - Staff Responsible";
                    _bResult = _pool.SITR.CCUO.ETCS1StatusReport.MmiMModeReadback.Equals(253);
                    break;
                case EVC102modeReadback.Shunting:
                    _modeRead = "SH - Shunting";
                    _bResult = _pool.SITR.CCUO.ETCS1StatusReport.MmiMModeReadback.Equals(252);
                    break;
                case EVC102modeReadback.Unfitted:
                    _modeRead = "UN - Unfitted";
                    _bResult = _pool.SITR.CCUO.ETCS1StatusReport.MmiMModeReadback.Equals(251);
                    break;
                case EVC102modeReadback.Sleeping:
                    _modeRead = "SL - Sleeping";
                    _bResult = _pool.SITR.CCUO.ETCS1StatusReport.MmiMModeReadback.Equals(250);
                    break;
                case EVC102modeReadback.StandBy:
                    _modeRead = "SB - Stand By";
                    _bResult = _pool.SITR.CCUO.ETCS1StatusReport.MmiMModeReadback.Equals(249);
                    break;
                case EVC102modeReadback.Trip:
                    _modeRead = "TR - Trip";
                    _bResult = _pool.SITR.CCUO.ETCS1StatusReport.MmiMModeReadback.Equals(248);
                    break;
                case EVC102modeReadback.PostTrip:
                    _modeRead = "PT - Post Trip";
                    _bResult = _pool.SITR.CCUO.ETCS1StatusReport.MmiMModeReadback.Equals(247);
                    break;
                case EVC102modeReadback.SystemFailure:
                    _modeRead = "SF - System Failure";
                    _bResult = _pool.SITR.CCUO.ETCS1StatusReport.MmiMModeReadback.Equals(246);
                    break;
                case EVC102modeReadback.Isolation:
                    _modeRead = "IS - Isolation";
                    _bResult = _pool.SITR.CCUO.ETCS1StatusReport.MmiMModeReadback.Equals(245);
                    break;
                case EVC102modeReadback.NonLeading:
                    _modeRead = "NL - Non Leading";
                    _bResult = _pool.SITR.CCUO.ETCS1StatusReport.MmiMModeReadback.Equals(244);
                    break;
                case EVC102modeReadback.LimitedSupervision:
                    _modeRead = "LS - Limited Supervision";
                    _bResult = _pool.SITR.CCUO.ETCS1StatusReport.MmiMModeReadback.Equals(243);
                    break;
                case EVC102modeReadback.NationalSystem:
                    _modeRead = "SN - National System";
                    _bResult = _pool.SITR.CCUO.ETCS1StatusReport.MmiMModeReadback.Equals(242);
                    break;
                case EVC102modeReadback.Reversing:
                    _modeRead = "RV - Reversing";
                    _bResult = _pool.SITR.CCUO.ETCS1StatusReport.MmiMModeReadback.Equals(241);
                    break;
                case EVC102modeReadback.PassiveShunting:
                    _modeRead = "PS - Passive Shunting";
                    _bResult = _pool.SITR.CCUO.ETCS1StatusReport.MmiMModeReadback.Equals(240);
                    break;
                case EVC102modeReadback.NoPower:
                    _modeRead = "NP - No Power";
                    _bResult = _pool.SITR.CCUO.ETCS1StatusReport.MmiMModeReadback.Equals(239);
                    break;
            }

            if (_bResult)
            {
                _pool.TraceReport("DMI->ETCS: EVC-102 [MMI_STATUS_REPORT.MMI_M_MODE_READBACK] = \"" + _modeRead +
                                  "\" PASSED.");
            }
            else
            {
                _pool.TraceError("DMI->ETCS: Check EVC-102 [MMI_STATUS_REPORT.MMI_M_MODE_READBACK] = " +
                                 _pool.SITR.CCUO.ETCS1StatusReport.MmiMModeReadback.Value
                                 + "FAILED.");
            }
        }

        public enum EVC102modeReadback
        {
            ShownModeInvalid,
            NoModeDisplayed,
            FullSupervision,
            OnSight,
            StaffResponsible,
            Shunting,
            Unfitted,
            Sleeping,
            StandBy,
            Trip,
            PostTrip,
            SystemFailure,
            Isolation,
            NonLeading,
            LimitedSupervision,
            NationalSystem,
            Reversing,
            PassiveShunting,
            NoPower
        }
    }
}