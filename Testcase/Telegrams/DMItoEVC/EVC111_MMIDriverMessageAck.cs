using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using CL345;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.Telegrams
{
    /// <summary>
    /// This packet shall be sent when the driver requests for an action from the ATP, 
    /// typically by pressing a button..
    /// </summary>
    static class EVC111_MMIDriverMessageAck
    {
        private static SignalPool _pool;
        private static bool _bResult;
        private static string _buttonState;
        private static Variables.MMI_Q_BUTTON _buttonStateTested;

        private static void CheckButtonState(Variables.MMI_Q_BUTTON buttonStateTested)
        {
            // Convert byte EVC111_alias_1 into an array of bits.
            BitArray _evc111alias1 = new BitArray(new[] { _pool.SITR.CCUO.ETCS1DriverMessageAck.EVC111alias1.Value });
            // Extract bool MMI_Q_BUTTON (4th bit according to VSIS 2.9)
            bool _mmiQButton = _evc111alias1[3];

            // Convert byte buttonStateTested to bool
            BitArray _baButtonStateTested = new BitArray(new[] { (byte) buttonStateTested });
            bool _bButtonStateTested = _baButtonStateTested[0];

            //For each element of enum MMI_Q_BUTTON 
            foreach (Variables.MMI_Q_BUTTON mmiQButtonElement in Enum.GetValues(typeof(Variables.MMI_Q_BUTTON)))
            {
                //Compare to the value to be checked
                if (mmiQButtonElement == buttonStateTested)
                {
                    _buttonState = mmiQButtonElement.ToString();
                    _bResult = _mmiQButton.Equals(_bButtonStateTested);
                    break;
                }
            }

            //if check passes
            if (_bResult)
            {
                _pool.TraceReport("DMI->ETCS: EVC-111 [MMI_DRIVER_MESSAGE_ACK.MMI_Q_BUTTON] = " + _bButtonStateTested +
                    " - \"" + _buttonState + "\" PASSED. TimeStamp = " + _pool.SITR.CCUO.ETCS1DriverMessageAck.MmiTButtonEvent);
            }
            else
            {
                _pool.TraceError("DMI->ETCS: Check EVC-111 [MMI_DRIVER_MESSAGE_ACK.MMI_Q_BUTTON] = " + _mmiQButton + "FAILED." +
                    "TimeStamp = " + _pool.SITR.CCUO.ETCS1DriverMessageAck.MmiTButtonEvent);
            }
        }

        /// <summary>
        /// Button event (pressed or released)
        /// Values:
        /// 0 = "released"
        /// 1 = "pressed"
        /// </summary>
        public static Variables.MMI_Q_BUTTON Check_MMI_Q_BUTTON
        {
            set
            {
                _buttonStateTested = value;
                CheckButtonState(_buttonStateTested);
            }
        }      
    }
}