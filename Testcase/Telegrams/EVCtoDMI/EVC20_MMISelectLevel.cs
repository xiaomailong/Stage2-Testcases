#region usings

using System;
using System.Collections.Generic;
using CL345;
using static Testcase.Telegrams.EVCtoDMI.Variables;

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
        private static SignalPool _pool;
        private static MMI_Q_LEVEL_NTC_ID[] _qLevelNtcId;
        private static MMI_M_CURRENT_LEVEL[] _mCurrentLevel;
        private static MMI_M_LEVEL_FLAG[] _mLevelFlag;
        private static MMI_M_INHIBITED_LEVEL[] _mInhibitedLevel;
        private static MMI_M_INHIBIT_ENABLE[] _mInhibitEnable;
        private static MMI_M_LEVEL_NTC_ID[] _mLevelNtcId;
        private static ushort _nLevels;

        /// <summary>
        /// Initialise an instance of EVC-20 MMI Select Level telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // Set as dynamic
            _pool.SITR.SMDCtrl.ETCS1.SelectLevel.Value = 0x8;

            // Set default values
            _pool.SITR.ETCS1.SelectLevel.MmiMPacket.Value = 20; // Packet ID
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

            for (var k = 0; k < _nLevels; k++)
            {
                // xxxx xxxx => x000 0000
                uint uintMmiQLevelNtcId = Convert.ToUInt32(_qLevelNtcId[k]);
                uintMmiQLevelNtcId <<= 7;
                
                // xxxx xxxx => 0x00 0000
                uint uintMmiMCurrentLevel = Convert.ToUInt32(_mCurrentLevel[k]);
                uintMmiMCurrentLevel <<= 6;
                
                // xxxx xxxx => 00x0 0000
                uint uintMmiMLevelFlag = Convert.ToUInt32(_mLevelFlag[k]);
                uintMmiMLevelFlag <<= 5;
                
                // xxxx xxxx => 000x 0000
                uint uintMmiMInhibitedLevel = Convert.ToUInt32(_mInhibitedLevel[k]);
                uintMmiMInhibitedLevel <<= 4;
                
                // xxxx xxxx => 0000 x000
                uint uintMmiMInhibitEnable = Convert.ToUInt32(_mInhibitEnable[k]);
                uintMmiMInhibitEnable <<= 3;

                // Build EVC20_alias_1 
                byte evc20Alias1 = Convert.ToByte(uintMmiQLevelNtcId | uintMmiMCurrentLevel |
                                                  uintMmiMLevelFlag | uintMmiMInhibitedLevel |
                                                  uintMmiMInhibitEnable);

                // Populate telegram with dynamic fields
                if (k < 10)
                {
                    _pool.SITR.Client.Write("ETCS1_SelectLevel_EVC20SelectLevelSub0" + k + "_EVC20alias1", evc20Alias1);
                    _pool.SITR.Client.Write("ETCS1_SelectLevel_EVC20SelectLevelSub0" + k + "_MmiMLevelNtcId",
                        Convert.ToByte(_mLevelNtcId[k]));
                }
                else
                {
                    _pool.SITR.Client.Write("ETCS1_SelectLevel_EVC20SelectLevelSub" + k + "_EVC20alias1", evc20Alias1);
                    _pool.SITR.Client.Write("ETCS1_SelectLevel_EVC20SelectLevelSub" + k + "_MmiMLevelNtcId",
                        Convert.ToByte(_mLevelNtcId[k]));
                }
            }
        }

        /// <summary>
        /// Send EVC-20 telegram to the DMI.
        /// </summary>
        public static void Send()
        {
            SetLevelInfoK();
            _pool.SITR.ETCS1.SelectLevel.MmiLPacket.Value = (ushort) (56 + (_nLevels * 16));
            _pool.SITR.SMDCtrl.ETCS1.SelectLevel.Value = 0x09;
        }

        /// <summary>
        /// Qualifier for the variable MMI_M_LEVEL_NTC_ID
        /// 
        /// Values:
        /// 0 = "MMI_M_LEVEL_NTC_ID contains an STM ID (0-255)"
        /// 1 = "MMI_M_LEVEL_NTC_ID contains a level number (0-3)"
        /// </summary>
        public static MMI_Q_LEVEL_NTC_ID[] MMI_Q_LEVEL_NTC_ID
        {
            set => _qLevelNtcId = value;
        }

        /// <summary>
        /// Indicates if MMI_M_LEVEL_STM_ID is the latest used level
        /// 
        /// Values:
        /// 0 = "MMI_M_LEVEL_STM_ID is not the latest used level"
        /// 1 = "MMI_M_LEVEL_STM_ID is the latest used level"
        /// </summary>
        public static MMI_M_CURRENT_LEVEL[] MMI_M_CURRENT_LEVEL
        {
            set => _mCurrentLevel = value;
        }

        /// <summary>
        /// Indicates if MMI_M_LEVEL_NTC_ID is marked or not.
        /// The interpretation of the mark needs to be defined by related requirements. 
        /// Basic idea is that 'marked' levels are allowed for edit by the driver
        /// (see ERA_ERTMS_15560, v.3.4.9, ch. 11.3.2.7, 11.3..2.8)
        /// 
        /// Values:
        /// 0 = "MMI_M_LEVEL_NTC_ID is 'not marked'"
        /// 1 = "MMI_M_LEVEL_NTC_ID is 'marked'"
        /// </summary>
        public static MMI_M_LEVEL_FLAG[] MMI_M_LEVEL_FLAG
        {
            set => _mLevelFlag = value;
        }

        /// <summary>
        /// Indicates if MMI_M_LEVEL_NTC_ID is currently inhibited by driver or not.
        /// 
        /// Values:
        /// 0 = "MMI_M_LEVEL_NTC_ID is not inhibited"
        /// 1 = "MMI_M_LEVEL_NTC_ID is inhibited"
        /// </summary>
        public static MMI_M_INHIBITED_LEVEL[] MMI_M_INHIBITED_LEVEL
        {
            set => _mInhibitedLevel = value;
        }

        /// <summary>
        /// Indicates if MMI_M_LEVEL_NTC_ID is allowed (configurable) for inhibiting or not.
        /// 
        /// Values:
        /// 0 = "MMI_M_LEVEL_NTC_ID is not allowed for inhibiting"
        /// 1 = "MMI_M_LEVEL_NTC_ID is allowed for inhibiting"
        /// </summary>
        public static MMI_M_INHIBIT_ENABLE[] MMI_M_INHIBIT_ENABLE
        {
            set => _mInhibitEnable = value;
        }

        /// <summary>
        /// Identity of level or NTC
        /// 
        /// Note: If MMI_Q_LEVEL_NTC_ID is 0 the value means NTC Identity 0-255
        /// If MMI_Q_LEVEL_NTC_ID is 1 the value means Level 0-3
        /// </summary>
        public static MMI_M_LEVEL_NTC_ID[] MMI_M_LEVEL_NTC_ID
        {
            set => _mLevelNtcId = value;          
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
        public static MMI_Q_CLOSE_ENABLE MMI_Q_CLOSE_ENABLE
        {
            set => _pool.SITR.ETCS1.SelectLevel.MmiQCloseEnable.Value = (byte) value;               
        }
    }

}

