using System;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using CL345;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.Telegrams.DMItoEVC
{
    /// <summary>
    /// This packet shall be sent when the driver requests for an action from the ATP, typically by pressing a button.
    /// </summary>
    public static class EVC102_MMIStatusReport
    {
        private static TestcaseBase _pool;
        private static bool _checkResult;
        private static MMI_M_MODE_READBACK _mModeReadBack;
        private static MMI_M_MMI_STATUS _mMmiStatus;
        private static Variables.MMI_M_ACTIVE_CABIN _mActiveCabin;
        private const string BaseString = "DMI->ETCS: Check EVC-102 [MMI_STATUS_REPORT]";

        /// <summary>
        /// Initialise EVC-102 MMI_Status_Report telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(TestcaseBase pool)
        {
            _pool = pool;
            _pool.SITR.SMDCtrl.ETCS1.Status.Value = 0x0001;
        }

        private static void CheckActiveCabin(Variables.MMI_M_ACTIVE_CABIN mActiveCabin)
        {
            // Get EVC102_alias_1_B0
            byte evc102Alias1B0 = _pool.SITR.CCUS.ETCS1StatusReport.EVC102alias1B0.Value;

            // Extract MMI_M_ACTIVE_CABIN (4th and 5th bits according to VSIS 2.9)
            byte mmiMActiveCabin = (byte) ((evc102Alias1B0 & 0x30) >> 4); // xxxx xxxx -> 00xx 0000 -> 0000 00xx

            // Check MMI_M_ACTIV_CABIN value
            _checkResult = mmiMActiveCabin.Equals((byte) mActiveCabin);

            // If passed
            if (_checkResult)
            {
                _pool.TraceReport(string.Format("{0} - MMI_M_ACTIVE_CABIN = \"{1}\"", BaseString, mActiveCabin) +
                                  Environment.NewLine +
                                  "Result: PASSED.");
            }
            // Display the real value extracted from EVC-102 [MMI_STATUS_REPORT.MMI_M_ACTIVE_CABIN]
            else
            {
                _pool.TraceError(string.Format("{0} - MMI_M_ACTIVE_CABIN = \"{1}\"", BaseString, mActiveCabin) +
                                 Environment.NewLine +
                                 "Result: FAILED!" + Environment.NewLine +
                                 string.Format("Current active cab = {0} - ", mmiMActiveCabin) +
                                 Enum.GetName(typeof(Variables.MMI_M_ACTIVE_CABIN), mmiMActiveCabin));
            }
        }

        private static void CheckModeReadBack(MMI_M_MODE_READBACK mModeReadBack)
        {
            // Check MMI_M_MODE_READBACK value
            _checkResult =
                _pool.SITR.CCUS.ETCS1StatusReport.MmiMModeReadback.WaitForCondition(Is.Equal, (byte) mModeReadBack, 5000, 100);

            // If passed
            if (_checkResult)
            {
                _pool.TraceReport(string.Format("{0} - MMI_M_MODE_READBACK = \"{1}\"", BaseString, mModeReadBack) +
                                  Environment.NewLine +
                                  "Result: PASSED.");
            }
            // Display the real value extracted from EVC-102
            else
            {
                // Get current mode
                byte currentMode = _pool.SITR.CCUS.ETCS1StatusReport.MmiMModeReadback.Value;

                _pool.TraceError(string.Format("{0} - MMI_M_MODE_READBACK = \"{1}\"", BaseString, mModeReadBack) +
                                 Environment.NewLine +
                                 "Result: FAILED!" + Environment.NewLine +
                                 string.Format("Current active mode = {0} - ", currentMode) +
                                 Enum.GetName(typeof(MMI_M_MODE_READBACK), currentMode));
            }
        }

        private static void CheckMmiStatus()
        {
            // Get EVC102_alias_1_B1
            byte evc102Alias1B1 = _pool.SITR.CCUS.ETCS1StatusReport.EVC102alias1B1.Value;

            // Extract MMI_M_MM_STATUS (upper 4 bits according to VSIS 2.9)
            byte mmiMStatus = (byte) ((evc102Alias1B1 & 0xf0) >> 4); // xxxx xxxx -> xxxx 0000 -> 0000 xxxx

            // Check MMI_M_ACTIV_CABIN value
            _checkResult = mmiMStatus.Equals((byte) _mMmiStatus);

            // If passed
            if (_checkResult)
            {
                _pool.TraceReport(string.Format("{0} - MMI_M_MMI_STATUS = \"{1}\"", BaseString, _mMmiStatus) +
                                  Environment.NewLine +
                                  "Result: PASSED.");
            }
            // Display the real value extracted from EVC-102 [MMI_STATUS_REPORT.MMI_M_MM_STATUS]
            else
            {
                _pool.TraceError(string.Format("{0} - MMI_M_MMI_STATUS = \"{1}\"", BaseString, mmiMStatus) +
                                 Environment.NewLine +
                                 "Result: FAILED!");
            }
        }

        /// <summary>
        /// Defines the identity of the activated cabin
        /// 
        /// Values:
        /// 0 = "No cabin is active"
        /// 1 = "Cabin 1 is active"
        /// 2 = "Cabin 2 is active"
        /// 3 = "Spare"
        /// </summary>
        public static Variables.MMI_M_ACTIVE_CABIN Check_MMI_M_ACTIVE_CABIN
        {
            set
            {
                _mActiveCabin = value;
                CheckActiveCabin(_mActiveCabin);
            }
        }

        /// <summary>
        /// Contains the current mode as shown on the DMI (bit-inverted compared to the received mode)
        /// 
        /// Values:
        /// 0 = "Shown Mode Invalid"
        /// 1 = "No Mode displayed"
        /// 2..238 = "Not used"
        /// 239 = "NP - No Power"
        /// 240 = "PS - Passive Shunting"
        /// 241 = "RV - Reversing"
        /// 242 = "SN - National System"
        /// 243 = "LS - Limited Supervision"
        /// 244 = "NL - Non-leading"
        /// 245 = "IS - Isolation"
        /// 246 = "SF - System failure"
        /// 247 = "PT - Post trip"
        /// 248 = "TR - Trip"
        /// 249 = "SB - Standby"
        /// 250 = "SL - Sleeping"
        /// 251 = "UN - Unfitted"
        /// 252 = "SH - Shunting"
        /// 253 = "SR - Staff Responsible"
        /// 254 = "OS - On-sight"
        /// 255 = "FS - Full Supervision"
        /// 
        /// Note: The Read-Back values of the mode shall be bit-inverted compared to the sent mode.
        /// </summary>
        public static MMI_M_MODE_READBACK Check_MMI_M_MODE_READBACK
        {
            set
            {
                _mModeReadBack = value;
                CheckModeReadBack(_mModeReadBack);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static MMI_M_MMI_STATUS Check_MMI_M_MMI_STATUS
        {
            set
            {
                _mMmiStatus = value;
                CheckMmiStatus();
            }
        }

        /// <summary>
        /// Check_MMI_M_MODE_READBACK enum
        /// 
        /// Values:
        /// 0 = "Shown Mode Invalid"
        /// 1 = "No Mode displayed"
        /// 2..238 = "Not used"
        /// 239 = "NP - No Power"
        /// 240 = "PS - Passive Shunting"
        /// 241 = "RV - Reversing"
        /// 242 = "SN - National System"
        /// 243 = "LS - Limited Supervision"
        /// 244 = "NL - Non-leading"
        /// 245 = "IS - Isolation"
        /// 246 = "SF - System failure"
        /// 247 = "PT - Post trip"
        /// 248 = "TR - Trip"
        /// 249 = "SB - Standby"
        /// 250 = "SL - Sleeping"
        /// 251 = "UN - Unfitted"
        /// 252 = "SH - Shunting"
        /// 253 = "SR - Staff Responsible"
        /// 254 = "OS - On-sight"
        /// 255 = "FS - Full Supervision"
        /// 
        /// Note: The Read-Back values of the mode shall be bit-inverted compared to the sent mode.
        /// </summary>
        public enum MMI_M_MODE_READBACK : ushort
        {
            ShownModeInvalid = 0,
            NoModeDisplayed = 1,
            NoPower = 239,
            PassiveShunting = 240,
            Reversing = 241,
            NationalSystem = 242,
            LimitedSupervision = 243,
            NonLeading = 244,
            Isolation = 245,
            SystemFailure = 246,
            PostTrip = 247,
            Trip = 248,
            StandBy = 249,
            Sleeping = 250,
            Unfitted = 251,
            Shunting = 252,
            StaffResponsible = 253,
            OnSight = 254,
            FullSupervision = 255,
        }

        /// <summary>
        /// Defines the MMI status
        /// 
        /// Values:
        /// 0 = "Unknown"
        /// 1 = "Failure"
        /// 2 = "Idle"
        /// 3 = "Active"
        /// 4 = "Spare"
        /// 5 = "ATP Down, NACK"
        /// 6 = "ATP Down, ACK "
        /// 7..15 = "Spare"
        /// </summary>
        public enum MMI_M_MMI_STATUS : byte
        {
            StatusUnknown = 0,
            StatusFailure = 1,
            StatusIdle = 2,
            StatusActive = 3,
            StatusSpare = 4,
            StatusATPDownNACK = 5,
            StatusATPDownACK = 6
        }
    }
}