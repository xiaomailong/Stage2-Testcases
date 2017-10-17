#region usings

using System;
using System.Collections.Generic;
using System.Xml.Schema;
using CL345;

#endregion

namespace Testcase.Telegrams.EVCtoDMI
{

    /// <summary>
    /// Builds an EVC27 STM test request message for testing purposes
    /// </summary>

    public static class EVC27_MMISpecificSTMTestRequest
    {
        /// <summary>
        /// the attributes associated with the text message
        /// </summary>
        public static UInt16 MMI_EVC_M_ATTRIBUTE { get; set; }

        /// <summary>
        /// Identity of the STM that requested the data
        /// </summary>
        public static byte MMI_NID_NTC { get; set; }

        /// <summary>
        /// Initialise EVC-27 MMI Specific STM Test Request telegram
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            _stmXTextList = new List<char>();

            // Activate dynamic array
            _pool.SITR.SMDCtrl.ETCS1.SpecificStmTestRequest.Value = 0x8;

            // Set default values
            _pool.SITR.ETCS1.SpecificStmTestRequest.MmiMPacket.Value = 27; // Packet ID
        }

        /// <summary>
        /// Send EVC-27 MMI Specific STM Test Request telegram
        /// </summary>
        public static void Send()
        {
            int messageSize = MMI_M_PACKET_SZ + MMI_L_PACKET_SZ + MMI_EVC_M_XATTRIBUTE_SZ +
                                 MMI_NID_NTC_SZ + SPARE_SZ + MMI_STM_L_TEXT_IPT_SZ +
                                 (_stmXTextList.Count * MMI_STM_X_TEXT_SZ);

            // cannot exceed max number of messages due to restriction in add method
            _pool.SITR.ETCS1.SpecificStmTestRequest.MmiStmLTextIpt.Value = (ushort)_stmXTextList.Count;

            // setup text attributes variable
            _pool.SITR.ETCS1.SpecificStmTestRequest.MmiEvcMXattribute.Value = MMI_EVC_M_ATTRIBUTE;

            // write out the MMI_NID_NTC value
            _pool.SITR.ETCS1.SpecificStmTestRequest.MmiNidNtc.Value = MMI_NID_NTC;

            // build dynamic array
            string packetName = "ETCS1_SpecificStmTestRequest_";

            // For all added characters
            for (int i = 0; i < _stmXTextList.Count; i++)
            {
                string tagName = packetName + $"EVC27SpecificStmTestRequestSub0{i}_";

                string requestName = tagName + "MmiStmXText";

                _pool.SITR.Client.Write(requestName, _stmXTextList[i]);
            }

            // Set packet length
            _pool.SITR.ETCS1.SpecificStmTestRequest.MmiLPacket.Value = (ushort)messageSize;

            // Send dynamic packet
            _pool.SITR.SMDCtrl.ETCS1.SpecificStmTestRequest.Value = 0x9;
            
        }

        /// <summary>
        /// Add a text character to the variable array list
        /// </summary>
        public static void AddTextCharToList(char val)
        {
            if (_stmXTextList.Count == MAX_CHARS)
            {
                throw new ArgumentOutOfRangeException("Max number of allowed characters (40) exceeeded");
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
        /// List of text characters in text message
        /// /// </summary>
        private static List<char> _stmXTextList;
        /// <summary>
        /// the signal pool
        /// </summary>
        private static SignalPool _pool;

        /// <summary>
        /// constant size definitions for EVC-27
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
}
