using System;
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
    public static class EVC123_MMISpecificSTMDataToSTM
    {
        private static SignalPool _pool;

        private static EVC123_StmData _stmData;

        private static string baseString0 = "DMI->ETCS: EVC-123 [MMI_SPECIFIC_STR_DATA_TO_STM]";
        private static string baseString1 = "CCUO_ETCS1SpecificStmDataToStm_EVC123SpecificStmDataToStmSub10";

        /// <summary>
        /// Initialise EVC107 MMI_New_Train_Data telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            _pool.SITR.SMDCtrl.CCUO.ETCS1SpecificStmDataToStm.Value = 0x00;
            _pool.SITR.SMDStat.CCUO.ETCS1SpecificStmDataToStm.Value = 0x01;            
        }

        private static void CheckStmDataEntered()
        {
            // Reset telegram received flag in RTSim
            _pool.SITR.SMDStat.CCUO.ETCS1SpecificStmDataToStm.Value = 0x00;

            // Check if telegram received flag has been set. Allows 20 seconds to enter train data.
            if (_pool.SITR.SMDStat.CCUO.ETCS1SpecificStmDataToStm.WaitForCondition(Is.Equal, 1, 20000, 100))
            {
                // Check all static fields
                if (_pool.SITR.CCUO.ETCS1SpecificStmDataToStm.MmiNidNtc.Value.Equals(_stmData.nidNtc))
                {
                    _pool.TraceReport(baseString0 + Environment.NewLine +
                                      "MMI_NID_NTC = " + _stmData.nidNtc + Environment.NewLine +
                                      "Result = PASSED.");

                    if (_pool.SITR.CCUO.ETCS1SpecificStmDataToStm.MmiNIter.Value.Equals(_stmData.elements.Count))
                    {
                        _pool.TraceReport(baseString0 + Environment.NewLine +
                                          "MMI_NID_N_ITER = " + _stmData.elements.Count.ToString() +
                                          Environment.NewLine +
                                          "Result = PASSED.");

                        //string tagName1 = "_EVC123SpecificStmDataToStmSub10";

                        bool checkProperties = true;

                        for (int k = 0; k < _stmData.elements.Count; k++)
                        {
                            string tagName = $"{baseString1}{k}_";

                            string requestName = tagName + "MmiNidNtc";
                            byte nidNtc = (byte)_pool.SITR.Client.Read(requestName);

                            requestName = tagName + "MMiNidData";
                            byte nidData = (byte) _pool.SITR.Client.Read(requestName);

                            requestName = tagName + "MMiStmLValue";
                            ushort stmLValue = (ushort) _pool.SITR.Client.Read(requestName);

                            string tagSubName1 = tagName + "EVC123SpecificStmDataToStmSub10";
                            
                            StringBuilder stmcharArray = new StringBuilder();

                            for (int l = 0; l < stmLValue; l++)
                            {
                                requestName = $"{tagSubName1}{l}_MmiStmXValue";
                                stmcharArray.Append((char)_pool.SITR.Client.Read(requestName));
                            }

                            EVC123_StmDataElement stmElement = _stmData.elements[k];
                            checkProperties = ((nidNtc == stmElement.stmNidNtc) &&
                                               (nidData == stmElement.stmNidData) &&
                                               (stmcharArray.ToString() == stmElement.stmXValue));
                            if ( checkProperties == false )
                            {
                                // Print out the offending element's values
                                _pool.TraceReport(baseString0 + Environment.NewLine +
                                                  "Element {k} of data not matched:" + Environment.NewLine +
                                                  $"In packet NidNtc {nidNtc} - value supplied {stmElement.stmNidNtc}" +
                                                  Environment.NewLine +
                                                  $"          NidData {nidData} - value supplied {stmElement.stmNidData}" +
                                                  Environment.NewLine +
                                                  $"          StmXValue {stmcharArray.ToString()} - value supplied {stmElement.stmXValue}");
                                break;
                            }
                        }

                        if (checkProperties)
                        {
                            _pool.TraceReport(baseString0 + Environment.NewLine + "Result = PASSED.");

                        }
                    }
                    else
                    {
                        _pool.TraceError(baseString0 + Environment.NewLine +
                                         "MMI_NID_N_ITER = \"" + _pool.SITR.CCUO.ETCS1SpecificStmDataToStm.MmiNIter.Value +
                                         "\"" + Environment.NewLine +
                                         "Result: FAILED!");
                    }
                }
                // Else display the real values extracted from EVC-123
                else
                {
                    _pool.TraceError(baseString0 + Environment.NewLine +
                                     "MMI_NID_NTC = \"" + _pool.SITR.CCUO.ETCS1SpecificStmDataToStm.MmiNIter.Value +
                                     "\"" + Environment.NewLine +
                                     "Result: FAILED!");
                }
                
            }            
            
            // Show generic DMI -> EVC telegram failure
            else
            {
                DmiExpectedResults.DMItoEVC_Telegram_Not_Received(_pool, baseString0);
            }
            
        }

        /// <summary>
        /// Data for the packet to be tested against
        /// 
        /// </summary>
        public static EVC123_StmData StmData
        {
            set
            {
                _stmData = value;
                CheckStmDataEntered();
            }
        }

    }
}