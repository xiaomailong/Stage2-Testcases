using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using CL345;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.DMITestCases;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using static Testcase.Telegrams.EVCtoDMI.Variables;

namespace Testcase.Telegrams.DMItoEVC
{
    /// <summary>
    /// This packet shall be sent when the driver has selected an ETCS or NTC level or has changed the inhibit status of an installed level.
    /// </summary>
    public static class EVC106_MMINewSrRules
    {
        private static SignalPool _pool;
        private static bool _checkResult;  

        static string baseString0 = "DMI->ETCS: EVC-106 [MMI_NEW_SR_RULES]";
        static string baseString1 = "CCUO_ETCS1NewSrRules_EVC106NewSrRulesSub";

        /// <summary>
        /// Initialise EVC-106 MMI_New_Sr_Rules telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            MMI_N_DATA_ELEMENTS = new List<byte>();
            _pool.SITR.SMDCtrl.CCUO.ETCS1NewSrRules.Value = 0x0009;
            _pool.SITR.SMDStat.CCUO.ETCS1NewSrRules.Value = 0x00;
        }

        public static void CheckPacketContent()
        {
            // Reset telegram received flag in RTSim
            _pool.SITR.SMDStat.CCUO.ETCS1NewSrRules.Value = 0x00;

            // Check if telegram received flag has been set. Allows 20 seconds to enter train data.
            if (_pool.SITR.SMDStat.CCUO.ETCS1NewSrRules.WaitForCondition(Is.Equal, 1, 20000, 100))
            {
                // Check all static fields
                _checkResult = _pool.SITR.CCUO.ETCS1NewSrRules.MmiLStff.Value.Equals(MMI_L_STFF) &
                               _pool.SITR.CCUO.ETCS1NewSrRules.MmiVStff.Value.Equals(MMI_V_STFF) &
                               _pool.SITR.CCUO.ETCS1NewSrRules.MmiMButtons.Value.Equals((byte) MMI_M_BUTTONS);
            }
            // EVC-106 could not be received
            else
            {
                DmiExpectedResults.DMItoEVC_Telegram_Not_Received(_pool, baseString1);
            }
        }

        /// <summary>
        /// Distance on which the train is allowed to run in Staff Responsible mode.
        /// 
        /// Values:
        /// 0..100000 = "Distance in Staff Responsible Mode"
        /// 100001..4294967295  = "Not Used"
        /// </summary>
        public static uint MMI_L_STFF { get; set; }

        /// <summary>
        /// Speed value to override the default max Staff Responsible speed in the system
        /// 
        /// Values:
        /// 0..600 = "Speed Value"
        /// 601..65535 = "Reserved"
        /// 
        /// Note:
        /// According[SS026] Ch. 3, A.3.11 and Ch. 7.5.1, all speed resolution have 5 km/h.
        /// </summary>
        public static ushort MMI_V_STFF { get; set; }

        /// <summary>
        /// Only MMI Buttons used in EVC-106. 
        /// 
        /// Values:        
        /// 36 = "BTN_YES_DATA_ENTRY_COMPLETE"
        /// 37 = "BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE"       
        /// 253 = "BTN_ENTER_DELAY_TYPE"
        /// 254 = "BTN_ENTER"
        /// 255 = "no button"
        /// Note: the definition is according to preliminary SubSet-121 'M_BUTTONS' definition.
        /// </summary>
        public static MMI_M_BUTTONS_SR_RULES MMI_M_BUTTONS { get; set; }


        public static List<byte> MMI_N_DATA_ELEMENTS { get; set; }
       
    }
}