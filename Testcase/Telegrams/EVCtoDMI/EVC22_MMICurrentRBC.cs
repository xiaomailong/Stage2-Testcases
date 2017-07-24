using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL345;
using static Testcase.Telegrams.EVCtoDMI.Variables;

namespace Testcase.Telegrams.EVCtoDMI
{
    static class EVC22_MMICurrentRBC
    {
        private static SignalPool _pool;
        private static uint _nidC;
        private static uint _nidRbc;

        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            NetworkCaptions = new List<string>();
            DataElements = new List<DataElement>();

            // activate dynamic array
            _pool.SITR.SMDCtrl.ETCS1.CurrentRbcData.Value = 0x8;

            // set default values
            _pool.SITR.ETCS1.CurrentRbcData.MmiMPacket.Value = 22; // Packet Id
        }

        public static void Send()
        {
            ushort numberOfNetworks = (ushort) NetworkCaptions.Count;
            if (numberOfNetworks > 10)
                throw new ArgumentOutOfRangeException();
            if (DataElements.Count > 9)
                throw new ArgumentOutOfRangeException();

            _pool.SITR.ETCS1.CurrentRbcData.MmiNNetworks.Value = numberOfNetworks; // Number of networks
            
            ushort totalsizecounter = 176;

            //For all networks
            for (int k = 0; k < numberOfNetworks; k++)
            {
                var caption = NetworkCaptions[k].ToCharArray();
                ushort numberNetworkCaptionChars = (ushort) caption.Length;
                var varnamestring = $"ETCS1_CurrentTrainData_EVC22CurrentRbcDataSub1{k}_";
               
                // Limit number of caption characters to 16
                if(caption.Length > 16)
                    throw new ArgumentOutOfRangeException();
                
                // Write individual network chars
                _pool.SITR.Client.Write(
                    $"{varnamestring}MmiNCaptionNetwork",
                    numberNetworkCaptionChars);
                totalsizecounter += 16;

                // Dynamic fields 2nd dimension
                for (int l = 0; l < numberNetworkCaptionChars; l++)
                {
                    // Network caption text character
                    if (l < 10)
                    {
                        _pool.SITR.Client.Write(
                            $"{varnamestring}EVC22CurrentRbcDataSub110{l}_MmiXCaptionNetwork",
                            caption[l]);
                    }
                    else
                    {
                        _pool.SITR.Client.Write(
                            $"{varnamestring}EVC22CurrentRbcDataSub11{l}_MmiXCaptionNetwork",
                            caption[l]);
                    }
                    totalsizecounter += 8;
                }
            }

            _pool.SITR.ETCS1.CurrentRbcData.MmiNDataElements.Value = (ushort)DataElements.Count; // Number of data elements to enter

            totalsizecounter = PopulateDataElements("ETCS1_CurrentRbcData_EVC22CurrentRbcDataSub2", totalsizecounter, _pool);

            // Packet length
            _pool.SITR.ETCS1.CurrentRbcData.MmiLPacket.Value = totalsizecounter;

            // send
            _pool.SITR.SMDCtrl.ETCS1.CurrentRbcData.Value = 0x9;
        }

        /// <summary>
        /// The NID_C part of MMI_NID_RBC
        /// </summary>
        public static uint NID_C
        {
            get => _nidC;
            set
            {
                _nidC = value; 
                SetMMINidRBC();
            }
        }

        /// <summary>
        /// RBC id
        /// </summary>
        public static uint NID_RBC
        {
            get => _nidRbc;
            set
            {
                _nidRbc = value; 
                SetMMINidRBC();
            }
        }

        private static void SetMMINidRBC()
        {
            MMI_NID_RBC = _nidC << 14 | _nidRbc << 8;
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
        /// 24 bit (10 bit unsigned int for NID_C +
        /// 14 bit unsigned int for NID_RBC)
        ///
        /// </summary>
        private static uint MMI_NID_RBC { set => _pool.SITR.ETCS1.CurrentRbcData.MmiNidRbc.Value = value; }

        /// <summary>
        /// RBC phone number
        /// </summary>
        public static ulong MMI_NID_RADIO {
            set
            {
                var bytes = BitConverter.GetBytes(value);
                _pool.SITR.ETCS1.CurrentRbcData.MmiNidRadio.Value = new[] {BitConverter.ToUInt32(bytes, 2), BitConverter.ToUInt32(bytes, 0)};
            } }

        /// <summary>
        /// Identifier of currently active ETCS windows. Not all
        /// possible windows are controlled/used by this
        /// variable in the current packet. The relevant
        /// windows are specified by related requirements.
        /// 
        /// Values:
        /// 0 = "Default"
        /// 1 = "Main"
        /// 2 = "Override"
        /// 3 = "Special"
        /// 4 = "Settings"
        /// 5 = "RBC contact"
        /// 6 = "Train running number"
        /// 7 = "Level"
        /// 8 = "Driver ID"
        /// 9 = "radio network ID"
        /// 10 = "RBC data"
        /// 11 = "Train data"
        /// 12 = "SR speed/distance"
        /// 13 = "Adhesion"
        /// 14 = "Set VBC"
        /// 15 = "Remove VBC"
        /// 16 = "Train data validation"
        /// 17 = "Set VBC validation"
        /// 18 = "Remove VBC validation"
        /// 19 = "Data View"
        /// 20 = "System version"
        /// 21 = "NTC data entry selection"
        /// 22 = "NTC X data"
        /// 23 = "NTC X data validation"
        /// 24 = "NTC X data view"
        /// 25..252 = "Spare"
        /// 253 = "Language"
        /// 254 = "close current window, return to parent"
        /// 255 = "no window specified"
        /// Note1: The definition is according to preliminary SubSet-121 'NID_WINDOW' definition + some
        /// extension for BT specific design.
        /// Note2: The variable is already prepared for future use and contains all known possible windows; it
        /// is assumed that there are use cases where the variable is contained in a sent package, but there is
        /// no intention to do a statement about a specific window. Then the special value 'no window
        /// specified' can be used.
        /// </summary>
        public static ushort MMI_NID_WINDOW
        {
            set => _pool.SITR.ETCS1.CurrentRbcData.MmiNidWindow.Value = (byte)value;
        }

        /// <summary>
        /// Enabling close button in EVC-14, EVC-20 and EVC-22.
        /// True = enabled
        /// </summary>
        public static bool MMI_Q_CLOSE_ENABLE
        {
            set => _pool.SITR.ETCS1.CurrentDriverId.MmiQCloseEnable.Value = (byte)(value ? 0x80 : 0x00);
        }

        /// <summary>
        /// Intended to be used to distinguish between 
        /// 'BTN_YES_DATA_ENTRY_COMPLETE', 
        /// 'BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE' and
        /// 'no button' (here this shall be interpreted as 'Yes button disabled'). 
        /// Other buttons are not in scope of packet EVC-22.
        ///
        /// </summary>
        public static EVC22BUTTONS MMI_M_BUTTONS {
            set => _pool.SITR.ETCS1.CurrentRbcData.MmiMButtons.Value = (byte) value;
        }

        public enum EVC22BUTTONS : ushort
        {
            BTN_YES_DATA_ENTRY_COMPLETE = 36,
            BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE = 37,
            NoButton = 255
        }

        public static List<string> NetworkCaptions { get; set; }

        public static List<DataElement> DataElements { get; set; }
    }
}
