#region usings

using System;
using System.Collections.Generic;
using System.Xml.Schema;
using CL345;

#endregion

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This packet is sent to the MMI if a STM requests a national specific test procedure.
    /// The STM issues such a request via packet STM-19 according to [FFFIS_058].
    /// ETCS Onboard transfers the information of STM-19 to packet EVC-27. Packet STM-19 is not seen by the MMI.
    /// 
    /// List of text characters in text message
    /// </summary>
    public static class EVC27_MMISpecificSTMTestRequest
    {
        private static SignalPool _pool;

        // List of text characters in text message
        private static List<char> _stmXTextList;

        /// <summary>
        /// The attributes associated with the text message.
        /// Use the MmiEvcMAttribute..... enums to set the individual parts of the attribute.
        /// 
        /// Values:
        /// 0xxxxxxxxx = Reserved
        /// 10xxxxxxxx = Normal flashing
        /// 11xxxxxxxx = Counter phase flashing
        /// 1x00xxxxxx = No flashing
        /// 1x01xxxxxx = Slow flashing
        /// 1x10xxxxxx = Fast flashing
        /// 1x11xxxxxx = Reserved
        /// 1xxx000xxx = Black text background
        /// 1xxx001xxx = White text background
        /// 1xxx010xxx = Red text background
        /// 1xxx011xxx = Blue text background
        /// 1xxx100xxx = Green text background
        /// 1xxx101xxx = Yellow text background
        /// 1xxx110xxx = Light red text background
        /// 1xxx111xxx = Light green text background
        /// 1xxxxxx000 = Black text
        /// 1xxxxxx001 = White text
        /// 1xxxxxx010 = Red text
        /// 1xxxxxx011 = Blue text
        /// 1xxxxxx100 = Green text
        /// 1xxxxxx101 = Yellow text
        /// 1xxxxxx110 = Light red text
        /// 1xxxxxx111 = Light green text
        /// </summary>
        public static ushort MMI_EVC_M_ATTRIBUTE
        {
            set { _pool.SITR.ETCS1.SpecificStmTestRequest.MmiEvcMXattribute.Value = value; }
        }

        /// <summary>
        /// NTC Identity.
        /// This variable identifies the non-ETCS track equipment on a given section of line
        /// for which the train requires NTC support (via e.g. STM or standalone system).
        /// (The definition of this variable is done by ERA ref [ETCS_VARIABLES])
        /// 
        /// Note: Refer to [ETCS_VARIABLES].
        /// Values not yet assigned to a dedicated NTC shall be handled as Not Defined.
        /// In case of an insertion of text instead of values of MMI_NID_NTC (e.g.text messages)
        /// undefined values shall lead to textstring ‘<Unknown>’.
        /// 
        /// Note 1: Value 255 is used in packets EVC-25 and EVC-26 to indicate termination.
        /// </summary>
        public static byte MMI_NID_NTC { get; set; }

        /// <summary>
        /// Initialise EVC-27 MMI Specific STM Test Request telegram.
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            _stmXTextList = new List<char>();

            // Activate dynamic array
            _pool.SITR.SMDCtrl.ETCS1.SpecificStmTestRequest.Value = 0x0008;

            // Set default values
            _pool.SITR.ETCS1.SpecificStmTestRequest.MmiMPacket.Value = 27;
        }

        /// <summary>
        /// Send EVC-27 MMI Specific STM Test Request telegram.
        /// </summary>
        public static void Send()
        {
            int messageSize = MMI_M_PACKET_SZ + MMI_L_PACKET_SZ + MMI_EVC_M_XATTRIBUTE_SZ +
                              MMI_NID_NTC_SZ + SPARE_SZ + MMI_STM_L_TEXT_IPT_SZ +
                              _stmXTextList.Count * MMI_STM_X_TEXT_SZ;

            // Cannot exceed maximum number of messages due to restriction in add method
            _pool.SITR.ETCS1.SpecificStmTestRequest.MmiStmLTextIpt.Value = (ushort) _stmXTextList.Count;

            // Write out the MMI_NID_NTC value
            _pool.SITR.ETCS1.SpecificStmTestRequest.MmiNidNtc.Value = MMI_NID_NTC;

            // Build dynamic array
            const string packetName = "ETCS1_SpecificStmTestRequest_";

            // For all added characters
            for (int i = 0; i < _stmXTextList.Count; i++)
            {
                string tagName = packetName + $"EVC27SpecificStmTestRequestSub0{i}_";

                string requestName = tagName + "MmiStmXText";

                _pool.SITR.Client.Write(requestName, _stmXTextList[i]);
            }

            // Set packet length
            _pool.SITR.ETCS1.SpecificStmTestRequest.MmiLPacket.Value = (ushort) messageSize;

            // Send dynamic packet
            _pool.SITR.SMDCtrl.ETCS1.SpecificStmTestRequest.Value = 0x0009;
        }

        /// <summary>
        /// Add a text character to the variable array list
        /// </summary>
        public static void AddTextCharToList(char val)
        {
            if (_stmXTextList.Count == MAX_CHARS)
            {
                throw new ArgumentOutOfRangeException("Maximum number of allowed characters (40) exceeded!");
            }
            else
            {
                _stmXTextList.Add(val);
            }
        }

        /// <summary>
        /// Clear the character list
        /// </summary>
        public static void ClearTextCharList()
        {
            _stmXTextList.Clear();
        }

        /// <summary>
        /// Constant size definitions for EVC-27
        /// /// </summary>
        private const UInt16 MMI_M_PACKET_SZ = 16;

        private const UInt16 MMI_L_PACKET_SZ = 16;
        private const UInt16 MMI_EVC_M_XATTRIBUTE_SZ = 16;
        private const UInt16 MMI_NID_NTC_SZ = 8;
        private const UInt16 SPARE_SZ = 8;
        private const UInt16 MMI_STM_L_TEXT_IPT_SZ = 16;
        private const UInt16 MMI_STM_X_TEXT_SZ = 8;
        private const UInt16 MAX_CHARS = 40;
    }

    /// <summary>
    /// Enum used to set the flash type of STM text/indicator, as set in MMI_EVC_M_ATTRIBUTE.
    /// </summary>
    public enum MmiEvcMAttribute_FlashType : ushort
    {
        No_flash = 0x0000,
        Normal_flashing = 0x0200,
        Counter_phase_flashing = 0x0300
    }

    /// <summary>
    /// Enum used to set the flash speed of STM text/indicator, as set in MMI_EVC_M_ATTRIBUTE.
    /// </summary>
    public enum MmiEvcMAttribute_FlashSpeed : ushort
    {
        None = 0x0000,
        Slow_flashing = 0x0240,
        Fast_flashing = 0x0280
    }

    /// <summary>
    /// Enum used to set the background text colour of STM text/indicator, as set in MMI_EVC_M_ATTRIBUTE.
    /// </summary>
    public enum MmiEvcMAttribute_BackgroundColour : ushort
    {
        None = 0x0000,
        Black_text_background = 0x0200,
        White_text_background = 0x0208,
        Red_text_background = 0x0210,
        Blue_text_background = 0x0218,
        Green_text_background = 0x0220,
        Yellow_text_background = 0x0228,
        Light_red_text_background = 0x0230,
        Light_green_text_background = 0x0238
    }

    /// <summary>
    /// Enum used to set the text colour of STM text/indicator, as set in MMI_EVC_M_ATTRIBUTE.
    /// </summary>
    public enum MmiEvcMAttribute_TextColour : ushort
    {
        None = 0x0000,
        Black_text = 0x0200,
        White_text = 0x0201,
        Red_text = 0x0202,
        Blue_text = 0x0203,
        Green_text = 0x0204,
        Yellow_text = 0x0205,
        Light_red_text = 0x0206,
        Light_green_text = 0x0207
    }
}