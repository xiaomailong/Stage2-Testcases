#region usings

using System;
using CL345;

#endregion

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This packet shall be sent when the ETC requests the driver to select level.
    /// The packet contains a list of ETCS and NTC levels and related additional status information.
    /// Possible use cases are:
    ///     display of 'default level list',
    ///     display of 'trackside supported level list',
    ///     display of 'inhibit level list'.
    /// </summary>
    public static class EVC20_MMISelectLevel
    {
        private static TestcaseBase _pool;
        private static Variables.MMI_Q_LEVEL_NTC_ID[] _qLevelNtcId;
        private static Variables.MMI_M_CURRENT_LEVEL[] _mCurrentLevel;
        private static Variables.MMI_M_LEVEL_FLAG[] _mLevelFlag;
        private static Variables.MMI_M_INHIBITED_LEVEL[] _mInhibitedLevel;
        private static Variables.MMI_M_INHIBIT_ENABLE[] _mInhibitEnable;
        private static Variables.MMI_M_LEVEL_NTC_ID[] _mLevelNtcId;
        private static ushort _nLevels;
        private const string BaseString = "ETCS1_SelectLevel_EVC20SelectLevelSub";

        /// <summary>
        /// Initialise EVC-20 MMI Select Level telegram.
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        public static void Initialise(TestcaseBase pool)
        {
            _pool = pool;

            // Set as dynamic
            _pool.SITR.SMDCtrl.ETCS1.SelectLevel.Value = 0x0008;

            // Set default values
            _pool.SITR.ETCS1.SelectLevel.MmiMPacket.Value = 20;
        }

        private static void SetLevelInfoK()
        {
            // Check that every array has the same length
            if (!(Equals(_qLevelNtcId.Length, _mCurrentLevel.Length) &&
                  Equals(_mCurrentLevel.Length, _mLevelFlag.Length) &&
                  Equals(_mLevelFlag.Length, _mInhibitedLevel.Length) &&
                  Equals(_mInhibitedLevel.Length, _mInhibitEnable.Length) &&
                  Equals(_mInhibitEnable.Length, _mLevelNtcId.Length) &&
                  Equals(_mLevelNtcId.Length, _mCurrentLevel.Length)))
            {
                throw new Exception("Array lengths do not match!");
            }

            // Determine how many levels will be sent
            _nLevels = (ushort) _qLevelNtcId.Length;

            // Populate telegram with MMI_N_LEVELS
            _pool.SITR.ETCS1.SelectLevel.MmiNLevels.Value = _nLevels;

            for (int k = 0; k < _nLevels; k++)
            {
                // xxxx xxxx => x000 0000
                uint uintMmiQLevelNtcId = (uint) _qLevelNtcId[k];
                uintMmiQLevelNtcId <<= 7;

                // xxxx xxxx => 0x00 0000
                uint uintMmiMCurrentLevel = (uint) _mCurrentLevel[k];
                uintMmiMCurrentLevel <<= 6;

                // xxxx xxxx => 00x0 0000
                uint uintMmiMLevelFlag = (uint) _mLevelFlag[k];
                uintMmiMLevelFlag <<= 5;

                // xxxx xxxx => 000x 0000
                uint uintMmiMInhibitedLevel = (uint) _mInhibitedLevel[k];
                uintMmiMInhibitedLevel <<= 4;

                // xxxx xxxx => 0000 x000
                uint uintMmiMInhibitEnable = (uint) _mInhibitEnable[k];
                uintMmiMInhibitEnable <<= 3;

                // Build EVC20_alias_1 
                byte evc20Alias1 = (byte) (uintMmiQLevelNtcId |
                                           uintMmiMCurrentLevel |
                                           uintMmiMLevelFlag |
                                           uintMmiMInhibitedLevel |
                                           uintMmiMInhibitEnable);

                // Populate telegram with dynamic fields
                if (k < 10)
                {
                    _pool.SITR.Client.Write(string.Format("{0}0{1}_EVC20alias1", BaseString, k), evc20Alias1);
                    _pool.SITR.Client.Write(string.Format("{0}0{1}_MmiMLevelNtcId", BaseString, k),
                        (byte) _mLevelNtcId[k]);
                }
                else
                {
                    _pool.SITR.Client.Write(string.Format("{0}{1}_EVC20alias1", BaseString, k), evc20Alias1);
                    _pool.SITR.Client.Write(string.Format("{0}{1}_MmiMLevelNtcId", BaseString, k),
                        (byte) _mLevelNtcId[k]);
                }
            }
        }

        /// <summary>
        /// Send EVC-20 MMI Select Level telegram.
        /// </summary>
        public static void Send()
        {
            SetLevelInfoK();
            _pool.SITR.ETCS1.SelectLevel.MmiLPacket.Value = (ushort) (56 + (_nLevels * 16));
            _pool.SITR.SMDCtrl.ETCS1.SelectLevel.Value = 0x000B;
            _pool.WaitForAck(_pool.SITR.SMDStat.ETCS1.SelectLevel);
        }

        /// <summary>
        /// Send EVC-20 MMI Select Level telegram.
        /// </summary>
        public static void SendClose()
        {
            _pool.SITR.ETCS1.SelectLevel.MmiNLevels.Value = 0;
            _pool.SITR.ETCS1.SelectLevel.MmiLPacket.Value = 56;
            _pool.SITR.SMDCtrl.ETCS1.SelectLevel.Value = 0x0003;
            _pool.WaitForAck(_pool.SITR.SMDStat.ETCS1.SelectLevel);
        }

        /// <summary>
        /// Qualifier for the variable MMI_M_LEVEL_NTC_ID
        /// 
        /// Values:
        /// 0 = "MMI_M_LEVEL_NTC_ID contains an STM ID (0-255)"
        /// 1 = "MMI_M_LEVEL_NTC_ID contains a level number (0-3)"
        /// </summary>
        public static Variables.MMI_Q_LEVEL_NTC_ID[] MMI_Q_LEVEL_NTC_ID
        {
            set { _qLevelNtcId = value; }
        }

        /// <summary>
        /// Indicates if MMI_M_LEVEL_STM_ID is the latest used level
        /// 
        /// Values:
        /// 0 = "MMI_M_LEVEL_STM_ID is not the latest used level"
        /// 1 = "MMI_M_LEVEL_STM_ID is the latest used level"
        /// </summary>
        public static Variables.MMI_M_CURRENT_LEVEL[] MMI_M_CURRENT_LEVEL
        {
            set { _mCurrentLevel = value; }
        }

        /// <summary>
        /// Indicates if MMI_M_LEVEL_NTC_ID is marked or not.
        /// For Crossrail purposes:
        /// 'Marked' = visible
        /// 'Not Marked' = invisible
        /// The interpretation of the mark needs to be defined by related requirements. 
        /// Basic idea is that 'marked' levels are allowed for edit by the driver
        /// (see ERA_ERTMS_15560, v.3.4.9, ch. 11.3.2.7, 11.3..2.8)
        /// 
        /// Values:
        /// 0 = "MMI_M_LEVEL_NTC_ID is 'not marked'"
        /// 1 = "MMI_M_LEVEL_NTC_ID is 'marked'"
        /// </summary>
        public static Variables.MMI_M_LEVEL_FLAG[] MMI_M_LEVEL_FLAG
        {
            set { _mLevelFlag = value; }
        }

        /// <summary>
        /// Indicates if MMI_M_LEVEL_NTC_ID is currently inhibited by driver or not.
        /// 
        /// Values:
        /// 0 = "MMI_M_LEVEL_NTC_ID is not inhibited"
        /// 1 = "MMI_M_LEVEL_NTC_ID is inhibited"
        /// </summary>
        public static Variables.MMI_M_INHIBITED_LEVEL[] MMI_M_INHIBITED_LEVEL
        {
            set { _mInhibitedLevel = value; }
        }

        /// <summary>
        /// Indicates if MMI_M_LEVEL_NTC_ID is allowed (configurable) for inhibiting or not.
        /// 
        /// Values:
        /// 0 = "MMI_M_LEVEL_NTC_ID is not allowed for inhibiting"
        /// 1 = "MMI_M_LEVEL_NTC_ID is allowed for inhibiting"
        /// </summary>
        public static Variables.MMI_M_INHIBIT_ENABLE[] MMI_M_INHIBIT_ENABLE
        {
            set { _mInhibitEnable = value; }
        }

        /// <summary>
        /// Identity of level or NTC
        /// 
        /// Note: If MMI_Q_LEVEL_NTC_ID is 0 the value means NTC Identity 0-255
        /// If MMI_Q_LEVEL_NTC_ID is 1 the value means Level 0-3
        /// </summary>
        public static Variables.MMI_M_LEVEL_NTC_ID[] MMI_M_LEVEL_NTC_ID
        {
            set { _mLevelNtcId = value; }
        }

        /// <summary>
        /// Enabling close button in EVC-14, EVC-20 and EVC-22.
        /// 
        /// Bits:
        /// 0 = "disable/enable close button"
        /// 1..7 = "Spare"
        ///
        /// Note: Bit0 = 0 -> disable close button, Bit0 = 1 -> enable close button
        /// </summary>
        public static Variables.MMI_Q_CLOSE_ENABLE MMI_Q_CLOSE_ENABLE
        {
            set { _pool.SITR.ETCS1.SelectLevel.MmiQCloseEnable.Value = (byte) value; }
        }
    }
}