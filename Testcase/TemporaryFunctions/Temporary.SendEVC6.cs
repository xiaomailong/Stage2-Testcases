using System.Collections.Generic;
using Testcase.Telegrams;
using Testcase.Telegrams.EVCtoDMI;
using static Testcase.Telegrams.EVCtoDMI.Variables;

namespace Testcase.TemporaryFunctions
{
    internal static partial class Temporary
    {
        /// <summary>
        ///     Send EVC6_MMI_Current_Train_Data
        ///     Sends existing Train Data values to the DMI
        ///     <param name="mmiVMaxTrain">Max train speed</param>
        ///     <param name="mmiNidKeyTrainCat">Train category (range 3-20)</param>
        ///     <param name="mmiMBrakePerc">Brake percentage</param>
        ///     <param name="mmiNidKeyAxleLoad">Axle load category (range 21-33) </param>
        ///     <param name="mmiMAirtight">Train equipped with airtight system</param>
        ///     <param name="mmiNidKeyLoadGauge">Axle load category (range 34-38)</param>
        ///     <param name="mmiMButtons">
        ///         Intended to be used to dstinguish between 'BTN_YES_DATA_ENTRY_COMPLETE',
        ///         'BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE','no button'
        ///     </param>
        ///     <param name="mmiMTrainsetId">ID of preconfigured train data set</param>
        ///     <param name="mmiMAltDem">Control variable for alternative train data entry method</param>
        /// </summary>
        public static void SendEVC6_MMICurrentTrainData(MMI_M_DATA_ENABLE mmiMDataEnable, ushort mmiLTrain,
            ushort mmiVMaxTrain, MMI_NID_KEY mmiNidKeyTrainCat, byte mmiMBrakePerc, MMI_NID_KEY mmiNidKeyAxleLoad,
            byte mmiMAirtight, MMI_NID_KEY mmiNidKeyLoadGauge, EVC6_MMICurrentTrainData.MMI_M_BUTTONS_CURRENT_TRAIN_DATA mmiMButtons,
            ushort mmiMTrainsetId,
            ushort mmiMAltDem, string[] trainSetCaptions, DataElement[] dataElements)
        {
            // Train data enabled

            EVC6_MMICurrentTrainData.MMI_M_DATA_ENABLE = mmiMDataEnable;

            EVC6_MMICurrentTrainData.MMI_L_TRAIN = mmiLTrain; // Train length

            EVC6_MMICurrentTrainData.MMI_V_MAXTRAIN = mmiVMaxTrain; // Max train speed
            EVC6_MMICurrentTrainData.MMI_NID_KEY_TRAIN_CAT = mmiNidKeyTrainCat; // Train category
            EVC6_MMICurrentTrainData.MMI_M_BRAKE_PERC = mmiMBrakePerc; // Brake percentage
            EVC6_MMICurrentTrainData.MMI_NID_KEY_AXLE_LOAD = mmiNidKeyAxleLoad; // Axle load category
            EVC6_MMICurrentTrainData.MMI_M_AIRTIGHT = mmiMAirtight; // Train equipped with airtight system
            EVC6_MMICurrentTrainData.MMI_NID_KEY_LOAD_GAUGE =
                mmiNidKeyLoadGauge; // Loading gauge type of train 
            EVC6_MMICurrentTrainData.MMI_M_BUTTONS = mmiMButtons; // Button available

            EVC6_MMICurrentTrainData.MMI_M_TRAINSET_ID = mmiMTrainsetId;
            EVC6_MMICurrentTrainData.MMI_M_ALT_DEM = mmiMAltDem;

            EVC6_MMICurrentTrainData.TrainSetCaptions = new List<string>(trainSetCaptions);
            EVC6_MMICurrentTrainData.DataElements = new List<DataElement>(dataElements);

            EVC6_MMICurrentTrainData.Send();
        }

        /// <summary>
        ///     Sends EVC-6 telegram with Fixed Data Entry for up to 9 trainset strings.
        /// </summary>
        /// <param name="fixedTrainsetCaptions"> Array of strings for trainset captions</param>
        /// <param name="mmiMTrainsetId">Index of trainset to be pre-selected on DMI</param>
        public static void SendEVC6_MMICurrentTrainData_FixedDataEntry(string[] fixedTrainsetCaptions,
            ushort mmiMTrainsetId)

        {
            // Train data enabled
            EVC6_MMICurrentTrainData.MMI_M_DATA_ENABLE = MMI_M_DATA_ENABLE.TrainSetID; // "Train Set ID" data enabled

            EVC6_MMICurrentTrainData.MMI_L_TRAIN = 0; // Train length
            EVC6_MMICurrentTrainData.MMI_V_MAXTRAIN = 0; // Max train speed

            EVC6_MMICurrentTrainData.MMI_NID_KEY_TRAIN_CAT = MMI_NID_KEY.NoDedicatedKey; // Train category
            EVC6_MMICurrentTrainData.MMI_M_BRAKE_PERC = 0; // Brake percentage
            EVC6_MMICurrentTrainData.MMI_NID_KEY_AXLE_LOAD = MMI_NID_KEY.NoDedicatedKey; // Axle load category
            EVC6_MMICurrentTrainData.MMI_M_AIRTIGHT = 0; // Train equipped with airtight system
            EVC6_MMICurrentTrainData.MMI_NID_KEY_LOAD_GAUGE =
                MMI_NID_KEY.NoDedicatedKey; // Loading gauge type of train 
            EVC6_MMICurrentTrainData.MMI_M_BUTTONS = 0; // No Buttons available

            EVC6_MMICurrentTrainData.MMI_M_TRAINSET_ID = mmiMTrainsetId; // Preselected Trainset ID
            // MMI_Alt_Dem = 0: No alternative train data entry method available

            EVC6_MMICurrentTrainData.TrainSetCaptions = new List<string>(fixedTrainsetCaptions);
            EVC6_MMICurrentTrainData.DataElements = new List<DataElement>(); // no train data elements


            EVC6_MMICurrentTrainData.Send();
        }
    }
}