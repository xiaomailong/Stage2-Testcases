﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL345;
using static Testcase.Telegrams.EVCtoDMI.Variables;

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This packet shall be sent when stored maintenance data shall be presented via the MMI. 
    /// This packet is used in relation with packets EVC-41, EVC-140 and EVC-141. 
    /// The purpose of those packets is to provide vital maintenance data to the ATP.
    /// </summary>
    public static class EVC41_MMIEchoedMaintenanceData
    {
        private static SignalPool _pool;

        /// <summary>
        /// Initialise EVC-40 MMI_Current_Maintenance_Data telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // Set default values
            _pool.SITR.ETCS1.EchoedMaintenanceData.MmiMPacket.Value = 41;  // Packet ID
            _pool.SITR.ETCS1.EchoedMaintenanceData.MmiLPacket.Value = 144; // Packet length
        }

        /// <summary>
        /// Send EVC40 Additional Order telegram.
        /// </summary>
        public static void Send()
        {
            _pool.SITR.SMDCtrl.ETCS1.EchoedMaintenanceData.Value = 0x0001;
        }

        /// <summary>
        /// This is a maintenance parameter for Doppler radar 1. 
        /// It gives the number of pulses per km-travelled distance.
        /// 
        /// Values:
        /// 0 = "No Radar 1 on board"
        /// 1..20000 = "Reserved"
        /// 85535..4294967289 = "Reserved"
        /// 4294967290 = "Technical Range Check failed"
        /// 4294967291 = "Technical Resolution Check failed"
        /// 4294967292 = "Technical Cross Check failed"
        /// 4294967293 = "Operational Range Check failed"
        /// 4294967294 = "Operational Cross Check failed"
        /// 4294967295 = "Reserved"
        /// 
        /// Note: All special values concerning cross/range checks are only used in packets EVC-40 and EVC-41.
        /// </summary>
        public static MMI_M_PULSE_PER_KM MMI_M_PULSE_PER_KM_1
        {
            set
            {
                uint pulsesPerKm = (uint)value;
                pulsesPerKm = ~pulsesPerKm;

                _pool.SITR.ETCS1.EchoedMaintenanceData.MmiMPulsePerKm1R.Value = BitReverser32(pulsesPerKm);
            }
        }

        /// <summary>
        /// This is a maintenance parameter for Doppler radar 2. 
        /// It gives the number of pulses per km-travelled distance.
        /// 
        /// Values:
        /// 0 = "No Radar 1 on board"
        /// 1..20000 = "Reserved"
        /// 85535..4294967289 = "Reserved"
        /// 4294967290 = "Technical Range Check failed"
        /// 4294967291 = "Technical Resolution Check failed"
        /// 4294967292 = "Technical Cross Check failed"
        /// 4294967293 = "Operational Range Check failed"
        /// 4294967294 = "Operational Cross Check failed"
        /// 4294967295 = "Reserved"
        /// 
        /// Note: All special values concerning cross/range checks are only used in packets EVC-40 and EVC-41.
        /// </summary>
        public static MMI_M_PULSE_PER_KM MMI_M_PULSE_PER_KM_2
        {
            set
            {
                uint pulsesPerKm = (uint)value;
                pulsesPerKm = ~pulsesPerKm;

                _pool.SITR.ETCS1.EchoedMaintenanceData.MmiMPulsePerKm2R.Value = BitReverser32(pulsesPerKm);
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
        /// 
        /// Note: All special values concerning cross/range checks are only used in packets EVC-40 and EVC-41.
        /// </summary>
        public static MMI_M_SDU_WHEEL_SIZE MMI_M_SDU_WHEEL_SIZE_1
        {
            set
            {
                uint wheelSize = (uint)value;
                wheelSize = ~wheelSize;

                _pool.SITR.ETCS1.EchoedMaintenanceData.MmiMSduWheelSize1R.Value = BitReverser16((ushort)wheelSize);
            }
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
        /// 
        /// Note: All special values concerning cross/range checks are only used in packets EVC-40 and EVC-41.
        /// </summary>
        public static MMI_M_SDU_WHEEL_SIZE MMI_M_SDU_WHEEL_SIZE_2
        {
            set
            {
                uint wheelSize = (uint)value;
                wheelSize = ~wheelSize;

                _pool.SITR.ETCS1.EchoedMaintenanceData.MmiMSduWheelSize2R.Value = BitReverser16((ushort)wheelSize);
            }
        }

        /// <summary>
        /// Indicates the content of the maintenance telegram
        /// 
        /// Values:
        /// 0 = "Wheel diameter"
        /// 1 = "Doppler"
        /// </summary>
        public static MMI_Q_MD_DATASET MMI_Q_MD_DATASET
        {
            set
            {
                int mmiQMdDataset = (int)value;
                mmiQMdDataset = ~mmiQMdDataset;

                _pool.SITR.ETCS1.EchoedMaintenanceData.MmiQMdDatasetR.Value = BitReverser8((byte)mmiQMdDataset);
            }
        }

        /// <summary>
        /// Accuracy of wheel diameter
        /// 
        /// Values:
        /// 33..249 = "Reserved"
        /// 250 = "Technical Range Check failed"
        /// 251 = "Technical Resolution Check failed"
        /// 252 = "Technical Cross Check failed"
        /// 253 = "Operational Range Check failed"
        /// 254 = "Operational Cross Check failed"
        /// 255 = "Reserved"
        /// 
        /// Note: All special values concerning cross/range checks are only used in packets EVC-40 and EVC-41.
        /// </summary>
        public static MMI_M_WHEEL_SIZE_ERR MMI_M_WHEEL_SIZE_ERR
        {
            set
            {
                int wheelSizeError = (int)value;
                wheelSizeError = ~wheelSizeError;

                _pool.SITR.ETCS1.EchoedMaintenanceData.MmiMWheelSizeErrR.Value = BitReverser8((byte)wheelSizeError);
            }
        }
    }
}
