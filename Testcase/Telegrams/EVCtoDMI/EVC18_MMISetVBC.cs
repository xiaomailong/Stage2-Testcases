#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL345;

#endregion

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This packet is sent sporadically from ETC when the 'Set VBC' procedure
    /// is ongoing and is intended to support the following use cases:
    /// 1. Prompt the driver to enter a VBC code
    /// 2. Display/change echo text after data checks have been performed by EVC;
    ///     this also includes control over the allowed driver actions in case some data check has failed
    /// It also gives the ETC the ability to control the status/type of the "Yes" button,
    ///     if specified by functional requirements for ETC and DMI.
    /// 
    /// Note: Parameter 'MMI_N_VBC' distinguishes between use case 1 and 2
    /// </summary>
    public static class EVC18_MMISetVBC
    {
        private static SignalPool _pool;
        private static uint _nidVbcmk;
        private static uint _tVbc;
        private static string _echoText;

        private static string Basestring = "ETCS1_SetVbc_EVC18SetVbcSub10";

        /// <summary>
        /// Initialise EVC-18 MMI_Set_VBC telegram
        /// (VBC = Virtual Balise Cover)
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // Set default values
            _pool.SITR.ETCS1.SetVbc.MmiMPacket.Value = 18; // Packet ID
            MMI_N_VBC = 0;
        }

        /// <summary>
        /// Send EVC-18 MMI_Set_VBC telegram
        /// </summary>
        public static void Send()
        {
            if (MMI_N_VBC == 0)
            {
                // Set fixed packet size
                _pool.SITR.ETCS1.SetVbc.MmiLPacket.Value = 64;

                // Send non-dynamic packet to display Set VBC screen
                _pool.SITR.SMDCtrl.ETCS1.SetVbc.Value = 1;
            }

            else
            {
                // Set packet size
                ushort totalSizeCounter = 128;

                // Echo Text array
                var echoText = _echoText.ToCharArray();
                int numberOfEchoTextCharacters = echoText.Length;

                // Set number of characters as per Echo Text
                _pool.SITR.Client.Write(Basestring + "_MmiNText", numberOfEchoTextCharacters);

                for (int k = 0; k < numberOfEchoTextCharacters; k++)
                {
                    // Write individual Echo Text characters to signal pool
                    _pool.SITR.Client.Write($"{Basestring}_EVC18SetVbcSub11{k}", echoText[k]);

                    // Increase packet size for each character in Echo Text
                    totalSizeCounter += 8;
                }

                // Set final packet size
                _pool.SITR.ETCS1.SetVbc.MmiLPacket.Value = totalSizeCounter;

                // Send dynamic packet.
                _pool.SITR.SMDCtrl.ETCS1.SetVbc.Value = 0x09;
            }
            
        }

        /// <summary>
        /// VBC Identifier Code
        /// 
        /// Values:
        /// Bits:
        /// 0..9 = "NID_C"
        /// 10..15 = "NID_VBCMK"
        /// 16..23 = "T_VBC"
        /// 24..31 = "spare"
        /// </summary>
        public static void SetVBCCode()
        {
            var vbcCoverCode = _tVbc << 15 | _nidVbcmk << 9 | Variables.NidC;

            _pool.SITR.Client.Write(Basestring + "_MmiVbcCode", vbcCoverCode);
        }

        /// <summary>
        /// Number of VBC Identifiers
        /// 
        /// Values:
        /// 0 = Prompt driver to enter VBC code
        /// 1 = Display/change echo text after data checks have been performed by EVC
        /// </summary>
        public static ushort MMI_N_VBC
        {
            get => _pool.SITR.ETCS1.SetVbc.MmiNVbc.Value;

            set => _pool.SITR.ETCS1.SetVbc.MmiNVbc.Value = value;
        }

        /// <summary>
        /// Set the VBC marker number
        /// 
        /// Valid values:
        /// 0..63
        /// </summary>
        public static byte NID_VBCMK
        {
            set
            {
                if (value > 63)
                {
                    throw new ArgumentOutOfRangeException("NID_VBCMK", "Virtual Balise Cover marker must be less than 64.");
                }

                else
                {
                    _nidVbcmk = value;
                    SetVBCCode();
                }            
            }
        }

        /// <summary>
        /// Set the Q_Data_Check parameter of the VBC.
        /// 
        /// Values:
        /// 0 = "All checks have passed"
        /// 1 = "Technical Range Check failed"
        /// 2 = "Technical Resolution Check failed"
        /// 3 = "Technical Cross Check failed"
        /// 4 = "Operational Range Check failed"
        /// 5 = "Operational Cross Check failed"
        /// </summary>
        public static Variables.Q_DATA_CHECK MMI_Q_DATA_CHECK
        {
            set
            {
                _pool.SITR.Client.Write(Basestring + "_MmiQDataCheck", (byte)value);
            }
        }

        /// <summary>
        /// Set the VBC Echo text
        /// 
        /// Note: Maximum of 10 characters only.
        /// </summary>
        public static string ECHO_TEXT
        {
            get => _echoText;

            set
            {
                if (value.Length > 10)
                {
                    throw new ArgumentException("VBC Echo text must be 10 characters or less.");
                }

                else
                {
                    _echoText = value;
                }         
            }
        }

        /// <summary>
        /// Set the VBC validity time
        /// 
        /// Values:
        /// 0..255 days
        /// </summary>
        public static byte T_VBC
        {
            set
            {
                _tVbc = value;
                SetVBCCode();
            }
        }     

        /// <summary>
        /// Intended to be used to distinguish between:
        ///     'BTN_YES_DATA_ENTRY_COMPLETE',
        ///     'BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE',
        ///     'no button' (here this shall be interpreted as 'Yes button disabled').
        /// The 'BTN_SETTINGS' shall be used as special value to indicate that DMI shall
        /// close the 'Set VBC' window and return to the parent 'settings' window.
        /// Other buttons are not in scope of packet EVC-18
        /// </summary>
        public static EVC18BUTTONS MMI_M_BUTTONS
        {
            set => _pool.SITR.ETCS1.SetVbc.MmiMButtons.Value = (byte)value;
        }

    }

    /// <summary>
    /// MMI_M_Buttons for EVC-18 enum
    /// </summary>
    public enum EVC18BUTTONS : byte
    {
        BTN_SETTINGS = 4,
        BTN_YES_DATA_ENTRY_COMPLETE = 36,
        BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE = 37,
        NoButton = 255
    }
}
