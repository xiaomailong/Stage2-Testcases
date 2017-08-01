using CL345;

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// The signals in this telegram are outputs from the generic ETCS OB R4 system.
    /// This telegram collects miscellaneous output signals to the train.
    /// Note:
    /// This packet is routed via dedicated port and thus no header nor length information is contained in the (plain) data set.
    /// It is also protected via SDTv2.
    /// </summary>
    static class EVC7_MMIEtcsMiscOutSignals
    {
        private static SignalPool _pool;
        private static MMI_OBU_TR_BRAKETEST_STATUS _brakeTestStatus;
        private static MMI_OBU_TR_M_LEVEL _level;

        /// <summary>
        /// Initialise EVC-7 telegram
        /// </summary>
        /// <param name="pool"></param>
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
            var level = (byte) _level;
            _pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7alias1B1.Value = (byte) (brakeTestStatus << 4 | level);
        }

        /// <summary>
        /// EB test in progress
        /// 
        /// Values
        /// 0 = BrakeTestNotInProgress
        /// 1 = BrakeTestInProgress
        /// 2 = BrakeTestSuccessful
        /// 3 = BrakeTestFailed
        /// 4 = UnableToStartBrakeTest
        /// 5 = Aborted
        /// </summary>
        public static MMI_OBU_TR_BRAKETEST_STATUS MMI_OBU_TR_BrakeTest_Status
        {
            set
            {
                _brakeTestStatus = value;
                SetAliasB1();
            }
        }

        /// <summary>
        /// ETCS Mode
        /// 
        /// Values:
        /// 0 = "Level 0"
        /// 1 = "Level NTC"
        /// 2 = "Level 1"
        /// 3 = "Level 2"
        /// 4 = "Level 3"
        /// 5..254 = "spare"
        /// 255 = "unknown"
        /// </summary>
        public static MMI_OBU_TR_M_LEVEL MMI_OBU_TR_M_Level
        {
            set
            {
                _level = value;
                SetAliasB1();
            }
        }

        /// <summary>
        /// ETCS Mode
        /// 
        /// Values:
        /// 0 = "FS - Full Supervision"
        /// 1 = "OS - On-sight"
        /// 2 = "SR - Staff Responsible"
        /// 3 = "SH - Shunting"
        /// 4 = "UN - Unfitted"
        /// 5 = "SL - Sleeping"
        /// 6 = "SB - Standby"
        /// 7 = "TR - Trip"
        /// 8 = "PT - Post trip"
        /// 9 = "SF - System failure"
        /// 10 = "IS - Isolation"
        /// 11 = "NL - Non-leading"
        /// 12 = "LS - Limited Supervision"
        /// 13 = "SN - National System"
        /// 14 = "RV - Reversing"
        /// 15 = "PS - Passive Shunting"
        /// 16 = "NP - No Power"
        /// 17..127 = "Not used"
        /// 128 = "Unknown" (DEFAULT)
        /// 129..255 = "Not used"
        /// </summary>
        public static MMI_OBU_TR_M_MODE MMI_OBU_TR_M_Mode
        {
            set => _pool.SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrMMode.Value = (byte) value;
        }

        /// <summary>
        /// Brake test mode enum
        /// </summary>
        public enum MMI_OBU_TR_BRAKETEST_STATUS : byte
        {
            BrakeTestNotInProgress = 0,
            BrakeTestInProgress = 1,
            BrakeTestSuccessful = 2,
            BrakeTestFailed = 3,
            UnableToStartBrakeTest = 4,
            Aborted = 5
        }

        /// <summary>
        /// ETCS Level enum
        /// </summary>
        public enum MMI_OBU_TR_M_LEVEL : byte
        {
            L0 = 0,
            LNTC = 1,
            L1 = 2,
            L2 = 3,
            L3 = 4,
            Unknown = 15
        }

        /// <summary>
        /// ETCS Mode enum
        /// </summary>
        public enum MMI_OBU_TR_M_MODE : byte
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