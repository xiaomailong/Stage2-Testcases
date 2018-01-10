#region usings

using CL345;
using System;
using System.Collections.Generic;

#endregion

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This packet is used when a STM requests specific data input from the driver. Each MMI packet contains the data of
    /// one packet STM-179 according to [FFFIS_058], which in turn holds up to 5 variables. If more than 5 variables need
    /// to be presented this MMI packet will be repeated. NID_PACKET and L_PACKET of packet STM-179 are stripped.
    /// Because the content of this packet is given by the STM-functionality the assignment, ranges, values and meaning of
    /// all variables can only be given in the project-specific documentation.
    /// </summary>
    public static class EVC25_MMISpecificSTMDERequest
    {
        private static SignalPool _pool;
        private static byte _evc25Alias1;
        private const int StmMaximumIterations = 5;
        private const int StmCaptionMaximumLength = 20;
        private const int StmXValueMaximumLength = 10;
        private const int StmPickupXValueMaximumLength = 10;
        private const int StmXValueMaximumIterations = 16;
        private const int BasicPacketLength = 3 * sizeof(UInt16) + 2 * sizeof(byte);

        private const int MaximumPacketLength = BasicPacketLength +
                                                StmMaximumIterations *
                                                (StmXValueMaximumLength + StmCaptionMaximumLength +
                                                 4 * sizeof(ushort) + 2 * sizeof(byte) +
                                                 StmXValueMaximumIterations *
                                                 (StmPickupXValueMaximumLength + sizeof(ushort)));

        public class EVC25_StmData
        {
            public byte nidNtc;
            public byte stmNidData;
            public ushort evcMXAttribute;
            public string stmCaption;
            public string stmXValue;
            public List<string> stmPickupList;
        }

        /// <summary>
        /// Initialise EVC-25 MMI Specific STM DE Request telegram.
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            _evc25Alias1 = 0x00;
            StmData = new List<EVC25_StmData>();

            // Activate dynamic array
            _pool.SITR.SMDCtrl.ETCS1.SpecificStmDeRequest.Value = 0x0008;

            // Set default values
            _pool.SITR.ETCS1.SpecificStmDeRequest.MmiMPacket.Value = 25;
        }

        private static uint FindPacketSize()
        {
            int sizeCalculated = BasicPacketLength; // Up to MMI_N_ITER

            foreach (var stmElement in StmData)
            {
                // MMI_NTC + MMI_STM_NID_DATA + MMI_EVC_ATTRIBUTE + MMI_L_CAPTION + MMI_L_VALUE 
                sizeCalculated += (2 * sizeof(byte)) + (2 * sizeof(ushort));

                //MMI_STM_X_CAPTION[k] chars MMI_STM_X_VALUE chars
                sizeCalculated += (stmElement.stmCaption.Length + stmElement.stmXValue.Length) * sizeof(byte);

                // MMI_N_ITER2
                sizeCalculated += sizeof(ushort);

                for (int l = 0; l < stmElement.stmPickupList.Count; l++)
                {
                    // MMI_STM_L_VALUE[l] + MMI_STM_X_VALUE chars.
                    sizeCalculated += sizeof(ushort) + (stmElement.stmPickupList[l].Length * sizeof(byte));
                }
            }

            return (uint) sizeCalculated;
        }

        /// <summary>
        /// Send EVC-25 MMI Specific STM DE Request telegram.
        /// </summary>
        public static void Send()
        {
            _pool.SITR.ETCS1.SpecificStmDeRequest.EVC25alias1.Value = _evc25Alias1;

            ushort numberOfDataUnits = (ushort) StmData.Count;

            // Limit number of data units to 5
            if (numberOfDataUnits > 5)
            {
                throw new ArgumentOutOfRangeException("Too many STM Data units!");
            }
            else
            {
                _pool.SITR.ETCS1.SpecificStmDeRequest.MmiNIter.Value = numberOfDataUnits;
            }

            ushort totalSizeCounter = BasicPacketLength;

            // Check for exceeding maximum packet size
            if (MaximumPacketLength < FindPacketSize())
            {
                throw new ArgumentOutOfRangeException("Packet size exceeds maximum allowed");
            }

            string packetName = "ETCS1_SpecificStmDeRequest_";

            // For all data units
            for (int k = 0; k < numberOfDataUnits; k++)
            {
                string tagName1 = packetName + $"EVC25SpecificStmDeRequestSub1{k}_";

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
                if (caption.Length > StmCaptionMaximumLength)
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
                string tagName2 = $"{tagName1}EVC25SpecificStmDeRequestSub11";

                for (int l = 0; l < caption.Length; l++)
                {
                    requestName = $"{tagName2}{l.ToString("00")}_MmiStmXCaption";

                    _pool.SITR.Client.Write(requestName, caption[l]);
                    totalSizeCounter += sizeof(byte);
                }

                var xValue = StmData[k].stmXValue.ToCharArray();

                if (xValue.Length > StmXValueMaximumLength)
                {
                    throw new ArgumentOutOfRangeException("Too many characters in x value string!");
                }
                else
                {
                    // Write out the MMI_STM_L_VALUE (length of xvalue string)
                    requestName = $"{tagName1}MmiStmLValue";

                    _pool.SITR.Client.Write(requestName, xValue.Length);
                    totalSizeCounter += sizeof(ushort);
                }

                tagName2 = $"{tagName1}EVC25SpecificStmDeRequestSub12";

                // Write out the XValue characters
                for (int l = 0; l < xValue.Length; l++)
                {
                    requestName = $"{tagName2}{l.ToString("00")}_MmiStmXValue";

                    _pool.SITR.Client.Write(requestName, xValue[l]);
                    totalSizeCounter += sizeof(byte);
                }

                // Pick-up list repeats additional captions
                ushort numberInPickupList = (ushort) StmData[k].stmPickupList.Count;

                if (numberInPickupList > StmXValueMaximumIterations)
                {
                    throw new ArgumentOutOfRangeException("Too many STM Pick-up list items!");
                }
                else
                {
                    requestName = $"{tagName1}MmiNIter2";

                    _pool.SITR.Client.Write(requestName, numberInPickupList);
                    totalSizeCounter += sizeof(ushort);

                    string tagName3 = $"{tagName1}EVC25SpecificStmDeRequestSub13";

                    for (int l = 0; l < numberInPickupList; l++)
                    {
                        var pickUpXValue = StmData[k].stmPickupList[l].ToCharArray();

                        if (pickUpXValue.Length > StmPickupXValueMaximumLength)
                        {
                            throw new ArgumentOutOfRangeException(
                                "Too many characters in pick-up list x value string!");
                        }
                        else
                        {
                            string tagName4 = $"{tagName3}{l.ToString("00")}_";
                            requestName = $"{tagName3}_MmiStmLValue";

                            _pool.SITR.Client.Write(requestName, pickUpXValue.Length);

                            string tagName5 = $"{tagName4}EVC25SpecificStmDRequestSub131";

                            for (int m = 0; m < pickUpXValue.Length; m++)
                            {
                                requestName = $"{tagName5}{m}_MmiStmXValue";
                                _pool.SITR.Client.Write(requestName, pickUpXValue[m]);
                                totalSizeCounter += sizeof(byte);
                            }
                        }
                    }
                }
            }

            // Set packet length
            _pool.SITR.ETCS1.SpecificStmDeRequest.MmiLPacket.Value = totalSizeCounter;

            // Send dynamic packet
            _pool.SITR.SMDCtrl.ETCS1.SpecificStmDeRequest.Value = 0x0009;
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
            set => _pool.SITR.ETCS1.SpecificStmDeRequest.MmiNidNtc.Value = value;
        }

        /// <summary>
        /// Need for driver intervention during Specific STM Data Entry.
        /// Qualifier indicating if the STM still need Specific STM Data Entry or not.
        /// 
        /// Values:
        /// 0 = "No driver intervention requested"
        /// 1 = "Driver intervention requested"
        /// </summary>
        public static bool MMI_STM_Q_DRIVERINT
        {
            set
            {
                if (value)
                {
                    _evc25Alias1 |= 0x01;
                }
                else
                {
                    _evc25Alias1 &= 0xfe;
                }
            }
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
                    _evc25Alias1 |= 0x02;
                }
                else
                {
                    _evc25Alias1 &= 0xfd;
                }
            }
        }

        /// <summary>
        /// List of STM elements.
        /// /// </summary>
        public static List<EVC25_StmData> StmData { get; set; }
    }
}