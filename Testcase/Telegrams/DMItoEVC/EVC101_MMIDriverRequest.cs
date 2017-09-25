﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using CL345;
using Testcase.Telegrams.EVCtoDMI;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;

namespace Testcase.Telegrams.DMItoEVC
{
    /// <summary>
    /// This packet shall be sent when the driver requests for an action from the ATP, typically by pressing a button.
    /// </summary>
    public static class EVC101_MMIDriverRequest
    {
        private static SignalPool _pool;
        private static bool _checkResult;
        private static Variables.MMI_M_REQUEST _mRequest;
        private static byte _qbutton;

        /// <summary>
        /// Initialise EVC-101 MMI_Driver_Request telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            _pool.SITR.SMDCtrl.CCUO.ETCS1DriverRequest.Value = 1;
        }

        private static void CheckMRequestState(Variables.MMI_M_REQUEST mRequest, Variables.MMI_Q_BUTTON qButton)
        {
            // Convert qButton to a Byte value. All alignment bits in evc101alias1 should be set to 0
            //  automatically hence bit-shifting should be no problem.
            _qbutton = Convert.ToByte((byte)qButton << 7);
            
            // List containing button type and pressed/released state
            var list = new List<Atomic>
            {
                _pool.SITR.CCUO.ETCS1DriverRequest.MmiMRequest.Atomic.WaitForCondition(Is.Equal, (byte)mRequest),
                _pool.SITR.CCUO.ETCS1DriverRequest.EVC101alias1.Atomic.WaitForCondition(Is.Equal, _qbutton)
            };

            _checkResult = _pool.WaitForConditionAtomic(list, 10000, 20);
            
            // If check passes
            if (_checkResult) 
            {
                _pool.TraceReport("DMI->ETCS: EVC-101 [MMI_DRIVER_REQUEST] => " + (byte)mRequest + " - \"" + mRequest +
                                    "\" -> " + qButton + " PASSED." + Environment.NewLine +
                                    "Timestamp = " + _pool.SITR.CCUO.ETCS1DriverRequest.MmiTButtonevent);
            }

            // Else display the real values extracted from EVC-101 [MMI_DRIVER_REQUEST]
            else
            {
                _pool.TraceError("DMI->ETCS: Check EVC-101 [MMI_DRIVER_REQUEST] => MMI_M_REQUEST = " +
                                    Enum.GetName(typeof(Variables.MMI_M_REQUEST), mRequest) + ", MMI_Q_BUTTON = " +
                                    Enum.GetName(typeof(Variables.MMI_Q_BUTTON), qButton) + " Result: FAILED!");
            }
        }

        /// <summary>
        /// Driver Request enum
        /// 
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
        /// Note 1: Values 3 and 4 also apply on customised Train Data Entry(packets EVC-60, EVC-61, EVC-160, EVC-161).
        /// Note 2: The number of the NTC x in 'start NTC x data entry' will match the sequence number of the related
        ///         NTC in the list provided with EVC-31.
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
        /// 
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
        /// Note 1: Values 3 and 4 also apply on customised Train Data Entry(packets EVC-60, EVC-61, EVC-160, EVC-161).
        /// Note 2: The number of the NTC x in 'start NTC x data entry' will match the sequence number of the related
        ///         NTC in the list provided with EVC-31.
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