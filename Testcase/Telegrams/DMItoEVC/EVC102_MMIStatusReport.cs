using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using CL345;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.Telegrams.DMItoEVC
{
    /// <summary>
    /// This packet shall be sent when the driver requests for an action from the ATP, typically by pressing a button.
    /// </summary>
    static class EVC102_MMIStatusReport
    {
        private static SignalPool _pool;
        private static bool _bResult;
        private static MMI_M_MODE_READBACK _mModeReadBack;
        private static Variables.MMI_M_ACTIVE_CABIN _mActiveCabin;

        /// <summary>
        /// Initialise EVC-102 MMI_Status_Report telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;            
        }

        private static void CheckActiveCabin(Variables.MMI_M_ACTIVE_CABIN mActiveCabin)
        {
            //Get EVC102_alias_1_B0
            byte _evc102alias1B0 = _pool.SITR.CCUO.ETCS1StatusReport.EVC102alias1B0.Value;
            // Extract MMI_M_ACTIVE_CABIN (4th and 5th bits according to VSIS 2.9)
            byte _mmiMActiveCabin = (byte)((_evc102alias1B0 & 0x30) >> 4); // xxxx xxxx -> 00xx 0000 -> 0000 00xx

            //For each element of enum MMI_M_ACTIV_CABIN
            foreach (Variables.MMI_M_ACTIVE_CABIN mmiMActiveCabinElement in Enum.GetValues(typeof(Variables.MMI_M_ACTIVE_CABIN)))
            {
                //Compare to the value to be checked
                if (mmiMActiveCabinElement == mActiveCabin)
                {
                    // Check MMI_M_ACTIV_CABIN value
                    _bResult = _mmiMActiveCabin.Equals(mActiveCabin);
                    break;
                }
            }

            if (_bResult) //if check passes
            {
                _pool.TraceReport("DMI->ETCS: EVC-102 [MMI_STATUS_REPORT.MMI_M_ACTIVE_CABIN] = " + mActiveCabin +
                    " - \"" + mActiveCabin.ToString() + "\" PASSED.");
            }
            else // else display the real value extracted from EVC-102 [MMI_STATUS_REPORT.MMI_M_MODE_READBACK] 
            {
                _pool.TraceError("DMI->ETCS: Check EVC-102 [MMI_STATUS_REPORT.MMI_M_MODE_READBACK] = " +
                    _mmiMActiveCabin + " - \"" +
                    Enum.GetName(typeof(Variables.MMI_M_ACTIVE_CABIN), _mmiMActiveCabin) + "\" FAILED.");
            }
        }

        private static void CheckModeReadBack(MMI_M_MODE_READBACK mModeReadBack)
        {
            //For each element of enum MMI_M_MODE_READBACK 
            foreach (MMI_M_MODE_READBACK mmiMModeReadBackElement in Enum.GetValues(typeof(MMI_M_MODE_READBACK)))
            {
                //Compare to the value to be checked
                if (mmiMModeReadBackElement == mModeReadBack)
                {
                    // Check MMI_M_MODE_READBACK value
                    _bResult = _pool.SITR.CCUO.ETCS1StatusReport.MmiMModeReadback.Value.Equals(mModeReadBack);
                    break;
                }
            }
            
            if (_bResult) //if check passes
            {
                _pool.TraceReport("DMI->ETCS: EVC-102 [MMI_STATUS_REPORT.MMI_M_MODE_READBACK] = " + mModeReadBack +
                    " - \"" + mModeReadBack.ToString() + "\" PASSED.");
            }
            else // else display the real value extracted from EVC-102 [MMI_STATUS_REPORT.MMI_M_MODE_READBACK] 
            {
                _pool.TraceError("DMI->ETCS: Check EVC-102 [MMI_STATUS_REPORT.MMI_M_MODE_READBACK] = " + 
                    _pool.SITR.CCUO.ETCS1StatusReport.MmiMModeReadback.Value + " - \"" + 
                    Enum.GetName(typeof(MMI_M_MODE_READBACK), _pool.SITR.CCUO.ETCS1StatusReport.MmiMModeReadback.Value ) +
                    "\" FAILED.");
            }
        }

        /// <summary>
        /// Defines the identity of the activated cabin
        /// Values:
        /// 0 = "No cabin is active"
        /// 1 = "Cabin 1 is active"
        /// 2 = "Cabin 2 is active"
        /// 3 = "Spare"
        /// </summary>
        public static Variables.MMI_M_ACTIVE_CABIN Check_MMI_M_ACTIVE_CABIN
        {
            set
            {
                _mActiveCabin = value;
                CheckActiveCabin(_mActiveCabin);
            }
        }

        /// <summary>
        /// Contains the current mode as shown on the DMI (bit-inverted compared to the received mode)
        /// 
        /// Values:
        /// 0 = "Shown Mode Invalid"
        /// 1 = "No Mode displayed"
        /// 255 = "FS - Full Supervision"
        /// 254 = "OS - On-sight"
        /// 253 = "SR - Staff Responsible"
        /// 252 = "SH - Shunting"
        /// 251 = "UN - Unfitted"
        /// 250 = "SL - Sleeping"
        /// 249 = "SB - Standby"
        /// 248 = "TR - Trip"
        /// 247 = "PT - Post trip"
        /// 246 = "SF - System failure"
        /// 245 = "IS - Isolation"
        /// 244 = "NL - Non-leading"
        /// 243 = "LS - Limited Supervision"
        /// 242 = "SN - National System"
        /// 241 = "RV - Reversing"
        /// 240 = "PS - Passive Shunting"
        /// 239 = "NP - No Power"
        /// 2..238 = "Not used"
        /// Note: The Read-Back values of the mode shall be bit-inverted compared to the sent mode.
        /// </summary>
        public static MMI_M_MODE_READBACK Check_MMI_M_MODE_READBACK
        {
            set
            {
                _mModeReadBack = value;
                CheckModeReadBack(_mModeReadBack);
            }
        }

        /// <summary>
        /// Enum used for Check_MMI_M_MODE_READBACK
        /// </summary>
        public enum MMI_M_MODE_READBACK : ushort
        {
            ShownModeInvalid = 0,
            NoModeDisplayed = 1,
            FullSupervision = 255,
            OnSight = 254,
            StaffResponsible = 253,
            Shunting = 252,
            Unfitted = 251,
            Sleeping = 250,
            StandBy = 249,
            Trip = 248,
            PostTrip = 247,
            SystemFailure = 246,
            Isolation = 245,
            NonLeading = 244,
            LimitedSupervision = 243,
            NationalSystem = 242,
            Reversing = 241,
            PassiveShunting = 240,
            NoPower = 239
        }
    }
}