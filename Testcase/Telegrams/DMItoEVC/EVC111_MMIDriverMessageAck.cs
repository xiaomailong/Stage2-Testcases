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
    public static class EVC111_MMIDriverMessageAck
    {
        private static SignalPool _pool;
        private static bool _bResult;
        private static Variables.MMI_Q_BUTTON _qButton;
        private static MMI_Q_ACK _qAck;

        /// <summary>
        /// Initialise EVC-111 MMI_Driver_Message_Ack telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            _pool.SITR.SMDCtrl.CCUO.ETCS1DriverMessageAck.Value = 1;
        }

        private static void CheckButtonState(Variables.MMI_Q_BUTTON qButton)
        {
            // Convert byte EVC111_alias_1 into an array of bits.
            BitArray _evc111alias1 = new BitArray(new[] { _pool.SITR.CCUO.ETCS1DriverMessageAck.EVC111alias1.Value });
            // Extract bool MMI_Q_BUTTON (4th bit according to VSIS 2.9)
            bool _mmiQButton = _evc111alias1[3];

            // Convert byte qButton to bool
            BitArray _baqButton = new BitArray(new[] { (byte)qButton });
            bool _bqButton = _baqButton[0];

            //For each element of enum MMI_Q_BUTTON 
            foreach (Variables.MMI_Q_BUTTON mmiQButtonElement in Enum.GetValues(typeof(Variables.MMI_Q_BUTTON)))
            {
                //Compare to the value to be checked
                if (mmiQButtonElement == qButton)
                {
                    // Check MMI_Q_BUTTON value
                    _bResult = _mmiQButton.Equals(_bqButton);
                    break;
                }
            }

            if (_bResult) // if check passes
            {
                _pool.TraceReport("DMI->ETCS: EVC-111 [MMI_DRIVER_MESSAGE_ACK.MMI_Q_BUTTON] = \"" +
                    qButton.ToString() + "\" PASSED. TimeStamp = " +
                    _pool.SITR.CCUO.ETCS1DriverMessageAck.MmiTButtonEvent.Value);
            }
            else // else display the real value extracted from EVC-111 [MMI_DRIVER_MESSAGE_ACK] 
            {
                _pool.TraceError("DMI->ETCS: EVC-111 [MMI_DRIVER_MESSAGE_ACK.MMI_Q_BUTTON] = \"" +
                    Enum.GetName(typeof(Variables.MMI_Q_BUTTON), _mmiQButton) + "\" FAILED. TimeStamp = " +                    
                    _pool.SITR.CCUO.ETCS1DriverMessageAck.MmiTButtonEvent.Value);
            }


        }

        private static void CheckQAck(MMI_Q_ACK qAck)
        {
            // Get EVC111_alias_1
            byte _evc111alias1 = _pool.SITR.CCUO.ETCS1DriverMessageAck.EVC111alias1.Value;
            // Extract MMI_Q_ACK (7th -> 4th bits according to VSIS 2.9)
            byte _mmiQAck = (byte)((_evc111alias1 & 0xF0) >> 4); // xxxx xxxx -> xxxx 0000 -> 0000 xxxx

            // For each element of enum MMI_Q_ACK
            foreach (MMI_Q_ACK mmiQAckElement in Enum.GetValues(typeof(MMI_Q_ACK)))
            {
                // Compare to the value to be checked
                if (mmiQAckElement == qAck)
                {
                    // Check MMI_Q_ACK value
                    _bResult = _mmiQAck.Equals(qAck);
                    break;
                }
            }

            if (_bResult) // if check passes
            {
                _pool.TraceReport("DMI->ETCS: EVC-111 [MMI_DRIVER_MESSAGE_ACK.MMI_Q_ACK] = \"" +
                    qAck.ToString() + "\" PASSED. TimeStamp = " +
                    _pool.SITR.CCUO.ETCS1DriverMessageAck.MmiTButtonEvent.Value);
            }
            else // else display the real value extracted from EVC-111 [MMI_DRIVER_MESSAGE_ACK] 
            {
                _pool.TraceError("DMI->ETCS: EVC-111 [MMI_DRIVER_MESSAGE_ACK.MMI_Q_ACK] = \"" +
                    Enum.GetName(typeof(MMI_Q_ACK), _mmiQAck) + "\" FAILED. TimeStamp = " +
                    _pool.SITR.CCUO.ETCS1DriverMessageAck.MmiTButtonEvent.Value);
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
                _qButton = value;
                CheckButtonState(_qButton);
            }
        }

        /// <summary>
        /// The logical value of the driver’s acknowledgement
        /// Values:
        /// 0 = "Spare"
        /// 1 = "Acknowledge / YES"
        /// 2 = "Not Acknowledge / NO"
        /// 3..15 = "spare"
        /// </summary>
        public static MMI_Q_ACK Check_MMI_Q_ACK
        {
            set
            {
                _qAck = value;
                CheckQAck(_qAck);
            }
        }
        
        /// <summary>
        /// Ack event enum
        /// </summary>
        public enum MMI_Q_ACK : byte
        {
            AcknowledgeYES = 1,
            NotAcknowledgeNO = 2,
            Spare = 0
        }
    }
}