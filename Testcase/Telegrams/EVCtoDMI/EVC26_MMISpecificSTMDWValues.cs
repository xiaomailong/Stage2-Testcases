﻿#region usings

using CL345;
using System;
using System.Collections.Generic;

#endregion

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This packet is used when a STM presents specific data to the driver as the result of a STM specific data
    /// view request. Each MMI packet contains the data of one packet STM-183 according to SUBSET-058, which in turn
    /// holds up to 5 variables. If more than 5 variables need to be presented this MMI packet will be repeated.
    /// NID_PACKET and L_PACKET of packet STM-183 are stripped. Because the content of this packet is given by the
    /// STM-functionality the assignment, ranges, values and meaning of all variables can only be given in the
    /// project-specific documentation.
    /// </summary>
    public static class EVC26_MMISpecificSTMDWValues
    {
        private static SignalPool _pool;
        private static byte _evc26Alias1;
        private const ushort MMI_M_PACKET_bits = 16;
        private const ushort MMI_L_PACKET_bits = 16;
        private const ushort MMI_NID_NTC_bits = 8;
        private const ushort EVC26_alias_1_bits = 8;
        private const ushort MMI_N_ITER_bits = 16;
        private const ushort StmMaxIterations = 5;
        private const ushort StmCaptionMaxLength = 20;
        private const ushort StmXValueMaxLength = 10;

        private const ushort BasicPacketLength = MMI_M_PACKET_bits +
                                                 MMI_L_PACKET_bits +
                                                 MMI_NID_NTC_bits +
                                                 EVC26_alias_1_bits +
                                                 MMI_N_ITER_bits;

        /// <summary>
        /// List of STM elements.
        /// </summary>
        public static List<EVC26_StmData> StmData { get; set; }

        /// <summary>
        /// This structure collects the repeated data for the EVC-26 packet
        /// </summary>
        public class EVC26_StmData
        {
            public byte nidNtc;
            public byte stmNidData;
            public ushort evcMXAttribute;
            public string stmCaption;
            public string stmXValue;
        }

        /// <summary>
        /// Initialise EVC-26 MMI Specific STM DW Values telegram.
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            _evc26Alias1 = 0x00;
            StmData = new List<EVC26_StmData>();

            // Activate dynamic array
            _pool.SITR.SMDCtrl.ETCS1.SpecificStmDwValue.Value = 0x0008;

            // Set default values
            _pool.SITR.ETCS1.SpecificStmDwValue.MmiMPacket.Value = 26;
        }

        /// <summary>
        /// Send EVC-26 MMI Specific STM DW Values telegram.
        /// </summary>
        public static void Send()
        {
            _pool.SITR.ETCS1.SpecificStmDwValue.EVC26alias1.Value = _evc26Alias1;

            ushort numberOfDataUnits = (ushort) StmData.Count;

            if (numberOfDataUnits > StmMaxIterations)
            {
                throw new ArgumentOutOfRangeException("Too many STM Data units!");
            }
            else
            {
                _pool.SITR.ETCS1.SpecificStmDwValue.MmiNIter.Value = numberOfDataUnits;
            }

            ushort totalSizeCounter = BasicPacketLength;

            string packetName = "ETCS1_SpecificStmDwValue_";

            // For all data units
            for (int k = 0; k < numberOfDataUnits; k++)
            {
                string tagName1 = packetName + string.Format("EVC26SpecificStmDwValueSub1{0}_", k);

                // Write out the MMI_NID_NTC value
                string requestName = tagName1 + "MmiNidNtc";

                _pool.SITR.Client.Write(requestName, StmData[k].nidNtc);
                totalSizeCounter += sizeof(byte);

                // Write out the MMI_STM_NID_DATA value (index)
                requestName = tagName1 + "MmiStmNidData";

                _pool.SITR.Client.Write(requestName, StmData[k].stmNidData);
                totalSizeCounter += sizeof(byte);

                // Write out the MMI_EVC_M_XATTRIBUTE value (type)
                requestName = tagName1 + "MmiEvcMXattribute";

                _pool.SITR.Client.Write(requestName, StmData[k].evcMXAttribute);
                totalSizeCounter += sizeof(ushort);

                var caption = StmData[k].stmCaption.ToCharArray();

                // Limit number of caption characters to 20
                if (caption.Length > StmCaptionMaxLength)
                {
                    throw new ArgumentOutOfRangeException("Too many characters in caption string!");
                }
                else
                {
                    // Write out the MMI_L_CAPTION value (length)
                    requestName = tagName1 + "MmiLCaption";

                    _pool.SITR.Client.Write(requestName, (ushort) caption.Length);
                    totalSizeCounter += sizeof(ushort);
                }

                // Write individual caption characters
                // Dynamic fields 2nd dimension
                string tagName2 = string.Format("{0}EVC26SpecificStmDwValueSub11", tagName1);

                for (int l = 0; l < caption.Length; l++)
                {
                    requestName = string.Format("{0}{1}_MmiStmXCaption", tagName2, l.ToString("00"));

                    _pool.SITR.Client.Write(requestName, caption[l]);
                    totalSizeCounter += sizeof(byte);
                }

                var xValue = StmData[k].stmXValue.ToCharArray();

                if (xValue.Length > StmXValueMaxLength)
                {
                    throw new ArgumentOutOfRangeException("Too many characters in x value string!");
                }
                else
                {
                    // Write out the MMI_STM_L_VALUE (length of xvalue string)
                    requestName = string.Format("{0}MmiStmLValue", tagName1);

                    _pool.SITR.Client.Write(requestName, xValue.Length);
                    totalSizeCounter += sizeof(ushort);
                }

                tagName2 = string.Format("{0}EVC26SpecificStmDwValueSub120", tagName1);

                // Write out the XValue characters
                for (int l = 0; l < xValue.Length; l++)
                {
                    requestName = string.Format("{0}{1}_MmiStmXValue", tagName2, l);

                    _pool.SITR.Client.Write(requestName, xValue[l]);
                    totalSizeCounter += sizeof(byte);
                }
            }

            // Set packet length
            _pool.SITR.ETCS1.SpecificStmDwValue.MmiLPacket.Value = totalSizeCounter;

            // Send dynamic packet
            _pool.SITR.SMDCtrl.ETCS1.SpecificStmDwValue.Value = 0x0009;
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
        /// undefined values shall lead to text string ‘<Unknown>’.
        /// 
        /// Note 1: Value 255 is used in packets EVC-25 and EVC-26 to indicate termination.
        /// </summary>
        public static byte MMI_NID_NTC
        {
            set { _pool.SITR.ETCS1.SpecificStmDwValue.MmiNidNtc.Value = value; }
        }

        /// <summary>
        /// Indicate a following request.
        /// Due to the possible length of an STM request for Specific STM Data, this qualifier is used to indicate
        /// to the ETCS Onboard whether there is a request for Specific STM Data following that has to be managed
        /// together with the current request by the ETCS Onboard or not.
        /// 
        /// Values:
        /// 0 = "No following request to be managed together with the current one"
        /// 1 = "There is a following request to be managed together with the current one"
        /// </summary>
        public static bool MMI_STM_Q_FOLLOWING
        {
            set
            {
                if (value)
                {
                    _evc26Alias1 |= 0x01;
                }
                else
                {
                    _evc26Alias1 &= 0xfe;
                }
            }
        }
    }
}