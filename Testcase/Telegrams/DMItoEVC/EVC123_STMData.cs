using System.Collections.Generic;


namespace Testcase.Telegrams.DMItoEVC
{
    /// <summary>
    /// This structure collects the repeated data for the EVC25 packet
    /// </summary>
    public class EVC123_StmDataElement
    {
        public byte stmNidNtc;
        public byte stmNidData;
        public string stmXValue;
    }

    public class EVC123_StmData
    {
        public byte nidNtc;
        public List<EVC123_StmDataElement> elements;
    }
}