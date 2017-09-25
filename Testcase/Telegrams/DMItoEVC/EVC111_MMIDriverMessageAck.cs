using System;
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
    public static class EVC111_MMIDriverMessageAck
    {
        private static SignalPool _pool;
        private static byte _mmiIText;
        private static bool _checkResult;

        private static MMI_Q_ACK _qAck;
        private static uint _timeStamp;

        /// <summary>
        /// Initialise EVC-111 MMI_Driver_Message_Ack telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            _pool.SITR.SMDCtrl.CCUO.ETCS1DriverMessageAck.Value = 1;
        }

        /// <summary>
        /// Check that a particular MMI_I_TEXT is Acknowledged
        /// </summary>
        /// <param name="mmiIText">Identifier of the acknowledged text</param>
        private static void Check_Driver_Message_Ack(byte mmiIText)
        {
            var list = new List<Atomic>
            {
                _pool.SITR.CCUO.ETCS1DriverMessageAck.MmiIText.Atomic.WaitForCondition(Is.Equal, mmiIText),
                // EVC111_alias_1: MMI_Q_ACK bits = 00xx 0000
                //  EVC111_alias_1 Acknowledge = 1 -> 0001 0000 -> Check for less than or equal to 0001 1111
                _pool.SITR.CCUO.ETCS1DriverMessageAck.EVC111alias1.Atomic.WaitForCondition(Is.LessOrEqual, 0x11)
            };

            _checkResult = _pool.WaitForConditionAtomic(list, 10000, 20);

            // Get time stamp of received packet
            _timeStamp = _pool.SITR.CCUO.ETCS1DriverMessageAck.MmiTButtonEvent.Value;

            // If check passes
            if (_checkResult)
            {
                _pool.TraceReport("DMI->ETCS: EVC-111 [MMI_DRIVER_MESSAGE_ACK.MMI_I_TEXT] = \"" +
                                    mmiIText + "\" Result: Acknowledge PASSED." + Environment.NewLine +
                                    "Time stamp = " + _timeStamp);
            }

            // Else display failure
            else
            {
                _pool.TraceError("DMI->ETCS: EVC-111 [MMI_DRIVER_MESSAGE_ACK.MMI_I_TEXT] = \"" + mmiIText + "\" " +
                                    "Result: Acknowledge FAILED." + Environment.NewLine +
                                    "Time stamp = " + _timeStamp);
            }
        }

        /// <summary>
        /// Check that a particular MMI_I_TEXT is NOT Acknowledged
        /// </summary>
        /// <param name="mmiIText">Identifier of the acknowledged text</param>
        private static void Check_Driver_Message_Not_Ack(byte mmiIText)
        {
            var list = new List<Atomic>
            {
                _pool.SITR.CCUO.ETCS1DriverMessageAck.MmiIText.Atomic.WaitForCondition(Is.Equal, mmiIText),
                // EVC111_alias_1: MMI_Q_ACK bits = 00xx 0000
                //  EVC111_alias_1 NOT Acknowledge = 2 -> 0010 0000 -> Check for greater than or equal to 0010 0000
                _pool.SITR.CCUO.ETCS1DriverMessageAck.EVC111alias1.Atomic.WaitForCondition(Is.GreaterOrEqual, 0x20)
            };

            _checkResult = _pool.WaitForConditionAtomic(list, 10000, 20);

            // Get time stamp of received packet
            _timeStamp = _pool.SITR.CCUO.ETCS1DriverMessageAck.MmiTButtonEvent.Value;

            // If check passes
            if (_checkResult)
            {
                _pool.TraceReport("DMI->ETCS: EVC-111 [MMI_DRIVER_MESSAGE_ACK.MMI_I_TEXT] = \"" +
                                    mmiIText + "\" Result: NOT Acknowledge PASSED." + Environment.NewLine +
                                    "Time stamp = " + _timeStamp);
            }

            // Else display failure
            else
            {
                _pool.TraceError("DMI->ETCS: EVC-111 [MMI_DRIVER_MESSAGE_ACK.MMI_I_TEXT] = \"" + mmiIText + "\" " +
                                    "Result: NOT Acknowledge FAILED." + Environment.NewLine +
                                    "Time stamp = " + _timeStamp);
            }
        }

        /// <summary>
        /// Identifier of the acknowledged text to be checked
        /// </summary>
        public static byte MMI_I_TEXT
        {
            get => _mmiIText;

            set => _mmiIText = value;
        }

        /// <summary>
        /// The logical value of the driver’s acknowledgement
        /// 
        /// Values:
        /// 0 = "Spare"
        /// 1 = "Acknowledge / YES"
        /// 2 = "Not Acknowledge / NO"
        /// 3..15 = "spare"
        /// </summary>
        public static MMI_Q_ACK CHECK_MMI_Q_ACK
        {
            set
            {
                _qAck = value;

                if (_qAck == MMI_Q_ACK.AcknowledgeYES)
                    Check_Driver_Message_Ack(_mmiIText);

                else if (_qAck == MMI_Q_ACK.NotAcknowledgeNO)
                    Check_Driver_Message_Not_Ack(_mmiIText);

                else _pool.TraceError("MMI_Q_ACK is not a valid value!");
            }
        }
        
        /// <summary>
        /// Ack event enum
        /// </summary>
        public enum MMI_Q_ACK : byte
        {
            Spare = 0,
            AcknowledgeYES = 1,
            NotAcknowledgeNO = 2
        }
    }
}