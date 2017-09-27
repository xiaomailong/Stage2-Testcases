﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using CL345;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.DMITestCases;
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

        static string baseString1 = "DMI->ETCS: EVC-121 [MMI_NEW_LEVEL]";
        static string baseString2 = "CCUO_ETCS1NewLevel_EVC121Subset";

        /// <summary>
        /// Initialise EVC-121 MMI_New_Level telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
            _pool.SITR.SMDCtrl.CCUO.ETCS1NewLevel.Value = 0x0009;
            _pool.SITR.SMDStat.CCUO.ETCS1NewLevel.Value = 0x00;
        }

        private static void CheckLevelSelected(MMI_M_LEVEL_NTC_ID mLevelNtcId)
        {
            
            // Get MMI_N_LEVELS info sent by EVC to the DMI via EVC-20
            _nLevels = _pool.SITR.ETCS1.SelectLevel.MmiNLevels.Value;

            // Reset telegram received flag in RTSim
            _pool.SITR.SMDStat.CCUO.ETCS1NewLevel.Value = 0x00;

            // Wait 10 seconds for SMDStat to become set
            if (_pool.WaitForCondition(_pool.SITR.SMDStat.CCUO.ETCS1NewLevel,Is.Equal, 0x01, 10000, 100))
            {
                // Check MMI_N_LEVELS from EVC-121 packet
                _checkResult = _pool.SITR.CCUO.ETCS1NewLevel.MmiNLevels.Value.Equals(_nLevels);

                // If check passes
                if (_checkResult)
                {
                    _pool.TraceReport($"{baseString1}.MMI_N_LEVELS = {_nLevels}, PASSED.");
                }
                // Else display the real value extracted
                else
                {
                    _pool.TraceError($"{baseString1}.MMI_N_LEVELS = {_pool.SITR.CCUO.ETCS1NewLevel.MmiNLevels.Value}, FAILED.");
                }

                for (int k = 0; k < _nLevels; k++)
                {
                    // For each level, read and stored MMI_M_LEVEL_NTC_ID and EVC121_alias_1 values from EVC-121 packet
                    if (_nLevels <= 10)
                    {
                        _evc121Alias1 = (byte)_pool.SITR.Client.Read($"{baseString2}0{k}_EVC121alias1");
                        _mLevelNtcId = (byte)_pool.SITR.Client.Read($"{baseString2}0{k}_MmiMLevelNtcID");                        
                    }
                    else
                    {
                        _evc121Alias1 = (byte)_pool.SITR.Client.Read($"{baseString2}{k}_EVC121alias1");
                        _mLevelNtcId = (byte)_pool.SITR.Client.Read($"{baseString2}{k}_MmiMLevelNtcID");
                    }

                    // Extract MMI_M_LEVEL_FLAG from EVC121_alias_1
                    _mLevelFlag = (byte)((_evc121Alias1 & 0x40) >> 6); // xxxx xxxx -> 0x0 0000 -> 0000 000x

                    // If MMI_M_LEVEL_NTC_ID matches with the selected level Id
                    if (_mLevelNtcId.Equals((byte)mLevelNtcId))
                    {
                        // Check that that level is indicated as SELECTED
                        _checkResult = _mLevelFlag.Equals((byte)MMI_M_LEVEL_FLAG.MarkedLevel);

                        // If check passes
                        if (_checkResult) 
                        {
                            _pool.TraceReport($"{baseString1} - MMI_M_LEVEL_NTC_ID[{k}] = {_mLevelNtcId}. {mLevelNtcId} is SELECTED." +
                                                Environment.NewLine +
                                              $"{baseString1} - MMI_M_LEVEL_FLAG[{k}] = " + Enum.GetName(typeof(MMI_M_LEVEL_FLAG), _mLevelFlag) + ", PASSED.");
                        }
                        // Else display the real value extracted
                        else
                        {
                            _pool.TraceError($"{baseString1} - MMI_M_LEVEL_NTC_ID[{k}] = {_mLevelNtcId}. {mLevelNtcId} is NOT indicated as SELECTED." +
                                                Environment.NewLine +
                                             $"{baseString1} - MMI_M_LEVEL_FLAG[{k}] = " + Enum.GetName(typeof(MMI_M_LEVEL_FLAG), _mLevelFlag) + ", FAILED.");
                        }
                    }
                    else
                    {
                        // Check that that level is indicated as NOT SELECTED
                        _checkResult = _mLevelFlag.Equals((byte)MMI_M_LEVEL_FLAG.NotMarkedLevel);
                        
                        // If check passes
                        if (_checkResult) 
                        {
                            _pool.TraceReport($"{baseString1} - MMI_M_LEVEL_NTC_ID[{k}] = {_mLevelNtcId} - " +
                                                Enum.GetName(typeof(MMI_M_LEVEL_NTC_ID), _mLevelNtcId) + " is NOT SELECTED." +
                                                Environment.NewLine +
                                              $"{baseString1} - MMI_M_LEVEL_FLAG[{k}] = " + Enum.GetName(typeof(MMI_M_LEVEL_FLAG), _mLevelFlag) + ", PASSED.");
                        }
                        // Else display the real value extracted
                        else
                        {
                            _pool.TraceError($"{baseString1} - MMI_M_LEVEL_NTC_ID[{k}] = {_mLevelNtcId}." +
                                                Enum.GetName(typeof(MMI_M_LEVEL_NTC_ID), _mLevelNtcId) + " is NOT indicated as NOT SELECTED." +
                                                Environment.NewLine +
                                             $"{baseString1} - MMI_M_LEVEL_FLAG[{k}] = " + Enum.GetName(typeof(MMI_M_LEVEL_FLAG), _mLevelFlag) + ", FAILED.");
                        }
                    }
                }
            }
            // EVC-121 could not be received
            else
            {
                DmiExpectedResults.DMItoEVC_Telegram_Not_Received(_pool, baseString1);
            }
        }

        /// <summary>
        /// Identity of level selected
        /// 
        /// Values:
        /// L0 = 0
        /// L1 = 1
        /// L2 = 2
        /// L3 = 3
        /// AWS_TPWS = 20
        /// CBTC = 50
        /// </summary>
        public static MMI_M_LEVEL_NTC_ID LevelSelected
        {
            set
            {
                _levelSelected = value;
                CheckLevelSelected(_levelSelected);
            }
        }
    }
}