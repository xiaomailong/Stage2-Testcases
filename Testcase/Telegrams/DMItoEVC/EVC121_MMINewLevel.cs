using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using CL345;
using Testcase.Telegrams.EVCtoDMI;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using static Testcase.Telegrams.EVCtoDMI.Variables;

namespace Testcase.Telegrams.DMItoEVC
{
    /// <summary>
    /// This packet shall be sent when the driver has selected an ETCS or NTC level or has changed the inhibit status of an installed level.
    /// </summary>
    public static class EVC121_MMINewLevel
    {
        private static SignalPool _pool;
        private static bool _checkResult;
        private static ushort _nLevels;
        private static byte _mLevelNtcId;
        private static byte _evc121Alias1;
        private static byte _mLevelFlag;
        private static MMI_M_LEVEL_NTC_ID _levelSelected;

        static string baseString = "DMI->ETCS: EVC-121 [MMI_NEW_LEVEL]";

        /// <summary>
        /// Initialise EVC-121 MMI_New_Level telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            _pool.SITR.SMDCtrl.CCUO.ETCS1NewLevel.Value = 0x09;
            _pool.SITR.SMDStat.CCUO.ETCS1NewLevel.Value = 0;
        }

        /*public static void GetPacketContent()
        {
            _nLevels = _pool.SITR.ETCS1.SelectLevel.MmiNLevels.Value;

            if (_pool.WaitForCondition(_pool.SITR.SMDStat.CCUO.ETCS1NewLevel, Is.Equal, 1, 10000, 20))
            {
                // Check MMI_N_LEVELS from EVC-121 packet
                _checkResult = _pool.SITR.CCUO.ETCS1NewLevel.MmiNLevels.Value.Equals(_nLevels);
                if (_checkResult) // if check passes
                {
                    _pool.TraceReport($"{baseString}.MMI_N_LEVELS = {_nLevels}, PASSED.");
                }
                else // else display the real value extracted
                {
                    _pool.TraceError($"{baseString}.MMI_N_LEVELS = {_pool.SITR.CCUO.ETCS1NewLevel.MmiNLevels.Value}, FAILED.");
                }
            }
            else
            {
                _pool.TraceError(baseString + " : is not received!!!");
            }

            _pool.SITR.SMDStat.CCUO.ETCS1NewLevel.Value = 0;

        }*/

