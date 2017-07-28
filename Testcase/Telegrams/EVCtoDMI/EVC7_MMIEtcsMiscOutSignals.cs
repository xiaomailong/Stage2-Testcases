using CL345;

namespace Testcase.Telegrams.EVCtoDMI
{
    static class EVC7_MMIEtcsMiscOutSignals
    {
        private static SignalPool _pool;
        private static MMI_OBU_TR_BRAKETEST_STATUS _brakeTestStatus;
        private static MMI_OBU_TR_M_LEVEL _level;

        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // Set default values
            _pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7alias1B0.Value = 0x0;
            _pool.SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrMAdhesion.Value = 0x64; // 100 in decimal
            _pool.SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrNidStmHs.Value = 0xff; // 255 in decimal
            _pool.SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrNidStmDa.Value = 0xff; // 255 in decimal
            _pool.SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrBrakeTestTimeOut.Value = 0x700; // 1792 in decimal
            _pool.SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrOTrain.Value = 1000000000;
            _pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity1.Value = 0x7c88; // 31880 in decimal
            _pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity2.Value = 0xfc00; // 64512 in decimal
            _pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7SSW1.Value = 0x8000; // 32768 in decimal
            _pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7SSW2.Value = 0x8000; // 32768 in decimal
            _pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7SSW3.Value = 0x8000; // 32768 in decimal

        }

        private static void SetAliasB1()
        {
            var brakeTestStatus = (byte) _brakeTestStatus;
            var level = (byte)_level;
            _pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7alias1B1.Value = (byte)(brakeTestStatus << 4 | level);
        }      
        
        public static MMI_OBU_TR_BRAKETEST_STATUS MMI_OBU_TR_BrakeTest_Status
        {
            set
            {
                _brakeTestStatus = value;
                SetAliasB1();
            }
        }
        public static MMI_OBU_TR_M_LEVEL MMI_OBU_TR_M_Level
        {
            set
            {
                _level = value;
                SetAliasB1();
            }
        }

        public static MMI_OBU_TR_M_MODE MMI_OBU_TR_M_Mode
        {
            set => _pool.SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrMMode.Value = (byte)value;
        }

        public enum MMI_OBU_TR_BRAKETEST_STATUS : ushort
        {
            BrakeTestNotInProgress = 0,
            BrakeTestInProgress = 1,
            BrakeTestSuccessful = 2,
            BrakeTestFailed = 3,
            UnableToStartBrakeTest = 4,
            Aborted = 5
        }

        public enum MMI_OBU_TR_M_LEVEL : ushort
        {
            L0 = 0,
            LNTC = 1,
            L1 = 2,
            L2 = 3,
            L3 = 4,
            Unknown = 15
        }       

        public enum MMI_OBU_TR_M_MODE : ushort
        {
            FullSupervision = 0,
            OnSight = 1,
            StaffResponsible = 2,
            Shunting = 3,
            Unfitted = 4,
            Sleeping = 5,
            StandBy = 6,
            Trip = 7,
            PostTrip = 8,
            SystemFailure = 9,
            Isolation = 10,
            NonLeading = 11,
            LimitedSupervision = 12,
            NationalSystem = 13,
            Reversing = 14,
            PassiveShunting = 15,
            NoPower = 16,
            Unknown = 128
        }

    }
}