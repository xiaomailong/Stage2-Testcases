using System;
using CL345;
using Testcase.DMITestCases;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.Telegrams.DMItoEVC
{
    /// <summary>
    /// This packet shall be sent when the driver acts during the Train Data Entry procedure. It covers the following use cases:
    /// 1. Driver accepts a data element by pressing 'Enter'.
    /// 2. Driver accepts a data element by pressing 'Enter_Delay_Type'.
    /// 3. Driver completes entering a data block by pressing 'Yes'.
    /// 4. Driver overrules an operational check rule by pressing 'Delay Type Yes'.
    /// </summary>
    public static class EVC107_MMINewTrainData
    {
        private static TestcaseBase _pool;
        private static bool _checkResult;
        private static Variables.Fixed_Trainset_Captions _trainsetSelected;
        private static ushort _nDataElements;
        private static Variables.MMI_M_BUTTONS_TRAIN_DATA _mButtons;
        private static byte _nidData;

        private const string BaseString0 = "DMI->ETCS: EVC-107 [MMI_NEW_TRAIN_DATA]";
        private const string BaseString1 = "CCUO_ETCS1NewTrainData_EVC107NewTrainDataSub";

        /// <summary>
        /// Initialise EVC107 MMI New Train Data telegram.
        /// </summary>
        /// <param name="pool">The SignalPool</param>
        public static void Initialise(TestcaseBase pool)
        {
            _pool = pool;
            _pool.SITR.SMDCtrl.CCUO.ETCS1NewTrainData.Value = 0x0009;
            _pool.SITR.SMDStat.CCUO.ETCS1NewTrainData.Value = 0x00;
        }

        private static void CheckFixedTrainDataEntered(Variables.Fixed_Trainset_Captions mmiMTrainsetId)
        {
            // Check if telegram received flag has been set. Allows 20 seconds to enter train data.
            if (_pool.SITR.SMDStat.CCUO.ETCS1NewTrainData.WaitForCondition(Is.Equal, 1, 20000, 100))
            {
                // Check all static fields
                _checkResult = _pool.SITR.CCUO.ETCS1NewTrainData.MmiLTrain.Value.Equals(0) &
                               _pool.SITR.CCUO.ETCS1NewTrainData.MmiVMaxtrain.Value.Equals(0) &
                               _pool.SITR.CCUO.ETCS1NewTrainData.MmiNidKeyTrainCat.Value.Equals(
                                   (byte) Variables.MMI_NID_KEY.NoDedicatedKey) &
                               _pool.SITR.CCUO.ETCS1NewTrainData.MmiMBrakePerc.Value.Equals(0) &
                               _pool.SITR.CCUO.ETCS1NewTrainData.MmiNidKeyAxleLoad.Value.Equals(
                                   (byte) Variables.MMI_NID_KEY.NoDedicatedKey) &
                               _pool.SITR.CCUO.ETCS1NewTrainData.MmiMAirtight.Value.Equals(0) &
                               _pool.SITR.CCUO.ETCS1NewTrainData.MmiNidKeyLoadGauge.Value.Equals(
                                   (byte) Variables.MMI_NID_KEY.NoDedicatedKey) &
                               _pool.SITR.CCUO.ETCS1NewTrainData.EVC107alias1.Value.Equals(
                                   (byte) ((byte) mmiMTrainsetId << 4)) &
                               _pool.SITR.CCUO.ETCS1NewTrainData.MmiMButtons.Value.Equals((byte) _mButtons);

                // If check passes
                if (_checkResult)
                {
                    _pool.TraceReport(BaseString0 + Environment.NewLine +
                                      "MMI_L_TRAIN = 0" + Environment.NewLine +
                                      "MMI_V_MAXTRAIN = 0" + Environment.NewLine +
                                      "MMI_NID_KEY_TRAIN_CAT = \"" + Variables.MMI_NID_KEY.NoDedicatedKey + "\"" +
                                      Environment.NewLine +
                                      "MMI_M_BRAKE_PERC = 0" + Environment.NewLine +
                                      "MMI_NID_KEY_AXLE_LOAD = \"" + Variables.MMI_NID_KEY.NoDedicatedKey + "\"" +
                                      Environment.NewLine +
                                      "MMI_M_AIRTIGHT = 0" + Environment.NewLine +
                                      "MMI_NID_KEY_LOAD_GAUGE = \"" + Variables.MMI_NID_KEY.NoDedicatedKey + "\"" +
                                      Environment.NewLine +
                                      "MMI_M_TRAINSET_ID = " + mmiMTrainsetId + Environment.NewLine +
                                      "MMI_M_ALT_DEM = 0" + Environment.NewLine +
                                      "MMI_M_BUTTONS = \"" + _mButtons + "\"" + Environment.NewLine +
                                      "Result = PASSED.");
                }
                // Else display the real value extracted from EVC-107
                else
                {
                    _pool.TraceError(BaseString0 + Environment.NewLine +
                                     "MMI_L_TRAIN = \"" + _pool.SITR.CCUO.ETCS1NewTrainData.MmiLTrain.Value + "\"" +
                                     Environment.NewLine +
                                     "MMI_V_MAXTRAIN = \"" + _pool.SITR.CCUO.ETCS1NewTrainData.MmiLTrain.Value + "\"" +
                                     Environment.NewLine +
                                     "MMI_NID_KEY_TRAIN_CAT = \"" +
                                     Enum.GetName(typeof(Variables.MMI_NID_KEY),
                                         _pool.SITR.CCUO.ETCS1NewTrainData.MmiNidKeyTrainCat.Value) + "\"" +
                                     Environment.NewLine +
                                     "MMI_M_BRAKE_PERC = \"" + _pool.SITR.CCUO.ETCS1NewTrainData.MmiLTrain.Value +
                                     "\"" + Environment.NewLine +
                                     "MMI_NID_KEY_AXLE_LOAD = \"" +
                                     Enum.GetName(typeof(Variables.MMI_NID_KEY),
                                         _pool.SITR.CCUO.ETCS1NewTrainData.MmiNidKeyAxleLoad.Value) + "\"" +
                                     Environment.NewLine +
                                     "MMI_M_AIRTIGHT = \"" + _pool.SITR.CCUO.ETCS1NewTrainData.MmiLTrain.Value + "\"" +
                                     Environment.NewLine +
                                     "MMI_NID_KEY_LOAD_GAUGE = \"" +
                                     Enum.GetName(typeof(Variables.MMI_NID_KEY),
                                         _pool.SITR.CCUO.ETCS1NewTrainData.MmiNidKeyLoadGauge.Value) + "\"" +
                                     Environment.NewLine +
                                     "MMI_M_TRAINSET_ID = \"" +
                                     Enum.GetName(typeof(Variables.Fixed_Trainset_Captions),
                                         (_pool.SITR.CCUO.ETCS1NewTrainData.EVC107alias1.Value & 0xF0) >> 4) + "\"" +
                                     Environment.NewLine +
                                     "MMI_M_ALT_DEM = \"" +
                                     ((_pool.SITR.CCUO.ETCS1NewTrainData.EVC107alias1.Value & 0x0C) >> 2) + "\"" +
                                     Environment.NewLine +
                                     "MMI_M_BUTTONS = \"" +
                                     Enum.GetName(typeof(Variables.MMI_M_BUTTONS),
                                         _pool.SITR.CCUO.ETCS1NewTrainData.MmiMButtons.Value) + "\"" +
                                     Environment.NewLine +
                                     "Result: FAILED!");
                }

                // Get number of data elements                
                _nDataElements = _pool.SITR.CCUO.ETCS1NewTrainData.MmiNDataElements.Value;

                // MMI_gen 9460: "In case of [Enter] | [Enter_Delay_Type] the [MMI_NEW_TRAIN_DATA (EVC-107)].MMI_N_DATA_ELEMENTS
                // shall be set to '1', as the driver is only allowed to accept one data at a time."
                if (_pool.SITR.CCUO.ETCS1NewTrainData.MmiMButtons.Value.Equals(
                    (byte) Variables.MMI_M_BUTTONS_TRAIN_DATA.BTN_ENTER))
                {
                    if (_nDataElements == 1)
                    {
                        // Get MMI_NID_DATA
                        _nidData = (byte) _pool.SITR.Client.Read(string.Format("{0}0_MmiNidData", BaseString1));

                        // For Trainset selection, MMI_NID_DATA should be equal to 6 (= Train Type)
                        _checkResult = _nidData.Equals(6);

                        // If check passes
                        if (_checkResult)
                        {
                            _pool.TraceReport("MMI_N_DATA_ELEMENTS = 1" + Environment.NewLine +
                                              "MMI_NID_DATA[0] = 6" + Environment.NewLine +
                                              "Result = PASSED.");
                        }
                        // Else display the real value extracted
                        else
                        {
                            _pool.TraceError("MMI_N_DATA_ELEMENTS = 1" + Environment.NewLine +
                                             string.Format("MMI_NID_DATA[0] = {0}", _nidData) + Environment.NewLine +
                                             "Result = FAILED!");
                        }
                    }
                    else
                    {
                        // Display the real value extracted
                        _pool.TraceError(string.Format("MMI_N_DATA_ELEMENTS = {0}", _nDataElements));

                        for (int k = 0; k < _nDataElements; k++)
                        {
                            _nidData = (byte) _pool.SITR.Client.Read(string.Format("{0}{1}_MmiNidData", BaseString1, k));

                            _pool.TraceError(string.Format("MMI_NID_DATA[{0}] = {1}", k, _nidData) + Environment.NewLine +
                                             "Result = FAILED!");
                        }
                    }
                }

                // MMI_gen 9460 "In case of [Yes] | [Yes_Delay_Type] the [MMI_NEW_TRAIN_DATA (EVC-107)].MMI_N_DATA_ELEMENTS
                // shall be set to '0', as the [MMI_NEW_TRAIN_DATA (EVC-107)].MMI_M_BUTTONS clearly indicates that all data are affected. 
                else if (_pool.SITR.CCUO.ETCS1NewTrainData.MmiMButtons.Value.Equals(
                    (byte) Variables.MMI_M_BUTTONS_TRAIN_DATA.BTN_YES_DATA_ENTRY_COMPLETE))
                {
                    // If check passes
                    if (_nDataElements == 0)
                    {
                        _pool.TraceReport("MMI_N_DATA_ELEMENTS = 0" + Environment.NewLine +
                                          "Result = PASSED.");
                    }
                    // Else display the real value extracted
                    else
                    {
                        _pool.TraceError(string.Format("MMI_N_DATA_ELEMENTS = {0}", _nDataElements));

                        for (int k = 0; k < _nDataElements; k++)
                        {
                            _nidData = (byte) _pool.SITR.Client.Read(
                                string.Format("{0}0{1}_MmiNidData", BaseString1, k));

                            _pool.TraceError(string.Format("MMI_NID_DATA[{0}] = {1}", k, _nidData) +
                                             Environment.NewLine +
                                             "Result = FAILED!");
                        }
                    }
                }
                else
                {
                    _pool.TraceError("Unexpected Result!" + Environment.NewLine +
                                     string.Format("MMI_N_DATA_ELEMENTS = {0}", _nDataElements));

                    for (int k = 0; k < _nDataElements; k++)
                    {
                        _nidData = (byte) _pool.SITR.Client.Read(string.Format("{0}{1}_MmiNidData", BaseString1, k));

                        _pool.TraceError(string.Format("MMI_NID_DATA[{0}] = {1}", k, _nidData));
                    }
                }
            }
            // Show generic DMI -> EVC telegram failure
            else
            {
                DmiExpectedResults.DMItoEVC_Telegram_Not_Received(_pool, BaseString0);
            }

            // Reset telegram received flag in RTSim
            _pool.SITR.SMDStat.CCUO.ETCS1NewTrainData.Value = 0x00;
        }

        /// <summary>
        /// Identity of the fixed trainset selected.
        /// 
        /// Values:
        /// FLU = 1
        /// RLU = 2
        /// Rescue = 3
        /// </summary>
        public static Variables.Fixed_Trainset_Captions TrainsetSelected
        {
            set
            {
                _trainsetSelected = value;
                CheckFixedTrainDataEntered(_trainsetSelected);
            }
        }

        /// <summary>
        /// Only MMI Buttons used in EVC-6 and EVC-107
        /// 
        /// Values:        
        /// 36 = "BTN_YES_DATA_ENTRY_COMPLETE"
        /// 37 = "BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE"       
        /// 253 = "BTN_ENTER_DELAY_TYPE"
        /// 254 = "BTN_ENTER"
        /// 255 = "no button"
        /// Note: the definition is according to preliminary SubSet-121 'M_BUTTONS' definition.
        /// </summary>
        public static Variables.MMI_M_BUTTONS_TRAIN_DATA MMI_M_BUTTONS
        {
            set { _mButtons = value; }
        }
    }
}