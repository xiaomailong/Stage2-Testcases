#region usings

using System;
using System.Collections.Generic;
using CL345;

#endregion

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This packet contains trackside information to the driver.
    /// Whenever new information is received from trackside the speed profile and the gradient profile shall be sent to the MMI.
    /// </summary>
    public static class EVC4_MMITrackDescription
    {
        private static TestcaseBase _pool;

        /// <summary>
        /// Initialise EVC-4 MMI_Track_Descriptions telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(TestcaseBase pool)
        {
            _pool = pool;

            // Set default values
            _pool.SITR.ETCS1.TrackDescription.MmiMPacket.Value = 4; // Packet ID
            _pool.SITR.SMDCtrl.ETCS1.TrackDescription.Value = 0x8;

            TrackDescriptions = new List<TrackDescription>();
        }

        /// <summary>
        /// Send EVC-4 MMI_Track_Descriptions telegram.
        /// </summary>
        public static void Send()
        {
            ushort numberOfTrackDescriptions = (ushort) TrackDescriptions.Count;

            // Only 31 track descriptions allowed in telegram
            if (numberOfTrackDescriptions > 31)
                throw new ArgumentOutOfRangeException("Too many track descriptions.");

            // Set number of MRSP and Gradient track descriptions
            _pool.SITR.ETCS1.TrackDescription.MmiNMrsp.Value = numberOfTrackDescriptions;
            _pool.SITR.ETCS1.TrackDescription.MmiNGradient.Value = numberOfTrackDescriptions;

            // Initial packet size
            ushort totalSizeCounter = 96;

            // Base string used by SignalPool
            var baseString = "ETCS1_TrackDescription_EVC04TrackDescriptionSub";

            // For all track conditions
            for (int k = 0; k < numberOfTrackDescriptions; k++)
            {
                // Write SignalPool signals
                if (k < 10)
                {
                    _pool.SITR.Client.Write(string.Format("{0}10{1}_MmiOMrsp", baseString, k),
                        TrackDescriptions[k].MMI_O_MRSP);
                    _pool.SITR.Client.Write(string.Format("{0}10{1}_MmiVMrsp", baseString, k),
                        TrackDescriptions[k].MMI_V_MRSP);
                    _pool.SITR.Client.Write(string.Format("{0}20{1}_MmiOGradient", baseString, k),
                        TrackDescriptions[k].MMI_O_GRADIENT);
                    _pool.SITR.Client.Write(string.Format("{0}20{1}_MmiGGradient", baseString, k),
                        TrackDescriptions[k].MMI_G_GRADIENT);
                }

                else
                {
                    _pool.SITR.Client.Write(string.Format("{0}1{1}_MmiOMrsp", baseString, k),
                        TrackDescriptions[k].MMI_O_MRSP);
                    _pool.SITR.Client.Write(string.Format("{0}1{1}_MmiVMrsp", baseString, k),
                        TrackDescriptions[k].MMI_V_MRSP);
                    _pool.SITR.Client.Write(string.Format("{0}2{1}_MmiOGradient", baseString, k),
                        TrackDescriptions[k].MMI_O_GRADIENT);
                    _pool.SITR.Client.Write(string.Format("{0}2{1}_MmiGGradient", baseString, k),
                        TrackDescriptions[k].MMI_G_GRADIENT);
                }

                totalSizeCounter += 128;
            }

            _pool.SITR.ETCS1.TrackDescription.MmiLPacket.Value = totalSizeCounter;

            _pool.TraceInfo("ETCS->DMI: EVC-4 (MMI_TRACK_DESCRIPTIONS)");
            _pool.TraceInfo(string.Format("EVC-4: Number of track descriptions = {0}", numberOfTrackDescriptions));
            _pool.SITR.SMDCtrl.ETCS1.TrackDescription.Value = 0x000B;
            _pool.WaitForAck(_pool.SITR.SMDStat.ETCS1.TrackDescription);
        }

        /// <summary>
        /// The speed value at a discontinuity of the most restrictive speed profile.
        /// 
        /// Values:
        /// 0..11111 cm/s
        /// </summary>
        public static short MMI_V_MRSP_CURR
        {
            set
            {
                if (value > 11111)
                {
                    _pool.TraceWarning("Speed is greater than 11111 cm/.");
                    _pool.SITR.ETCS1.TrackDescription.MmiVMrspCurr.Value = value;
                }

                else
                    _pool.SITR.ETCS1.TrackDescription.MmiVMrspCurr.Value = value;
            }

            get { return _pool.SITR.ETCS1.TrackDescription.MmiVMrspCurr.Value; }
        }

        /// <summary>
        /// The speed value at a discontinuity of the most restrictive speed profile.
        /// 
        /// Values:
        /// 0..400 km/h
        /// </summary>
        public static short MMI_V_MRSP_CURR_KMH
        {
            set
            {
                if (value > 400)
                {
                    _pool.TraceWarning("Speed entered is greater than 400 km/h.");
                    _pool.SITR.ETCS1.TrackDescription.MmiVMrspCurr.Value = (short)(value / Variables.CmSToKmH);
                }

                else
                    _pool.SITR.ETCS1.TrackDescription.MmiVMrspCurr.Value = (short) (value / Variables.CmSToKmH);
            }

            get { return (short) (_pool.SITR.ETCS1.TrackDescription.MmiVMrspCurr.Value * Variables.CmSToKmH); }
        }

        /// <summary>
        /// The speed value at a discontinuity of the most restrictive speed profile.
        /// 
        /// Values:
        /// 0..248 mph
        /// </summary>
        public static short MMI_V_MRSP_CURR_MPH
        {
            set
            {
                if (value > 248)
                {
                    _pool.TraceWarning("Speed entered is greater than 248 mph.");
                    _pool.SITR.ETCS1.TrackDescription.MmiVMrspCurr.Value = (short)(value / Variables.CmSToMph);
                }
                    
                else
                    _pool.SITR.ETCS1.TrackDescription.MmiVMrspCurr.Value = (short) (value / Variables.CmSToMph);
            }

            get { return (short) (_pool.SITR.ETCS1.TrackDescription.MmiVMrspCurr.Value * Variables.CmSToMph); }
        }

        /// <summary>
        /// Gives the gradient value of a part of the track. Positive (up) and negative (down)
        /// 
        /// Values:
        /// -254..254 ‰ (Parts per 1000)
        /// -255 = "The gradient profile ends at the defined position
        /// </summary>
        public static short MMI_G_GRADIENT_CURR
        {
            set { _pool.SITR.ETCS1.TrackDescription.MmiGGradientCurr.Value = value; }
            get { return _pool.SITR.ETCS1.TrackDescription.MmiGGradientCurr.Value; }
        }

        /// <summary>
        /// List of track descriptions.
        /// </summary>
        public static List<TrackDescription> TrackDescriptions { get; set; }
    }

    /// <summary>
    /// Track description information.
    /// </summary>
    public class TrackDescription
    {
        private static int _mmiOMrsp;
        private static short _mmiVMrsp;
        private static int _mmiOGradient;
        private static short _mmiGGradient;

        /// <summary>
        /// This is the position in odometer co-ordinates of the start location of a speed discontinuity
        /// in the most restrictive speed profile. This position can be adjusted depending on supervision.
        /// 
        /// Values:
        /// 0..2147483647 in cm
        /// -1 = "Spare"
        /// 
        /// Note: The odometer related variables will only contain bit 0-31 of the source variable.
        ///         I.e. the variable will wrap from 2147483647 -> 0. The receiver should be able to handle this.
        /// </summary>
        public int MMI_O_MRSP
        {
            set { _mmiOMrsp = value; }
            get { return _mmiOMrsp; }
        }

        /// <summary>
        /// The speed value at a discontinuity of the most restrictive speed profile
        /// 
        /// Values:
        /// 1..11111 cm/s
        /// 0 = "Display as speed v=0 and use discontinuity symbol PL23 acc. to [ERA]"
        /// -1 = "Reserved"
        /// -2 = "Reserved"
        /// -3 = "Display as speed v=0 without any discontinuity symbol" (DEFAULT)
        /// </summary>
        public short MMI_V_MRSP
        {
            set { _mmiVMrsp = value; }
            get { return _mmiVMrsp; }
        }

        /// <summary>
        /// The speed value at a discontinuity of the most restrictive speed profile
        /// 
        /// Values:
        /// 1..400 km/h
        /// 0 = "Display as speed v=0 and use discontinuity symbol PL23 acc. to [ERA]"
        /// -1 = "Reserved"
        /// -2 = "Reserved"
        /// -3 = "Display as speed v=0 without any discontinuity symbol" (DEFAULT)
        /// </summary>
        public short MMI_V_MRSP_KMH
        {
            set
            {
                if (value > 400)
                {
                    throw new ArgumentOutOfRangeException();
                }
                    
                else if (value <= 0)
                    _mmiVMrsp = value;

                else
                    _mmiVMrsp = (short) (value / Variables.CmSToKmH);
            }

            get
            {
                if (_mmiVMrsp <= 0)
                    return _mmiVMrsp;

                else
                    return (short) (_mmiVMrsp * Variables.CmSToKmH);
            }
        }

        /// <summary>
        /// The speed value at a discontinuity of the most restrictive speed profile
        /// 
        /// Values:
        /// 1..248 mph
        /// 0 = "Display as speed v=0 and use discontinuity symbol PL23 acc. to [ERA]"
        /// -1 = "Reserved"
        /// -2 = "Reserved"
        /// -3 = "Display as speed v=0 without any discontinuity symbol" (DEFAULT)
        /// </summary>
        public short MMI_V_MRSP_MPH
        {
            set
            {
                if (value > 248)
                    throw new ArgumentOutOfRangeException();

                else if (value <= 0)
                    _mmiVMrsp = value;

                else
                    _mmiVMrsp = (short) (value / Variables.CmSToMph);
            }

            get
            {
                if (_mmiVMrsp <= 0)
                    return _mmiVMrsp;

                else
                    return (short) (_mmiVMrsp * Variables.CmSToMph);
            }
        }

        /// <summary>
        /// This is the position, in odometer co-ordinates, without tolerance correction,
        /// of the start location of a gradient value for a part of the track.
        /// The remaining distances shall be computed taking into account the estimated train front end position.
        /// 
        /// Values:
        /// 0..2147483647 in cm
        /// -1 = "Spare"
        /// 
        /// Note: The odometer related variables will only contain bit 0-31 of the source variable.
        ///         I.e. the variable will wrap from 2147483647 -> 0. The receiver should be able to handle this.
        /// </summary>
        public int MMI_O_GRADIENT
        {
            set { _mmiOGradient = value; }
            get { return _mmiOGradient; }
        }

        /// <summary>
        /// Gives the gradient value of a part of the track. Positive (up) and negative (down)
        /// 
        /// Values:
        /// -254..254 ‰ (Parts per 1000)
        /// -255 = "No current gradient profile (DEFAULT)
        /// </summary>
        public short MMI_G_GRADIENT
        {
            set { _mmiGGradient = value; }
            get { return _mmiGGradient; }
        }
    }
}