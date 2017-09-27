using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using CL345;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.DMITestCases;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;


namespace Testcase.Telegrams.DMItoEVC
{
    /// <summary>
    /// This packet shall be sent when the driver has entered or validated driver identity number.
    /// </summary>
    public static class EVC104_MMINewDriverData
    {
        private static SignalPool _pool;
        private static bool _checkResult;
        private static string _xDriverID;
        const string baseString = "DMI->ETCS: EVC-104 [MMI_NEW_DRIVER_DATA]";

        /// <summary>
        /// Initialise EVC-104 MMI_New_Driver_Data.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            _pool.SITR.SMDStat.CCUO.ETCS1NewDriverData.Value = 0;
            _pool.SITR.SMDCtrl.CCUO.ETCS1NewDriverData.Value = 1;
        }

        private static void CheckXDriverID(string xDriverID)
        {
            // Check if telegram received flag has been set. Allows 15 seconds to enter driver ID.
            if (_pool.SITR.SMDStat.CCUO.ETCS1NewDriverData.WaitForCondition(Is.Equal, 1, 20000, 20))
            {
                // Check if Driver ID matches
                _checkResult = _pool.SITR.CCUO.ETCS1NewDriverData.MmiXDriverId.Value.Equals(xDriverID);

                // If check passes
                if (_checkResult)
                {
                    _pool.TraceReport($"{baseString} - MMI_X_DRIVER_ID = {xDriverID}" + Environment.NewLine +
                                        "Result = PASSED.");
                }
                // Else display the real value extracted from EVC-104
                else
                {
                    _pool.TraceError($"{baseString} - MMI_X_DRIVER_ID = " + _pool.SITR.CCUO.ETCS1NewDriverData.MmiXDriverId.Value +
                                        Environment.NewLine + "Result: FAILED");
                }
            }
            // Show generic DMI -> EVC telegram failure
            else
            {
                DmiExpectedResults.DMItoEVC_Telegram_Not_Received(_pool, baseString);
            }

            // Reset telegram received flag in RTSim
            _pool.SITR.SMDStat.CCUO.ETCS1NewDriverData.Value = 0;
        }

        /// <summary>
        /// This is the driver’s identity (max 16 character long).
        /// Values:
        /// 0 = "Empty string (null character)"
        /// Note: 16 alphanumeric characters(ISO 8859-1, also known as Latin Alphabet #1).
        /// Note 1: If the value is unknown the table will be filled with null characters (0, not '0').
        /// Note 2: If Driver ID is shorter than 16 characters the free charcters will be filled with null characters.
        /// Note 3: If Driver ID is 16 characters there will be no null character in the string.
        /// </summary>
        public static string Check_X_DRIVER_ID
        {
            set
            {
                _xDriverID = value;
                CheckXDriverID(_xDriverID);
            }
        }

        /// <summary>
        /// This is the driver’s identity (max 16 character long).
        /// 
        /// Values:
        /// 0 = "Empty string (null character)"
        /// Note: 16 alphanumeric characters(ISO 8859-1, also known as Latin Alphabet #1).
        /// Note 1: If the value is unknown the table will be filled with null characters (0, not '0').
        /// Note 2: If Driver ID is shorter than 16 characters the free charcters will be filled with null characters.
        /// Note 3: If Driver ID is 16 characters there will be no null character in the string.
        /// </summary>
        public static string Get_X_DRIVER_ID
        {
            get
            {
                return _pool.SITR.CCUO.ETCS1NewDriverData.MmiXDriverId.Value;
            }
        }
    }
}