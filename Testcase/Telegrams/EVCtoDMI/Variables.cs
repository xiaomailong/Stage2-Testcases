using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL345;

namespace Testcase.Telegrams.EVCtoDMI
{
    static class Variables
    {
        /// <summary>
        /// This populates the Data Elements of EVC-6, 11 and 22
        /// </summary>
        /// <param name="basestring">The base RTSIM signal name to use</param>
        /// <param name="totalsizecounter">Reference counter for total size of telegram</param>
        /// <param name="_pool">The SignalPool</param>
        /// <returns></returns>
        public static ushort PopulateDataElements(string basestring, ushort totalsizecounter, SignalPool _pool)
        {
            // populate the data elements array
            for (var tdeindex = 0; tdeindex < EVC6_MMICurrentTrainData.DataElements.Count; tdeindex++)
            {
                var traindataelement = EVC6_MMICurrentTrainData.DataElements[tdeindex];

                var varnamestring = basestring + tdeindex + "_";
                var charArray = traindataelement.EchoText.ToCharArray();
                if (charArray.Length > 10)
                    throw new ArgumentOutOfRangeException();

                // set identifier
                _pool.SITR.Client.Write(varnamestring + "MmiNidData", traindataelement.Identifier);

                // set data check result
                _pool.SITR.Client.Write(varnamestring + "MmiQDataCheck", traindataelement.QDataCheck);

                // set number of chars
                _pool.SITR.Client.Write(varnamestring + "MmiNText", charArray.Length);


                totalsizecounter += 32;

                // populate the array

                for (var charindex = 0; charindex < charArray.Length; charindex++)
                {
                    var character = charArray[charindex];

                    if (charindex < 10)
                    {
                        _pool.SITR.Client.Write(
                            basestring + $"10{charindex}_MmiXText",
                            character);
                    }
                    else
                    {
                        _pool.SITR.Client.Write(
                            basestring + $"1{charindex}_MmiXText",
                            character);
                    }
                    totalsizecounter += 8;
                }
            }
            return totalsizecounter;
        }

        public class DataElement
        {
            public ushort Identifier { get; set; }
            public ushort QDataCheck { get; set; }
            public string EchoText { get; set; }
        }
    }
}
