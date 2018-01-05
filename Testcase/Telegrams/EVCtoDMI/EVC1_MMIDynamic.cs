﻿#region usings
using CL345;
using System;
using static Testcase.Telegrams.EVCtoDMI.Variables;
#endregion

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This packet contains dynamic information, such as current train speed, current position and current target data for the driver.
    /// 
    /// Note:
    /// This packet is routed via dedicated port and thus no header nor length information is contained in the (plain) data set.
    /// It is also protected via SDTv2.
    /// Some variables that were formerly contained in EVC-1 are now part of EVC-7, which has to be evaluated by the DMI as well.
    /// </summary>
    public static class EVC1_MMIDynamic
    {
        private static SignalPool _pool;            // Signal pool
        private static byte _mmiMSlip;              // Slip status
        private static byte _mmiMSlide;             // Slide status
        private static MMI_M_WARNING _mmiMWarning;  // Warning/indication status

        /// <summary>
        /// Initialise EVC-1 MMI Dynamic telegram.
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // Set default values
            _pool.SITR.ETCS1.Dynamic.EVC1alias1.Value = 0;          // No slip/slide, normal indication, ceiling speed monitoring
            _pool.SITR.ETCS1.Dynamic.MmiVTrain.Value = 0;           // Speed = 0
            _pool.SITR.ETCS1.Dynamic.MmiATrain.Value = 0;           // Acceleration = 0
            _pool.SITR.ETCS1.Dynamic.MmiVTarget.Value = -1;         // No target speed         
            _pool.SITR.ETCS1.Dynamic.MmiVPermitted.Value = 0;       // Permitted speed = 0
            _pool.SITR.ETCS1.Dynamic.MmiVIntervention.Value = -1;   // No intervention speed
            _pool.SITR.ETCS1.Dynamic.MmiVRelease.Value = -1;        // No release speed
            _pool.SITR.ETCS1.Dynamic.MmiOBraketarget.Value = -1;    // No restrictive discontinuity of the static speed profile
            _pool.SITR.ETCS1.Dynamic.MmiOIml.Value = -1;            // Spare
            _pool.SITR.ETCS1.Dynamic.EVC01Validity1.Value = 0xc800; // 51200 in decimal
            _pool.SITR.ETCS1.Dynamic.EVC01Validity2.Value = 0xff00; // 65280 in decimal
            _pool.SITR.ETCS1.Dynamic.EVC01SSW1.Value = 0x8000;      // 32768 in decimal
            _pool.SITR.ETCS1.Dynamic.EVC01SSW2.Value = 0x8000;      // 32768 in decimal
            _pool.SITR.ETCS1.Dynamic.EVC01SSW3.Value = 0x8000;      // 32768 in decimal
        }

        private static void SetAlias()
        {
            byte mmiMWarning = (byte)_mmiMWarning;

            _pool.SITR.ETCS1.Dynamic.EVC1alias1.Value = (byte)(_mmiMSlip << 7 | _mmiMSlide << 6 | mmiMWarning);
        }

        /// <summary>
        /// This function allows to stop sending periodically any EVC-1 forcing the DMI to go to "ATP-Down" state
        /// MMI_gen 244-- 	If the ETCS-MMI is in “active” state and [EVC-1] is lost*, the MMI shall enter “ATP-Down” state...
        /// </summary>
        /// <param name="pool"></param>
        public static void ForceComunicationLoss(SignalPool pool)
        {
            _pool.SITR.STGCtrl.ETCS1.Dynamic.Force(0x0000);
        }

        public static void UnforceCommunicationLoss(SignalPool pool)
        {
            _pool.SITR.STGCtrl.ETCS1.Dynamic.Unforce();
            _pool.SITR.STGCtrl.ETCS1.Dynamic.Value = 0x0001;
        }

        /// <summary>
        /// Qualifier telling whether the MMI shall initiate an indication or a warning
        /// </summary>
        public static MMI_M_WARNING MMI_M_WARNING
        {
            get => _mmiMWarning;

            set
            {
                _mmiMWarning = value;
                SetAlias();
            }
        }

        /// <summary>
        /// Slip status
        /// 
        /// Values:
        /// 0 = "No axle is slipping"   (DEFAULT)
        /// 1 = "Any axle is slipping"
        /// </summary>
        public static byte MMI_M_SLIP
        {
            get => _mmiMSlip;

            set
            {
                _mmiMSlip = value;
                SetAlias();
            }
        }

        /// <summary>
        /// Slide status
        /// 
        /// Values:
        /// 0 = "No axle is sliding"    (DEFAULT)
        /// 1 = "Any axle is sliding"
        /// </summary>
        public static byte MMI_M_SLIDE
        {
            get => _mmiMSlide;

            set
            {
                _mmiMSlide = value;
                SetAlias();
            }
        }

        /// <summary>
        /// Estimated train speed at the actual time, without tolerance added.
        /// 
        /// Values:
        /// 0...11111 cm/s
        /// -1 = "Speed unknown"
        /// </summary>
        public static short MMI_V_TRAIN
        {
            get => _pool.SITR.ETCS1.Dynamic.MmiVTrain.Value;
            set => _pool.SITR.ETCS1.Dynamic.MmiVTrain.Value = value;
        }

        /// <summary>
        /// Estimated train speed at the actual time, without tolerance added.
        /// 
        /// Values:
        /// 0...399 km/h
        /// -1 = "Speed unknown" (DEFAULT)
        /// </summary>
        public static short MMI_V_TRAIN_KMH
        {
            get => Convert.ToInt16(_pool.SITR.ETCS1.Dynamic.MmiVTrain.Value * CmSToKmH);
            set => _pool.SITR.ETCS1.Dynamic.MmiVTrain.Value = Convert.ToInt16(value / CmSToKmH);
        }

        /// <summary>
        /// Estimated train speed at the actual time, without tolerance added.
        /// 
        /// Values:
        /// 0...248 mph
        /// -1 = "Speed unknown" (DEFAULT)
        /// </summary>
        public static short MMI_V_TRAIN_MPH
        {
            get => Convert.ToInt16(_pool.SITR.ETCS1.Dynamic.MmiVTrain.Value * CmSToMph);
            set => _pool.SITR.ETCS1.Dynamic.MmiVTrain.Value = Convert.ToInt16(value / CmSToMph);
        }

        /// <summary>
        /// Sets train speed to "unknown"
        /// </summary>
        public static void MmiVTrainUnknown()
        {
            _pool.SITR.ETCS1.Dynamic.MmiVTrain.Value = -1;
        }

        /// <summary>
        /// Current train acceleration (positive) or deceleration (negative) in cm/s^2
        /// </summary>
        public static short MMI_A_TRAIN
        {
            get => _pool.SITR.ETCS1.Dynamic.MmiVTrain.Value;
            set => _pool.SITR.ETCS1.Dynamic.MmiVTrain.Value = value;
        }

        /// <summary>
        /// Current train acceleration (positive) or deceleration (negative) in m/s^2
        /// </summary>
        public static short MMI_A_TRAIN_M
        {
            get => Convert.ToInt16 (_pool.SITR.ETCS1.Dynamic.MmiATrain.Value * 0.01);
            set => _pool.SITR.ETCS1.Dynamic.MmiATrain.Value = Convert.ToInt16(value / 0.01);
        }

        /// <summary>
        /// Speed to be applied at the next restrictive static speed profile discontinuity which has influence on the braking curve.
        /// 
        /// Values:
        /// 0...11111 = cm/s
        /// -1 = "No target" (DEFAULT)
        /// </summary>
        public static short MMI_V_TARGET
        {
            get => _pool.SITR.ETCS1.Dynamic.MmiVTarget.Value;
            set => _pool.SITR.ETCS1.Dynamic.MmiVTarget.Value = value;
        }

        /// <summary>
        /// Speed to be applied at the next restrictive static speed profile discontinuity which has influence on the braking curve.
        /// 
        /// Values:
        /// 0...399 = km/h
        /// -1 = "No target" (DEFAULT)
        /// </summary>
        public static short MMI_V_TARGET_KMH
        {
            get => Convert.ToInt16(_pool.SITR.ETCS1.Dynamic.MmiVTarget.Value * CmSToKmH);
            set => _pool.SITR.ETCS1.Dynamic.MmiVTarget.Value = Convert.ToInt16(value / CmSToKmH);
        }

        /// <summary>
        /// Speed to be applied at the next restrictive static speed profile discontinuity which has influence on the braking curve.
        /// 
        /// Values:
        /// 0...248 = mph
        /// -1 = "No target" (DEFAULT)
        /// </summary>
        public static short MMI_V_TARGET_MPH
        {
            get => Convert.ToInt16(_pool.SITR.ETCS1.Dynamic.MmiVTarget.Value * CmSToMph);
            set => _pool.SITR.ETCS1.Dynamic.MmiVTarget.Value = Convert.ToInt16(value / CmSToMph);
        }

        /// <summary>
        /// Sets V Target to "No target"
        /// </summary>
        public static void MmiVNoTarget()
        {
            _pool.SITR.ETCS1.Dynamic.MmiVTarget.Value = -1;
        }

        /// <summary>
        /// Permitted speed according to current ATP rules.
        /// 
        /// Values:
        /// 0...11111 = cm/s
        /// </summary>
        public static ushort MMI_V_PERMITTED
        {
            get => Convert.ToUInt16(_pool.SITR.ETCS1.Dynamic.MmiVPermitted.Value);
            set => _pool.SITR.ETCS1.Dynamic.MmiVPermitted.Value = Convert.ToInt16(value);
        }

        /// <summary>
        /// Permitted speed according to current ATP rules.
        /// 
        /// Values:
        /// 0...399 = km/h
        /// </summary>
        public static ushort MMI_V_PERMITTED_KMH
        {
            get => Convert.ToUInt16(_pool.SITR.ETCS1.Dynamic.MmiVPermitted.Value * CmSToKmH);
            set => _pool.SITR.ETCS1.Dynamic.MmiVPermitted.Value = Convert.ToInt16(value / CmSToKmH);
        }

        /// <summary>
        /// Permitted speed according to current ATP rules.
        /// 
        /// Values:
        /// 0...248 = mph
        /// </summary>
        public static ushort MMI_V_PERMITTED_MPH
        {
            get => Convert.ToUInt16(_pool.SITR.ETCS1.Dynamic.MmiVPermitted.Value * CmSToMph);
            set => _pool.SITR.ETCS1.Dynamic.MmiVPermitted.Value = Convert.ToInt16(value / CmSToMph);
        }

        /// <summary>
        /// Gives the speed of the first intervention curve (the speed for service brake intervention) (see SRS 4.5.6.1 and 4.5.6.2) at the current location of the train.
        /// 
        /// Values:
        /// 0...11111 = cm/s
        /// 
        /// Note:
        /// 11111 = 400 km/h
        /// </summary>
        public static short MMI_V_INTERVENTION
        {
            get => _pool.SITR.ETCS1.Dynamic.MmiVIntervention.Value;
            set => _pool.SITR.ETCS1.Dynamic.MmiVIntervention.Value = value;
        }

        /// <summary>
        /// Gives the speed of the first intervention curve (the speed for service brake intervention) (see SRS 4.5.6.1 and 4.5.6.2) at the current location of the train.
        /// 
        /// Values:
        /// 0...399 = km/h
        /// </summary>
        public static short MMI_V_INTERVENTION_KMH
        {
            get => Convert.ToInt16(_pool.SITR.ETCS1.Dynamic.MmiVIntervention.Value * CmSToKmH);
            set => _pool.SITR.ETCS1.Dynamic.MmiVIntervention.Value = Convert.ToInt16(value / CmSToKmH);
        }

        /// <summary>
        /// Gives the speed of the first intervention curve (the speed for service brake intervention) (see SRS 4.5.6.1 and 4.5.6.2) at the current location of the train.
        /// 
        /// Values:
        /// 0...248 = mph
        /// </summary>
        public static short MMI_V_INTERVENTION_MPH
        {
            get => Convert.ToInt16(_pool.SITR.ETCS1.Dynamic.MmiVIntervention.Value * CmSToMph);
            set => _pool.SITR.ETCS1.Dynamic.MmiVIntervention.Value = Convert.ToInt16(value / CmSToMph);
        }

        /// <summary>
        /// Release speed at the EOA.
        /// 
        /// Values:
        /// 0...11111 = cm/s
        /// -1 = "Not applicable, i.e. train currently not in release speed area" (DEFAULT)
        /// </summary>
        public static short MMI_V_RELEASE
        {
            get => _pool.SITR.ETCS1.Dynamic.MmiVRelease.Value;
            set => _pool.SITR.ETCS1.Dynamic.MmiVRelease.Value = value;
        }

        /// <summary>
        /// Release speed at the EOA.
        /// 
        /// Values:
        /// 0...399 = km/h
        /// -1 = "Not applicable, i.e. train currently not in release speed area" (DEFAULT)
        /// </summary>
        public static short MMI_V_RELEASE_KMH
        {
            get => Convert.ToInt16(_pool.SITR.ETCS1.Dynamic.MmiVRelease.Value * CmSToKmH);
            set => _pool.SITR.ETCS1.Dynamic.MmiVRelease.Value = Convert.ToInt16(value / CmSToKmH);
        }

        /// <summary>
        /// Release speed at the EOA.
        /// 
        /// Values:
        /// 0...248 = mph
        /// -1 = "Not applicable, i.e. train currently not in release speed area" (DEFAULT)
        /// </summary>
        public static short MMI_V_RELEASE_MPH
        {
            get => Convert.ToInt16(_pool.SITR.ETCS1.Dynamic.MmiVRelease.Value * CmSToMph);
            set => _pool.SITR.ETCS1.Dynamic.MmiVRelease.Value = Convert.ToInt16(value / CmSToMph);
        }

        /// <summary>
        /// Sets V Release to "Not applicable, i.e. train currently not in release speed area"
        /// </summary>
        public static void MmiVNoRelease()
        {
            _pool.SITR.ETCS1.Dynamic.MmiVRelease.Value = -1;
        }

        /// <summary>
        /// This is the position in odometer co-ordinates of the next restrictive discontinuity of the static speed profile or target,
        /// which has influence on the braking curve. This position can be adjusted depending on supervision.
        /// 
        /// Values:
        /// 0...2147483646 cm
        /// 2147483647 = "Infinite distance in Reversing mode (RV)"
        /// -1 = "No target. Note that a target most often exists also before any target indication according to M_Warning is given.
        /// The "no target" value only applies in low level modes where wayside data is not used or only partly used" (DEFAULT)
        /// </summary>
        public static int MMI_O_BRAKETARGET
        {
            get => _pool.SITR.ETCS1.Dynamic.MmiOBraketarget.Value;
            set => _pool.SITR.ETCS1.Dynamic.MmiOBraketarget.Value = value;
        }

        /// <summary>
        /// This is the position in odometer co-ordinates of the next restrictive discontinuity of the static speed profile or target,
        /// which has influence on the braking curve. This position can be adjusted depending on supervision.
        /// 
        /// Values:
        /// 0...21474836 m
        /// 2147483647 = "Infinite distance in Reversing mode (RV)"
        /// -1 = "No target. Note that a target most often exists also before any target indication according to M_Warning is given.
        /// The "no target" value only applies in low level modes where wayside data is not used or only partly used" (DEFAULT)
        /// </summary>
        public static int MMI_O_BRAKETARGET_M
        {
            get => _pool.SITR.ETCS1.Dynamic.MmiOBraketarget.Value * 100;
            set => _pool.SITR.ETCS1.Dynamic.MmiOBraketarget.Value = value / 100;
        }

        /// <summary>
        /// Sets MMI_O_BRAKETARGET to "No target"
        /// </summary>
        public static void MmiONoBrakeTarget()
        {
            _pool.SITR.ETCS1.Dynamic.MmiOBraketarget.Value = -1;
        }

        /// <summary>
        /// Sets MMI_O_BRAKETARGET to "Infinite distance in Reversing mode (RV)"
        /// </summary>
        public static void MmiONoBrakeTargetInfiniteReverse()
        {
            _pool.SITR.ETCS1.Dynamic.MmiOBraketarget.Value = 2147483647;
        }

        /// <summary>
        /// This is the location in odometer co-ordinates of the indication marker for the next brake target. This position can be adjusted depending on supervision.
        /// 
        /// Values:
        /// 0...2147483647 cm
        /// -1 = "spare"
        /// 
        /// Note 1:
        /// The odometer related variables will only contain bit 0-31 of the source variable.
        /// I.e. the variable will wrap from 2147483647 -> 0. The receiver should be able to handle this.
        /// </summary>
        public static int MMI_O_IML
        {
            get => _pool.SITR.ETCS1.Dynamic.MmiOIml.Value;
            set => _pool.SITR.ETCS1.Dynamic.MmiOIml.Value = value;
        }

        /// <summary>
        /// This is the location in odometer co-ordinates of the indication marker for the next brake target. This position can be adjusted depending on supervision.
        /// 
        /// Values:
        /// 0...21474837 m
        /// -1 = "spare"
        /// 
        /// Note 1:
        /// The odometer related variables will only contain bit 0-31 of the source variable.
        /// I.e. the variable will wrap from 2147483647 -> 0. The receiver should be able to handle this.
        /// </summary>
        public static int MMI_O_IML_M
        {
            get => _pool.SITR.ETCS1.Dynamic.MmiOIml.Value * 100;
            set => _pool.SITR.ETCS1.Dynamic.MmiOIml.Value = value / 100;
        }

        /// <summary>
        /// Sets MMI_O_IML to "spare"
        /// </summary>
        public static void MmiOImlSpare()
        {
            _pool.SITR.ETCS1.Dynamic.MmiOIml.Value = -1;
        }
    }

    /// <summary>
    /// Qualifier telling whether the MMI shall initiate an indication or a warning
    /// 
    /// Values:
    /// 0 = "Status = NoS,              Supervision=CSM"
    /// 1 = "Status = IndS,             Supervision=TSM"
    /// 2 = "Status = NoS,              Supervision=PIM"
    /// 3 = "Status = IndS,             Supervision=RSM"
    /// 4 = "Status = WaS,              Supervision=CSM"
    /// 5 = "Status = WaS and IndS,     Supervision=TSM"
    /// 6 = "Status = WaS,              Supervision=PIM"
    /// 7 = "Spare"
    /// 8 = "Status = OvS,              Supervision=CSM"
    /// 9 = "Status = OvS and IndS,     Supervision=TSM"
    /// 10 = "Status = OvS,             Supervision=PIM"
    /// 11 = "Status = NoS,             Supervision=TSM"
    /// 12 = "Status = IntS,            Supervision=CSM"
    /// 13 = "Status = IntS and IndS,   Supervision=TSM"
    /// 14 = "Status = IntS,            Supervision=PIM"
    /// 15 = "Status = IntS and IndS,   Supervision=RSM"
    ///
    /// Abbreviations:
    /// 1. Status
    ///     IndS:	Indication Status
    ///     IntS:	Intervention Status
    ///     NoS:	Normal Status
    ///     OvS:	Over-speed Status
    ///     WaS:	Warning Status
    /// 2. Supervision:
    /// 	CSM:	Ceiling Speed Monitoring
    ///     RSM:	Release Speed Monitoring
    ///     TSM:	Target Speed Monitoring
    ///     PIM:	Pre-Indication Monitoring
    /// </summary>
    public enum MMI_M_WARNING : byte
    {
        Normal_Status_Ceiling_Speed_Monitoring = 0,
        Indication_Status_Target_Speed_Monitoring = 1,
        Normal_Status_PreIndication_Monitoring = 2,
        Indication_Status_Release_Speed_Monitoring = 3,
        Warning_Status_Ceiling_Speed_Monitoring = 4,
        Warning_Status_Indication_Status_Target_Speed_Monitoring = 5,
        Warning_Status_PreIndication_Monitoring = 6,
        Spare = 7,
        Overspeed_Status_Ceiling_Speed_Monitoring = 8,
        Overspeed_Status_Indication_Status_Target_Speed_Monitoring = 9,
        Overspeed_Status_PreIndication_Monitoring = 10,
        Normal_Status_Target_Speed_Monitoring = 11,
        Intervention_Status_Ceiling_Speed_Monitoring = 12,
        Intervention_Status_Indication_Status_Target_Speed_Monitoring = 13,
        Intervention_Status_PreIndication_Monitoring = 14,
        Intervention_Status_Indication_Status_Release_Speed_Monitoring = 15
    }
}