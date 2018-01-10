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
        private static byte _nidData;

        static string baseString0 = "DMI->ETCS: EVC-106 [MMI_NEW_SR_RULES]";
        static string baseString1 = "CCUO_ETCS1NewSrRules_EVC106NewSrRulesSub";

        /// <summary>
        /// Initialise EVC-106 MMI_New_Sr_Rules telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            MMI_NID_DATA = new List<byte>();
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

                // If check passes
                if (_checkResult)
                {
                    _pool.TraceReport(baseString0 + Environment.NewLine +
                                      "MMI_L_STFF = " + MMI_L_STFF + Environment.NewLine +
                                      "MMI_V_STFF = " + MMI_V_STFF + Environment.NewLine +
                                      "MMI_M_BUTTONS = \"" + MMI_M_BUTTONS + "\"" + Environment.NewLine +
                                      "Result: PASSED.");
                }
                // Else display the real value extracted from EVC-106
                else
                {
                    _pool.TraceError(baseString0 + Environment.NewLine +
                                     "MMI_L_STFF = \"" + _pool.SITR.CCUO.ETCS1NewSrRules.MmiLStff.Value + "\"" +
                                     Environment.NewLine +
                                     "MMI_V_STFF = \"" + _pool.SITR.CCUO.ETCS1NewSrRules.MmiVStff.Value + "\"" +
                                     Environment.NewLine +
                                     "MMI_M_BUTTONS = \"" +
                                     Enum.GetName(typeof(MMI_M_BUTTONS_SR_RULES),
                                         _pool.SITR.CCUO.ETCS1NewSrRules.MmiMButtons.Value) + Environment.NewLine +
                                     "Result: FAILED!");
                }

                // Compare number of data element.                
                if (_pool.SITR.CCUO.ETCS1NewSrRules.MmiNDataElements.Value.Equals(MMI_NID_DATA.Count))
                {
                    // if comparaison matches
                    _pool.TraceReport("MMI_N_DATA_ELEMENTS = \"" + MMI_NID_DATA.Count + "\" Result: PASSED.");

                    for (var _nidDataIndex = 0; _nidDataIndex < MMI_NID_DATA.Count; _nidDataIndex++)
                    {
                        _nidData = (byte) _pool.SITR.Client.Read($"{baseString1}{_nidDataIndex}_MmiNidData");
                        // Compare each data element
                        _checkResult = _nidData.Equals(MMI_NID_DATA[_nidDataIndex]);

                        //if comparaison matches
                        if (_checkResult)
                        {
                            _pool.TraceReport(
                                $"MMI_NID_DATA{_nidDataIndex} = \"{MMI_NID_DATA[_nidDataIndex]}\" Result: PASSED!");
                        }
                        // Else display the real value extracted from EVC-106
                        else
                        {
                            _pool.TraceError($"MMI_NID_DATA{_nidDataIndex} = \"{_nidData}\" Result: FAILED!");
                        }
                    }
                }
                else
                {
                    // Else display the real value extracted from EVC-106
                    _pool.TraceError(baseString0 + Environment.NewLine +
                                     "MMI_N_DATA_ELEMENTS = \"" +
                                     _pool.SITR.CCUO.ETCS1NewSrRules.MmiNDataElements.Value + "\" Result: FAILED!");

                    // Display data elements value indicating that the test has failed
                    for (var _nidDataIndex = 0;
                        _nidDataIndex < _pool.SITR.CCUO.ETCS1NewSrRules.MmiNDataElements.Value;
                        _nidDataIndex++)
                    {
                        _nidData = (byte) _pool.SITR.Client.Read($"{baseString1}{_nidDataIndex}_MmiNidData");
                        _pool.TraceError($"{baseString0}" + Environment.NewLine +
                                         $"MMI_NID_DATA{_nidDataIndex} = \"{_nidData}\" Result: FAILED!");
                    }
                }
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

        /// <summary>
        /// List of data entry element identifier.
        /// 
        /// Values:
        /// 0 = "Train running number"
        /// 1 = "ERTMS/ETCS Level"
        /// 2 = "Driver ID"
        /// 3 = "Radio network ID"
        /// 4 = "RBC ID"
        /// 5 = "RBC phone number"
        /// 6 = "Train Type (Train Data Set Identifier)"
        /// 7 = "Train category"
        /// 8 = "Length"
        /// 9 = "Brake percentage"
        /// 10 = "Maximun speed"
        /// 11 = "Axle load category"
        /// 12 = "Airtight"
        /// 13 = "Loading gauge"
        /// 14 = "Operated system version"
        /// 15 = "SR Speed"
        /// 16 = "SR Distance"
        /// 17 = "Adhesion"
        /// 18 = "Set VBC code"
        /// 19 = "Remove VBC code"
        /// 20..254 = "spare"
        /// 255 = "no specific data element defined"
        /// 
        /// Note: the definition is according to preliminary SubSet-121 'NID_DATA' definition.
        /// </summary>
        public static List<byte> MMI_NID_DATA { get; set; }
    }
}