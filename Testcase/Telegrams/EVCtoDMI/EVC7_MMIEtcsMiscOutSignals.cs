using CL345;

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// The signals in this telegram are outputs from the generic ETCS OB R4 system.
    /// This telegram collects miscellaneous output signals to the train.
    /// 
    /// Note:
    /// This packet is routed via dedicated port and thus no header nor length information is contained in the (plain) data set.
    /// It is also protected via SDTv2.
    /// </summary>
    public static class EVC7_MMIEtcsMiscOutSignals
    {
        private static TestcaseBase _pool;
        private static byte _trainEBTestInProgress;
        private static byte _trainEBStatus;
        private static byte _radioStatusInformation;
        private static byte _stmInHsStateExists;
        private static byte _stmInDaStateExists;
        private static MMI_OBU_TR_BRAKETEST_STATUS _etcsBrakeTestStatus;
        private static MMI_OBU_TR_M_LEVEL _level;

        /// <summary>
        /// Initialise EVC-7 MMI ETCS Misc Out Signals telegram
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(TestcaseBase pool)
        {
            _pool = pool;

            // Set default values
            _pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7alias1B0.Value = 0x00; // No EB test in progress, no EB, no radio
            _pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7alias1B1.Value = 0x02; // Brake test not in progress, Level 1
            _pool.SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrMMode.Value = (byte) MMI_OBU_TR_M_MODE.StandBy; // Standby mode
            _pool.SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrMAdhesion.Value = 1; // Non-slippery rail
            _pool.SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrNidStmHs.Value = 255; // No STM in hot standby
            _pool.SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrNidStmDa.Value = 255; // No STM
            _pool.SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrBrakeTestTimeOut.Value = 2880; // 48 hours
            _pool.SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrOTrain.Value = 100; // Initial position
            SetValidityBits(true);
            _pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7SSW1.Value = 0x8000; // 32768 in decimal
            _pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7SSW2.Value = 0x8000; // 32768 in decimal
            _pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7SSW3.Value = 0x8000; // 32768 in decimal
        }

        private static void SetAlias1B0()
        {
            _pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7alias1B0.Value =
                (byte) (_trainEBTestInProgress << 6 | _trainEBStatus << 5 | _radioStatusInformation << 4 |
                        _stmInHsStateExists << 3 | _stmInDaStateExists << 2);
        }

        private static void SetAlias1B1()
        {
            var etcsBrakeTestStatus = (byte) _etcsBrakeTestStatus;
            var level = (byte) _level;
            _pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7alias1B1.Value = (byte) (etcsBrakeTestStatus << 4 | level);
        }

        /// <summary>
        /// Current applied adhesion coefficient.
        /// 
        /// Values:
        /// 0 = "Slippery rail" (DEFAULT)
        /// 1 = "Non-slippery rail"
        /// 2..255 = "spare"
        /// </summary>
        public static byte MMI_OBU_TR_M_ADHESION
        {
            get { return _pool.SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrMAdhesion.Value; }
            set { _pool.SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrMAdhesion.Value = (byte) value; }
        }

        /// <summary>
        /// Current nominal position of the train.
        /// 
        /// Values:
        /// -2147483648 = "Unknown" (DEFAULT)
        /// 
        /// Note 1: Currently the ETCS Onboard counts this coordinate from 0 to maximum value. Negative values are not used.
        /// Note 2: The odometer related variables will only contain bit 0-31 of the source variable.
        ///         I.e. The variable will wrap from 2147483647 -> 0. The receiver should be able to handle this.
        /// </summary>
        public static int MMI_OBU_TR_O_TRAIN
        {
            get { return _pool.SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrOTrain.Value; }
            set { _pool.SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrOTrain.Value = (int) value; }
        }

        /// <summary>
        /// The time until the next brake test is due
        /// 
        /// Values:
        /// 0 = "Brake Test mandatory"
        /// 1..64800 = "Remaining time in minutes to brake test timeout"
        /// 64801..65534 = "spare"
        /// 65535 = "brake test will never timeout"
        /// </summary>
        public static ushort BRAKE_TEST_TIMEOUT
        {
            get { return _pool.SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrBrakeTestTimeOut.Value; }
            set { _pool.SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrBrakeTestTimeOut.Value = (ushort) value; }
        }

        /// <summary>
        /// NID_NTC in "Data Available" (DA) state.
        /// 
        /// Values:
        /// 0..254 = "NID_NTC of the related STM"
        /// 255 = "no STM" (DEFAULT)
        /// 
        /// Note: Used NID_NTC values shall be coded as defined the same as for variable "NID_NTC" in Subset-026, ch.7.5.1.98
        /// </summary>
        public static byte OBU_TR_NID_STM_DA
        {
            get { return _pool.SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrNidStmDa.Value; }
            set { _pool.SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrNidStmDa.Value = (byte) value; }
        }

        /// <summary>
        /// NID_NTC in "Hot Standby" (HS) state.
        /// 
        /// Values:
        /// 0..254 = "NID_NTC of the related STM"
        /// 255 = "no STM" (DEFAULT)
        /// 
        /// Note: Used NID_NTC values shall be coded as defined the same as for variable "NID_NTC" in Subset-026, ch.7.5.1.98
        /// </summary>
        public static byte OBU_TR_NID_STM_HS
        {
            get { return _pool.SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrNidStmHs.Value; }
            set { _pool.SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrNidStmHs.Value = (byte) value; }
        }

        /// <summary>
        /// Train EB test in progress
        /// 
        /// Values:
        /// 0 = "No EB test in progress"    (DEFAULT)
        /// 1 = "EB test in progress"
        /// </summary>
        public static byte MMI_OBU_TR_EBTestInProgress
        {
            get { return _trainEBTestInProgress; }

            set
            {
                _trainEBTestInProgress = value;
                SetAlias1B0();
            }
        }

        /// <summary>
        /// Train EB status
        /// 
        /// Values:
        /// 0 = "No EB has been commanded by ETCS"
        /// 1 = "EB has been commanded by ETCS" (DEFAULT)
        /// </summary>
        public static byte MMI_OBU_TR_EB_Status
        {
            get { return _trainEBStatus; }

            set
            {
                _trainEBStatus = value;
                SetAlias1B0();
            }
        }

        /// <summary>
        /// Radio status information
        /// 
        /// Values:
        /// 0 = "No radio connection"   (DEFAULT)
        /// 1 = "Radio connection established"
        /// </summary>
        public static byte MMI_OBU_TR_RadioStatus
        {
            get { return _radioStatusInformation; }

            set
            {
                _radioStatusInformation = value;
                SetAlias1B0();
            }
        }

        /// <summary>
        /// Defines if a STM is allowed to enter HS state
        /// 
        /// Values:
        /// 0 = "No STM is in HS state" (DEFAULT)
        /// 1 = "One STM is allowed to enter HS state. See corresponding OBU_TR_NID_STM_HS variable"
        /// </summary>
        public static byte MMI_OBU_TR_STM_HS_ENABLED
        {
            get { return _stmInHsStateExists; }

            set
            {
                _stmInHsStateExists = value;
                SetAlias1B0();
            }
        }

        /// <summary>
        /// Defines if a STM is allowed to enter DA state.
        /// 
        /// Values:
        /// 0 = "No STM is in DA state" (DEFAULT)
        /// 1 = "One STM is allowed to enter DA state. See corresponding OBU_TR_NID_STM_DA variable"
        /// </summary>
        public static byte MMI_OBU_TR_STM_DA_ENABLED
        {
            get { return _stmInDaStateExists; }

            set
            {
                _stmInDaStateExists = value;
                SetAlias1B0();
            }
        }

        /// <summary>
        /// ETCS EB test status
        /// 
        /// Values:
        /// 0 = BrakeTestNotInProgress
        /// 1 = BrakeTestInProgress
        /// 2 = BrakeTestSuccessful
        /// 3 = BrakeTestFailed
        /// 4 = UnableToStartBrakeTest
        /// 5 = Aborted
        /// 6..255 = "spare"
        /// </summary>
        public static MMI_OBU_TR_BRAKETEST_STATUS MMI_OBU_TR_BrakeTest_Status
        {
            get { return _etcsBrakeTestStatus; }

            set
            {
                _etcsBrakeTestStatus = value;
                SetAlias1B1();
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
            get { return _level; }

            set
            {
                _level = value;
                SetAlias1B1();
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
            set { _pool.SITR.ETCS1.EtcsMiscOutSignals.MmiObuTrMMode.Value = (byte) value; }
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
            Invalid = 17,
            Unknown = 128
        }

        /// <summary>
        /// Sets all validity bits of EVC 7 to valid or invalid
        /// </summary>
        /// <param name="valid"></param>
        public static void SetValidityBits(bool valid)
        {
            SetValidityBits(valid, valid, valid, valid, valid, valid, valid, valid, valid, valid, valid, valid, valid);
        }

        public static void SetValidityBits(bool ebinprogress, bool ebstatus, bool radiostatus, bool hsenabled,
            bool daenabled, bool braketeststatus,
            bool mlevel, bool mmode, bool adhesion, bool stmhs, bool stmda, bool braketesttimeout, bool otrain)
        {
            ushort val1 = 0;
            ushort val2 = 0;

            if (ebinprogress)
                val1 += 1 << 14;
            if (ebstatus)
                val1 += 1 << 13;
            if (radiostatus)
                val1 += 1 << 12;
            if (hsenabled)
                val1 += 1 << 11;
            if (daenabled)
                val1 += 1 << 10;
            if (braketeststatus)
                val1 += 1 << 7;
            if (mlevel)
                val1 += 1 << 3;

            _pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity1.Value = val1;

            if (mmode)
                val2 += 1 << 15;
            if (adhesion)
                val2 += 1 << 14;
            if (stmhs)
                val2 += 1 << 13;
            if (stmda)
                val2 += 1 << 12;
            if (braketesttimeout)
                val2 += 1 << 11;
            if (otrain)
                val2 += 1 << 10;

            _pool.SITR.ETCS1.EtcsMiscOutSignals.EVC7Validity2.Value = val2;
        }
    }
}