        private static void CheckLevelSelected(MMI_M_LEVEL_NTC_ID mLevelNtcId)
        {
            //Get MMI_N_LEVELS info sent by EVC to the DMI via EVC-20
            _nLevels = _pool.SITR.ETCS1.SelectLevel.MmiNLevels.Value;

            // Wait 10 second that SMDStat is set to 1
            if (_pool.WaitForCondition(_pool.SITR.SMDStat.CCUO.ETCS1NewLevel,Is.Equal, 0x01, 10000, 20))
            {
                // Check MMI_N_LEVELS from EVC-121 packet
                _checkResult = _pool.SITR.CCUO.ETCS1NewLevel.MmiNLevels.Value.Equals(_nLevels);
                if (_checkResult) // if check passes
                {
                    _pool.TraceReport($"{baseString}.MMI_N_LEVELS = {_nLevels}, PASSED.");
                }
                else // else display the real value extracted
                {
                    _pool.TraceError($"{baseString}.MMI_N_LEVELS = {_pool.SITR.CCUO.ETCS1NewLevel.MmiNLevels.Value}, FAILED.");
                }

                //_nLevels = _pool.SITR.CCUO.ETCS1NewLevel.MmiNLevels.Value;

                for (int k = 0; k < _nLevels; k++)
                {
                    // For each level, read and store MMI_M_LEVEL_NTC_ID and EVC121_alias_1 values from EVC-121 packet
                    if (_nLevels <= 10)
                    {
                        _evc121Alias1 = (byte)_pool.SITR.Client.Read("CCUO_ETCS1NewLevel_EVC121Subset0" + k + "_EVC121alias1");
                        _mLevelNtcId = (byte)_pool.SITR.Client.Read("CCUO_ETCS1NewLevel_EVC121Subset0" + k + "_MmiMLevelNtcID");                        
                    }
                    else
                    {
                        _evc121Alias1 = (byte)_pool.SITR.Client.Read("CCUO_ETCS1NewLevel_EVC121Subset" + k + "_EVC121alias1");
                        _mLevelNtcId = (byte)_pool.SITR.Client.Read("CCUO_ETCS1NewLevel_EVC121Subset" + k + "_MmiMLevelNtcID");
                    }

                    // Extract MMI_M_LEVEL_FLAG from EVC121_alias_1
                    _mLevelFlag = (byte)((_evc121Alias1 & 0x40) >> 6); // xxxx xxxx -> 0x0 0000 -> 0000 000x

                    // If MMI_M_LEVEL_NTC_ID matches with the selected level Id, ...
                    if (_mLevelNtcId.Equals((byte)mLevelNtcId))
                    {
                        // Check that that level is indicated as SELECTED
                        _checkResult = _mLevelFlag.Equals((byte)MMI_M_LEVEL_FLAG.MarkedLevel);
                        if (_checkResult) // if check passes
                        {
                            _pool.TraceReport(baseString + ".MMI_M_LEVEL_NTC_ID[" + k + "]  = " + 
                                _mLevelNtcId + " - " + mLevelNtcId + " is SELECTED." + Environment.NewLine +
                                              baseString + ".MMI_M_LEVEL_FLAG[" + k + "]  = " + 
                                _mLevelFlag + " - " + Enum.GetName(typeof(MMI_M_LEVEL_FLAG), _mLevelFlag) + ", PASSED.");
                        }
                        else // else display the real value extracted
                        {
                            _pool.TraceError(baseString + ".MMI_M_LEVEL_NTC_ID[" + k + "]  = " +
                                _mLevelNtcId + " - " + mLevelNtcId + " is NOT indicated as SELECTED." + Environment.NewLine +
                                              baseString + ".MMI_M_LEVEL_FLAG[" + k + "]  = " +
                                _mLevelFlag + " - " + Enum.GetName(typeof(MMI_M_LEVEL_FLAG), _mLevelFlag) + ", FAILED.");
                        }
                    }
                    else
                    {
                        // Check that that level is indicated as NOT SELECTED
                        _checkResult = _mLevelFlag.Equals((byte)MMI_M_LEVEL_FLAG.NotMarkedLevel);
                        if (_checkResult) // if check passes
                        {
                            _pool.TraceReport(baseString + ".MMI_M_LEVEL_NTC_ID[" + k + "]  = " +
                                _mLevelNtcId + " - " + mLevelNtcId + " is NOT SELECTED." + Environment.NewLine +
                                              baseString + ".MMI_M_LEVEL_FLAG[" + k + "]  = " +
                                _mLevelFlag + " - " + Enum.GetName(typeof(MMI_M_LEVEL_FLAG), _mLevelFlag) + ", PASSED.");
                        }
                        else // else display the real value extracted
                        {
                            _pool.TraceError(baseString + ".MMI_M_LEVEL_NTC_ID[" + k + "]  = " +
                                _mLevelNtcId + " - " + mLevelNtcId + " is NOT indicated as NOT SELECTED." + Environment.NewLine +
                                              baseString + ".MMI_M_LEVEL_FLAG[" + k + "]  = " +
                                _mLevelFlag + " - " + Enum.GetName(typeof(MMI_M_LEVEL_FLAG), _mLevelFlag) + ", FAILED.");
                        }
                    }
                }
            }
            else // EVC-121 could not be received 
            {
                _pool.TraceError(baseString + " : is not received!!!");
            }

            // Reset SMDStat to 0.
            _pool.SITR.SMDStat.CCUO.ETCS1NewLevel.Value = 0;
            
        }

        /// <summary>
        /// Identity of level selected
        /// Value:
        /// L0 = 0,
        /// L1 = 1,
        /// L2 = 2,
        /// L3 = 3,
        /// CBTC = 50,
        /// AWS_TPWS = 20
        /// </summary>
        public static Variables.MMI_M_LEVEL_NTC_ID LevelSelected
        {
            set
            {
                _levelSelected = value;
                CheckLevelSelected(_levelSelected);
            }
        }
    }
}