using System;
using System.Collections.Generic;
using CL345;
using Testcase.DMITestCases;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.Telegrams.DMItoEVC
{
    /// <summary>
    /// This packet shall be sent when the driver has selected an ETCS or NTC level or has changed the inhibit status of an installed level.
    /// </summary>
    public static class EVC112_MMINewRbcData
    {
        private static TestcaseBase _pool;
        private static bool _checkResult;
        private static byte _nidData;
        private static uint[] _nidRadio;

        static string baseString0 = "DMI->ETCS: EVC-112 [MMI_NEW_RBC_DATA]";
        static string baseString1 = "CCUO_ETCS1NewRbcData_EVC112NewRbcDataSub";

        /// <summary>
        /// Initialise EVC-112 MMI_New_Rbc_Data telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(TestcaseBase pool)
        {
            _pool = pool;
            MMI_NID_DATA = new List<byte>();
            _pool.SITR.SMDCtrl.CCUO.ETCS1NewRbcData.Value = 0x0009;
            _pool.SITR.SMDStat.CCUO.ETCS1NewRbcData.Value = 0x00;
        }

        public static void CheckPacketContent()
        {
            // Check if telegram received flag has been set. Allows 20 seconds to enter train data.
            if (_pool.SITR.SMDStat.CCUO.ETCS1NewRbcData.WaitForCondition(Is.Equal, 1, 20000, 100))
            {
                // Check all static fields
                _checkResult = _pool.SITR.CCUO.ETCS1NewRbcData.MmiNidRbc.Value.Equals(MMI_NID_RBC) &
                               _pool.SITR.CCUO.ETCS1NewRbcData.MmiNidRadio.GetValueAtIndex(0).Equals(_nidRadio[0]) &
                               _pool.SITR.CCUO.ETCS1NewRbcData.MmiNidRadio.GetValueAtIndex(1).Equals(_nidRadio[1]) &
                               _pool.SITR.CCUO.ETCS1NewRbcData.MmiNidMn.Value.Equals(MMI_NID_MN) &
                               _pool.SITR.CCUO.ETCS1NewRbcData.MmiMButtons.Value.Equals((byte) MMI_M_BUTTONS);

                // If check passes
                if (_checkResult)
                {
                    _pool.TraceReport(baseString0 + Environment.NewLine +
                                      "MMI_NID_RBC = " + MMI_NID_RBC + Environment.NewLine +
                                      "MMI_NID_RADIO = " + _nidRadio[0] + _nidRadio[1] + Environment.NewLine +
                                      "MMI_NID_MN = " + MMI_NID_MN + Environment.NewLine +
                                      "MMI_M_BUTTONS = \"" + MMI_M_BUTTONS + "\"" + Environment.NewLine +
                                      "Result: PASSED.");
                }
                // Else display the real value extracted from EVC-106
                else
                {
                    _pool.TraceError(baseString0 + Environment.NewLine +
                                     "MMI_NID_RBC = \"" + _pool.SITR.CCUO.ETCS1NewRbcData.MmiNidRbc.Value + "\"" +
                                     Environment.NewLine +
                                     "MMI_NID_RADIO = " +
                                     _pool.SITR.CCUO.ETCS1NewRbcData.MmiNidRadio.GetValueAtIndex(0).ToString() +
                                     _pool.SITR.CCUO.ETCS1NewRbcData.MmiNidRadio.GetValueAtIndex(1).ToString() +
                                     Environment.NewLine +
                                     "MMI_NID_MN = \"" + _pool.SITR.CCUO.ETCS1NewRbcData.MmiNidMn.Value + "\"" +
                                     Environment.NewLine +
                                     "MMI_M_BUTTONS = \"" +
                                     Enum.GetName(typeof(Variables.MMI_M_BUTTONS_RBC_DATA),
                                         _pool.SITR.CCUO.ETCS1NewRbcData.MmiMButtons.Value) + Environment.NewLine +
                                     "Result: FAILED!");
                }

                // Compare number of data element.                
                if (_pool.SITR.CCUO.ETCS1NewRbcData.MmiNDataElements.Value.Equals(MMI_NID_DATA.Count))
                {
                    // if comparaison matches
                    _pool.TraceReport("MMI_N_DATA_ELEMENTS = \"" + MMI_NID_DATA.Count + "\" Result: PASSED.");

                    for (var _nidDataIndex = 0; _nidDataIndex < MMI_NID_DATA.Count; _nidDataIndex++)
                    {
                        _nidData = (byte) _pool.SITR.Client.Read(string.Format("{0}{1}_MmiNidData", baseString1,
                            _nidDataIndex));
                        // Compare each data element
                        _checkResult = _nidData.Equals(MMI_NID_DATA[_nidDataIndex]);

                        //if comparaison matches
                        if (_checkResult)
                        {
                            _pool.TraceReport(
                                string.Format("MMI_NID_DATA{0} = \"{1}\" Result: PASSED!", _nidDataIndex,
                                    MMI_NID_DATA[_nidDataIndex]));
                        }
                        // Else display the real value extracted from EVC-112
                        else
                        {
                            _pool.TraceError(string.Format("MMI_NID_DATA{0} = \"{1}\" Result: FAILED!", _nidDataIndex,
                                _nidData));
                        }
                    }
                }
                else
                {
                    // Else display the real value extracted from EVC-112
                    _pool.TraceError(baseString0 + Environment.NewLine +
                                     "MMI_N_DATA_ELEMENTS = \"" +
                                     _pool.SITR.CCUO.ETCS1NewRbcData.MmiNDataElements.Value + "\" Result: FAILED!");

                    // Display data elements value indicating that the test has failed
                    for (var _nidDataIndex = 0;
                        _nidDataIndex < _pool.SITR.CCUO.ETCS1NewRbcData.MmiNDataElements.Value;
                        _nidDataIndex++)
                    {
                        _nidData = (byte) _pool.SITR.Client.Read(string.Format("{0}{1}_MmiNidData", baseString1,
                            _nidDataIndex));
                        _pool.TraceError(string.Format("{0}", baseString0) + Environment.NewLine +
                                         string.Format("MMI_NID_DATA{0} = \"{1}\" Result: FAILED!", _nidDataIndex,
                                             _nidData));
                    }
                }
            }
            // EVC-112 could not be received
            else
            {
                DmiExpectedResults.DMItoEVC_Telegram_Not_Received(_pool, baseString1);
            }

            // Reset telegram received flag in RTSim
            _pool.SITR.SMDStat.CCUO.ETCS1NewRbcData.Value = 0x00;
        }

        /// <summary>
        /// This variable provides the ETCS ID of an RBC. Each RBC belongs to a certain NID_C.
        /// The contents of the variable is the result of a concatenation of NID_C(10 bits) + NID_RBC(14 bits).
        /// This variable must not be mixed up with NID_RBC as defined in [SRS_026] part 7 chapter ‘NID_RBC’.
        /// 
        /// Values:
        /// 0..16777214 = ""
        /// Note:
        /// Bit 0..9 contain 'NID_C'
        /// Bits 10..23 contain 'NID_RBC'
        /// Bits 24..31 = 'spare'
        /// Special Value NID_RBC = 16383 - 'contact last known RBC'
        /// 
        /// 24 bit(10 bit unsigned int for NID_C +
        /// 14 bit unsigned int for NID_RBC)
        /// </summary>
        public static uint MMI_NID_RBC { get; set; }

        /// <summary>
        /// RBC phone number
        /// </summary>
        public static ulong MMI_NID_RADIO
        {
            set
            {
                var bytes = BitConverter.GetBytes(value);
                _nidRadio = new[] {BitConverter.ToUInt32(bytes, 2), BitConverter.ToUInt32(bytes, 0)};
            }
        }

        /// <summary>
        /// Selected radio Network ID index starting from 0.
        /// 
        /// Values:
        /// 0..254 = "Selection"
        /// 255 = "Unknown"
        /// Note: Selected Network ID index starting from 0.
        /// </summary>
        public static uint MMI_NID_MN { get; set; }

        /// <summary>
        /// Only MMI Buttons used in EVC-112. 
        /// 
        /// Values:        
        /// 36 = "BTN_YES_DATA_ENTRY_COMPLETE"
        /// 37 = "BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE"       
        /// 253 = "BTN_ENTER_DELAY_TYPE"
        /// 254 = "BTN_ENTER"
        /// 255 = "no button"
        /// Note: the definition is according to preliminary SubSet-121 'M_BUTTONS' definition.
        /// </summary>
        public static Variables.MMI_M_BUTTONS_RBC_DATA MMI_M_BUTTONS { get; set; }

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