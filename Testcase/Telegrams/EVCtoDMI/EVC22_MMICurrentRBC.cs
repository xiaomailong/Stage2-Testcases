﻿#region usings

using System;
using System.Collections.Generic;
using CL345;

#endregion

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This packet is sent sporadically by ETC and is intended to support the following use cases:
    /// 1.) Display 'RBC contact', 'RBC data' or 'Radio Network ID' when entering RBC data. Send 'RBC' data in "data view" procedure.
    /// 2.) Display/change echo text after data checks have been performed by ETC;
    ///     this also includes control over the allowed driver actions in case some data check has failed.
    /// It also gives the ETC the ability to control the status/type of the "Yes" button, if specified by functional requirements for ETC and DMI.
    /// </summary>
    public static class EVC22_MMICurrentRBC
    {
        private static SignalPool _pool;
        private static uint _nidC = Variables.NidC;
        private static uint _nidRbc;
        private const string BaseString = "ETCS1_CurrentRbcData_EVC22CurrentRbcDataSub";

        /// <summary>
        /// Initialise EVC-22 MMI Current RBC Data telegram
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            NetworkCaptions = new List<string>();
            DataElements = new List<Variables.DataElement>();

            // Activate dynamic array
            _pool.SITR.SMDCtrl.ETCS1.CurrentRbcData.Value = 0x0008;

            // Set default values
            _pool.SITR.ETCS1.CurrentRbcData.MmiMPacket.Value = 22;
        }

        /// <summary>
        /// Send EVC-22 MMI Current RBC Data telegram.
        /// </summary>
        public static void Send()
        {
            ushort numberOfNetworks = (ushort) NetworkCaptions.Count;

            if (numberOfNetworks > 10)
                throw new ArgumentOutOfRangeException("Too many RBC networks!");
            if (DataElements.Count > 9)
                throw new ArgumentOutOfRangeException("Too many Data elements!");

            // Number of networks
            _pool.SITR.ETCS1.CurrentRbcData.MmiNNetworks.Value = numberOfNetworks;

            ushort totalSizeCounter = 176;

            // For all networks
            for (int k = 0; k < numberOfNetworks; k++)
            {
                var caption = NetworkCaptions[k].ToCharArray();

                ushort numberNetworkCaptionChars = (ushort) caption.Length;

                string varnamestring = $"{BaseString}1{k}_";

                // Limit number of caption characters to 16
                if (caption.Length > 16)
                    throw new ArgumentOutOfRangeException("Too many characters in caption string!");

                // Write individual network characters
                _pool.SITR.Client.Write($"{varnamestring}MmiNCaptionNetwork", numberNetworkCaptionChars);

                // Increment packet size
                totalSizeCounter += 16;

                // Dynamic fields 2nd dimension
                for (int l = 0; l < numberNetworkCaptionChars; l++)
                {
                    // Network caption text character
                    _pool.SITR.Client.Write(
                        $"{varnamestring}EVC22CurrentRbcDataSub11{l.ToString("00")}_MmiXCaptionNetwork", caption[l]);

                    // Increment packet size
                    totalSizeCounter += 8;
                }
            }

            // Number of data elements to enter
            _pool.SITR.ETCS1.CurrentRbcData.MmiNDataElements.Value = (ushort) DataElements.Count;

            totalSizeCounter = Variables.PopulateDataElements($"{BaseString}2", totalSizeCounter, DataElements, _pool);

            // Set packet length
            _pool.SITR.ETCS1.CurrentRbcData.MmiLPacket.Value = totalSizeCounter;

            // Send dynamic packet
            _pool.SITR.SMDCtrl.ETCS1.CurrentRbcData.Value = 0x0009;
        }

        /// <summary>
        /// The NID_C part of MMI_NID_RBC
        /// </summary>
        public static uint NID_C
        {
            get { return _nidC; }

            set
            {
                _nidC = value;
                SetMMINidRBC();
            }
        }

        /// <summary>
        /// RBC ID
        /// </summary>
        public static uint NID_RBC
        {
            get { return _nidRbc; }

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
        private static uint MMI_NID_RBC
        {
            set { _pool.SITR.ETCS1.CurrentRbcData.MmiNidRbc.Value = value; }
        }

        /// <summary>
        /// RBC phone number
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
                _pool.SITR.ETCS1.CurrentRbcData.MmiNidRadio.Value = new[]
                    {BitConverter.ToUInt32(bytes, 2), BitConverter.ToUInt32(bytes, 0)};
            }
        }

        /// <summary>
        /// Identifier of currently active ETCS window. Not all possible windows are controlled/used by this variable in the current packet.
        /// The relevant windows are specified by related requirements.
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
        /// 
        /// Note1: The definition is according to preliminary SubSet-121 'NID_WINDOW' definition + some extension for BT specific design.
        /// 
        /// Note2: The variable is already prepared for future use and contains all known possible windows; it is assumed that there are use
        /// cases where the variable is contained in a sent package, but there is no intention to do a statement about a specific window.
        /// Then the special value 'no window specified' can be used.
        /// </summary>
        public static ushort MMI_NID_WINDOW
        {
            set { _pool.SITR.ETCS1.CurrentRbcData.MmiNidWindow.Value = (byte) value; }
        }

        /// <summary>
        /// Enabling close button in EVC-14, EVC-20 and EVC-22.
        /// 
        /// Bits:
        /// 0 = "disable/enable close button"
        /// 1..7 = "Spare"
        /// 
        /// Note: Bit0 = 0 -> disable close button, Bit0 = 1 -> enable close button
        /// </summary>
        public static Variables.MMI_Q_CLOSE_ENABLE MMI_Q_CLOSE_ENABLE
        {
            set { _pool.SITR.ETCS1.CurrentRbcData.MmiQCloseEnable.Value = (byte) value; }
        }

        /// <summary>
        /// Intended to be used to distinguish between 
        /// 'BTN_YES_DATA_ENTRY_COMPLETE', 
        /// 'BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE' and
        /// 'no button' (here this shall be interpreted as 'Yes button disabled'). 
        /// Other buttons are not in scope of packet EVC-22.
        /// </summary>
        public static EVC22BUTTONS MMI_M_BUTTONS
        {
            set { _pool.SITR.ETCS1.CurrentRbcData.MmiMButtons.Value = (byte) value; }
        }

        /// <summary>
        /// EVC-22 Button type to be shown in RBC Data window
        /// </summary>
        public enum EVC22BUTTONS : byte
        {
            BTN_YES_DATA_ENTRY_COMPLETE = 36,
            BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE = 37,
            BTN_ENTER = 254,
            NoButton = 255
        }

        /// <summary>
        /// List of captions for the RBC elements.
        /// String must be limited to 10 chars compared to the allowed 16, otherwise the PopulateDataElements method will throw an exception
        /// </summary>
        public static List<string> NetworkCaptions { get; set; }

        /// <summary>
        /// List of DataElements
        /// </summary>
        public static List<Variables.DataElement> DataElements { get; set; }
    }
}