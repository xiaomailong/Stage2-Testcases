using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using CL345;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.Telegrams.DMItoEVC
{
    /// <summary>
    /// This packet shall be sent when the driver has selected an ETCS or NTC level or has changed the inhibit status of an installed level.
    /// </summary>
    static class EVC121_MMINewLevel
    {
        private static SignalPool _pool;
        private static bool _bResult;
        private static Variables.MMI_Q_LEVEL_NTC_ID _qLevelNtcId;
        private static Variables.MMI_M_LEVEL_FLAG _mLevelFlag;
        private static Variables.MMI_M_INHIBITED_LEVEL _mInhibitedLevel;
        private static Variables.MMI_M_INHIBIT_ENABLE _mInhibitEnable;
        private static Variables.MMI_M_LEVEL_NTC_ID _mLevelNtcId;

        /// <summary>
        /// Initialise EVC-111 MMI_Driver_Message_Ack telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;
        }

        /// <summary>
        /// This function is meant to be used privatly within the EVC-121 class.
        /// For now, this method shall only be called with MMI_N_LEVELS lesser or equal 1.
        /// In order not to duplicate the same function for the different variables contained
        /// into EVC121_alias_1, this method has been scripted so that it automatically detects 
        /// which variable needs to be extracted then checked.
        /// </summary>
        /// <param name="nLevels"></param>
        /// <param name="varToCheck"></param>
        private static void CheckEVC121_alias_1(ushort nLevels, object varToCheck)
        {
            bool _matched = false;

            // Convert byte EVC121_alias_1 into an array of bits.
            BitArray _evc121Alias1 = new BitArray(new[] {
                (byte) _pool.SITR.Client.Read("CCUO_ETCS1NewLevel_EVC121Subset" + (nLevels - 1) +
                "_EVC121alias1") });

            // List of the 4 variables contained into EVC121_alias_1 respecting to their position
            // (spare bits are expelled)
            List<Type> _evc121Alias1types = new List<Type> {typeof(Variables.MMI_Q_LEVEL_NTC_ID),
                                                            typeof(Variables.MMI_M_LEVEL_FLAG),
                                                            typeof(Variables.MMI_M_INHIBITED_LEVEL),
                                                            typeof(Variables.MMI_M_INHIBIT_ENABLE)};            

            // for each type from EVC121_alias_1
            foreach (Type _varType in _evc121Alias1types)
            {
                // find out which type is matching with varToCheck's type
                if (_varType.Equals(varToCheck.GetType()))
                {
                    // the matching type's index will give the corresponding bit within EVC121_alias_1 
                    int _pos = _evc121Alias1types.IndexOf(_varType);
                    byte _value = Convert.ToByte(_evc121Alias1[_pos]);

                    // do the check
                    _bResult = _value.Equals(varToCheck);

                    if (_bResult) // if check passes
                    {
                        _pool.TraceReport("DMI->ETCS: EVC-121 [MMI_NEW_LEVEL." + varToCheck.GetType().ToString() + "] = " +
                            varToCheck + " - \"" + Enum.GetName(varToCheck.GetType(), varToCheck) + "\" PASSED.");
                    }
                    else // else display the real value extracted
                    {
                        _pool.TraceError("DMI->ETCS: EVC-121 [MMI_NEW_LEVEL." + varToCheck.GetType().ToString() + "] = " +
                            _value + " - \"" + Enum.GetName(varToCheck.GetType(), _value) + "\" FAILED.");
                    }

                    // if the varToCheck type matches one of the variable types from EVC121_Alias_1
                    _matched = true;
                    break; // exit foreach loop 
                } 
            }

            if (!_matched) // if none variable type matched    
            {
                _pool.TraceError("Variable Type " + varToCheck.GetType().ToString() + " not found!!");
            }          
        }

        private static void CheckMLevelNtcId(ushort nLevels, Variables.MMI_M_LEVEL_NTC_ID mLevelNtcId)
        {

        }

        /// <summary>
        /// Qualifier for the variable MMI_M_LEVEL_NTC_ID
        /// Values:
        /// Values:
        /// 0 = "MMI_M_LEVEL_NTC_ID contains an STM ID (0-255)"
        /// 1 = "MMI_M_LEVEL_NTC_ID contains a level number (0-3)"
        /// </summary>
        public static Variables.MMI_Q_LEVEL_NTC_ID Check_MMI_Q_LEVEL_NTC_ID
        {
            set
            {
                _qLevelNtcId = value;
                CheckEVC121_alias_1(1, _qLevelNtcId);
            }
        }

        /// <summary>
        /// Indicates if MMI_M_LEVEL_NTC_ID is marked or not. 
        /// The interpretation of the mark needs to be defined by related requirements.
        /// Basic idea is that 'marked' levels are allowed for edit by the driver
        /// (see ERA_ERTMS_15560, v.3.4.9, ch. 11.3.2.7, 11.3..2.8)
        /// Values:
        /// 0 = "MMI_M_LEVEL_NTC_ID is 'not marked'"
        /// 1 = "MMI_M_LEVEL_NTC_ID is 'marked'"
        /// </summary>
        public static Variables.MMI_M_LEVEL_FLAG Check_MMI_M_LEVEL_FLAG
        {
            set
            {
                _mLevelFlag = value;
                CheckEVC121_alias_1(1, _mLevelFlag);
            }
        }

        /// <summary>
        /// Indicates if MMI_M_LEVEL_NTC_ID is currently inhibited by driver or not 
        /// Values:
        /// 0 = "MMI_M_LEVEL_NTC_ID is not inhibited"
        /// 1 = "MMI_M_LEVEL_NTC_ID is inhibited"
        /// </summary>
        public static Variables.MMI_M_INHIBITED_LEVEL Check_MMI_M_INHIBITED_LEVEL
        {
            set
            {
                _mInhibitedLevel = value;
                CheckEVC121_alias_1(1, _mInhibitedLevel);
            }
        }

        /// <summary>
        /// Indicates if MMI_M_LEVEL_NTC_ID is allowed (configurable) for inhibiting or not
        /// Values:
        /// 0 = "MMI_M_LEVEL_NTC_ID is not allowed for inhibiting"
        /// 1 = "MMI_M_LEVEL_NTC_ID is allowed for inhibiting"
        /// </summary>
        public static Variables.MMI_M_INHIBIT_ENABLE Check_MMI_M_INHIBIT_ENABLE
        {
            set
            {
                _mInhibitEnable = value;
                CheckEVC121_alias_1(1, _mInhibitEnable);
            }
        }

        public static Variables.MMI_M_LEVEL_NTC_ID Check_MMI_M_LEVEL_NTC_ID
        {
            set
            {
                _mLevelNtcId = value;
                CheckMLevelNtcId(1, _mLevelNtcId);
            }
        }
    }
}