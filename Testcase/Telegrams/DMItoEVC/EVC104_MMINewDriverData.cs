using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using CL345;
using Testcase.Telegrams.DMItoEVC;
using Testcase.Telegrams.EVCtoDMI;
using static Testcase.Telegrams.EVCtoDMI.Variables;


namespace Testcase.Telegrams.DMItoEVC
{
    /// <summary>
    /// This packet shall be sent when the driver has entered or validated driver identity number.
    /// </summary>
    public static class EVC104_MMINewDriverData
    {
        private static SignalPool _pool;
        private static bool _bResult;
        private static string _xDriverID;

        /// <summary>
        /// Initialise EVC-104 MMI_New_Driver_Data.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
        }

        private static void CheckXDriverID(string xDriverID)
        {
            _bResult = _pool.SITR.CCUO.ETCS1NewDriverData.MmiXDriverId.Value.Equals(xDriverID);            
           
            if (_bResult) // if check passes
            {
                _pool.TraceReport("DMI->ETCS: EVC-104 [MMI_NEW_DRIVER_DATA.MMI_X_DRIVER_ID] => " + 
                    xDriverID + " PASSED.");
            }
            else // else display the real value extracted from EVC-104 [MMI_NEW_DRIVER_DATA.MMI_X_DRIVER_ID]
            {
                _pool.TraceError("DMI->ETCS: Check EVC-104 [MMI_NEW_DRIVER_DATA.MMI_X_DRIVER_ID] => " +
                    _pool.SITR.CCUO.ETCS1NewDriverData.MmiXDriverId.Value + " FAILED");
            }
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
    }
}