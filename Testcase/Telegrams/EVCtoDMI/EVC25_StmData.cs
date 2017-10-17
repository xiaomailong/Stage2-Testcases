#region usings

using System;
using System.Collections.Generic;
using System.Xml.Schema;

#endregion

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This structure collects the repeated data for the EVC25 packet
    /// </summary>
    public class EVC25_StmData
    {
        public const int StmMaximumIterations = 5;
        public const int StmCaptionMaximumLength = 20;
        public const int StmXValueMaximumLength = 10;
        public const int StmPickupXValueMaximumLength = 10;
        public const int StmXValueMaximumIterations = 16;
        public const int BasicPacketLength = (3 * sizeof(UInt16)) + (2 * sizeof(byte));

        public const int MaximumPacketLength = BasicPacketLength +
                                               (StmMaximumIterations *
                                                (StmXValueMaximumLength + StmCaptionMaximumLength +
                                                 (4 * sizeof(ushort)) + (2 * sizeof(byte)) +
                                                 (StmXValueMaximumIterations *
                                                  (StmPickupXValueMaximumLength + sizeof(ushort)))));

        public byte nidNtc;
        public byte stmNidData;
        public ushort evcMXAttribute;
        public string stmCaption;
        public string stmXValue;
        public List<string> stmPickupList;
    }
}
