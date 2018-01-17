using System;
using System.Collections.Generic;
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
        private static uint _timeStamp;
        private static byte _mmiIText;
        private static MMI_Q_ACK _mmiQAck;
        private static Variables.MMI_Q_BUTTON _mmiQButton;
        private static bool _checkResult;

        const string baseString = "DMI->ETCS: EVC-111 [MMI_DRIVER_MESSAGE_ACK] MMI_I_TEXT = ";

        /// <summary>
        /// Initialise EVC-111 MMI_Driver_Message_Ack telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            _pool.SITR.SMDStat.CCUO.ETCS1DriverMessageAck.Value = 0;
            _pool.SITR.SMDCtrl.CCUO.ETCS1DriverMessageAck.Value = 1;
        }

        /// <summary>
        /// Identifier of the acknowledged text to be checked
        /// </summary>
        public static byte MMI_I_TEXT
        {
            get { return _mmiIText; }
            set { _mmiIText = value; }
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
        public static MMI_Q_ACK MMI_Q_ACK
        {
            get { return _mmiQAck; }
            set { _mmiQAck = value; }
        }

        /// <summary>
        /// Check whether a specific Driver Message Acknowledge Pressed or Released telegram has been received,
        /// based on the MMI_I_Text set previously.
        /// </summary>
        public static Variables.MMI_Q_BUTTON MMI_Q_BUTTON
        {
            set
            {
                _mmiQButton = value;

                if (_mmiQButton == Variables.MMI_Q_BUTTON.Pressed)
                {
                    if (_mmiQAck == MMI_Q_ACK.AcknowledgeYES)
                    {
                        Check_Driver_Message_Ack_Pressed();
                    }
                    else if (_mmiQAck == MMI_Q_ACK.NotAcknowledgeNO)
                    {
                        Check_Driver_Message_Not_Ack_Pressed();
                    }
                    else _pool.TraceError("MMI_Q_ACK is not valid.");
                }
                else if (_mmiQButton == Variables.MMI_Q_BUTTON.Released)
                {
                    if (_mmiQAck == MMI_Q_ACK.AcknowledgeYES)
                    {
                        Check_Driver_Message_Ack_Released();
                    }
                    else if (_mmiQAck == MMI_Q_ACK.NotAcknowledgeNO)
                    {
                        Check_Driver_Message_Not_Ack_Released();
                    }
                    else _pool.TraceError("MMI_Q_ACK is not valid.");
                }
                else
                {
                    _pool.TraceError(
                        "EVC-111 error. Make sure MMI_I_TEXT, MMI_Q_BUTTON, and MMI_Q_ACK are set to valid values.");
                }
            }
        }

        /// <summary>
        /// Check that a particular MMI_I_TEXT Acknowledged is Pressed.
        /// </summary>
        /// <param name="mmiIText">Identifier of the acknowledged text</param>
        private static void Check_Driver_Message_Ack_Pressed()
        {
            // Reset telegram received flag in RTSim
            _pool.SITR.SMDStat.CCUO.ETCS1DriverMessageAck.Value = 0x00;

            var list = new List<Atomic>
            {
                _pool.SITR.SMDStat.CCUO.ETCS1DriverMessageAck.Atomic.WaitForCondition(Is.Equal, 1),
                _pool.SITR.CCUO.ETCS1DriverMessageAck.MmiIText.Atomic.WaitForCondition(Is.Equal, _mmiIText),

                // EVC111_alias_1: MMI_Q_ACK bits = 00xx 0000
                // EVC111_alias_1: MMI_Q_BUTTON bit = 0000 x000
                _pool.SITR.CCUO.ETCS1DriverMessageAck.EVC111alias1.Atomic.WaitForCondition(Is.Equal, 0x18)
            };

            _checkResult = _pool.WaitForConditionAtomic(list, 10000, 100);

            // Get time stamp of received packet
            _timeStamp = _pool.SITR.CCUO.ETCS1DriverMessageAck.MmiTButtonEvent.Value;

            // If check passes
            if (_checkResult)
            {
                _pool.TraceReport(baseString + _mmiIText + Environment.NewLine +
                                  "Time stamp = " + _timeStamp + Environment.NewLine +
                                  "Result: Acknowledge Pressed PASSED.");
            }

            // Else display failure
            else
            {
                _pool.TraceError(baseString + _mmiIText + Environment.NewLine +
                                 "Time stamp = " + _timeStamp + Environment.NewLine +
                                 "Result: Acknowledge Pressed FAILED.");
            }
        }

        /// <summary>
        /// Check that a particular MMI_I_TEXT Acknowledged is Released
        /// </summary>
        /// <param name="mmiIText">Identifier of the acknowledged text</param>
        private static void Check_Driver_Message_Ack_Released()
        {
            // Reset telegram received flag in RTSim
            _pool.SITR.SMDStat.CCUO.ETCS1DriverMessageAck.Value = 0x00;

            var list = new List<Atomic>
            {
                _pool.SITR.SMDStat.CCUO.ETCS1DriverMessageAck.Atomic.WaitForCondition(Is.Equal, 1),
                _pool.SITR.CCUO.ETCS1DriverMessageAck.MmiIText.Atomic.WaitForCondition(Is.Equal, _mmiIText),

                // EVC111_alias_1: MMI_Q_ACK bits = 00xx 0000
                // EVC111_alias_1: MMI_Q_BUTTON bit = 0000 x000
                _pool.SITR.CCUO.ETCS1DriverMessageAck.EVC111alias1.Atomic.WaitForCondition(Is.Equal, 0x10)
            };

            _checkResult = _pool.WaitForConditionAtomic(list, 10000, 100);

            // Get time stamp of received packet
            _timeStamp = _pool.SITR.CCUO.ETCS1DriverMessageAck.MmiTButtonEvent.Value;

            // If check passes
            if (_checkResult)
            {
                _pool.TraceReport(baseString + _mmiIText + Environment.NewLine +
                                  "Time stamp = " + _timeStamp + Environment.NewLine +
                                  "Result: Acknowledge Released PASSED.");
            }

            // Else display failure
            else
            {
                _pool.TraceError(baseString + _mmiIText + Environment.NewLine +
                                 "Time stamp = " + _timeStamp + Environment.NewLine +
                                 "Result: Acknowledge Released FAILED.");
            }
        }

        /// <summary>
        /// Check that a particular MMI_I_TEXT NOT Acknowledged is Pressed
        /// </summary>
        /// <param name="mmiIText">Identifier of the acknowledged text</param>
        private static void Check_Driver_Message_Not_Ack_Pressed()
        {
            // Reset telegram received flag in RTSim
            _pool.SITR.SMDStat.CCUO.ETCS1DriverMessageAck.Value = 0x00;

            var list = new List<Atomic>
            {
                _pool.SITR.SMDStat.CCUO.ETCS1DriverMessageAck.Atomic.WaitForCondition(Is.Equal, 1),
                _pool.SITR.CCUO.ETCS1DriverMessageAck.MmiIText.Atomic.WaitForCondition(Is.Equal, _mmiIText),

                // EVC111_alias_1: MMI_Q_ACK bits = 00xx 0000
                // EVC111_alias_1: MMI_Q_BUTTON bit = 0000 x000
                _pool.SITR.CCUO.ETCS1DriverMessageAck.EVC111alias1.Atomic.WaitForCondition(Is.Equal, 0x28)
            };

            _checkResult = _pool.WaitForConditionAtomic(list, 10000, 100);

            // Get time stamp of received packet
            _timeStamp = _pool.SITR.CCUO.ETCS1DriverMessageAck.MmiTButtonEvent.Value;

            // If check passes
            if (_checkResult)
            {
                _pool.TraceReport(baseString + _mmiIText + Environment.NewLine +
                                  "Time stamp = " + _timeStamp + Environment.NewLine +
                                  "Result: NOT Acknowledge Pressed PASSED.");
            }

            // Else display failure
            else
            {
                _pool.TraceError(baseString + _mmiIText + Environment.NewLine +
                                 "Time stamp = " + _timeStamp + Environment.NewLine +
                                 "Result: NOT Acknowledge Pressed FAILED.");
            }
        }

        /// <summary>
        /// Check that a particular MMI_I_TEXT NOT Acknowledged is Released
        /// </summary>
        /// <param name="mmiIText">Identifier of the acknowledged text</param>
        private static void Check_Driver_Message_Not_Ack_Released()
        {
            // Reset telegram received flag in RTSim
            _pool.SITR.SMDStat.CCUO.ETCS1DriverMessageAck.Value = 0x00;

            var list = new List<Atomic>
            {
                _pool.SITR.SMDStat.CCUO.ETCS1DriverMessageAck.Atomic.WaitForCondition(Is.Equal, 1),
                _pool.SITR.CCUO.ETCS1DriverMessageAck.MmiIText.Atomic.WaitForCondition(Is.Equal, _mmiIText),

                // EVC111_alias_1: MMI_Q_ACK bits = 00xx 0000
                // EVC111_alias_1: MMI_Q_BUTTON bit = 0000 x000
                _pool.SITR.CCUO.ETCS1DriverMessageAck.EVC111alias1.Atomic.WaitForCondition(Is.Equal, 0x20)
            };

            _checkResult = _pool.WaitForConditionAtomic(list, 10000, 100);

            // Get time stamp of received packet
            _timeStamp = _pool.SITR.CCUO.ETCS1DriverMessageAck.MmiTButtonEvent.Value;

            // If check passes
            if (_checkResult)
            {
                _pool.TraceReport(baseString + _mmiIText + Environment.NewLine +
                                  "Time stamp = " + _timeStamp + Environment.NewLine +
                                  "Result: NOT Acknowledge Released PASSED.");
            }

            // Else display failure
            else
            {
                _pool.TraceError(baseString + _mmiIText + Environment.NewLine +
                                 "Time stamp = " + _timeStamp + Environment.NewLine +
                                 "Result: NOT Acknowledge Released FAILED.");
            }
        }
    }

    /// <summary>
    /// MMI_Q_ACK enum
    /// 
    /// Values:
    /// "Spare" = 0
    /// "AcknowledgeYES" = 1
    /// "NotAcknowledgeNO" = 2
    /// </summary>
    public enum MMI_Q_ACK : byte
    {
        Spare = 0,
        AcknowledgeYES = 1,
        NotAcknowledgeNO = 2
    }
}