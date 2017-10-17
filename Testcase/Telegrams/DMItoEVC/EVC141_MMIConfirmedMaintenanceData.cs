using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using CL345;
using Testcase.DMITestCases;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.Telegrams.DMItoEVC
{
    /// <summary>
    /// This packet shall be sent when the presented maintenance data have been confirmed.
    /// The content is the same as in packet EVC-41,“MMI_ECHOED_MAINTENANCE_DATA”. 
    /// This packet is used in relation with packets EVC-40, EVC-41 and EVC-140..
    /// </summary>
    public static class EVC141_MMIConfirmedMaintenanceData
    {
        private const string BaseString = "DMI->ETCS: EVC-141 [MMI_CONFIRMED_MAINTENANCE_DATA]";

        private static SignalPool _pool;
        private static ushort _wheelSize1;
        private static ushort _wheelSize2;
        private static byte _wheelSizeError;
        private static uint _pulsePerKm1;
        private static uint _pulsePerKm2;
        private static Variables.MMI_Q_MD_DATASET _mdDataset;


        /// <summary>
        /// Initialise EVC-141 MMI_CONFIRMED_MAINTENANCE_DATA
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            _pool.SITR.SMDStat.CCUO.ETCS1NewMaintenanceData.Value = 0x00;
            _pool.SITR.SMDCtrl.CCUO.ETCS1NewMaintenanceData.Value = 0x01;
        }

        public static void CheckTelegram()
        {
            // Reset telegram received flag in RTSim
            _pool.SITR.SMDStat.CCUO.ETCS1NewMaintenanceData.Value = 0x00;

            // Check if telegram received flag has been set. Allows 10 seconds.
            if (_pool.SITR.SMDStat.CCUO.ETCS1NewMaintenanceData.WaitForCondition(Is.Equal, 1, 10000, 100))
            {
                bool valuesMatch = (_mdDataset == Variables.MMI_Q_MD_DATASET.WheelDiameter)
                    ? (_pool.SITR.CCUO.ETCS1ConfirmedMaintenanceData.MmiMSduWheelSize1R.Value.Equals(_wheelSize1) &&
                       _pool.SITR.CCUO.ETCS1ConfirmedMaintenanceData.MmiMSduWheelSize2R.Value.Equals(_wheelSize2) &&
                       _pool.SITR.CCUO.ETCS1ConfirmedMaintenanceData.MmiMWheelSizeErrR.Value.Equals(_wheelSizeError))
                    : (_pool.SITR.CCUO.ETCS1ConfirmedMaintenanceData.MmiMPulsePerKm1R.Value.Equals(_pulsePerKm1) &&
                       _pool.SITR.CCUO.ETCS1ConfirmedMaintenanceData.MmiMPulsePerKm2R.Value.Equals(_pulsePerKm2));

                // Check if values match
                if (valuesMatch)
                {
                    _pool.TraceReport($"{BaseString}:" + Environment.NewLine +
                                      ((_mdDataset == Variables.MMI_Q_MD_DATASET.WheelDiameter)
                                          ? $"MMI_M_WHEEL_SIZE_1 = {_wheelSize1} " + Environment.NewLine +
                                            $"MMI_M_WHEEL_SIZE_2 = {_wheelSize2} " + Environment.NewLine +
                                            $"MMI_M_WHEEL_SIZE_ERR = {_wheelSizeError} "
                                          : $"MMI_M_PULSE_PER_KM_1 = {_pulsePerKm1} " + Environment.NewLine +
                                            $"MMI_M_PULSE_PER_KM_2 = {_pulsePerKm2} ")
                                      + Environment.NewLine + "Result = PASSED.");
                }
                // Else display the real value extracted from EVC-141
                else
                {
                    _pool.TraceError($"{BaseString}:" + Environment.NewLine +
                                     ((_mdDataset == Variables.MMI_Q_MD_DATASET.WheelDiameter)
                                         ? $"MMI_M_WHEEL_SIZE_1 = {_pool.SITR.CCUO.ETCS1ConfirmedMaintenanceData.MmiMSduWheelSize1R} " +
                                           Environment.NewLine +
                                           $"MMI_M_WHEEL_SIZE_2 = {_pool.SITR.CCUO.ETCS1ConfirmedMaintenanceData.MmiMSduWheelSize2R} " +
                                           Environment.NewLine +
                                           $"MMI_M_WHEEL_SIZE_ERR = {_pool.SITR.CCUO.ETCS1ConfirmedMaintenanceData.MmiMWheelSizeErrR} "
                                         : $"MMI_M_PULSE_PER_KM_1 = {_pool.SITR.CCUO.ETCS1ConfirmedMaintenanceData.MmiMPulsePerKm1R} " +
                                           Environment.NewLine +
                                           $"MMI_M_PULSE_PER_KM_2 = {_pool.SITR.CCUO.ETCS1ConfirmedMaintenanceData.MmiMPulsePerKm2R} "
                                     ) +
                                     Environment.NewLine + "Result: FAILED.");
                }
            }
            // Show generic DMI -> EVC telegram failure
            else
            {
                DmiExpectedResults.DMItoEVC_Telegram_Not_Received(_pool, BaseString);
            }
        }

        /// <summary>
        /// Wheel diameter for tacho 1
        /// 
        /// Values:
        /// 0..499 = "Reserved"
        /// 1501..65529 = "Reserved"
        /// 65530 = "Technical Range Check failed"
        /// 65531 = "Technical Resolution Check failed"
        /// 65532 = "Technical Cross Check failed"
        /// 65533 = "Operational Range Check failed"
        /// 65534 = "Operational Cross Check failed"
        /// 65535 = "Reserved"
        /// Note: All special values concerning cross/range checks are only used in packets EVC-40 and EVC-41.
        /// </summary>
        public static Variables.MMI_M_SDU_WHEEL_SIZE MMI_M_SDU_WHEEL_SIZE_1
        {
            set => _wheelSize1 = (ushort) value;
        }

        /// <summary>
        /// Wheel diameter for tacho 2
        /// 
        /// Values:
        /// 0..499 = "Reserved"
        /// 1501..65529 = "Reserved"
        /// 65530 = "Technical Range Check failed"
        /// 65531 = "Technical Resolution Check failed"
        /// 65532 = "Technical Cross Check failed"
        /// 65533 = "Operational Range Check failed"
        /// 65534 = "Operational Cross Check failed"
        /// 65535 = "Reserved"
        /// Note: All special values concerning cross/range checks are only used in packets EVC-40 and EVC-41.
        /// </summary>
        public static Variables.MMI_M_SDU_WHEEL_SIZE MMI_M_SDU_WHEEL_SIZE_2
        {
            set => _wheelSize2 = (ushort) value;
        }

        /// <summary>
        /// Accuracy of wheel diameter
        /// Values:
        /// 
        /// 33..249 = "Reserved"
        /// 250 = "Technical Range Check failed"
        /// 251 = "Technical Resolution Check failed"
        /// 252 = "Technical Cross Check failed"
        /// 253 = "Operational Range Check failed"
        /// 254 = "Operational Cross Check failed"
        /// 255 = "Reserved"
        /// Note: All special values concerning cross/range checks are only used in packets EVC-40 and EVC-41.
        /// </summary>
        public static Variables.MMI_M_WHEEL_SIZE_ERR MMI_M_WHEEL_SIZE_ERR
        {
            set => _wheelSizeError = (byte) value;
        }

        /// <summary>
        /// This is a maintenance parameter for Doppler radar 1. It gives the number of pulses per km-travelled distance.
        /// 
        /// Values:Values:
        /// 0 = "No Radar 1 on board"
        /// 1..20000 = "Reserved"
        /// 85535..4294967289 = "Reserved"
        /// 4294967290 = "Technical Range Check failed"
        /// 4294967291 = "Technical Resolution Check failed"
        /// 4294967292 = "Technical Cross Check failed"
        /// 4294967293 = "Operational Range Check failed"
        /// 4294967294 = "Operational Cross Check failed"
        /// 4294967295 = "Reserved"
        /// Note: All special values concerning cross/range checks are only used in packets EVC-40 and EVC-41.
        /// </summary>
        public static Variables.MMI_M_PULSE_PER_KM MMI_M_PULSE_PER_KM_1
        {
            set => _pulsePerKm1 = (uint) value;
        }

        /// <summary>
        /// This is a maintenance parameter for Doppler radar 2. It gives the number of pulses per km-travelled distance.
        /// 
        /// Values:Values:
        /// 0 = "No Radar 1 on board"
        /// 1..20000 = "Reserved"
        /// 85535..4294967289 = "Reserved"
        /// 4294967290 = "Technical Range Check failed"
        /// 4294967291 = "Technical Resolution Check failed"
        /// 4294967292 = "Technical Cross Check failed"
        /// 4294967293 = "Operational Range Check failed"
        /// 4294967294 = "Operational Cross Check failed"
        /// 4294967295 = "Reserved"
        /// Note: All special values concerning cross/range checks are only used in packets EVC-40 and EVC-41.
        /// </summary>
        public static Variables.MMI_M_PULSE_PER_KM MMI_M_PULSE_PER_KM_2

        {
            set => _pulsePerKm2 = (uint) value;
        }

        /// <summary>
        /// Indicates the content of the maintenance telegram
        /// 
        /// Values:
        /// 0 = "Wheel diameter"
        /// 1 = "Doppler"
        /// </summary>
        public static Variables.MMI_Q_MD_DATASET MMI_Q_MD_DATASET
        {
            set => _mdDataset = value;
        }
    }
}