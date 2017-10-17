using System;
using System.Collections.Generic;
using CL345;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using static Testcase.Telegrams.EVCtoDMI.Variables;


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

