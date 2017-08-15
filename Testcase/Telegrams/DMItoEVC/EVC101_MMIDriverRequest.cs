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
    /// This packet shall be sent when the driver requests for an action from the ATP, 
    /// typically by pressing a button..
    /// </summary>
    public static class EVC101_MMIDriverRequest
    {
        private static SignalPool _pool;
        private static bool _bResult;
        private static Variables.MMI_M_REQUEST _mRequest;

        /// <summary>
        /// Initialise EVC-101 MMI_Driver_Request telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
        }

        private static void CheckMRequestState(Variables.MMI_M_REQUEST mRequest, Variables.MMI_Q_BUTTON qButton)
        {
            // Convert byte EVC101_alias_1 into an array of bits.
            BitArray _evc101alias1 = new BitArray(new[] { _pool.SITR.CCUO.ETCS1DriverRequest.EVC101alias1.Value });
            // Extract bool MMI_Q_BUTTON (7th bit according to VSIS 2.9)
            bool _mmiQButton = _evc101alias1[7];

            // Convert byte qButton to bool
            BitArray _baqButton = new BitArray(new[] { (byte)qButton });
            bool _bqButton = _baqButton[0];

            // For each element of enum MMI_M_REQUEST
            foreach (Variables.MMI_M_REQUEST mmiMRequestElement in Enum.GetValues(typeof(Variables.MMI_M_REQUEST)))
            {
                // Compare to the value to be checked
                if (mmiMRequestElement == mRequest)
                {
                    // For each element of enum MMI_Q_BUTTON
                    foreach (Variables.MMI_Q_BUTTON mmiQButtonElement in Enum.GetValues(typeof(Variables.MMI_Q_BUTTON)))
                    {
                        // Compare to the value to be checked
                        if (mmiQButtonElement == qButton)
                        {
                            // Double check: MMI_M_REQUEST & MMI_Q_BUTTON values
                            _bResult = (_pool.SITR.CCUO.ETCS1DriverRequest.MmiMRequest.Value.Equals(mRequest)) &&
                                (_mmiQButton.Equals(_bqButton));
                            break;
                        }
                    }

                    break;
                }
            }
           
            if (_bResult) // if check passes
            {
                _pool.TraceReport("DMI->ETCS: EVC-101 [MMI_DRIVER_REQUEST] => " + mRequest +
                    " - \"" + mRequest.ToString() + "\" -> " + qButton.ToString() + " PASSED. TimeStamp = " + 
                    _pool.SITR.CCUO.ETCS1DriverRequest.MmiTButtonevent);
            }
            else // else display the real values extracted from EVC-101 [MMI_DRIVER_REQUEST] 
            {
                _pool.TraceError("DMI->ETCS: Check EVC-101 [MMI_DRIVER_REQUEST] => MMI_M_REQUEST = " + 
                    Enum.GetName(typeof(Variables.MMI_M_REQUEST), _pool.SITR.CCUO.ETCS1DriverRequest.MmiMRequest.Value) + 
                    ", MMI_Q_BUTTON = " + Enum.GetName(typeof(Variables.MMI_Q_BUTTON), _mmiQButton) + " FAILED. TimeStamp = " +
                    _pool.SITR.CCUO.ETCS1DriverRequest.MmiTButtonevent);
            }
        }

        /// <summary>
        /// Driver Request enum
        /// Values:
        /// Values:
        /// 0 = "Spare"
        /// 1 = "Start Shunting"
        /// 2 = "Exit Shunting"
        /// 3 = "Start Train Data Entry"
        /// 4 = "Exit Train Data Entry"
        /// 5 = "Start Non-Leading"
        /// 6 = "Exit Non-Leading"
        /// 7 = "Start Override EOA (Pass stop)"
        /// 8 = "Geographical position request"
        /// 9 = "Start"
        /// 10 = "Restore adhesion coefficient to 'non-slippery rail'"
        /// 11 = "Set adhesion coefficient to 'slippery rail'"
        /// 12 = "Exit Change SR rules"
        /// 13 = "Change SR rules"
        /// 14 = "Continue shunting on desk closure"
        /// 15 = "Spare"
        /// 16 = "Spare"
        /// 17 = "Spare"
        /// 18 = "Spare"
        /// 19 = "Spare"
        /// 20 = "Change Driver identity"
        /// 21 = "Start Train Data View"
        /// 22 = "Start Brake Test"
        /// 23 = "Start Set VBC"
        /// 24 = "Start Remove VBC"
        /// 25 = "Exit Set VBC"
        /// 26 = "Exit Remove VBC"
        /// 27 = "Change Level (or inhibit status)"
        /// 28 = "Start RBC Data Entry"
        /// 29 = "System Info request"
        /// 30 = "Change Train Running Number"
        /// 31 = "Exit Change Train Running Number"
        /// 32 = "Exit Change Level (or inhibit status)"
        /// 33 = "Exit RBC Data Entry"
        /// 34 = "Exit Driver Data Entry"
        /// 35 = "Spare"
        /// 36 = "Spare"
        /// 37 = "Spare"
        /// 38 = "Start procedure 'Train Integrity'"
        /// 39 = "Exit RBC contact"
        /// 40 = "Level entered"
        /// 41 = "start NTC 1 data entry"
        /// 42 = "start NTC 2 data entry"
        /// 43 = "start NTC 3 data entry"
        /// 44 = "start NTC 4 data entry"
        /// 45 = "start NTC 5 data entry"
        /// 46 = "start NTC 6 data entry"
        /// 47 = "start NTC 7 data entry"
        /// 48 = "start NTC 8 data entry"
        /// 49 = "Exit NTC data entry"
        /// 50 = "Exit NTC data entry selection"
        /// 51 = "Change Brake Percentage"
        /// 52 = "Change Doppler"
        /// 53 = "Change Wheel Diameter"
        /// 54 = "Exit maintenance"
        /// 55 = "System Version request"
        /// 56 = "Start Network ID"
        /// 57 = "Contact last RBC"
        /// 58 = "Settings"
        /// 59 = "Switch"
        /// 60 = "Exit brake percentage"
        /// 61 = "Exit RBC Network ID"
        /// 62..255 = "Spare"
        /// 
        /// Note1: Values 3 and 4 also apply on customised Train Data Entry(packets EVC-60, EVC-61, EVC-160, EVC-161).
        /// Note 2: The number of the NTC x in 'start NTC x data entry' will match the sequence number of the related NTC in the list provided with EVC-31.
        /// </summary>
        public static Variables.MMI_M_REQUEST CheckMRequestPressed
        {
            set
            {
                _mRequest = value;
                CheckMRequestState(_mRequest, Variables.MMI_Q_BUTTON.Pressed);
            }
        }

        /// <summary>
        /// Driver Request enum
        /// Values:
        /// Values:
        /// 0 = "Spare"
        /// 1 = "Start Shunting"
        /// 2 = "Exit Shunting"
        /// 3 = "Start Train Data Entry"
        /// 4 = "Exit Train Data Entry"
        /// 5 = "Start Non-Leading"
        /// 6 = "Exit Non-Leading"
        /// 7 = "Start Override EOA (Pass stop)"
        /// 8 = "Geographical position request"
        /// 9 = "Start"
        /// 10 = "Restore adhesion coefficient to 'non-slippery rail'"
        /// 11 = "Set adhesion coefficient to 'slippery rail'"
        /// 12 = "Exit Change SR rules"
        /// 13 = "Change SR rules"
        /// 14 = "Continue shunting on desk closure"
        /// 15 = "Spare"
        /// 16 = "Spare"
        /// 17 = "Spare"
        /// 18 = "Spare"
        /// 19 = "Spare"
        /// 20 = "Change Driver identity"
        /// 21 = "Start Train Data View"
        /// 22 = "Start Brake Test"
        /// 23 = "Start Set VBC"
        /// 24 = "Start Remove VBC"
        /// 25 = "Exit Set VBC"
        /// 26 = "Exit Remove VBC"
        /// 27 = "Change Level (or inhibit status)"
        /// 28 = "Start RBC Data Entry"
        /// 29 = "System Info request"
        /// 30 = "Change Train Running Number"
        /// 31 = "Exit Change Train Running Number"
        /// 32 = "Exit Change Level (or inhibit status)"
        /// 33 = "Exit RBC Data Entry"
        /// 34 = "Exit Driver Data Entry"
        /// 35 = "Spare"
        /// 36 = "Spare"
        /// 37 = "Spare"
        /// 38 = "Start procedure 'Train Integrity'"
        /// 39 = "Exit RBC contact"
        /// 40 = "Level entered"
        /// 41 = "start NTC 1 data entry"
        /// 42 = "start NTC 2 data entry"
        /// 43 = "start NTC 3 data entry"
        /// 44 = "start NTC 4 data entry"
        /// 45 = "start NTC 5 data entry"
        /// 46 = "start NTC 6 data entry"
        /// 47 = "start NTC 7 data entry"
        /// 48 = "start NTC 8 data entry"
        /// 49 = "Exit NTC data entry"
        /// 50 = "Exit NTC data entry selection"
        /// 51 = "Change Brake Percentage"
        /// 52 = "Change Doppler"
        /// 53 = "Change Wheel Diameter"
        /// 54 = "Exit maintenance"
        /// 55 = "System Version request"
        /// 56 = "Start Network ID"
        /// 57 = "Contact last RBC"
        /// 58 = "Settings"
        /// 59 = "Switch"
        /// 60 = "Exit brake percentage"
        /// 61 = "Exit RBC Network ID"
        /// 62..255 = "Spare"
        /// 
        /// Note1: Values 3 and 4 also apply on customised Train Data Entry(packets EVC-60, EVC-61, EVC-160, EVC-161).
        /// Note 2: The number of the NTC x in 'start NTC x data entry' will match the sequence number of the related NTC in the list provided with EVC-31.
        /// </summary>
        public static Variables.MMI_M_REQUEST CheckMRequestReleased
        {
            set
            {
                _mRequest = value;
                CheckMRequestState(_mRequest, Variables.MMI_Q_BUTTON.Released);
            }
        }
    }
}