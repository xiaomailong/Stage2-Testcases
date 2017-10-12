#region usings

using CL345;
using System;
using System.Collections.Generic;

#endregion

namespace Testcase.Telegrams.EVCtoDMI
{

    /// 
    /// 
    /// 
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
        private static byte _driverAndFollowing;

        /// <summary>
        /// Initialise EVC-25 MMI Specific STM DE Request telegram
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            _driverAndFollowing = 0x00;
            StmData = new List<EVC25_StmData>();


            // Activate dynamic array
            _pool.SITR.SMDCtrl.ETCS1.SpecificStmDeRequest.Value = 0x8;

            // Set default values
            _pool.SITR.ETCS1.SpecificStmDeRequest.MmiMPacket.Value = 25; // Packet ID
        }

        private static uint FindPacketSize()
        {
            int sizeCalculated = EVC25_StmData.BasicPacketLength;       // up to MMI_N_ITER

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
            return (uint)sizeCalculated;
        }

        /// <summary>
        /// Send EVC-25 MMI Specific STM DE Request telegram
        /// </summary>
        public static void Send()
        {
            _pool.SITR.ETCS1.SpecificStmDeRequest.EVC25alias1.Value = _driverAndFollowing;

            ushort numberOfDataUnits = (ushort)StmData.Count;

            if (numberOfDataUnits > 5)
            {
                throw new ArgumentOutOfRangeException("Too many STM Data units!");
            }
            else
            {
                _pool.SITR.ETCS1.SpecificStmDeRequest.MmiNIter.Value = numberOfDataUnits;
            }

            ushort totalSizeCounter = EVC25_StmData.BasicPacketLength;

            if (EVC25_StmData.MaximumPacketLength < FindPacketSize())
            {
                throw new ArgumentOutOfRangeException("Packet size exceeds maximum allowed");
            }

            // For all data units
            for (int k = 0; k < numberOfDataUnits; k++)
            {
                var varnamestring = $"ETCS1_SpecificStmDeRequest_EVC25SpecificStmDeRequestSub10{k}_";

                // write out the MMI_NID_NTC value
                _pool.SITR.Client.Write($@"{varnamestring}MmiNidNtc", StmData[k].nidNtc);
                totalSizeCounter += sizeof(byte);

                // write out the MMI_STM_NID_DATA value (index
                _pool.SITR.Client.Write($@"{varnamestring}MmiStmNidData", StmData[k].stmNidData);
                totalSizeCounter += sizeof(byte);

                // write out the MMI_EVC_M_XATTRIBUTE value (type)
                _pool.SITR.Client.Write($@"{varnamestring}MmiEvcMXattribute", StmData[k].evcMXAttribute);
                totalSizeCounter += sizeof(ushort);

                var caption = StmData[k].stmCaption.ToCharArray();

                // Limit number of caption characters to 20
                if (caption.Length > EVC25_StmData.StmCaptionMaximumLength)
                {
                    throw new ArgumentOutOfRangeException("Too many characters in caption string!");
                }
                else
                {
                    // write out the MMI_L_CAPTION value (length)
                    _pool.SITR.Client.Write($@"{varnamestring}MmiLCaption", (ushort) caption.Length);
                    totalSizeCounter += sizeof(ushort);
                }

                // Write individual caption chars
                // Dynamic fields 2nd dimension
                for (int l = 0; l < caption.Length; l++)
                {
                    _pool.SITR.Client.Write($"{varnamestring}EVC25SpecificStmDeRequestSub110{k}_MmiStmXCaption",
                        caption[l]);
                    totalSizeCounter += sizeof(byte);
                }

                var xValue = StmData[k].stmXValue.ToCharArray();

                if (xValue.Length > EVC25_StmData.StmXValueMaximumLength)
                {
                    throw new ArgumentOutOfRangeException("Too many characters in x value string!");
                }
                else
                {
                    // write out the MMI_STM_L_VALUE (length of xvalue string)
                    _pool.SITR.Client.Write($"{varnamestring}MmiStmLValue", xValue.Length);
                    totalSizeCounter += sizeof(ushort);
                }

                // write out the XValue chars
                for (int l = 0; l < xValue.Length; l++)
                {
                    _pool.SITR.Client.Write($"{varnamestring}EVC25SpecificStmDeRequestSub120{l}_MmiStmXValue", xValue[l]);
                    totalSizeCounter += sizeof(byte);
                }

                // Pick-up list repeats additional captions
                ushort numberInPickupList = (ushort)StmData[k].stmPickupList.Count;

                if (numberInPickupList > EVC25_StmData.StmXValueMaximumIterations)
                {
                    throw new ArgumentOutOfRangeException("Too many STM Pick-up list items!");
                }
                else
                {
                    _pool.SITR.Client.Write($"{varnamestring}MmiNIter2", numberInPickupList);
                    totalSizeCounter += sizeof(ushort);

                    for (int l = 0; l < numberInPickupList; l++)
                    {
                        var pickUpXValue = StmData[k].stmPickupList[l].ToCharArray();
                        if (pickUpXValue.Length > EVC25_StmData.StmPickupXValueMaximumLength)
                        {
                            throw new ArgumentOutOfRangeException("Too many characters in pick-up list x value string!");
                        }
                        else
                        {
                            var varSubNameString = $"{varnamestring}EVC25SpecificStmDeRequestSub130{l}_";
                            _pool.SITR.Client.Write($"{varSubNameString}MmiStmLValue", pickUpXValue.Length);

                            for (int m = 0; m < pickUpXValue.Length; m++)
                            {
                                _pool.SITR.Client.Write($"{varSubNameString}EVC25SpecificStmDRequestSub1310{m}_MmiStmXValue",
                                                        pickUpXValue[m]);
                                totalSizeCounter += sizeof(byte);
                            }
                        }
                    }
                }
            }

            // Set packet length
            _pool.SITR.ETCS1.SpecificStmDeRequest.MmiLPacket.Value = totalSizeCounter;

            // Send dynamic packet
            _pool.SITR.SMDCtrl.ETCS1.SpecificStmDeRequest.Value = 0x9;
            
        }

        /// <summary>
        /// Identity of the STM that requested the data
        /// </summary>
        public static byte MMI_NID_NTC
        {
            set => _pool.SITR.ETCS1.SpecificStmDeRequest.MmiNidNtc.Value = value;
        }

        /// <summary>
        /// MMI_STM_Q_DRIVERINT
        /// </summary>
        public static bool MMI_STM_Q_DRIVERINT
        {
            set
            {
                if (value)
                {
                    _driverAndFollowing |= 0x01;
                }
                else
                {
                    _driverAndFollowing &= 0xfe;
                }
            } 
        }

        /// <summary>
        /// MMI_STM_Q_FOLLOWING
        /// </summary>
        public static bool MMI_STM_Q_FOLLOWING
        {
            set
            {
                if (value)
                {
                    _driverAndFollowing |= 0x02;
                }
                else
                {
                    _driverAndFollowing &= 0xfd;
                }
            }
        }

        /// <summary>
        /// List of STM elements.
        /// /// </summary>
        public static List<EVC25_StmData> StmData { get; set; }
      
    }
}
