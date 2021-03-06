﻿using System;
using CL345;
using Testcase.DMITestCases;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using Testcase.Telegrams.EVCtoDMI;


namespace Testcase.Telegrams.DMItoEVC
{
    /// <summary>
    /// This packet shall be sent when the driver has entered or validated driver identity number.
    /// </summary>
    public static class EVC104_MMINewDriverData
    {
        private static TestcaseBase _pool;
        private static bool _checkResult;
        private static string _mmiXDriverId;
        private const string BaseString = "DMI->ETCS: EVC-104 [MMI_NEW_DRIVER_DATA]";

        /// <summary>
        /// Initialise EVC-104 MMI_New_Driver_Data.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(TestcaseBase pool)
        {
            _pool = pool;
            _pool.SITR.SMDStat.CCUO.ETCS1NewDriverData.Value = 0x00;
            _pool.SITR.SMDCtrl.CCUO.ETCS1NewDriverData.Value = 0x0001;
        }

        private static void CheckXDriverId(string xDriverId)
        {
            // Check if telegram received flag has been set. Allows 20 seconds to enter driver ID.
            if (_pool.SITR.SMDStat.CCUO.ETCS1NewDriverData.WaitForCondition(Is.Equal, 1, 20000, 100))
            {
                // Check if Driver ID matches
                _checkResult = _pool.SITR.CCUO.ETCS1NewDriverData.MmiXDriverId.Value.Equals(xDriverId);

                // If check passes
                if (_checkResult)
                {
                    _pool.TraceReport(string.Format("{0} - MMI_X_DRIVER_ID = {1}", BaseString, xDriverId) +
                                      Environment.NewLine +
                                      "Result = PASSED.");
                    EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = xDriverId;
                }
                // Else display the real value extracted from EVC-104
                else
                {
                    _pool.TraceError(string.Format("{0} - MMI_X_DRIVER_ID = ", BaseString) +
                                     _pool.SITR.CCUO.ETCS1NewDriverData.MmiXDriverId.Value +
                                     Environment.NewLine + "Result: FAILED");
                }
            }
            // Show generic DMI -> EVC telegram failure
            else
            {
                DmiExpectedResults.DMItoEVC_Telegram_Not_Received(_pool, BaseString);
            }

            // Reset telegram received flag in RTSim
            _pool.SITR.SMDStat.CCUO.ETCS1NewDriverData.Value = 0x00;
        }

        /// <summary>
        /// This is the driver’s identity (max 16 character long).
        /// 
        /// Values:
        /// 0 = "Empty string (null character)"
        /// 
        /// Note: 16 alphanumeric characters (ISO 8859-1, also known as Latin Alphabet #1).
        /// Note 1: If the value is unknown the table will be filled with null characters (0, not '0').
        /// Note 2: If Driver ID is shorter than 16 characters the free characters will be filled with null characters.
        /// Note 3: If Driver ID is 16 characters there will be no null character in the string.
        /// </summary>
        public static string Check_Entered_MMI_X_DRIVER_ID
        {
            set
            {
                _mmiXDriverId = value;
                CheckXDriverId(_mmiXDriverId);
            }
        }

        /// <summary>
        /// This is the driver’s identity (max 16 character long).
        /// 
        /// Values:
        /// 0 = "Empty string (null character)"
        /// 
        /// Note: 16 alphanumeric characters (ISO 8859-1, also known as Latin Alphabet #1).
        /// Note 1: If the value is unknown the table will be filled with null characters (0, not '0').
        /// Note 2: If Driver ID is shorter than 16 characters the free characters will be filled with null characters.
        /// Note 3: If Driver ID is 16 characters there will be no null character in the string.
        /// </summary>
        public static string Get_Entered_MMI_X_DRIVER_ID
        {
            get
            {
                // Reset telegram received flag in RTSim
                _pool.SITR.SMDStat.CCUO.ETCS1NewDriverData.Value = 0x00;

                if (_pool.SITR.SMDStat.CCUO.ETCS1NewDriverData.WaitForCondition(Is.Equal, 1, 20000, 100))
                {
                    _mmiXDriverId = _pool.SITR.CCUO.ETCS1NewDriverData.MmiXDriverId.Value;

                    EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = _mmiXDriverId;

                    return _mmiXDriverId;
                }
                else
                {
                    DmiExpectedResults.DMItoEVC_Telegram_Not_Received(_pool, BaseString);
                    return "";
                }
            }
        }
    }
}