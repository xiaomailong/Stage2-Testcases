using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL345;

namespace Testcase.Telegrams
{
    static class EVC1_MMIDynamic
    {
        private static SignalPool _pool;
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            
            // Set default values
            _pool.SITR.ETCS1.Dynamic.EVC1alias1.Value = 0;
            _pool.SITR.ETCS1.Dynamic.MmiVTrain.Value = 0;
            _pool.SITR.ETCS1.Dynamic.MmiATrain.Value = 0;
            _pool.SITR.ETCS1.Dynamic.MmiVTarget.Value = -1;
            _pool.SITR.ETCS1.Dynamic.MmiVPermitted.Value = 0;
            _pool.SITR.ETCS1.Dynamic.MmiVIntervention.Value = -1;
            _pool.SITR.ETCS1.Dynamic.MmiVRelease.Value = -1;
            _pool.SITR.ETCS1.Dynamic.MmiOBraketarget.Value = -1;
            _pool.SITR.ETCS1.Dynamic.MmiOIml.Value = -1;
            _pool.SITR.ETCS1.Dynamic.EVC01Validity1.Value = 0xc800; // 51200 in decimal
            _pool.SITR.ETCS1.Dynamic.EVC01Validity1.Value = 0xff00; // 65280 in decimal
            _pool.SITR.ETCS1.Dynamic.EVC01SSW1.Value = 0x8000; // 32768 in decimal
            _pool.SITR.ETCS1.Dynamic.EVC01SSW2.Value = 0x8000; // 32768 in decimal
            _pool.SITR.ETCS1.Dynamic.EVC01SSW3.Value = 0x8000; // 32768 in decimal
        }
    }
}
