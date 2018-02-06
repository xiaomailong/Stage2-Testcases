#region usings

using System;
using System.Collections.Generic;
using CL345;

#endregion

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This packet shall be sent when the driver has requested to open the Data View window.
    /// </summary>
    public static class EVC13_MMIDataView
    {
        private static TestcaseBase _pool;
        const string BaseString = "ETCS1_DataView_EVC13DataViewSub";

        /// <summary>
        /// Initialise EVC-13 MMI Data View telegram.
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        public static void Initialise(TestcaseBase pool)
        {
            _pool = pool;
            MMI_M_VBC_CODE = new List<byte>();

            // Set as dynamic
            _pool.SITR.SMDCtrl.ETCS1.DataView.Value = 0x0008;

            // Set default values
            _pool.SITR.ETCS1.DataView.MmiMPacket.Value = 13; // Packet ID
        }

        /// <summary>
        /// Send EVC-13 MMI Data View telegram.
        /// </summary>
        public static void Send()
        {
            // Check the size of the list
            if (MMI_M_VBC_CODE.Count > 30)
                throw new ArgumentOutOfRangeException("Too many VBC code entered!");

            // Set the value on EVC-13 packet
            _pool.SITR.ETCS1.DataView.MmiNVbc.Value = (ushort) MMI_M_VBC_CODE.Count;

            // For each element of the list
            for (int vbcIndex = 0; vbcIndex < MMI_M_VBC_CODE.Count; vbcIndex++)
            {
                // Get the VBC Identifier
                var _mVbcCode = MMI_M_VBC_CODE[vbcIndex];

                // Set the value on EVC-13 packet according to the VBC Identifier index
                if (vbcIndex < 10)
                {
                    _pool.SITR.Client.Write(string.Format("{0}10{1}_MmiMVbcCode", BaseString, vbcIndex), _mVbcCode);
                }
                else
                {
                    _pool.SITR.Client.Write(string.Format("{0}1{1}_MmiMVbcCode", BaseString, vbcIndex), _mVbcCode);
                }
            }

            // Check the length of the string
            if (Trainset_Caption.Length > 12)
                throw new ArgumentOutOfRangeException("Too many characters in Train Data Set name!");

            // Set the value on EVC-13 packet
            _pool.SITR.ETCS1.DataView.MmiNCaptionTrainset.Value = (ushort) Trainset_Caption.Length;

            // For each character of the string
            for (int charIndex = 0; charIndex < Trainset_Caption.Length; charIndex++)
            {
                // Get the value of that character
                var _xCaptionTrainset = Trainset_Caption[charIndex];

                // Set the value on EVC-13 packet according to the Trainset caption character index
                if (charIndex < 10)
                {
                    _pool.SITR.Client.Write(string.Format("{0}20{1}_MmiXCaptionTrainset", BaseString, charIndex),
                        _xCaptionTrainset);
                }
                else
                {
                    _pool.SITR.Client.Write(string.Format("{0}2{1}_MmiXCaptionTrainset", BaseString, charIndex),
                        _xCaptionTrainset);
                }
            }

            // Check the length of the string
            if (Network_Caption.Length > 16)
                throw new ArgumentOutOfRangeException("Too many characters in Network Id!");

            // Set the value on EVC-13 packet
            _pool.SITR.ETCS1.DataView.MmiNCaptionNetwork.Value = (ushort) Network_Caption.Length;

            // For each digit of the string
            for (int digitIndex = 0; digitIndex < Network_Caption.Length; digitIndex++)
            {
                // Get the value of that digit
                var _xCaptionNetwork = Network_Caption[digitIndex];

                // Set the value on EVC-13 packet according to the Network caption digit index
                if (digitIndex < 10)
                {
                    _pool.SITR.Client.Write(string.Format("{0}30{1}_MmiXCaptionTrainset", BaseString, digitIndex),
                        _xCaptionNetwork);
                }
                else
                {
                    _pool.SITR.Client.Write(string.Format("{0}3{1}_MmiXCaptionTrainset", BaseString, digitIndex),
                        _xCaptionNetwork);
                }
            }

            // Set the total length of the packet
            _pool.SITR.ETCS1.DataView.MmiLPacket.Value =
                (ushort) (424 + MMI_M_VBC_CODE.Count * 32 + Trainset_Caption.Length * 16 + Network_Caption.Length + 8);

            // Send dynamic telegram
            _pool.SITR.SMDCtrl.ETCS1.DataView.Value = 0x000A;
            _pool.WaitForAck(_pool.SITR.SMDStat.ETCS1.DataView);
        }

        /// <summary>
        /// Current driver identity
        /// 
        /// Values:
        /// 0 = "Empty string (null character)"
        /// 
        /// Note: 16 alphanumeric characters (ISO 8859-1, also known as Latin Alphabet #1).
        /// Note 1: If the value is unknown the table will be filled with null characters (0, not '0').
        /// Note 2: If Driver ID is shorter than 16 characters the free charcters will be filled with null characters.
        /// Note 3: If Driver ID is 16 characters there will be no null character in the string.
        /// </summary>
        public static string MMI_X_DRIVER_ID
        {
            get { return _pool.SITR.ETCS1.DataView.MmiXDriverId.Value; }

            set
            {
                if (value.Length > 16)
                    throw new ArgumentOutOfRangeException("Too many characters in Driver ID!");

                _pool.SITR.ETCS1.DataView.MmiXDriverId.Value = value;
            }
        }

        /// <summary>
        /// Note: Definition according to Subset-026, 7.5.1.92.
        /// 
        /// Binary Coded Decimal
        /// 
        /// For each digit:
        /// Values 0-9 Digit value
        /// Values A-E Not used, spare
        /// Value F No digit (used for shorter numbers or when not applicable) or special value
        /// (see below)
        /// E.g. “1234567” is coded as 0x1234567F
        /// 
        /// Special values:
        /// 0xFFFFFFFF 'Unknown Train Running Number'
        /// </summary>
        public static uint MMI_NID_OPERATION
        {
            get { return _pool.SITR.ETCS1.DataView.MmiNidOperation.Value; }
            set { _pool.SITR.ETCS1.DataView.MmiNidOperation.Value = value; }
        }

        /// <summary>
        /// A bit mask that, for each variable, tells if a data value is enabled (e.g. for 'edit' in EVC-6).
        /// 1 == 'enabled'.
        /// The variable supports the following use cases:
        /// 1.) Controls edit ability of related data object during TDE procedure (EVC-6, no data view).
        /// 2.) In case of a Train Data View procedure this variable controls visibility of data items (ERA_ERTMS_015560, v3.4.0, chapter 11.5.1.5).
        /// 3.) In packet EVC-10 this variable controls highlighting of changed data items (ERA_ERTMS_015560, v3.4.0, chapter 11.4.1.4, 10.3.3.5).
        /// </summary>
        public static Variables.MMI_M_DATA_ENABLE MMI_M_DATA_ENABLE
        {
            get { return (Variables.MMI_M_DATA_ENABLE) _pool.SITR.ETCS1.DataView.MmiMDataEnable.Value; }
            set { _pool.SITR.ETCS1.DataView.MmiMDataEnable.Value = (ushort) value; }
        }

        /// <summary>
        /// Max train length
        /// 
        /// Values:
        /// 0 = "'No default value' =&gt; Data field shall remain empty"
        /// 1..4095 = "total train length"
        /// 4096..65535 = "Reserved"
        /// </summary>
        public static ushort MMI_L_TRAIN
        {
            get { return _pool.SITR.ETCS1.DataView.MmiLTrain.Value; }
            set { _pool.SITR.ETCS1.DataView.MmiLTrain.Value = value; }
        }

        /// <summary>
        /// Max train speed
        /// 
        /// Values:
        /// 0 = "'No default value' =&gt; TD entry field shall remain empty."
        /// 1..600 = "max train speed"
        /// 601..65535 = "Reserved"
        /// </summary>
        public static ushort MMI_V_MAXTRAIN
        {
            get { return _pool.SITR.ETCS1.DataView.MmiVMaxtrain.Value; }
            set { _pool.SITR.ETCS1.DataView.MmiVMaxtrain.Value = value; }
        }

        /// <summary>
        /// Brake percentage as input for calculation of braking characteristics.
        /// 
        /// Values:
        /// 0 = "No default value' =&gt; Data field shall remain empty"
        /// 1..9 = "Reserved"
        /// 10..250 = "Brake Percentage given in '%'"
        /// 251..255 = "Reserved"
        /// </summary>
        public static ushort MMI_M_BRAKE_PERC
        {
            get { return _pool.SITR.ETCS1.DataView.MmiMBrakePerc.Value; }
            set { _pool.SITR.ETCS1.DataView.MmiMBrakePerc.Value = (byte) value; }
        }

        /// <summary>
        /// Identifies the axle load category related subset of MMI_NID_KEY.
        /// 
        /// Value range 21-33
        ///     Values:
        ///     21 = "A"
        ///     22 = "HS17"
        ///     23 = "B1"
        ///     24 = "B2"
        ///     25 = "C2"
        ///     26 = "C3"
        ///     27 = "C4"
        ///     28 = "D2"
        ///     29 = "D3"
        ///     30 = "D4"
        ///     31 = "D4XL"
        ///     32 = "E4"
        ///     33 = "E5"
        /// </summary>
        public static Variables.MMI_NID_KEY MMI_NID_KEY_AXLE_LOAD
        {
            get { return (Variables.MMI_NID_KEY) _pool.SITR.ETCS1.DataView.MmiNidKeyAxleLoad.Value; }
            set { _pool.SITR.ETCS1.DataView.MmiNidKeyAxleLoad.Value = (byte) value; }
        }

        /// <summary>
        /// Radio subscriber number (phone number), 32 high bits of 64
        /// 
        /// Note:
        /// For each digit:
        /// Value 0..9	Digit value
        /// Value A..E  Not Used
        /// Value F     Use value F for indication of ‘no digit’ (if number shorter than 16 digits)
        /// 
        /// Special values:
        /// 0xFFFFFFFF	'Unknown Subscriber Number'
        /// </summary>
        public static ulong MMI_NID_RADIO
        {
            set
            {
                var bytes = BitConverter.GetBytes(value);
                _pool.SITR.ETCS1.DataView.MmiNidRadio.Value = new[]
                    {BitConverter.ToUInt32(bytes, 2), BitConverter.ToUInt32(bytes, 0)};
            }
        }

        /// <summary>
        /// RBC-ID
        /// This variable provides the ETCS ID of an RBC. Each RBC belongs to a certain NID_C.
        /// The contents of the variable is the result of a concatenation of NID_C (10 bits) + NID_RBC (14 bits).
        /// This variable must not be mixed up with NID_RBC as defined in [SRS_026] part 7 chapter ‘NID_RBC’.
        ///
        /// Values:
        /// 0..16777214 = ""
        /// Note:
        /// Bit 0..9 contain 'NID_C'
        /// Bits 10..23 contain 'NID_RBC'
        /// Bits 24..31 = 'spare'
        /// Special Value NID_RBC = 16383 - 'contact last known RBC'
        /// 
        /// 24 bit (10 bit unsigned int for NID_C + 14 bit unsigned int for NID_RBC)
        /// </summary>
        public static uint MMI_NID_RBC
        {
            get { return _pool.SITR.ETCS1.DataView.MmiNidRbc.Value; }
            set { _pool.SITR.ETCS1.DataView.MmiNidRbc.Value = value; }
        }

        /// <summary>
        /// Train equipped with airtight system.
        /// 
        /// Values:
        /// 0 = "Not equipped"
        /// 1 = "Equipped"
        /// 2 = "'No default value' =&gt; TD entry field shall remain empty"
        /// 3..255 = "Spare"
        /// </summary>
        public static ushort MMI_M_AIRTIGHT
        {
            get { return _pool.SITR.ETCS1.DataView.MmiMAirtight.Value; }
            set { _pool.SITR.ETCS1.DataView.MmiMAirtight.Value = (byte) value; }
        }

        /// <summary>
        /// Identifies the loading gauge category related subset of MMI_NID_KEY.
        /// 
        /// Value range 34-38
        ///     Values:
        ///     34 = "G1"
        ///     35 = "GA"
        ///     36 = "GB"
        ///     37 = "GC"
        ///     38 = "Out of GC"
        /// </summary>
        public static Variables.MMI_NID_KEY MMI_NID_KEY_LOAD_GAUGE
        {
            get { return (Variables.MMI_NID_KEY) _pool.SITR.ETCS1.DataView.MmiNidKeyLoadGauge.Value; }
            set { _pool.SITR.ETCS1.DataView.MmiNidKeyLoadGauge.Value = (byte) value; }
        }

        /// <summary>
        /// List of VBC Identifier
        /// </summary>
        public static List<byte> MMI_M_VBC_CODE { get; set; }

        /// <summary>
        /// Train data set caption string name
        /// </summary>
        public static string Trainset_Caption { get; set; }

        /// <summary>
        /// Name of the preconfigured Network ID
        /// </summary>
        public static string Network_Caption { get; set; }

        /// <summary>
        /// Identifies the train category related subset of MMI_NID_KEY.
        /// 
        /// Value range 3-20
        ///     Values:
        ///     3 = "PASS 1"
        ///     4 = "PASS 2"
        ///     5 = "PASS 3"
        ///     6 = "TILT 1"
        ///     7 = "TILT 2"
        ///     8 = "TILT 3"
        ///     9 = "TILT 4"
        ///     10 = "TILT 5"
        ///     11 = "TILT 6"
        ///     12 = "TILT 7"
        ///     13 = "FP 1"
        ///     14 = "FP 2"
        ///     15 = "FP 3"
        ///     16 = "FP 4"
        ///     17 = "FG 1"
        ///     18 = "FG 2"
        ///     19 = "FG 3"
        ///     20 = "FG 4"
        /// </summary>
        public static Variables.MMI_NID_KEY MMI_NID_KEY_TRAIN_CAT
        {
            get { return (Variables.MMI_NID_KEY) _pool.SITR.ETCS1.DataView.MmiNidKeyTrainCat.Value; }
            set { _pool.SITR.ETCS1.DataView.MmiNidKeyTrainCat.Value = (byte) value; }
        }
    }
}