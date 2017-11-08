using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BT_Tools;
using BT_CSB_Tools;
using BT_CSB_Tools.Logging;
using BT_CSB_Tools.Utils.Xml;
using BT_CSB_Tools.SignalPoolGenerator.Signals;
using BT_CSB_Tools.SignalPoolGenerator.Signals.MwtSignal;
using BT_CSB_Tools.SignalPoolGenerator.Signals.MwtSignal.Misc;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal;
using BT_CSB_Tools.SignalPoolGenerator.Signals.PdSignal.Misc;
using CL345;
using Testcase.Telegrams;
using Testcase.Telegrams.EVCtoDMI;
using Testcase.Telegrams.DMItoEVC;
using Testcase.TemporaryFunctions;
using static Testcase.Telegrams.EVCtoDMI.Variables;
using System.Windows.Forms;

// ReSharper disable UnusedMember.Global

namespace Testcase.DMITestCases
{
    /// <summary>
    /// These are the generic methods used to perform actions on the DMI
    /// </summary>
    public class DmiActions
    {
        /// <summary>
        /// Forces DMI into completed SoM, L0, UN Mode. Displays Default window.
        /// No user input required.
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Complete_SoM_L0_UN(SignalPool pool)
        {
            // Start DMI/ETCS
            Start_ATP();

            // Set train running number, cab 1 active, and other defaults
            Activate_Cabin_1(pool);

            // Set driver ID
            Set_Driver_ID(pool, "1234");

            // Set to level 0 and UN mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L0;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Unfitted;

            // Enable standard buttons including Start, and display Default window.
            Finished_SoM_Default_Window(pool);
        }

        /// <summary>
        /// Forces DMI into completed SoM, L1, SB Mode. Displays Default window.
        /// No user input required.
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Complete_SoM_L1_SB(SignalPool pool)
        {
            // Start DMI/ETCS
            Start_ATP();

            // Set train running number, cab 1 active, and other defaults
            Activate_Cabin_1(pool);

            // Set driver ID
            Set_Driver_ID(pool, "1234");

            // Set to level 1 and SR mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;

            // Enable standard buttons including Start, and display Default window.
            Finished_SoM_Default_Window(pool);
        }

        /// <summary>
        /// Forces DMI into completed SoM, L1, FS Mode. Displays Default window.
        /// No user input required.
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Complete_SoM_L1_FS(SignalPool pool)
        {
            Start_ATP();

            // Set train running number, cab 1 active, and other defaults
            Activate_Cabin_1(pool);

            // Set driver ID
            Set_Driver_ID(pool, "1234");

            // Set to level 1 and SR mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;

            // Enable standard buttons including Start, and display Default window.
            Finished_SoM_Default_Window(pool);
        }

        /// <summary>
        /// Forces DMI into completed SoM, L1, SR Mode. Displays Default window.
        /// No user input required.
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Complete_SoM_L1_SR(SignalPool pool)
        {
            Start_ATP();

            // Set train running number, cab 1 active, and other defaults
            Activate_Cabin_1(pool);

            // Set driver ID
            Set_Driver_ID(pool, "1234");

            // Set to level 1 and SR mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;

            // Enable standard buttons including Start, and display Default window.
            Finished_SoM_Default_Window(pool);
        }

        /// <summary>
        /// Enable standard buttons including Start, and display Default window.
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Finished_SoM_Default_Window (SignalPool pool)
        {
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Start | standardFlags;
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 0;
            EVC30_MMIRequestEnable.Send();
        }

        /// <summary>
        /// Set Driver ID string
        /// </summary>
        /// <param name="pool">Signal pool</param>
        /// <param name="driverId"></param>
        public static void Set_Driver_ID(SignalPool pool, string driverId)
        {
            EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = driverId;
            EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE = EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.Settings |
                                                        EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.TRN;
            EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = MMI_Q_CLOSE_ENABLE.Enabled;
            EVC14_MMICurrentDriverID.Send();
        }

        /// <summary>
        /// Set Driver ID string
        /// </summary>
        /// <param name="pool">Signal pool</param>
        /// <param name="vbcCode"></param>
        public static void Set_VBC_Code(SignalPool pool, string vbcCode)
        {
            EVC18_MMISetVBC.MMI_M_BUTTONS = MMI_M_BUTTONS_VBC.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC18_MMISetVBC.MMI_N_VBC = 1;
            EVC18_MMISetVBC.NID_VBCMK = 0;
            EVC18_MMISetVBC.SetVBCCode();
            EVC18_MMISetVBC.MMI_Q_DATA_CHECK = Q_DATA_CHECK.All_checks_passed;
            EVC18_MMISetVBC.ECHO_TEXT = vbcCode;
            EVC18_MMISetVBC.Send();
        }

        /// <summary>
        /// Description: Set Override Mode. *
        /// This function shall be called after driveraction StartEOA has been performed
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Set_Override(SignalPool pool)
        {
            EVC2_MMIStatus.MMI_M_OVERRIDE_EOA = true;
            EVC2_MMIStatus.Send();

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = EVC30_MMIRequestEnable.EnabledRequests.Start | standardFlags;
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 0;
            EVC30_MMIRequestEnable.Send();
        }

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
        public static void Send_EVC6_MMICurrentTrainData(MMI_M_DATA_ENABLE mmiMDataEnable, ushort mmiLTrain,
            ushort mmiVMaxTrain, MMI_NID_KEY mmiNidKeyTrainCat, byte mmiMBrakePerc, MMI_NID_KEY mmiNidKeyAxleLoad,
            byte mmiMAirtight, MMI_NID_KEY mmiNidKeyLoadGauge, EVC6_MMICurrentTrainData.MMI_M_BUTTONS_CURRENT_TRAIN_DATA mmiMButtons,
            ushort mmiMTrainsetId, ushort mmiMAltDem, string[] trainSetCaptions, DataElement[] dataElements)
        {
            EVC6_MMICurrentTrainData.MMI_M_DATA_ENABLE = mmiMDataEnable;            // Train data enabled
            EVC6_MMICurrentTrainData.MMI_L_TRAIN = mmiLTrain;                       // Train length
            EVC6_MMICurrentTrainData.MMI_V_MAXTRAIN = mmiVMaxTrain;                 // Max train speed
            EVC6_MMICurrentTrainData.MMI_NID_KEY_TRAIN_CAT = mmiNidKeyTrainCat;     // Train category
            EVC6_MMICurrentTrainData.MMI_M_BRAKE_PERC = mmiMBrakePerc;              // Brake percentage
            EVC6_MMICurrentTrainData.MMI_NID_KEY_AXLE_LOAD = mmiNidKeyAxleLoad;     // Axle load category
            EVC6_MMICurrentTrainData.MMI_M_AIRTIGHT = mmiMAirtight;                 // Train equipped with airtight system
            EVC6_MMICurrentTrainData.MMI_NID_KEY_LOAD_GAUGE = mmiNidKeyLoadGauge;   // Loading gauge type of train 
            EVC6_MMICurrentTrainData.MMI_M_BUTTONS = mmiMButtons;                   // Button available

            EVC6_MMICurrentTrainData.MMI_M_TRAINSET_ID = mmiMTrainsetId;
            EVC6_MMICurrentTrainData.MMI_M_ALT_DEM = mmiMAltDem;

            EVC6_MMICurrentTrainData.TrainSetCaptions = new List<string>(trainSetCaptions);
            EVC6_MMICurrentTrainData.DataElements = new List<DataElement>(dataElements);

            EVC6_MMICurrentTrainData.Send();
        }

        /// <summary>
        ///     Sends EVC-6 telegram with Fixed Data Entry for up to 9 trainset strings.
        /// </summary>
        /// <param name="pool">Signal pool</param>
        /// <param name="fixedTrainsetCaptions"> Array of strings for trainset captions</param>
        /// <param name="mmiMTrainsetId">Index of trainset to be pre-selected on DMI</param>
        public static void Send_EVC6_MMICurrentTrainData_FixedDataEntry(SignalPool pool, string[] fixedTrainsetCaptions,
                                                                        ushort mmiMTrainsetId)            
        {
            // Train data enabled
            EVC6_MMICurrentTrainData.MMI_M_DATA_ENABLE = MMI_M_DATA_ENABLE.TrainSetID;      // "Train Set ID" data enabled
            EVC6_MMICurrentTrainData.MMI_L_TRAIN = 0;                                       // Train length
            EVC6_MMICurrentTrainData.MMI_V_MAXTRAIN = 0;                                    // Max train speed
            EVC6_MMICurrentTrainData.MMI_NID_KEY_TRAIN_CAT = MMI_NID_KEY.NoDedicatedKey;    // Train category
            EVC6_MMICurrentTrainData.MMI_M_BRAKE_PERC = 0;                                  // Brake percentage
            EVC6_MMICurrentTrainData.MMI_NID_KEY_AXLE_LOAD = MMI_NID_KEY.NoDedicatedKey;    // Axle load category
            EVC6_MMICurrentTrainData.MMI_M_AIRTIGHT = 0;                                    // Train equipped with airtight system
            EVC6_MMICurrentTrainData.MMI_NID_KEY_LOAD_GAUGE = MMI_NID_KEY.NoDedicatedKey;   // Loading gauge type of train 
            EVC6_MMICurrentTrainData.MMI_M_BUTTONS = 
                EVC6_MMICurrentTrainData.MMI_M_BUTTONS_CURRENT_TRAIN_DATA.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC6_MMICurrentTrainData.MMI_M_TRAINSET_ID = mmiMTrainsetId;                    // Preselected Trainset ID
            EVC6_MMICurrentTrainData.MMI_M_ALT_DEM = 0;                                     // No alternative train data available

            EVC6_MMICurrentTrainData.TrainSetCaptions = new List<string>(fixedTrainsetCaptions);
            EVC6_MMICurrentTrainData.DataElements = new List<DataElement>();                // No train data elements

            EVC6_MMICurrentTrainData.Send();
        }

        public static void Send_EVC10_MMIEchoedTrainData(SignalPool pool, MMI_M_DATA_ENABLE mmiMDataEnable, ushort mmiLTrain,
                                                         ushort mmiVMaxTrain, MMI_NID_KEY mmiNidKeyTrainCat, 
                                                         byte mmiMBrakePerc, MMI_NID_KEY mmiNidKeyAxleLoad,
                                                         byte mmiMAirtight, MMI_NID_KEY mmiNidKeyLoadGauge, 
                                                         string[] trainSetCaptions)
        {
            // EVC-10 inverts all the integral values except the alias
            EVC10_MMIEchoedTrainData.MMI_M_DATA_ENABLE_ = (ushort)mmiMDataEnable;                           // Train data enabled
            EVC10_MMIEchoedTrainData.MMI_L_TRAIN_ = mmiLTrain;                                              // Train length
            EVC10_MMIEchoedTrainData.MMI_V_MAXTRAIN_ = mmiVMaxTrain;                                        // Max train speed
            EVC10_MMIEchoedTrainData.MMI_NID_KEY_TRAIN_CAT_ = (byte)mmiNidKeyTrainCat;                      // Train category
            EVC10_MMIEchoedTrainData.MMI_M_BRAKE_PERC_ = mmiMBrakePerc;                                     // Brake percentage
            EVC10_MMIEchoedTrainData.MMI_NID_KEY_AXLE_LOAD_R = (byte)mmiNidKeyAxleLoad;                     // Axle load category
            EVC10_MMIEchoedTrainData.MMI_M_AIRTIGHT_R = mmiMAirtight;                                       // Train equipped with airtight system
            EVC10_MMIEchoedTrainData.MMI_NID_KEY_LOAD_GAUGE_ = (byte)mmiNidKeyLoadGauge;                    // Loading gauge type of train 
            EVC10_MMIEchoedTrainData.EVC10_alias_1 = pool.SITR.ETCS1.CurrentTrainData.EVC6alias1.Value;     // Alias variable for bit mapping
            EVC10_MMIEchoedTrainData.MMI_N_TRAINSETS_ = pool.SITR.ETCS1.CurrentTrainData.MmiNTrainset.Value;
            EVC10_MMIEchoedTrainData.TrainSetCaptions = new List<string>(trainSetCaptions);

            EVC10_MMIEchoedTrainData.Send();
        }

        /// <summary>
        ///     Sends EVC-10 telegram with Fixed Data Entry for up to 9 trainset strings.
        /// </summary>
        /// <param name="pool">Signal pool</param>
        /// <param name="fixedTrainsetCaptions"> Array of strings for trainset captions</param>
        /// <param name="mmiMTrainsetId">Index of trainset to be pre-selected on DMI</param>
        public static void Send_EVC10_MMIEchoedTrainData_FixedDataEntry(SignalPool pool, string[] fixedTrainsetCaptions)
        {            
            EVC10_MMIEchoedTrainData.MMI_M_DATA_ENABLE_ = (ushort)EVC6_MMICurrentTrainData.MMI_M_DATA_ENABLE;           // Train data enabled
            EVC10_MMIEchoedTrainData.MMI_L_TRAIN_ = EVC6_MMICurrentTrainData.MMI_L_TRAIN;                               // Train length
            EVC10_MMIEchoedTrainData.MMI_V_MAXTRAIN_ = EVC6_MMICurrentTrainData.MMI_V_MAXTRAIN;                         // Max train speed
            EVC10_MMIEchoedTrainData.MMI_NID_KEY_TRAIN_CAT_ = (byte)EVC6_MMICurrentTrainData.MMI_NID_KEY_TRAIN_CAT;     // Train category
            EVC10_MMIEchoedTrainData.MMI_M_BRAKE_PERC_ = (byte)EVC6_MMICurrentTrainData.MMI_M_BRAKE_PERC;               // Brake percentage
            EVC10_MMIEchoedTrainData.MMI_NID_KEY_AXLE_LOAD_R = (byte)EVC6_MMICurrentTrainData.MMI_NID_KEY_AXLE_LOAD;    // Axle load category
            EVC10_MMIEchoedTrainData.MMI_M_AIRTIGHT_R = (byte)EVC6_MMICurrentTrainData.MMI_M_AIRTIGHT;                  // Train equipped with airtight system
            EVC10_MMIEchoedTrainData.MMI_NID_KEY_LOAD_GAUGE_ = (byte)EVC6_MMICurrentTrainData.MMI_NID_KEY_LOAD_GAUGE;   // Loading gauge type of train 
            EVC10_MMIEchoedTrainData.EVC10_alias_1 = pool.SITR.ETCS1.CurrentTrainData.EVC6alias1.Value;                 // Alias variable for bit mapping
            EVC10_MMIEchoedTrainData.MMI_N_TRAINSETS_ = pool.SITR.ETCS1.CurrentTrainData.MmiNTrainset.Value;
            EVC10_MMIEchoedTrainData.TrainSetCaptions = new List<string>(fixedTrainsetCaptions);
            
            EVC10_MMIEchoedTrainData.Send();
        }

        /// <summary>
        /// Send standard EVC-20 telegram with Levels 0-3, CBTC, and AWS/TPWS selectable. Level 1 is preselected.
        /// </summary>
        public static void Send_EVC20_MMISelectLevel_AllLevels(SignalPool pool, bool closeEnable = true)
        {
            EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = paramEvc20MmiQLevelNtcId;
            EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = paramEvc20MmiMCurrentLevel;
            EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = paramEvc20MmiMLevelFlag;
            EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = paramEvc20MmiMInhibitedLevel;
            EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = paramEvc20MmiMInhibitEnable;
            EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = paramEvc20MmiMLevelNtcId;
            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = closeEnable ? MMI_Q_CLOSE_ENABLE.Enabled : MMI_Q_CLOSE_ENABLE.Disabled;
            EVC20_MMISelectLevel.Send();
        }

        /// <summary>
        /// Sends EVC-20 telegram to cancel previous MMI_Select_Level presentation
        /// </summary>
        public static void Send_EVC20_MMISelectLevel_Cancel(SignalPool pool)
        {
            EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = null;
            EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = null;
            EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = null;
            EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = null;
            EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = null;
            EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = null;
            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE = MMI_Q_CLOSE_ENABLE.Enabled;
            EVC20_MMISelectLevel.Send();
        }

        /// <summary>
        /// Send_EVC22_MMI_Current_Rbc_Data sends RBC Data to the DMI
        /// </summary>
        public static void Send_EVC22_MMI_Current_RBC(SignalPool pool, uint rbcId, ulong mmiNidRadio, ushort mmiNidWindow,
                                                        bool closeEnable, EVC22_MMICurrentRBC.EVC22BUTTONS mmiMButtons,
                                                        string[] textDataElements)
        {
            EVC22_MMICurrentRBC.NID_C = NidC;
            EVC22_MMICurrentRBC.NID_RBC = rbcId;
            EVC22_MMICurrentRBC.MMI_NID_RADIO = mmiNidRadio; // RBC phone number
            EVC22_MMICurrentRBC.MMI_NID_WINDOW = mmiNidWindow; // ETCS Window Id
            EVC22_MMICurrentRBC.MMI_Q_CLOSE_ENABLE = closeEnable ? MMI_Q_CLOSE_ENABLE.Enabled : MMI_Q_CLOSE_ENABLE.Disabled; // Close button enable?
            EVC22_MMICurrentRBC.MMI_M_BUTTONS = mmiMButtons; // Buttons available

            EVC22_MMICurrentRBC.NetworkCaptions = new List<string>(textDataElements);
            EVC22_MMICurrentRBC.DataElements = new List<DataElement>();

            EVC22_MMICurrentRBC.Send();
        }

        /// <summary>
        /// Prompts the tester with a dialog box showing a set of instructions.
        /// </summary>
        public static void ShowInstruction(SignalPool pool, string instruction)
        {
            pool.WaitForAcknowledgement(instruction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        /// <returns></returns>
        public static string ShowDialog(string text, string caption)
        {
            var prompt = new Form()
            {
                Width = 700,
                Height = 500,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };

            var textLabel = new Label() { Left = 50, Top = 20, Text = text };
            var textBox = new TextBox() { Left = 50, Top = 50, Width = 400 };
            var confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };

            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        /// <summary>
        /// Description: Activate cabin 1
        /// Used in:
        ///     Step 1 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void Activate_Cabin_1(SignalPool pool)
        {
            EVC2_MMIStatus.TrainRunningNumber = 1;
            EVC2_MMIStatus.MMI_M_ACTIVE_CABIN = MMI_M_ACTIVE_CABIN.Cabin1Active;
            EVC2_MMIStatus.MMI_M_ADHESION = 0x0;
            EVC2_MMIStatus.MMI_M_OVERRIDE_EOA = false;
            EVC2_MMIStatus.Send();
        }

        /// <summary>
        /// Description: Activate cabin 2
        /// Used in:
        ///     Step 12 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void Activate_Cabin_2(SignalPool pool)
        {
            EVC2_MMIStatus.TrainRunningNumber = 1;
            EVC2_MMIStatus.MMI_M_ACTIVE_CABIN = MMI_M_ACTIVE_CABIN.Cabin2Active;
            EVC2_MMIStatus.MMI_M_ADHESION = 0x0;
            EVC2_MMIStatus.MMI_M_OVERRIDE_EOA = false;
            EVC2_MMIStatus.Send();
        }

        /// <summary>
        /// Description: Activate cabin 2
        /// Used in:
        ///     Step 12 in TC-ID: 15.1.1 in 20.1.1
        ///     Step 10 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        public static void Deactivate_Cabin(SignalPool pool)
        {
            EVC2_MMIStatus.MMI_M_ACTIVE_CABIN = MMI_M_ACTIVE_CABIN.NoCabinActive;
            EVC2_MMIStatus.Send();
        }

        /// <summary>
        /// Description: ETCS requests driver to allow the brake test
        /// Used in:
        ///     Step 2 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        public static void Request_Brake_Test(SignalPool pool)
        {
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 2;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 514;
            EVC8_MMIDriverMessage.Send();
        }

        /// <summary>
        /// EB intervention symbol sent to be displayed on the DMI in area C9
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_EB_Intervention(SignalPool pool)
        {
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_EB_Status = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 255;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 260;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.Send();
        }

        /// <summary>
        /// Description: L0 sent to the DMI
        /// Used in:
        ///     Step 1 in TC-ID: 15.1.4 in 20.1.4
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_L0(SignalPool pool)
        {
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L0;
        }

        /// <summary>
        /// Description: L1 sent to the DMI
        /// Used in:
        ///     Step 2 in TC-ID: 15.2.1 in 20.2.1
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_L1(SignalPool pool)
        {
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
        }

        /// <summary>
        /// Description: L2 sent to the DMI
        /// Used in:
        ///     Step 1 in TC-ID: 15.2.2 in 20.2.2
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_L2(SignalPool pool)
        {
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L2;
        }

        /// <summary>
        /// Description: Level 0 announcement ack request sent to the driver
        /// Used in:
        ///     Step 1 in TC-ID: 15.1.4 in 20.1.4
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_L0_Announcement_Ack(SignalPool pool)
        {
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 257;     // "#3 LE07/LE11/LE13/LE15 (Ack Transition to Level #4)"
            EVC8_MMIDriverMessage.Send();
        }

        /// <summary>
        /// Description: SB mode sent to be displayed on th DMI
        /// Used in:
        ///     Step 1 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_SB_Mode(SignalPool pool)
        {
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;
        }

        /// <summary>
        /// Description: SR mode acknowledgement request sent to the driver
        /// Used in:
        ///     Step 2 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_SR_Mode_Ack(SignalPool pool)
        {
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 263;     // "#3 MO10 (Ack Staff Responsible Mode)"
            EVC8_MMIDriverMessage.Send();
        }

        /// <summary>
        /// Description: SR mode sent to be displayed on the DMI
        /// Used in:
        ///     Step 1 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_SR_Mode(SignalPool pool)
        {
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;
        }

        /// <summary>
        /// Description: UN mode sent to be displayed on the DMI
        /// Used in:
        ///     Step 9 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_UN_Mode(SignalPool pool)
        {
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Unfitted;
        }

        /// <summary>
        /// Description: UN mode acknowledgement request sent to the driver
        /// Used in:
        ///     Step 8 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_UN_Mode_Ack(SignalPool pool)
        {
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 264;     // "#3 MO17 (Ack Unfitted Mode)"
            EVC8_MMIDriverMessage.Send();
        }

        /// <summary>
        /// Description: FS mode sent to be displayed on the DMI
        /// Used in:
        ///     Step 5 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_FS_Mode(SignalPool pool)
        {
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
        }

        /// <summary>
        /// Description: OS mode sent to be displayed on the DMI
        /// Used in:
        ///     Step 12 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_OS_Mode(SignalPool pool)
        {
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.OnSight;
        }

        /// <summary>
        /// Description: OS mode acknowledgement request sent to the driver
        /// Used in:
        ///     Step 11 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_OS_Mode_Ack(SignalPool pool)
        {
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 259;     // "#3 MO08 (Ack On Sight Mode)"
            EVC8_MMIDriverMessage.Send();
        }

        /// <summary>
        /// Description: LS mode sent to be displayed on the DMI
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_LS_Mode(SignalPool pool)
        {
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.LimitedSupervision;
        }

        /// <summary>
        /// Description: Limited Supervision mode acknowledgement request sent to the driver
        /// Used in:
        ///     Step 1 in TC-ID: 15.1.6 in 20.1.6
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_LS_Mode_Ack(SignalPool pool)
        {
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 709;     // "#3 MO22 (Ack for Limited Supervision)"
            EVC8_MMIDriverMessage.Send();
        }

        /// <summary>
        /// Description: TR mode sent to be displayed on th DMI
        /// Used in:
        ///     Step 6 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_TR_Mode(SignalPool pool)
        {
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Trip;
        }

        /// <summary>
        /// Description: TR mode acknowledgement request sent to the driver
        /// Used in:
        ///     Step 7 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_TR_Mode_Ack(SignalPool pool)
        {
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 266;     // "#3 MO05 (Ack Train Trip)" 
            EVC8_MMIDriverMessage.Send();
        }

        /// <summary>
        /// Description: PT mode sent to be displayed on th DMI
        /// Used in:
        ///     Step 8 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_PT_Mode(SignalPool pool)
        {
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.PostTrip;
        }

        /// <summary>
        /// Description: RV mode sent to be displayed on th DMI
        /// Used in:
        ///     Step 4 in TC-ID: 15.1.2 in 20.1.2
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_RV_Mode(SignalPool pool)
        {
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Reversing;
        }

        /// <summary>
        /// Description: SL mode sent to be displayed on th DMI
        /// Used in:
        ///     Step 5 in TC-ID: 15.1.2 in 20.1.2
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_SL_Mode(SignalPool pool)
        {
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Sleeping;
        }

        /// <summary>
        /// Description: Main Window is Start Button enabled sent to be displayed on th DMI
        /// Used in:
        ///     Step 9 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Display_Main_Window_with_Start_button_enabled(SignalPool pool)
        {
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 1;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = standardFlags | EVC30_MMIRequestEnable.EnabledRequests.Start;
            EVC30_MMIRequestEnable.Send();
        }

        /// <summary>
        /// Description: Main Window is Start Button enabled sent to be displayed on th DMI
        /// Used in:
        ///     Step 3 in TC-ID: 15.1.3 in 20.1.3
        ///     Step 1 in TC-ID: 15.2.1 in 20.2.1
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Display_Main_Window_with_Start_button_not_enabled(SignalPool pool)
        {
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 1;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = standardFlags;
            EVC30_MMIRequestEnable.Send();
        }

        /// <summary>
        /// Description: SH mode sent to be displayed on th DMI
        /// Used in:
        ///     Step 10 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_SH_Mode(SignalPool pool)
        {
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Shunting;
        }

        /// <summary>
        /// Description: Driver Id Window sent to be displayed on th DMI
        /// Used in:
        ///     Step 11 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Display_Driver_ID_Window(SignalPool pool)
        {
            EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE = EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.Settings |
                                                        EVC14_MMICurrentDriverID.MMI_Q_ADD_ENABLE_BUTTONS.TRN;
            EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = MMI_Q_CLOSE_ENABLE.Enabled;
            EVC14_MMICurrentDriverID.MMI_X_DRIVER_ID = "";
            EVC14_MMICurrentDriverID.Send();
        }

        /// <summary>
        /// Description: Level Window sent to be displayed on th DMI
        /// Used in:
        ///     Step 2 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Display_Level_Window(SignalPool pool, bool closeEnable = true)
        {
            Send_EVC20_MMISelectLevel_AllLevels(pool, closeEnable ? true : false);
        }

        /// <summary>
        /// Description: Train Data sent to be displayed on th DMI
        /// Used in:
        ///     Step 4 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Display_Fixed_Train_Data_Window(SignalPool pool)
        {
            Send_EVC6_MMICurrentTrainData_FixedDataEntry(pool, new [] { "FLU", "RLU", "Rescue" }, 15);
            
            // Keep this line below please. Work in progress..
            //Send_EVC6_MMICurrentTrainData_FixedDataEntry(pool, paramEvc6FixedTrainsetCaptions, 15);
        }

        /// <summary>
        /// Description: Enable "Yes" button to validate Fixed Train data selection
        /// Used in:
        ///     Step 4 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="trainsetSelected"></param>
        public static void Enable_Fixed_Train_Data_Validation(SignalPool pool, Fixed_Trainset_Captions trainsetSelected)
        {
            DataElement[] dataElements = new DataElement[1]
            {
                new DataElement{
                    Identifier = 6,
                    QDataCheck = 0,
                    EchoText = Enum.GetName(typeof(Fixed_Trainset_Captions),trainsetSelected) }
            };

            DmiActions.Send_EVC6_MMICurrentTrainData(MMI_M_DATA_ENABLE.TrainSetID, 0, 0, MMI_NID_KEY.NoDedicatedKey, 0,
                MMI_NID_KEY.NoDedicatedKey, 0, MMI_NID_KEY.NoDedicatedKey,
                EVC6_MMICurrentTrainData.MMI_M_BUTTONS_CURRENT_TRAIN_DATA.BTN_YES_DATA_ENTRY_COMPLETE,
                Convert.ToUInt16((byte)(trainsetSelected)), 0, new string[] { }, dataElements);
        }

        /// <summary>
        /// Description: Finishes the Train Data Entry process in order to move to Train Data Validation
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="trainsetSelected"></param>
        public static void Complete_Fixed_Train_Data_Entry(SignalPool pool, Fixed_Trainset_Captions trainsetSelected)
        {
            DataElement[] dataElements = new DataElement[8]
            {
                new DataElement{ Identifier = 6, QDataCheck = 0, EchoText = "" },
                new DataElement{ Identifier = 9, QDataCheck = 0, EchoText = "" },
                new DataElement{ Identifier = 10, QDataCheck = 0, EchoText = "" },
                new DataElement{ Identifier = 11, QDataCheck = 0, EchoText = "" },
                new DataElement{ Identifier = 12, QDataCheck = 0, EchoText = "" },
                new DataElement{ Identifier = 13, QDataCheck = 0, EchoText = "" },
                new DataElement{ Identifier = 7, QDataCheck = 0, EchoText = "" },
                new DataElement{ Identifier = 8, QDataCheck = 0, EchoText = "" }
            };

            DmiActions.Send_EVC6_MMICurrentTrainData(MMI_M_DATA_ENABLE.NONE, 0, 0, MMI_NID_KEY.NoDedicatedKey, 0,
                MMI_NID_KEY.NoDedicatedKey, 0, MMI_NID_KEY.NoDedicatedKey,
                EVC6_MMICurrentTrainData.MMI_M_BUTTONS_CURRENT_TRAIN_DATA.BTN_YES_DATA_ENTRY_COMPLETE,
                Convert.ToUInt16((byte)(trainsetSelected)), 0, new string[] { }, dataElements);
        }

        /// <summary>
        /// Description: RBC Data sent to be displayed on th DMI
        /// Used in:
        ///     Step 1 in TC-ID: 15.2.2 in 20.2.2
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Display_RBC_Contact_Window_Data_Unknown(SignalPool pool)
        {
            Send_EVC22_MMI_Current_RBC
                (pool, 0, 0, 9 , true, EVC22_MMICurrentRBC.EVC22BUTTONS.NoButton, new []{ "Network1","Network2","Network3"});
        }

        /// <summary>
        /// Description: RBC Data sent to be displayed on th DMI
        /// Used in:
        ///     Step 1 in TC-ID: 22.27.1 in 27.27.1
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Display_Set_VBC_Window(SignalPool pool)
        {
            EVC18_MMISetVBC.MMI_M_BUTTONS = MMI_M_BUTTONS_VBC.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC18_MMISetVBC.MMI_N_VBC = 0;
            EVC18_MMISetVBC.Send();
        }

        /// <summary>
        /// Description: Train Data sent to be displayed on th DMI
        /// Used in:
        ///     Step 6 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Display_TRN_Window(SignalPool pool)
        {
            EVC16_CurrentTrainNumber.TrainRunningNumber = 0xffffffff;
            EVC16_CurrentTrainNumber.Send(); ;
        }

        /// <summary>
        /// Description: Override Window sent to be displayed on th DMI
        /// Used in:
        ///     Step 13 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Display_Override_Window(SignalPool pool)
        {
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 2;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =standardFlags | EVC30_MMIRequestEnable.EnabledRequests.EOA;               
            EVC30_MMIRequestEnable.Send();
        }

        /// <summary>
        /// Description: Default Window sent to be displayed on th DMI
        /// Used in:
        ///     Step 8 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Display_Default_Window(SignalPool pool)
        {
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 0;
            EVC30_MMIRequestEnable.Send();
        }

        /// <summary>
        /// Description: Asks the Tester to enter a LSSMA to be send to the DMI
        /// Used in:
        ///     Step 2 in TC-ID: 15.1.6 in 20.1.6
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_LSSMA(SignalPool pool)
        {
            // Tester enters LSSMA
            string lssma_string = DmiActions.ShowDialog(@"Perform the following actions: " + Environment.NewLine + Environment.NewLine +
                                "1. Enter a LSSMA value (integer lower than 601)." + Environment.NewLine, "LSSMA entering");

            ushort lssma_ushort;

            // Convert the entered value into a ushort
            try
            {
                lssma_ushort = UInt16.Parse(lssma_string);
            }
            catch (FormatException e)
            {
                throw e;
            }

            // Check the range value
            if (lssma_ushort > 600)
                throw new ArgumentOutOfRangeException();

            // Implement and send EVC-23
            EVC23_MMILssma.MMI_V_LSSMA = lssma_ushort;
            EVC23_MMILssma.Send();
        }


        /// <summary>
        /// Description: SR mode sent to be displayed on th DMI
        /// Used in:
        ///     Step 11 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_NL_Mode(SignalPool pool)
        {
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.NonLeading;
        }

        /// <summary>
        /// Description: SF mode sent to be displayed on th DMI
        /// Used in:
        ///     Step 12 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_SF_Mode(SignalPool pool)
        {
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.SystemFailure;
        }

        /// <summary>
        /// Description: RV Permitted_Symbol sent to be displayed on th DMI
        /// Used in:
        ///     Step 2 in TC-ID: 15.1.2 in 20.1.2
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_RV_Permitted_Symbol(SignalPool pool)
        {
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 286; // "#3 ST06 (Reversing is possible)"
            EVC8_MMIDriverMessage.Send();
        }

        /// <summary>
        /// Description: RV mode acknowledgement request sent to the driver
        /// Used in:
        ///     Step 3 in TC-ID: 15.1.2 in 20.1.2
        /// </summary>
        public static void Send_RV_Mode_Ack(SignalPool pool)
        {
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 262;     // "#3 MO15 (Ack Reversing Mode)"
            EVC8_MMIDriverMessage.Send();
        }

        /// <summary>
        /// Description: Apply Brakes
        /// Used in:
        ///     Step 16 in TC-ID: 5.10 in 10.10 Screen Layout: Button States
        ///     Step 2 in TC-ID: 12.1 in 17.1 Display of Speed Pointer and Speed Digital
        ///     Step 2 in TC-ID: 12.3 in 17.3.1 Speed Pointer: Sub-Area B1
        ///     Step 6 in TC-ID: 12.4 in 17.4 Current Train Speed Digital: Sub-Area B1
        ///     Step 5 in TC-ID: 12.7.1 in 17.7.1 Release Speed: At Sub-area B2 and B6
        ///     Step 6 in TC-ID: 12.12 in 17.12 Slip Indication
        ///     Step 5 in TC-ID: 13.1.1 in 18.1.1 Distance to Target  Bar: General Appearance
        ///     Step 6 in TC-ID: 13.1.5 in 18.1.5 Distance to Target in RV mode
        ///     Step 4 in TC-ID: 13.1.6 in 18.1.6 Distance to Target Digital in RV mode with special value
        ///     Step 2 in TC-ID: 13.1.9 in Distance to Target Bar and Digital: Appearance of distance to target for Imperial Unit
        ///     Step 8 in TC-ID: 15.5.1 in 20.5.1 Adhesion factor: General appearance
        ///     Step 3 in TC-ID: 17.3 in 22.3 Planning Area: PA Distance Scale
        ///     Step 5 in TC-ID: 17.4.1 in 22.4.1 PA Track Condition: Non stopping area in Sub-Area D2 and B3
        ///     Step 5 in TC-ID: 17.4.2 in 22.4.2 PA Track Condition: Sound Horn in Sub-Area D2 and B3
        ///     Step 5 in TC-ID: 17.4.3 in 22.4.3 PA Track Condition:  Lower Pantograph in Sub-Area D2 and B3
        ///     Step 10 in TC-ID: 17.4.3 in 22.4.3 PA Track Condition:  Lower Pantograph in Sub-Area D2 and B3
        ///     Step 5 in TC-ID: 17.4.4 in 22.4.4 PA Track Condition: Radio Hole in Sub-Area D2 and B3
        ///     Step 5 in TC-ID: 17.4.5 in 22.4.5 PA Track Condition: Air Tightness in Sub-Area D2 and B3
        ///     Step 10 in TC-ID: 17.4.5 in 22.4.5 PA Track Condition: Air Tightness in Sub-Area D2 and B3
        ///     Step 5 in TC-ID: 17.4.6 in 22.4.6 PA Track Condition: Switch off regenerative brake in Sub-Area D2 and B3
        ///     Step 5 in TC-ID: 17.4.7 in PA Track Condition: Switch off eddy current brake in Sub-Area D2 and B3
        ///     Step 5 in TC-ID: 17.4.8 in 22.4.8 PA Track Condition: Switch off magnetic shoe brake in Sub-Area D2 and B3
        ///     Step 5 in TC-ID: 17.4.9 in 22.4.9 PA Track Condition: Switch off main power switch in Sub-Area D2 and B3
        ///     Step 5 in TC-ID: 17.4.10 in 22.4.10 PA Track Condition: Change of traction system, not fitted Sub-Area D2 and B3
        ///     Step 5 in TC-ID: 17.4.11 in 22.4.11 PA Track Condition: Change of traction system, AC 25 KV 50 Hz Sub-Area D2 and B3
        ///     Step 5 in TC-ID: 17.4.12 in 22.4.12 PA Track Condition: Change of traction system, AC 15 KV 16.7 Hz Sub-Area D2 and B3
        ///     Step 5 in TC-ID: 17.4.13 in 22.4.13 PA Track Condition: Change of traction system, DC 3kV Sub-Area D2 and B3
        ///     Step 5 in TC-ID: 17.4.14 in 22.4.14 PA Track Condition: Change of traction system, DC 1.5 kV Sub-Area D2 and B3
        ///     Step 5 in TC-ID: 17.4.15 in 22.4.15 PA Track Condition: Change of traction system, DC 600/750 V Sub-Area D2 and B3
        ///     Step 7 in TC-ID: 17.6.1 in 22.6.1 PA Speed Profile Discontinuity: Display in sub-area D6 and D7
        ///     Step 2 in TC-ID: 17.7.1 in 22.7.1 PA Speed Profile (PASP): Display in sub-area D7 and D8
        ///     Step 9 in TC-ID: 17.8 in 22.8 PA Indication Marker: Sub-Area D7
        ///     Step 10 in TC-ID: 17.10.1 in 22.10.1 Zoom PA Function: General appearance
        ///     Step 5 in TC-ID: 21.1.1 in Sound S1 - Over Speed
        ///     Step 18 in TC-ID: 22.10 in 27.10 Special window
        ///     Step 7 in TC-ID: 26.1 in 1 Introduction
        ///     Step 5 in TC-ID: 36.1 in Sound S1 - Driving too fast
        ///     Step 12 in TC-ID: 36.1 in Sound S1 - Driving too fast
        ///     Step 5 in TC-ID: 36.2 in Sound S3 - End of Intervention
        ///     Step 11 in TC-ID: 36.2 in Sound S3 - End of Intervention
        ///     Step 7 in TC-ID: 36.3.1 in 39.3.1 Restrictive Target with Speed Monitoring in Full Supervision Mode
        ///     Step 5 in TC-ID: 36.3.2 in 39.3.2 Restrictive Target with Movement Authority Changed in Full Supervision Mode
        ///     Step 5 in TC-ID: 36.3.3 in 39.3.3 Restrictive Target with Speed Monitoring in Limited Supervision Mode
        ///     Step 5 in TC-ID: 36.3.4 in 39.3.4 Restrictive Target with Movement Authority Changed in Limited Supervision Mode
        ///     Step 6 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void Apply_Brakes(SignalPool pool)
        {
            EVC1_MMIDynamic.MMI_A_TRAIN = -50;
        }

        /// <summary>
        /// Description: Stop the train
        /// Used in:
        ///     Step 7 in TC-ID: 15.1.1 in 20.1.1
        ///     Step 2 in TC-ID: 15.1.2 in 20.1.2
        /// </summary>
        public static void Stop_the_train(SignalPool pool)
        {
            EVC1_MMIDynamic.MMI_V_TRAIN = 0;    // Set speed to zero
            EVC1_MMIDynamic.MMI_A_TRAIN = 0;
        }

        /// <summary>
        /// Description: Re-establish the communication between ETCS onboard and DMI
        /// Used in:
        ///     Step 6 in TC-ID: 12.7.2 in 17.7.2 Release Speed Digital is removed when communication between ETCS Onboard and DMI is lost
        ///     Step 6 in TC-ID: 13.1.4 in 18.1.4 Distance to Target Digital when the communication between ETCS  Onboard and DMI is lost
        ///     Step 4 in TC-ID: 15.5.1 in 20.5.1 Adhesion factor: General appearance
        ///     Step 10 in TC-ID: 17.3 in 22.3 Planning Area: PA Distance Scale
        ///     Step 4 in TC-ID: 17.5.1 in 22.5.1 PA Gradient Profile:  General appearance
        ///     Step 9 in TC-ID: 17.6.1 in 22.6.1 PA Speed Profile Discontinuity: Display in sub-area D6 and D7
        ///     Step 12 in TC-ID: 17.6.1 in 22.6.1 PA Speed Profile Discontinuity: Display in sub-area D6 and D7
        ///     Step 10 in TC-ID: 17.7.1 in 22.7.1 PA Speed Profile (PASP): Display in sub-area D7 and D8
        ///     Step 5 in TC-ID: 17.9.10 (Default Configuration) in 22.9.10 Hide PA Function with the communication loss between ETCS Onboard and DMI
        ///     Step 10 in TC-ID: 17.9.10 (Default Configuration) in 22.9.10 Hide PA Function with the communication loss between ETCS Onboard and DMI
        /// </summary>
        public static void Re_establish_communication_EVC_DMI(SignalPool pool)
        {
            // Commence sending EVC-1 and EVC-7 telegrams
            pool.SITR.STGCtrl.ETCS1.Dynamic.Value = 0x0001;
            pool.SITR.STGCtrl.ETCS1.EtcsMiscOutSignals.Value = 0x0001;
        }

        /// <summary>
        /// Description: Simulate the communication loss between ETCS Onboard and DMI
        /// Used in:
        ///     Step 3 in TC-ID: 15.5.1 in 20.5.1 Adhesion factor: General appearance
        ///     Step 9 in TC-ID: 17.3 in 22.3 Planning Area: PA Distance Scale
        ///     Step 3 in TC-ID: 17.5.1 in 22.5.1 PA Gradient Profile:  General appearance
        ///     Step 8 in TC-ID: 17.6.1 in 22.6.1 PA Speed Profile Discontinuity: Display in sub-area D6 and D7
        ///     Step 11 in TC-ID: 17.6.1 in 22.6.1 PA Speed Profile Discontinuity: Display in sub-area D6 and D7
        ///     Step 9 in TC-ID: 17.7.1 in 22.7.1 PA Speed Profile (PASP): Display in sub-area D7 and D8
        /// </summary>
        public static void Simulate_communication_loss_EVC_DMI(SignalPool pool)
        {
            // Stop sending EVC-1 and EVC-7 telegrams
            pool.SITR.STGCtrl.ETCS1.Dynamic.Value = 0x0000;
            pool.SITR.STGCtrl.ETCS1.EtcsMiscOutSignals.Value = 0x0000;
        }

        /// <summary>
        /// Description: Send packets needed to display Train Data Validation window on DMI
        /// Used in: Step 5 in TC-ID: 15.1.3
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Display_Train_data_validation_Window(SignalPool pool)
        {           
            /*
             * EVC-6 values are set to original without being sent to the DMI
             */
            EVC6_MMICurrentTrainData.MMI_M_DATA_ENABLE = MMI_M_DATA_ENABLE.TrainSetID;      // "Train Set ID" data enabled
            EVC6_MMICurrentTrainData.MMI_L_TRAIN = 0;                                       // Train length
            EVC6_MMICurrentTrainData.MMI_V_MAXTRAIN = 0;                                    // Max train speed
            EVC6_MMICurrentTrainData.MMI_NID_KEY_TRAIN_CAT = MMI_NID_KEY.NoDedicatedKey;    // Train category
            EVC6_MMICurrentTrainData.MMI_M_BRAKE_PERC = 0;                                  // Brake percentage
            EVC6_MMICurrentTrainData.MMI_NID_KEY_AXLE_LOAD = MMI_NID_KEY.NoDedicatedKey;    // Axle load category
            EVC6_MMICurrentTrainData.MMI_M_AIRTIGHT = 0;                                    // Train equipped with airtight system
            EVC6_MMICurrentTrainData.MMI_NID_KEY_LOAD_GAUGE = MMI_NID_KEY.NoDedicatedKey;   // Loading gauge type of train 
            EVC6_MMICurrentTrainData.MMI_M_BUTTONS =
                EVC6_MMICurrentTrainData.MMI_M_BUTTONS_CURRENT_TRAIN_DATA.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC6_MMICurrentTrainData.MMI_M_TRAINSET_ID = 15;                                // Preselected Trainset ID
            EVC6_MMICurrentTrainData.MMI_M_ALT_DEM = 0;                                     // No alternative train data available
            EVC6_MMICurrentTrainData.TrainSetCaptions = new List<string>(paramEvc6FixedTrainsetCaptions);
            EVC6_MMICurrentTrainData.DataElements = new List<DataElement>();                // No train data elements
            EVC6_MMICurrentTrainData.SetWithoutSending();

            // Send EVC 10
            Send_EVC10_MMIEchoedTrainData_FixedDataEntry(pool, paramEvc6FixedTrainsetCaptions);
        }

        /// <summary>
        /// Description: Perform SoM in SR mode, Level 1
        /// Used in:
        ///     Step 1 in TC-ID: 5.2 in 10.2 Screen Layout: Layers
        ///     Step 5 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 5 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        ///     Step 2 in TC-ID: 15.2.4 in 20.2.4 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (L1->L0, L0->L1)
        ///     Step 2 in TC-ID: 17.2.1 in 22.2.1 Planning Area-Layering: PASP and PA Distance scale
        ///     Step 2 in TC-ID: 17.2.2 in 22.2.2 Planning Area-Layering: Display information when PA data is empty
        ///     Step 10 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        public static void Perform_SoM_in_SR_mode_Level_1(SignalPool pool)
        {
            Display_Driver_ID_Window(pool);
            Set_Driver_ID(pool, "1234");
            Send_SB_Mode(pool);
            ShowInstruction(pool, "Enter and confirm Driver ID");

            Request_Brake_Test(pool);
            ShowInstruction(pool, "Perform Brake Test");

            Display_Level_Window(pool);
            ShowInstruction(pool, "Select and enter Level 1");

            Display_Main_Window_with_Start_button_not_enabled(pool);
            ShowInstruction(pool, @"Press ‘Train data’ button");

            Display_Fixed_Train_Data_Window(pool);
            ShowInstruction(pool, @"Perform the following actions on the DMI: " + Environment.NewLine + Environment.NewLine +
                                    "1. Enter and confirm value in each input field." + Environment.NewLine +
                                    "2. Press ‘Yes’ button.");

            Display_Train_data_validation_Window(pool);
            ShowInstruction(pool, @"Perform the following actions on the DMI: " + Environment.NewLine + Environment.NewLine +
                                    "1. Press ‘Yes’ button." + Environment.NewLine +
                                    "2. Confirmed the selected value by pressing the input field.");

            Display_TRN_Window(pool);
            ShowInstruction(pool, "Enter and confirm Train Running Number");

            Display_Main_Window_with_Start_button_enabled(pool);
            ShowInstruction(pool, @"Press ‘Start’ button");

            Send_SR_Mode_Ack(pool);
            ShowInstruction(pool, "Press and hold DMI Sub Area C1");

            Send_SR_Mode(pool);
            Finished_SoM_Default_Window(pool);
        }

        /// <summary>
        /// Description: Initialise power in DMI
        /// Used in:
        ///     Anywhere where DMI is re-started
        /// </summary>
        public static void Start_ATP()
        {
            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.VersionInfo;
            EVC0_MMIStartATP.Send();
            EVC0_MMIStartATP.Evc0Type = EVC0_MMIStartATP.EVC0Type.GoToIdle;
            EVC0_MMIStartATP.Send();
        }

        /// <summary>
        /// Description: Complete start of mission
        /// Used in:
        ///     Step 5 in TC-ID: 1.3.1 in 6.3.1 Performance of the new selection language
        ///     Step 10 in TC-ID: 35.2 in 38.2 NTC System Status Messages
        /// </summary>
        public static void Complete_start_of_mission(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Open the Settings window
        /// Used in:
        ///     Step 1 in TC-ID: 22.27 in 27.27
        /// </summary>
        public static void Open_the_Settings_window(SignalPool pool)
        {
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = standardFlags;
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = 4;
            EVC30_MMIRequestEnable.Send();
        }

        /// <summary>
        /// Description: Close the Settings window
        /// Used in:
        ///     Step 6 in TC-ID: 1.6 in 6.6 Adjustment of Sound Volume
        ///     Step 11 in TC-ID: 3.1 in 8.1 DMI language selection: Configurable at least eight langauges
        /// </summary>
        public static void Close_the_Settings_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Power on the system and activate cabin
        /// Used in:
        ///     Step 1 in TC-ID: 1.8 in 6.8 Accleration/Decleration interval -4.0m/s2 to +4.0 m/s2
        ///     Step 1 in TC-ID: 15.4.2 in 20.5.2 Building Texts: Brake test in Progress!
        ///     Step 1 in TC-ID: 17.4.17 in 22.4.17 PA Track Condition: First symbol prevails over the next coming symbol
        ///     Step 1 in TC-ID: 29.1 in 29.1 UTC time and offset time(by driver)
        /// </summary>
        public static void Power_on_the_system_and_activate_cabin(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: End of Test
        /// Used in:
        ///     Step 5 in TC-ID: 1.8 in 6.8 Accleration/Decleration interval -4.0m/s2 to +4.0 m/s2
        ///     Step 5 in TC-ID: 17.12 in Handle at least 31 PA Gradient Profile Segments
        /// </summary>
        public static void End_of_Test(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform SoM in SR mode, Level 2
        /// Used in:
        ///     Step 1 in TC-ID: 1.9 in 6.9 Performance of ETCS-DMI: Data handling
        ///     Step 1 in TC-ID: 1.10 in 6.10 Performance of ETCS-DMI Data Processing
        ///     Step 3 in TC-ID: 18.1.1.1.1 in 23.1.1.1.1 Concise Visualization
        ///     Step 2 in TC-ID: 18.1.1.1.2 in 23.1.1.1.2 Verbose Visualization
        /// </summary>
        public static void Perform_SoM_in_SR_mode_Level_2(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform SoM to Level 1 in SR mode
        /// Used in:
        ///     Step 1 in TC-ID: 1.11 in 6.11 Response Time with Up-Type Button
        ///     Step 1 in TC-ID: 21.1.1 in Sound S1 - Over Speed
        ///     Step 1 in TC-ID: 36.1 in Sound S1 - Driving too fast
        ///     Step 1 in TC-ID: 36.2 in Sound S3 - End of Intervention
        ///     Step 1 in TC-ID: 36.3.1 in 39.3.1 Restrictive Target with Speed Monitoring in Full Supervision Mode
        ///     Step 1 in TC-ID: 36.3.2 in 39.3.2 Restrictive Target with Movement Authority Changed in Full Supervision Mode
        ///     Step 1 in TC-ID: 36.3.3 in 39.3.3 Restrictive Target with Speed Monitoring in Limited Supervision Mode
        ///     Step 1 in TC-ID: 36.3.4 in 39.3.4 Restrictive Target with Movement Authority Changed in Limited Supervision Mode
        /// </summary>
        public static void Perform_SoM_to_Level_1_in_SR_mode(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Confirm all value of each input field.Then, press ‘Yes’ button
        /// Used in:
        ///     Step 2 in TC-ID: 2.6 in 7.6 Safety related Data Entry
        ///     Step 5 in TC-ID: 2.6 in 7.6 Safety related Data Entry
        ///     Step 14 in TC-ID: 2.6 in 7.6 Safety related Data Entry
        /// </summary>
        public static void Confirm_all_value_of_each_input_field_Then_press_Yes_button(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Deativate and activate the cabin
        /// Used in:
        ///     Step 6 in TC-ID: 5.10 in 10.10 Screen Layout: Button States
        ///     Step 8 in TC-ID: 5.10 in 10.10 Screen Layout: Button States
        ///     Step 11 in TC-ID: 5.10 in 10.10 Screen Layout: Button States
        /// </summary>
        public static void Deativate_and_activate_the_cabin(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Pass BG1 with Pkt 12,21 and 27
        /// Used in:
        ///     Step 14 in TC-ID: 5.10 in 10.10 Screen Layout: Button States
        ///     Step 2 in TC-ID: 18.4.1 in 23.4.1 Geographical Position: General presentation
        /// </summary>
        public static void Pass_BG1_with_Pkt_12_21_and_27(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Deactivate and activate cabin
        /// Used in:
        ///     Step 17 in TC-ID: 5.10 in 10.10 Screen Layout: Button States
        ///     Step 19 in TC-ID: 5.10 in 10.10 Screen Layout: Button States
        /// </summary>
        public static void Deactivate_and_activate_cabin(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Confirm entered data by pressing an input field
        /// Used in:
        ///     Step 3 in TC-ID: 9.1 in Data Validation Window for Flexible train data entry window
        ///     Step 8 in TC-ID: 9.1 in Data Validation Window for Flexible train data entry window
        ///     Step 3 in TC-ID: 9.2 in 14.2 Data Validation Window for Fixed train data entry window
        ///     Step 8 in TC-ID: 9.2 in 14.2 Data Validation Window for Fixed train data entry window
        ///     Step 11 in TC-ID: 10.2 in 15.2.1 State 'ST05': General Appearance
        ///     Step 11 in TC-ID: 22.6.4.1 in 27.6.4.1
        ///     Step 11 in TC-ID: 22.6.6.1 in 27.6.6.1
        ///     Step 3 in TC-ID: 22.22.4  in 27.22.4 Brake percentage validation window
        ///     Step 8 in TC-ID: 22.22.4  in 27.22.4 Brake percentage validation window
        ///     Step 11 in TC-ID: 22.27.2 in 27.27.2 ‘Set VBC’ Validation Window
        ///     Step 3 in TC-ID: 22.28.2 in 27.28.2 ‘Remove VBC’ Validation Window
        /// </summary>
        public static void Confirm_entered_data_by_pressing_an_input_field(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform the following procedure,Enter and confirm all data in Train data window.Press ‘Yes’ button
        /// Used in:
        ///     Step 4 in TC-ID: 9.1 in Data Validation Window for Flexible train data entry window
        ///     Step 4 in TC-ID: 9.2 in 14.2 Data Validation Window for Fixed train data entry window
        /// </summary>
        public static void
            Perform_the_following_procedure_Enter_and_confirm_all_data_in_Train_data_window_Press_Yes_button(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform the following procedure,Press ‘Train data’ button.Enter and confirm all data in Train data window.Press ‘Yes’ button
        /// Used in:
        ///     Step 6 in TC-ID: 9.1 in Data Validation Window for Flexible train data entry window
        ///     Step 6 in TC-ID: 9.2 in 14.2 Data Validation Window for Fixed train data entry window
        /// </summary>
        public static void
            Perform_the_following_procedure_Press_Train_data_button_Enter_and_confirm_all_data_in_Train_data_window_Press_Yes_button(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Re-establish communication between ETCS onboard and DMI
        /// Used in:
        ///     Step 10 in TC-ID: 9.1 in Data Validation Window for Flexible train data entry window
        ///     Step 10 in TC-ID: 9.2 in 14.2 Data Validation Window for Fixed train data entry window
        ///     Step 12 in TC-ID: 18.7 in 23.7 Tunnel stopping area track condition
        ///     Step 13 in TC-ID: 22.6.4.1 in 27.6.4.1
        ///     Step 13 in TC-ID: 22.6.6.1 in 27.6.6.1
        ///     Step 10 in TC-ID: 22.22.4  in 27.22.4 Brake percentage validation window
        ///     Step 13 in TC-ID: 22.27.2 in 27.27.2 ‘Set VBC’ Validation Window
        ///     Step 13 in TC-ID: 22.28.2 in 27.28.2 ‘Remove VBC’ Validation Window
        /// </summary>
        public static void Re_establish_communication_between_ETCS_onboard_and_DMI(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Use the test script file 10_2_b.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 4MMI_Q_TEXT = 716
        /// Used in:
        ///     Step 3 in TC-ID: 10.2 in 15.2.1 State 'ST05': General Appearance
        ///     Step 7 in TC-ID: 10.2 in 15.2.1 State 'ST05': General Appearance
        ///     Step 10 in TC-ID: 10.2 in 15.2.1 State 'ST05': General Appearance
        ///     Step 17 in TC-ID: 10.2 in 15.2.1 State 'ST05': General Appearance
        /// </summary>
        public static void Use_the_test_script_file_10_2_b_xml_to_send_EVC_8_withMMI_Q_TEXT_CRITERIA_4MMI_Q_TEXT_716(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Use the test script file 10_2_a.xml to send EVC-8 withMMI_Q_TEXT_CRITERIA = 3 MMI_Q_TEXT = 716
        /// Used in:
        ///     Step 6 in TC-ID: 10.2 in 15.2.1 State 'ST05': General Appearance
        ///     Step 9 in TC-ID: 10.2 in 15.2.1 State 'ST05': General Appearance
        ///     Step 13 in TC-ID: 10.2 in 15.2.1 State 'ST05': General Appearance
        ///     Step 16 in TC-ID: 10.2 in 15.2.1 State 'ST05': General Appearance
        /// </summary>
        public static void Use_the_test_script_file_10_2_a_xml_to_send_EVC_8_withMMI_Q_TEXT_CRITERIA_3_MMI_Q_TEXT_716(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Repeat action step 2 with Validate Train Data window
        /// Used in:
        ///     Step 9 in TC-ID: 10.2.2 in 15.2.2 State 'ST05': Main window and windows in main menu.
        ///     Step 13 in TC-ID: 10.2.2 in 15.2.2 State 'ST05': Main window and windows in main menu.
        /// </summary>
        public static void Repeat_action_step_2_with_Validate_Train_Data_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train forward passing BG1
        /// Used in:
        ///     Step 7 in TC-ID: 10.2.5 in 15.2.5 State 'ST05': Special window and windows in the special menu
        ///     Step 3 in TC-ID: 12.7.1 in 17.7.1 Release Speed: At Sub-area B2 and B6
        ///     Step 11 in TC-ID: 15.1.3 in 20.1.3 Mode Symbols in Sub-Area B7 for OS, UN mode
        ///     Step 1 in TC-ID: 15.1.6 in 20.1.6 Mode Symbols for LS mode
        ///     Step 3 in TC-ID: 17.1.1 in 22.1.1 Planning Area: General Appearance
        ///     Step 2 in TC-ID: 17.3 in 22.3 Planning Area: PA Distance Scale
        ///     Step 2 in TC-ID: 17.5.1 in 22.5.1 PA Gradient Profile:  General appearance
        ///     Step 1 in TC-ID: 18.3 in 23.3 Reversing Allowance: Sub-Area C6
        ///     Step 5 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void Drive_train_forward_passing_BG1(SignalPool pool)
        {
            
        }

        /// <summary>
        /// Description: Perform the following procedure;Enter VBC Code ‘65536’Confirm entered data by pressing an input field.Press ‘Yes’ button.Press ‘Yes’ button (on keypad)
        /// Used in:
        ///     Step 26 in TC-ID: 10.2.6 in 15.2.6 State 'ST05': Settings window and windows in setting menu
        ///     Step 30 in TC-ID: 10.2.6 in 15.2.6 State 'ST05': Settings window and windows in setting menu
        /// </summary>
        public static void
            Perform_the_following_procedureEnter_VBC_Code_65536Confirm_entered_data_by_pressing_an_input_field_Press_Yes_button_Press_Yes_button_on_keypad(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Power on the system and activate the cabin
        /// Used in:
        ///     Step 1 in TC-ID: 12.2.2 in 17.2.2 Speed Dial: Display Train maxinum speed
        ///     Step 1 in TC-ID: 22.5.4  in 27.5.4 Level Selection window: 8 STMs handling
        ///     Step 1 in TC-ID: 29.2 in 29.2 UTC time and offset time(by using EVC-3)
        ///     Step 1 in TC-ID: 35.2 in 38.2 NTC System Status Messages
        /// </summary>
        public static void Power_on_the_system_and_activate_the_cabin(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Increase the train speed to 46 km/h
        /// Used in:
        ///     Step 4 in TC-ID: 12.3.2 in 17.3.2 Speed Pointer: Colour of speed pointer in FS mode
        ///     Step 4 in TC-ID: 12.3.3 in 17.3.3 Speed Pointer: Colour of speed pointer in SR mode
        /// </summary>
        public static void Increase_the_train_speed_to_46_kmh(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train with speed = 5 km/h
        /// Used in:
        ///     Step 7 in TC-ID: 12.3.2 in 17.3.2 Speed Pointer: Colour of speed pointer in FS mode
        ///     Step 3 in TC-ID: 12.3.5 in 17.3.5 Speed Pointer: Colour of speed pointer in RV mode
        /// </summary>
        public static void Drive_the_train_with_speed_5_kmh(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train forward with speed = 100 km/h
        /// Used in:
        ///     Step 1 in TC-ID: 12.3.4 in 17.3.4 Speed Pointer: Colour of speed pointer in UN mode
        ///     Step 2 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        /// </summary>
        public static void Drive_the_train_forward_with_speed_100_kmh(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Increase the train speed to 101 km/h
        /// Used in:
        ///     Step 2 in TC-ID: 12.3.4 in 17.3.4 Speed Pointer: Colour of speed pointer in UN mode
        ///     Step 3 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        /// </summary>
        public static void Increase_the_train_speed_to_101_kmh(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Increase the train speed to 105 km/h.Note: dV_warning_max is defined in chapter 3 of [SUBSET-026]
        /// Used in:
        ///     Step 3 in TC-ID: 12.3.4 in 17.3.4 Speed Pointer: Colour of speed pointer in UN mode
        ///     Step 4 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        /// </summary>
        public static void
            Increase_the_train_speed_to_105_kmh_Note_dV_warning_max_is_defined_in_chapter_3_of_SUBSET_026(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Increase the train speed to 106 km/h
        /// Used in:
        ///     Step 4 in TC-ID: 12.3.4 in 17.3.4 Speed Pointer: Colour of speed pointer in UN mode
        ///     Step 5 in TC-ID: 12.3.7 in 17.3.7 Speed Pointer: Colour of speed pointer in LS mode
        /// </summary>
        public static void Increase_the_train_speed_to_106_kmh(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train forward pass BG1.Then stop the train
        /// Used in:
        ///     Step 1 in TC-ID: 12.3.5 in 17.3.5 Speed Pointer: Colour of speed pointer in RV mode
        ///     Step 1 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 1 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        ///     Step 1 in TC-ID: 14.3 in 19.3 Toggling function: Additional Configuration ‘TIMER’
        ///     Step 1 in TC-ID: 12.7.4 in 17.7.4 Release Speed Digital: Release speed removal when received an invalid value of EVC-1 or EVC-7
        ///     Step 1 in TC-ID: 13.1.8 in 18.1.8 Distance to Target Bar: Maximum digit of Distance to Target Digital
        ///     Step 1 in TC-ID: 15.4 in 20.4.1 Building Texts: General
        ///     Step 2 in TC-ID: 17.5.2 in 22.5.2 PA Gradient Profile:  Display of many PA Gradient Profile
        ///     Step 2 in TC-ID: 17.5.4 in 22.5.4 PA Gradient Profile:  Invalid Information Ignoring
        ///     Step 1 in TC-ID: 17.9.12 in 22.9.12 Hide PA Function with the occupied by system outside ETCS
        /// </summary>
        public static void Drive_the_train_forward_pass_BG1_Then_stop_the_train(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Increase the train speed to 31 km/h
        /// Used in:
        ///     Step 3 in TC-ID: 12.3.6 in 17.3.6 Speed Pointer: Colour of speed pointer in SH mode
        ///     Step 3 in TC-ID: 12.3.8 in 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
        /// </summary>
        public static void Increase_the_train_speed_to_31_kmh(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Increase the train speed to 35 km/h.Note: dV_warning_max is defined in chapter 3 of [SUBSET-026]
        /// Used in:
        ///     Step 4 in TC-ID: 12.3.6 in 17.3.6 Speed Pointer: Colour of speed pointer in SH mode
        ///     Step 4 in TC-ID: 12.3.8 in 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
        /// </summary>
        public static void
            Increase_the_train_speed_to_35_kmh_Note_dV_warning_max_is_defined_in_chapter_3_of_SUBSET_026(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Increase the train speed to 36 km/h
        /// Used in:
        ///     Step 5 in TC-ID: 12.3.6 in 17.3.6 Speed Pointer: Colour of speed pointer in SH mode
        ///     Step 5 in TC-ID: 12.3.8 in 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
        /// </summary>
        public static void Increase_the_train_speed_to_36_kmh(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train forward pass BG1. Then, press an acknowledgement of OS mode in sub-area C1
        /// Used in:
        ///     Step 1 in TC-ID: 12.3.8 in 17.3.8 Speed Pointer: Colour of speed pointer in OS mode
        ///     Step 8 in TC-ID: 14.6 in 19.6 Toggling function: Additional Configuration ‘TIMER’
        /// </summary>
        public static void Drive_the_train_forward_pass_BG1_Then_press_an_acknowledgement_of_OS_mode_in_sub_area_C1(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train forward with speed = 10 km/h
        /// Used in:
        ///     Step 1 in TC-ID: 12.3.9 in 17.3.9 Speed Pointer: Colour of speed pointer in SB mode and NL mode
        ///     Step 7 in TC-ID: 14.6 in 19.6 Toggling function: Additional Configuration ‘TIMER’
        /// </summary>
        public static void Drive_the_train_forward_with_speed_10_kmh(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train forward pass BG1
        /// Used in:
        ///     Step 1 in TC-ID: 12.3.10 in 17.3.10 Speed Pointer: Colour of speed pointer in TR mode and PT mode
        ///     Step 3 in TC-ID: 13.1.1 in 18.1.1 Distance to Target  Bar: General Appearance
        ///     Step 1 in TC-ID: 13.1.7.1 in 18.1.7.1 Distance to Target: Appearance of Distance to Target in FS mode
        ///     Step 1 in TC-ID: 14.4 in 19.4 Toggling function: Default state reset for Configuration ‘ON’ when communication loss
        ///     Step 1 in TC-ID: 14.5 in 19.5 Toggling function: Default state reset for Configuration ‘OFF’ when communication loss
        ///     Step 3 in TC-ID: 15.2.4 in 20.2.4 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (L1->L0, L0->L1)
        ///     Step 1 in TC-ID: 15.2.5 in 20.2.5 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (L2->L3, L3->L2)
        ///     Step 1 in TC-ID: 15.6 in 20.6.1 Level Crossing “not protected” Indication: General Display
        ///     Step 2 in TC-ID: 17.5.3 in 22.5.3 PA Gradient Profile:  Information updating
        ///     Step 3 in TC-ID: 17.10.3 in 22.10.3 Zoom PA Function with Scale Down
        ///     Step 3 in TC-ID: 17.10.4 in 22.10.4 Zoom PA Function with the communication loss between ETCS Onboard and DMI
        ///     Step 3 in TC-ID: 17.12 in Handle at least 31 PA Gradient Profile Segments
        ///     Step 1 in TC-ID: 18.4.3 in 23.4.3 Geographical Position: Additional requirements
        ///     Step 4 in TC-ID: 18.4.3 in 23.4.3 Geographical Position: Additional requirements
        ///     Step 1 in TC-ID: 18.7 in 23.7 Tunnel stopping area track condition
        ///     Step 1 in TC-ID: 20.6 in 25.6 Driver’s Action: Geographical Information
        ///     Step 14 in TC-ID: 22.10 in 27.10 Special window
        /// </summary>
        public static void Drive_the_train_forward_pass_BG1(SignalPool pool)
        {

        }

        /// <summary>
        /// Description: Force the train into TR mode by moving the train forward to position of EOA
        /// Used in:
        ///     Step 2 in TC-ID: 12.3.10 in 17.3.10 Speed Pointer: Colour of speed pointer in TR mode and PT mode
        ///     Step 6 in TC-ID: 15.1.1 in 20.1.1 Mode Symbols in Sub-Area B7 for SB, SR, FS, TR, PT, SH, NL and SF mode
        /// </summary>
        public static void Force_train_forward_overpassing_EOA(SignalPool pool)
        {

        }

        /// <summary>
        /// Description: Drive the train forward pass BG1 with speed = 30km/h
        /// Used in:
        ///     Step 3 in TC-ID: 12.5.1 in 17.5.1 Colouring Scheme of Circular Speed Gauge (SB mode, SR mode and FS mode for CSM Speed Monitoring)
        ///     Step 1 in TC-ID: 12.5.2 in 17.5.2 Colour Scheme of Circular Speed Gauge (TSM of Speed Monitoring)
        ///     Step 1 in TC-ID: 12.5.3 in 17.5.3 Colouring Scheme of Circular Speed Gauge (FS mode for RSM Speed Monitoring)
        /// </summary>
        public static void Drive_the_train_forward_pass_BG1_with_speed_30kmh(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Driver performs SoM to SR mode
        /// Used in:
        ///     Step 2 in TC-ID: 12.7.1 in 17.7.1 Release Speed: At Sub-area B2 and B6
        ///     Step 2 in TC-ID: 13.1.1 in 18.1.1 Distance to Target  Bar: General Appearance
        /// </summary>
        public static void Driver_performs_SoM_to_SR_mode(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train forward
        /// Used in:
        ///     Step 1 in TC-ID: 12.12 in 17.12 Slip Indication
        ///     Step 7 in TC-ID: 15.5.1 in 20.5.1 Adhesion factor: General appearance
        ///     Step 4 in TC-ID: 15.5.2 in 20.5.3 Adhesion factor: Controlled data packet from ETCS Onboard
        ///     Step 5 in TC-ID: 17.1.4 in 22.1.4 Planning Area forced into background by TAF Question box
        ///     Step 2 in TC-ID: 17.6.1 in 22.6.1 PA Speed Profile Discontinuity: Display in sub-area D6 and D7
        ///     Step 4 in TC-ID: 17.7.1 in 22.7.1 PA Speed Profile (PASP): Display in sub-area D7 and D8
        ///     Step 6 in TC-ID: 17.10.1 in 22.10.1 Zoom PA Function: General appearance
        ///     Step 17 in TC-ID: 22.10 in 27.10 Special window
        ///     Step 3 in TC-ID: 35.2 in 38.2 NTC System Status Messages
        ///     Step 3 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        public static void Drive_the_train_forward(SignalPool pool)
        {

        }

        /// <summary>
        /// Description: Drive the train forward with speed = 140 km/h
        /// Used in:
        ///     Step 2 in TC-ID: 12.12 in 17.12 Slip Indication
        ///     Step 2 in TC-ID: 12.13 in 17.13 Slide Indication
        ///     Step 2 in TC-ID: 12.14 in 17.14 Slip and Slide are configure to 1 at the same time
        ///     Step 2 in TC-ID: 12.15 in 17.15 Slip and Slide are configure to 0 at the same time
        /// </summary>
        public static void Drive_the_train_forward_with_speed_140_kmh(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Driver the train forward
        /// Used in:
        ///     Step 1 in TC-ID: 12.13 in 17.13 Slide Indication
        ///     Step 1 in TC-ID: 12.14 in 17.14 Slip and Slide are configure to 1 at the same time
        ///     Step 1 in TC-ID: 12.15 in 17.15 Slip and Slide are configure to 0 at the same time
        /// </summary>
        public static void Driver_the_train_forward(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train follow the permitted speed
        /// Used in:
        ///     Step 4 in TC-ID: 13.1.1 in 18.1.1 Distance to Target  Bar: General Appearance
        ///     Step 5 in TC-ID: 17.11 in 22.11 Handle at least 31 PA Speed Profile Segments
        /// </summary>
        public static void Drive_the_train_follow_the_permitted_speed(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Driver performs SoM to SR mode, level 1
        /// Used in:
        ///     Step 2 in TC-ID: 13.1.4 in 18.1.4 Distance to Target Digital when the communication between ETCS  Onboard and DMI is lost
        ///     Step 2 in TC-ID: 17.1.2 in 22.1.2 Planning Area is suppressed in Level 1 and OS mode
        ///     Step 2 in TC-ID: 17.1.3 in 22.1.3 Planning Area displays according to configuration in OS and SR mode.
        ///     Step 1 in TC-ID: 17.8 in 22.8 PA Indication Marker: Sub-Area D7
        ///     Step 2 in TC-ID: 17.10.2 in 22.10.2 Zoom PA Function with Scale Up
        ///     Step 2 in TC-ID: 17.10.3 in 22.10.3 Zoom PA Function with Scale Down
        ///     Step 2 in TC-ID: 17.10.4 in 22.10.4 Zoom PA Function with the communication loss between ETCS Onboard and DMI
        ///     Step 2 in TC-ID: 17.11 in 22.11 Handle at least 31 PA Speed Profile Segments
        ///     Step 2 in TC-ID: 17.12 in Handle at least 31 PA Gradient Profile Segments
        /// </summary>
        public static void Driver_performs_SoM_to_SR_mode_level_1(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train forward passing BG2
        /// Used in:
        ///     Step 4 in TC-ID: 13.1.4 in 18.1.4 Distance to Target Digital when the communication between ETCS  Onboard and DMI is lost
        ///     Step 1 in TC-ID: 15.1.2 in 20.1.2 Mode Symbols in Sub-Area B7 for RV and SL mode
        ///     Step 6 in TC-ID: 15.5.1 in 20.5.1 Adhesion factor: General appearance
        ///     Step 3 in TC-ID: 15.5.2 in 20.5.3 Adhesion factor: Controlled data packet from ETCS Onboard
        ///     Step 4 in TC-ID: 17.1.1 in 22.1.1 Planning Area: General Appearance
        ///     Step 4 in TC-ID: 15.1.6 in 20.1.6
        /// </summary>
        public static void Drive_train_forward_passing_BG2(SignalPool pool)
        {
            
        }

        /// <summary>
        /// Description: The train is in reversing area
        /// Used in:
        ///     Step 5 in TC-ID: 13.1.5 in 18.1.5 Distance to Target in RV mode
        ///     Step 3 in TC-ID: 13.1.6 in 18.1.6 Distance to Target Digital in RV mode with special value
        /// </summary>
        public static void The_train_is_in_reversing_area(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Change the direction of train to reverse. Then select and confirm RV mode
        /// Used in:
        ///     Step 7 in TC-ID: 13.1.5 in 18.1.5 Distance to Target in RV mode
        ///     Step 5 in TC-ID: 13.1.6 in 18.1.6 Distance to Target Digital in RV mode with special value
        /// </summary>
        public static void Change_the_direction_of_train_to_reverse_Then_select_and_confirm_RV_mode(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Continue to drive the train forward.Then, stop the train
        /// Used in:
        ///     Step 2 in TC-ID: 13.1.7.1 in 18.1.7.1 Distance to Target: Appearance of Distance to Target in FS mode
        ///     Step 3 in TC-ID: 13.1.7.1 in 18.1.7.1 Distance to Target: Appearance of Distance to Target in FS mode
        ///     Step 6 in TC-ID: 13.1.7.1 in 18.1.7.1 Distance to Target: Appearance of Distance to Target in FS mode
        ///     Step 2 in TC-ID: 13.1.7.2 in 18.1.7.2 Distance to Target: Appearance of Distance to Target in OS mode
        ///     Step 3 in TC-ID: 13.1.7.2 in 18.1.7.2 Distance to Target: Appearance of Distance to Target in OS mode
        ///     Step 4 in TC-ID: 13.1.7.2 in 18.1.7.2 Distance to Target: Appearance of Distance to Target in OS mode
        ///     Step 3 in TC-ID: 13.1.7.3 in 18.1.7.3 Distance to Target: Appearance of Distance to Target in SB, SR and SH mode
        ///     Step 4 in TC-ID: 13.1.7.3 in 18.1.7.3 Distance to Target: Appearance of Distance to Target in SB, SR and SH mode
        /// </summary>
        public static void Continue_to_drive_the_train_forward_Then_stop_the_train(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: This step is to clear the symbol ‘ST01’ after verification of the previous step.Use the test script file 13_2_1_b.xml to send EVC-8 with,MMI_Q_TEXT_CRITERIA = 4MMI_I_TEXT = 1
        /// Used in:
        ///     Step 10 in TC-ID: 13.2.1 in 18.2.1 General Appearance
        ///     Step 12 in TC-ID: 13.2.1 in 18.2.1 General Appearance
        /// </summary>
        public static void
            This_step_is_to_clear_the_symbol_ST01_after_verification_of_the_previous_step_Use_the_test_script_file_13_2_1_b_xml_to_send_EVC_8_with_MMI_Q_TEXT_CRITERIA_4MMI_I_TEXT_1(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform the following procedure,Chage the train direction to reversePress the symbol in sub-area C1
        /// Used in:
        ///     Step 2 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 2 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        ///     Step 2 in TC-ID: 14.3 in 19.3 Toggling function: Additional Configuration ‘TIMER’
        /// </summary>
        public static void
            Perform_the_following_procedure_Chage_the_train_direction_to_reversePress_the_symbol_in_sub_area_C1(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform the following procedure,De-activate Cabin AActivate Cabin A
        /// Used in:
        ///     Step 4 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 4 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        ///     Step 4 in TC-ID: 14.3 in 19.3 Toggling function: Additional Configuration ‘TIMER’
        /// </summary>
        public static void Perform_the_following_procedure_De_activate_Cabin_AActivate_Cabin_A(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform the following procedure,Press ‘Spec’ button.Press ‘SR speed/disาtance’ button.Enter and confirm the following data,SR speed = 40 km/hSR distance = 300 m
        /// Used in:
        ///     Step 6 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 6 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        /// </summary>
        public static void
            Perform_the_following_procedure_Press_Spec_button_Press_SR_speeddisาtance_button_Enter_and_confirm_the_following_data_SR_speed_40_kmhSR_distance_300_m(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train forward pass BG2 with speed = 20km/h (or below permitted speed).Then, press an area C1 for acknowledgement
        /// Used in:
        ///     Step 9 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 9 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        /// </summary>
        public static void
            Drive_the_train_forward_pass_BG2_with_speed_20kmh_or_below_permitted_speed_Then_press_an_area_C1_for_acknowledgement(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Stop the train.Press, at least twice, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified
        /// Used in:
        ///     Step 10 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 17 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 10 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        ///     Step 17 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        ///     Step 19 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        ///     Step 21 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        ///     Step 10 in TC-ID: 14.3 in 19.3 Toggling function: Additional Configuration ‘TIMER’
        ///     Step 17 in TC-ID: 14.3 in 19.3 Toggling function: Additional Configuration ‘TIMER’
        ///     Step 19 in TC-ID: 14.3 in 19.3 Toggling function: Additional Configuration ‘TIMER’
        ///     Step 21 in TC-ID: 14.3 in 19.3 Toggling function: Additional Configuration ‘TIMER’
        /// </summary>
        public static void
            Stop_the_train_Press_at_least_twice_on_area_A1_A4_and_area_B_respectively_Then_continue_to_drive_the_train_forward_after_expected_result_verified(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train forward pass BG3
        /// Used in:
        ///     Step 11 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 6 in TC-ID: 15.2.4 in 20.2.4 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (L1->L0, L0->L1)
        ///     Step 4 in TC-ID: 17.5.3 in 22.5.3 PA Gradient Profile:  Information updating
        /// </summary>
        public static void Drive_the_train_forward_pass_BG3(SignalPool pool)
        {

        }

        /// <summary>
        /// Description: Stop the train.Then, press, at least twice, on area A1-A4, and area B respectively.Then, continue to drive the train forward after expected result verified
        /// Used in:
        ///     Step 12 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 19 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 21 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 12 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        ///     Step 12 in TC-ID: 14.3 in 19.3 Toggling function: Additional Configuration ‘TIMER’
        /// </summary>
        public static void
            Stop_the_train_Then_press_at_least_twice_on_area_A1_A4_and_area_B_respectively_Then_continue_to_drive_the_train_forward_after_expected_result_verified(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train forward pass BG4. Then, acknowledge OS mode by press a sub-area C1
        /// Used in:
        ///     Step 13 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 13 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        /// </summary>
        public static void Drive_the_train_forward_pass_BG4_Then_acknowledge_OS_mode_by_press_a_sub_area_C1(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Stop the train.Press the speedometer once
        /// Used in:
        ///     Step 14 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 14 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        /// </summary>
        public static void Stop_the_train_Press_the_speedometer_once(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train forward pass BG5. Then, acknowledge LS mode by press a sub-area C1
        /// Used in:
        ///     Step 16 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 16 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        ///     Step 16 in TC-ID: 14.3 in 19.3 Toggling function: Additional Configuration ‘TIMER’
        /// </summary>
        public static void Drive_the_train_forward_pass_BG5_Then_acknowledge_LS_mode_by_press_a_sub_area_C1(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train pass through EOA
        /// Used in:
        ///     Step 18 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 18 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        ///     Step 18 in TC-ID: 14.3 in 19.3 Toggling function: Additional Configuration ‘TIMER’
        /// </summary>
        public static void Drive_the_train_pass_through_EOA(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Acknowledge TR mode by press a sub-area C1
        /// Used in:
        ///     Step 20 in TC-ID: 14.1 in 19.1 Toggling function: Additional Configuration ‘OFF’ (Default)
        ///     Step 20 in TC-ID: 14.2 in 19.2 Toggling function: Additional Configuration ‘ON’
        ///     Step 20 in TC-ID: 14.3 in 19.3 Toggling function: Additional Configuration ‘TIMER’
        /// </summary>
        public static void Acknowledge_TR_mode_by_press_a_sub_area_C1(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Re-establish communication between ETCS onboard and DMI (in 1 second).Note: Stopwatch is required for accuracy of test result
        /// Used in:
        ///     Step 26 in TC-ID: 14.3 in 19.3 Toggling function: Additional Configuration ‘TIMER’
        ///     Step 4 in TC-ID: 14.4 in 19.4 Toggling function: Default state reset for Configuration ‘ON’ when communication loss
        ///     Step 4 in TC-ID: 14.5 in 19.5 Toggling function: Default state reset for Configuration ‘OFF’ when communication loss
        /// </summary>
        public static void
            Re_establish_communication_between_ETCS_onboard_and_DMI_in_1_second_Note_Stopwatch_is_required_for_accuracy_of_test_result(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train forward pass BG2.Then, stop the train
        /// Used in:
        ///     Step 2 in TC-ID: 14.4 in 19.4 Toggling function: Default state reset for Configuration ‘ON’ when communication loss
        ///     Step 2 in TC-ID: 14.5 in 19.5 Toggling function: Default state reset for Configuration ‘OFF’ when communication loss
        /// </summary>
        public static void Drive_the_train_forward_pass_BG2_Then_stop_the_train(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Select and confirm Level 0
        /// Used in:
        ///     Step 3 in TC-ID: 15.1.3 in 20.1.3 Mode Symbols in Sub-Area B7 for OS, UN mode
        ///     Step 1 in TC-ID: 15.2.1 in 20.2.1 ETCS Level in Sub-Area C8: Level 0 and Level 1 by driver selection
        /// </summary>
        public static void Select_and_confirm_Level_0(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Acknowledge OS mode
        /// Used in:
        ///     Step 12 in TC-ID: 15.1.3 in 20.1.3 Mode Symbols in Sub-Area B7 for OS, UN mode
        ///     Step 18 in TC-ID: 17.9.11 in 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
        /// </summary>
        public static void Acknowledge_OS_mode(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train pass a distance to level transition
        /// Used in:
        ///     Step 5 in TC-ID: 15.2.4 in 20.2.4 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (L1->L0, L0->L1)
        ///     Step 7 in TC-ID: 15.2.4 in 20.2.4 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (L1->L0, L0->L1)
        /// </summary>
        public static void Drive_the_train_pass_a_distance_to_level_transition(SignalPool pool)
        {

        }

        /// <summary>
        /// Description: Drive the train forward pass a distance to level transition
        /// Used in:
        ///     Step 2 in TC-ID: 15.2.5 in 20.2.5 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (L2->L3, L3->L2)
        ///     Step 4 in TC-ID: 15.2.5 in 20.2.5 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (L2->L3, L3->L2)
        /// </summary>
        public static void Drive_the_train_forward_pass_a_distance_to_level_transition(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train forward pass BG2
        /// Used in:
        ///     Step 3 in TC-ID: 15.2.5 in 20.2.5 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (L2->L3, L3->L2)
        ///     Step 2 in TC-ID: 15.6 in 20.6.1 Level Crossing “not protected” Indication: General Display
        ///     Step 3 in TC-ID: 17.5.3 in 22.5.3 PA Gradient Profile:  Information updating
        ///     Step 4 in TC-ID: 17.12 in Handle at least 31 PA Gradient Profile Segments
        /// </summary>
        public static void Drive_the_train_forward_pass_BG2(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train forward with 30 km/h then pass BG0 with level transition announcement
        /// Used in:
        ///     Step 2 in TC-ID: 15.2.10 in 20.2.10 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (L0->LNTC)
        ///     Step 2 in TC-ID: 15.2.11 in 20.2.11 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (LNTC ->L1)
        /// </summary>
        public static void Drive_the_train_forward_with_30_kmh_then_pass_BG0_with_level_transition_announcement(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Pass the level transition acknowledgement area
        /// Used in:
        ///     Step 3 in TC-ID: 15.2.10 in 20.2.10 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (L0->LNTC)
        ///     Step 3 in TC-ID: 15.2.11 in 20.2.11 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (LNTC ->L1)
        ///     Step 3 in TC-ID: 15.2.12 in 20.2.12 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (LNTC ->L2)
        ///     Step 3 in TC-ID: 15.2.13 in 20.2.13 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (LNTC ->L3)
        /// </summary>
        public static void Pass_the_level_transition_acknowledgement_area(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Pass BG1 at level transition border
        /// Used in:
        ///     Step 5 in TC-ID: 15.2.10 in 20.2.10 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (L0->LNTC)
        ///     Step 5 in TC-ID: 15.2.11 in 20.2.11 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (LNTC ->L1)
        ///     Step 5 in TC-ID: 15.2.12 in 20.2.12 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (LNTC ->L2)
        ///     Step 5 in TC-ID: 15.2.13 in 20.2.13 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (LNTC ->L3)
        /// </summary>
        public static void Pass_BG1_at_level_transition_border(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform the following action:         Power on the systemActivate the cabin Perform start of mission to ATB STM mode , Level NTC
        /// Used in:
        ///     Step 1 in TC-ID: 15.2.11 in 20.2.11 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (LNTC ->L1)
        ///     Step 1 in TC-ID: 15.2.12 in 20.2.12 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (LNTC ->L2)
        ///     Step 1 in TC-ID: 15.2.13 in 20.2.13 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (LNTC ->L3)
        /// </summary>
        public static void
            Perform_the_following_action_Power_on_the_systemActivate_the_cabin_Perform_start_of_mission_to_ATB_STM_mode_Level_NTC(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train forward with 30 km/h and then pass BG0 with level transition announcement
        /// Used in:
        ///     Step 2 in TC-ID: 15.2.12 in 20.2.12 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (LNTC ->L2)
        ///     Step 2 in TC-ID: 15.2.13 in 20.2.13 ETCS Level: ETCS Level Transitions by receiving data packet from ETCS Onboard (LNTC ->L3)
        /// </summary>
        public static void Drive_the_train_forward_with_30_kmh_and_then_pass_BG0_with_level_transition_announcement(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform the following procedure,Press ‘Special’ button.Press ‘Adhesion’ button.Select and confirm ‘Non slippery rail’ button
        /// Used in:
        ///     Step 5 in TC-ID: 15.5.1 in 20.5.1 Adhesion factor: General appearance
        ///     Step 5 in TC-ID: 15.5.2 in 20.5.3 Adhesion factor: Controlled data packet from ETCS Onboard
        /// </summary>
        public static void
            Perform_the_following_procedure_Press_Special_button_Press_Adhesion_button_Select_and_confirm_Non_slippery_rail_button(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Continue to drive the train forward with speed below permitted speed
        /// Used in:
        ///     Step 6 in TC-ID: 15.6 in 20.6.1 Level Crossing “not protected” Indication: General Display
        ///     Step 7 in TC-ID: 15.6 in 20.6.1 Level Crossing “not protected” Indication: General Display
        ///     Step 8 in TC-ID: 15.6 in 20.6.1 Level Crossing “not protected” Indication: General Display
        /// </summary>
        public static void Continue_to_drive_the_train_forward_with_speed_below_permitted_speed(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Use the test script file 15_6_2_d.xml to send EVC-33 withMMI_Q_TRACKCOND_STEP = 4MMI_NID_TRACKCOND = 0
        /// Used in:
        ///     Step 4 in TC-ID: 15.6.2 in 20.6.2 Level Crossing “not protected” Indication: Packet Handling
        ///     Step 6 in TC-ID: 15.6.2 in 20.6.2 Level Crossing “not protected” Indication: Packet Handling
        ///     Step 8 in TC-ID: 15.6.2 in 20.6.2 Level Crossing “not protected” Indication: Packet Handling
        /// </summary>
        public static void
            Use_the_test_script_file_15_6_2_d_xml_to_send_EVC_33_withMMI_Q_TRACKCOND_STEP_4MMI_NID_TRACKCOND_0(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform SoM to SR mode, level 2.Then, drive the train forward with speed = 30km/h
        /// Used in:
        ///     Step 1 in TC-ID: 16.1 in 21.1 TAF Question Box
        ///     Step 1 in TC-ID: 16.2 in 21.2 TAF Question Box: Display of TAF Question box instead of the planning area information
        /// </summary>
        public static void Perform_SoM_to_SR_mode_level_2_Then_drive_the_train_forward_with_speed_30kmh(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Received information from RBC
        /// Used in:
        ///     Step 2 in TC-ID: 16.1 in 21.1 TAF Question Box
        ///     Step 2 in TC-ID: 16.2 in 21.2 TAF Question Box: Display of TAF Question box instead of the planning area information
        ///     Step 4 in TC-ID: 17.1.4 in 22.1.4 Planning Area forced into background by TAF Question box
        ///     Step 2 in TC-ID: 17.10.1 in 22.10.1 Zoom PA Function: General appearance
        ///     Step 7 in TC-ID: 17.10.1 in 22.10.1 Zoom PA Function: General appearance
        /// </summary>
        public static void Received_information_from_RBC(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Acknowledge OS mode by press at area C1
        /// Used in:
        ///     Step 3 in TC-ID: 16.1 in 21.1 TAF Question Box
        ///     Step 3 in TC-ID: 16.2 in 21.2 TAF Question Box: Display of TAF Question box instead of the planning area information
        ///     Step 3 in TC-ID: 17.10.1 in 22.10.1 Zoom PA Function: General appearance
        /// </summary>
        public static void Acknowledge_OS_mode_by_press_at_area_C1(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Received information from RBC.Then, stop the train
        /// Used in:
        ///     Step 5 in TC-ID: 16.1 in 21.1 TAF Question Box
        ///     Step 4 in TC-ID: 16.2 in 21.2 TAF Question Box: Display of TAF Question box instead of the planning area information
        /// </summary>
        public static void Received_information_from_RBC_Then_stop_the_train(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform SoM to SR mode, level 1
        /// Used in:
        ///     Step 2 in TC-ID: 17.1.1 in 22.1.1 Planning Area: General Appearance
        ///     Step 2 in TC-ID: 17.9.1 in 22.9.1 Hide PA Function: General appearance
        ///     Step 2 in TC-ID: 17.9.11 in 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
        ///     Step 5 in TC-ID: 17.9.11 in 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
        ///     Step 10 in TC-ID: 17.9.11 in 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
        ///     Step 16 in TC-ID: 17.9.11 in 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
        /// </summary>
        public static void Perform_SoM_to_SR_mode_level_1(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Touch main area D
        /// Used in:
        ///     Step 4 in TC-ID: 17.1.2 in 22.1.2 Planning Area is suppressed in Level 1 and OS mode
        ///     Step 6 in TC-ID: 17.1.2 in 22.1.2 Planning Area is suppressed in Level 1 and OS mode
        /// </summary>
        public static void Touch_main_area_D(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train forward with speed = 20 km/h
        /// Used in:
        ///     Step 1 in TC-ID: 17.4.1 in 22.4.1 PA Track Condition: Non stopping area in Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.1 in 22.4.1 PA Track Condition: Non stopping area in Sub-Area D2 and B3
        ///     Step 8 in TC-ID: 17.4.1 in 22.4.1 PA Track Condition: Non stopping area in Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.2 in 22.4.2 PA Track Condition: Sound Horn in Sub-Area D2 and B3
        ///     Step 8 in TC-ID: 17.4.2 in 22.4.2 PA Track Condition: Sound Horn in Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.3 in 22.4.3 PA Track Condition:  Lower Pantograph in Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.3 in 22.4.3 PA Track Condition:  Lower Pantograph in Sub-Area D2 and B3
        ///     Step 8 in TC-ID: 17.4.3 in 22.4.3 PA Track Condition:  Lower Pantograph in Sub-Area D2 and B3
        ///     Step 11 in TC-ID: 17.4.3 in 22.4.3 PA Track Condition:  Lower Pantograph in Sub-Area D2 and B3
        ///     Step 13 in TC-ID: 17.4.3 in 22.4.3 PA Track Condition:  Lower Pantograph in Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.4 in 22.4.4 PA Track Condition: Radio Hole in Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.4 in 22.4.4 PA Track Condition: Radio Hole in Sub-Area D2 and B3
        ///     Step 8 in TC-ID: 17.4.4 in 22.4.4 PA Track Condition: Radio Hole in Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.5 in 22.4.5 PA Track Condition: Air Tightness in Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.5 in 22.4.5 PA Track Condition: Air Tightness in Sub-Area D2 and B3
        ///     Step 8 in TC-ID: 17.4.5 in 22.4.5 PA Track Condition: Air Tightness in Sub-Area D2 and B3
        ///     Step 13 in TC-ID: 17.4.5 in 22.4.5 PA Track Condition: Air Tightness in Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.6 in 22.4.6 PA Track Condition: Switch off regenerative brake in Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.6 in 22.4.6 PA Track Condition: Switch off regenerative brake in Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.7 in PA Track Condition: Switch off eddy current brake in Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.7 in PA Track Condition: Switch off eddy current brake in Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.8 in 22.4.8 PA Track Condition: Switch off magnetic shoe brake in Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.8 in 22.4.8 PA Track Condition: Switch off magnetic shoe brake in Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.9 in 22.4.9 PA Track Condition: Switch off main power switch in Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.9 in 22.4.9 PA Track Condition: Switch off main power switch in Sub-Area D2 and B3
        ///     Step 9 in TC-ID: 17.4.9 in 22.4.9 PA Track Condition: Switch off main power switch in Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.10 in 22.4.10 PA Track Condition: Change of traction system, not fitted Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.10 in 22.4.10 PA Track Condition: Change of traction system, not fitted Sub-Area D2 and B3
        ///     Step 8 in TC-ID: 17.4.10 in 22.4.10 PA Track Condition: Change of traction system, not fitted Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.11 in 22.4.11 PA Track Condition: Change of traction system, AC 25 KV 50 Hz Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.11 in 22.4.11 PA Track Condition: Change of traction system, AC 25 KV 50 Hz Sub-Area D2 and B3
        ///     Step 8 in TC-ID: 17.4.11 in 22.4.11 PA Track Condition: Change of traction system, AC 25 KV 50 Hz Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.12 in 22.4.12 PA Track Condition: Change of traction system, AC 15 KV 16.7 Hz Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.12 in 22.4.12 PA Track Condition: Change of traction system, AC 15 KV 16.7 Hz Sub-Area D2 and B3
        ///     Step 8 in TC-ID: 17.4.12 in 22.4.12 PA Track Condition: Change of traction system, AC 15 KV 16.7 Hz Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.13 in 22.4.13 PA Track Condition: Change of traction system, DC 3kV Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.13 in 22.4.13 PA Track Condition: Change of traction system, DC 3kV Sub-Area D2 and B3
        ///     Step 8 in TC-ID: 17.4.13 in 22.4.13 PA Track Condition: Change of traction system, DC 3kV Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.14 in 22.4.14 PA Track Condition: Change of traction system, DC 1.5 kV Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.14 in 22.4.14 PA Track Condition: Change of traction system, DC 1.5 kV Sub-Area D2 and B3
        ///     Step 8 in TC-ID: 17.4.14 in 22.4.14 PA Track Condition: Change of traction system, DC 1.5 kV Sub-Area D2 and B3
        ///     Step 1 in TC-ID: 17.4.15 in 22.4.15 PA Track Condition: Change of traction system, DC 600/750 V Sub-Area D2 and B3
        ///     Step 6 in TC-ID: 17.4.15 in 22.4.15 PA Track Condition: Change of traction system, DC 600/750 V Sub-Area D2 and B3
        ///     Step 8 in TC-ID: 17.4.15 in 22.4.15 PA Track Condition: Change of traction system, DC 600/750 V Sub-Area D2 and B3
        /// </summary>
        public static void Drive_the_train_forward_with_speed_20_kmh(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train forward pass BG0 with MA and Track descriptionPkt 12,21 and 27
        /// Used in:
        ///     Step 2 in TC-ID: 17.4.1 in 22.4.1 PA Track Condition: Non stopping area in Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.2 in 22.4.2 PA Track Condition: Sound Horn in Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.3 in 22.4.3 PA Track Condition:  Lower Pantograph in Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.4 in 22.4.4 PA Track Condition: Radio Hole in Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.5 in 22.4.5 PA Track Condition: Air Tightness in Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.6 in 22.4.6 PA Track Condition: Switch off regenerative brake in Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.7 in PA Track Condition: Switch off eddy current brake in Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.8 in 22.4.8 PA Track Condition: Switch off magnetic shoe brake in Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.9 in 22.4.9 PA Track Condition: Switch off main power switch in Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.10 in 22.4.10 PA Track Condition: Change of traction system, not fitted Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.11 in 22.4.11 PA Track Condition: Change of traction system, AC 25 KV 50 Hz Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.12 in 22.4.12 PA Track Condition: Change of traction system, AC 15 KV 16.7 Hz Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.13 in 22.4.13 PA Track Condition: Change of traction system, DC 3kV Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.14 in 22.4.14 PA Track Condition: Change of traction system, DC 1.5 kV Sub-Area D2 and B3
        ///     Step 2 in TC-ID: 17.4.15 in 22.4.15 PA Track Condition: Change of traction system, DC 600/750 V Sub-Area D2 and B3
        /// </summary>
        public static void Drive_the_train_forward_pass_BG0_with_MA_and_Track_descriptionPkt_12_21_and_27(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Stop the train when the track condition symbol has been removed from sub-area B3
        /// Used in:
        ///     Step 9 in TC-ID: 17.4.1 in 22.4.1 PA Track Condition: Non stopping area in Sub-Area D2 and B3
        ///     Step 9 in TC-ID: 17.4.2 in 22.4.2 PA Track Condition: Sound Horn in Sub-Area D2 and B3
        ///     Step 14 in TC-ID: 17.4.3 in 22.4.3 PA Track Condition:  Lower Pantograph in Sub-Area D2 and B3
        ///     Step 9 in TC-ID: 17.4.4 in 22.4.4 PA Track Condition: Radio Hole in Sub-Area D2 and B3
        ///     Step 9 in TC-ID: 17.4.7 in PA Track Condition: Switch off eddy current brake in Sub-Area D2 and B3
        ///     Step 9 in TC-ID: 17.4.8 in 22.4.8 PA Track Condition: Switch off magnetic shoe brake in Sub-Area D2 and B3
        ///     Step 12 in TC-ID: 17.4.9 in 22.4.9 PA Track Condition: Switch off main power switch in Sub-Area D2 and B3
        ///     Step 9 in TC-ID: 17.4.10 in 22.4.10 PA Track Condition: Change of traction system, not fitted Sub-Area D2 and B3
        ///     Step 9 in TC-ID: 17.4.11 in 22.4.11 PA Track Condition: Change of traction system, AC 25 KV 50 Hz Sub-Area D2 and B3
        ///     Step 9 in TC-ID: 17.4.12 in 22.4.12 PA Track Condition: Change of traction system, AC 15 KV 16.7 Hz Sub-Area D2 and B3
        ///     Step 9 in TC-ID: 17.4.14 in 22.4.14 PA Track Condition: Change of traction system, DC 1.5 kV Sub-Area D2 and B3
        ///     Step 9 in TC-ID: 17.4.15 in 22.4.15 PA Track Condition: Change of traction system, DC 600/750 V Sub-Area D2 and B3
        /// </summary>
        public static void Stop_the_train_when_the_track_condition_symbol_has_been_removed_from_sub_area_B3(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Stop the train when the track condition symbol has been removed
        /// Used in:
        ///     Step 14 in TC-ID: 17.4.5 in 22.4.5 PA Track Condition: Air Tightness in Sub-Area D2 and B3
        ///     Step 9 in TC-ID: 17.4.6 in 22.4.6 PA Track Condition: Switch off regenerative brake in Sub-Area D2 and B3
        ///     Step 9 in TC-ID: 17.4.13 in 22.4.13 PA Track Condition: Change of traction system, DC 3kV Sub-Area D2 and B3
        /// </summary>
        public static void Stop_the_train_when_the_track_condition_symbol_has_been_removed(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Driver the train forward with speed = 40 km/h
        /// Used in:
        ///     Step 8 in TC-ID: 17.4.6 in 22.4.6 PA Track Condition: Switch off regenerative brake in Sub-Area D2 and B3
        ///     Step 8 in TC-ID: 17.4.7 in PA Track Condition: Switch off eddy current brake in Sub-Area D2 and B3
        /// </summary>
        public static void Driver_the_train_forward_with_speed_40_kmh(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Driver the train up to 40 km/h
        /// Used in:
        ///     Step 8 in TC-ID: 17.4.8 in 22.4.8 PA Track Condition: Switch off magnetic shoe brake in Sub-Area D2 and B3
        ///     Step 11 in TC-ID: 17.4.9 in 22.4.9 PA Track Condition: Switch off main power switch in Sub-Area D2 and B3
        /// </summary>
        public static void Driver_the_train_up_to_40_kmh(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train up to 20 km/h
        /// Used in:
        ///     Step 1 in TC-ID: 17.4.16 in 22.4.16 PA Track Condition: 30 PA Track Conditions
        ///     Step 3 in TC-ID: 17.4.17 in 22.4.17 PA Track Condition: First symbol prevails over the next coming symbol
        /// </summary>
        public static void Drive_the_train_up_to_20_kmh(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Pass BG0 with MA and Track descriptionPkt 12,21 and 27
        /// Used in:
        ///     Step 2 in TC-ID: 17.4.16 in 22.4.16 PA Track Condition: 30 PA Track Conditions
        ///     Step 4 in TC-ID: 17.4.17 in 22.4.17 PA Track Condition: First symbol prevails over the next coming symbol
        /// </summary>
        public static void Pass_BG0_with_MA_and_Track_descriptionPkt_12_21_and_27(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Continue driving with 20 Km/h
        /// Used in:
        ///     Step 4 in TC-ID: 17.4.16 in 22.4.16 PA Track Condition: 30 PA Track Conditions
        ///     Step 5 in TC-ID: 17.4.16 in 22.4.16 PA Track Condition: 30 PA Track Conditions
        ///     Step 6 in TC-ID: 17.4.16 in 22.4.16 PA Track Condition: 30 PA Track Conditions
        /// </summary>
        public static void Continue_driving_with_20_Kmh(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Simulate loss-communication between ETCS onboard and DMI
        /// Used in:
        ///     Step 7 in TC-ID: 17.4.16 in 22.4.16 PA Track Condition: 30 PA Track Conditions
        ///     Step 10 in TC-ID: 18.4.3 in 23.4.3 Geographical Position: Additional requirements
        ///     Step 11 in TC-ID: 18.7 in 23.7 Tunnel stopping area track condition
        /// </summary>
        public static void Simulate_loss_communication_between_ETCS_onboard_and_DMI(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform SoM to L1, SR mode
        /// Used in:
        ///     Step 2 in TC-ID: 17.4.17 in 22.4.17 PA Track Condition: First symbol prevails over the next coming symbol
        ///     Step 2 in TC-ID: 29.1 in 29.1 UTC time and offset time(by driver)
        ///     Step 2 in TC-ID: 29.2 in 29.2 UTC time and offset time(by using EVC-3)
        ///     Step 2 in TC-ID: 29.3 in 29.3 UTC time and offset time(By VAP acting as NTP server)
        /// </summary>
        public static void Perform_SoM_to_L1_SR_mode(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Activate cabin A. Driver performs SoM to SR mode, level 1
        /// Used in:
        ///     Step 1 in TC-ID: 17.5.1 in 22.5.1 PA Gradient Profile:  General appearance
        ///     Step 1 in TC-ID: 17.9.10 (Default Configuration) in 22.9.10 Hide PA Function with the communication loss between ETCS Onboard and DMI
        ///     Step 1 in TC-ID: 26.1 in 1 Introduction
        /// </summary>
        public static void Activate_cabin_A_Driver_performs_SoM_to_SR_mode_level_1(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Activate cabin A. Then  perform SoM to SR mode, level 1
        /// Used in:
        ///     Step 1 in TC-ID: 17.5.2 in 22.5.2 PA Gradient Profile:  Display of many PA Gradient Profile
        ///     Step 1 in TC-ID: 17.5.3 in 22.5.3 PA Gradient Profile:  Information updating
        ///     Step 1 in TC-ID: 17.5.4 in 22.5.4 PA Gradient Profile:  Invalid Information Ignoring
        /// </summary>
        public static void Activate_cabin_A_Then_perform_SoM_to_SR_mode_level_1(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Continue to drive the train forward
        /// Used in:
        ///     Step 5 in TC-ID: 17.5.2 in 22.5.2 PA Gradient Profile:  Display of many PA Gradient Profile
        ///     Step 3 in TC-ID: 17.6.1 in 22.6.1 PA Speed Profile Discontinuity: Display in sub-area D6 and D7
        ///     Step 5 in TC-ID: 17.7.1 in 22.7.1 PA Speed Profile (PASP): Display in sub-area D7 and D8
        ///     Step 6 in TC-ID: 17.7.1 in 22.7.1 PA Speed Profile (PASP): Display in sub-area D7 and D8
        ///     Step 7 in TC-ID: 17.7.1 in 22.7.1 PA Speed Profile (PASP): Display in sub-area D7 and D8
        ///     Step 8 in TC-ID: 17.7.1 in 22.7.1 PA Speed Profile (PASP): Display in sub-area D7 and D8
        ///     Step 37 in TC-ID: 17.11 in 22.11 Handle at least 31 PA Speed Profile Segments
        /// </summary>
        public static void Continue_to_drive_the_train_forward(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train forward pass BG1.Then, Stop the train
        /// Used in:
        ///     Step 1 in TC-ID: 17.6.2 in 22.6.2 PA Speed Profile Discontinuity: Information updating
        ///     Step 2 in TC-ID: 17.7.2 in 22.7.2 PA Speed Profile (PASP): Information updating
        /// </summary>
        public static void Drive_the_train_forward_pass_BG1_Then_Stop_the_train(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Use the test script file 17_8_d.xml to send EVC-1 with,MMI_O_IML = 1000120000 (1200m)Note: The result of test script file may interrupted by ATP-CU, need to execute test script file repeatly to see the result
        /// Used in:
        ///     Step 6 in TC-ID: 17.8 in 22.8 PA Indication Marker: Sub-Area D7
        ///     Step 7 in TC-ID: 17.8 in 22.8 PA Indication Marker: Sub-Area D7
        ///     Step 8 in TC-ID: 17.8 in 22.8 PA Indication Marker: Sub-Area D7
        /// </summary>
        public static void
            Use_the_test_script_file_17_8_d_xml_to_send_EVC_1_with_MMI_O_IML_1000120000_1200mNote_The_result_of_test_script_file_may_interrupted_by_ATP_CU_need_to_execute_test_script_file_repeatly_to_see_the_result(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train forward with speed = 40 km/h pass BG1
        /// Used in:
        ///     Step 3 in TC-ID: 17.9.1 in 22.9.1 Hide PA Function: General appearance
        ///     Step 2 in TC-ID: 17.9.2 in 22.9.2 Hide PA Function is configured ‘ON’ with reboot DMI
        ///     Step 3 in TC-ID: 17.9.3 in 22.9.3 Hide PA Function is configured ‘OFF’ with reboot DMI
        ///     Step 3 in TC-ID: 17.9.4 in 22.9.4 Hide PA Function is configured ‘STORED’ with reboot DMI
        ///     Step 3 in TC-ID: 17.9.6 in 22.9.5 Hide PA Function is configured ‘ON’ with reactivated Cabin A
        ///     Step 3 in TC-ID: 17.9.5 in 22.9.6 Hide PA Function is configured ‘TIMER’ with reboot DMI
        ///     Step 3 in TC-ID: 17.9.7 in 22.9.7 Hide PA Function is configured ‘OFF’ with reactivated Cabin A
        ///     Step 3 in TC-ID: 17.9.8 in 22.9.8 Hide PA Function is configured ‘STORED’ with reactivated Cabin A
        ///     Step 3 in TC-ID: 17.9.9 in 22.9.9 Hide PA Function is configured ‘TIMER’ with reactivated Cabin A
        ///     Step 2 in TC-ID: 17.9.10 (Default Configuration) in 22.9.10 Hide PA Function with the communication loss between ETCS Onboard and DMI
        /// </summary>
        public static void Drive_the_train_forward_with_speed_40_kmh_pass_BG1(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Activate cabin A and Perform SoM to SR mode, Level 1
        /// Used in:
        ///     Step 1 in TC-ID: 17.9.2 in 22.9.2 Hide PA Function is configured ‘ON’ with reboot DMI
        ///     Step 2 in TC-ID: 17.9.3 in 22.9.3 Hide PA Function is configured ‘OFF’ with reboot DMI
        ///     Step 2 in TC-ID: 17.9.4 in 22.9.4 Hide PA Function is configured ‘STORED’ with reboot DMI
        ///     Step 2 in TC-ID: 17.9.6 in 22.9.5 Hide PA Function is configured ‘ON’ with reactivated Cabin A
        ///     Step 6 in TC-ID: 17.9.6 in 22.9.5 Hide PA Function is configured ‘ON’ with reactivated Cabin A
        ///     Step 2 in TC-ID: 17.9.5 in 22.9.6 Hide PA Function is configured ‘TIMER’ with reboot DMI
        ///     Step 2 in TC-ID: 17.9.7 in 22.9.7 Hide PA Function is configured ‘OFF’ with reactivated Cabin A
        ///     Step 6 in TC-ID: 17.9.7 in 22.9.7 Hide PA Function is configured ‘OFF’ with reactivated Cabin A
        ///     Step 2 in TC-ID: 17.9.8 in 22.9.8 Hide PA Function is configured ‘STORED’ with reactivated Cabin A
        ///     Step 6 in TC-ID: 17.9.8 in 22.9.8 Hide PA Function is configured ‘STORED’ with reactivated Cabin A
        ///     Step 2 in TC-ID: 17.9.9 in 22.9.9 Hide PA Function is configured ‘TIMER’ with reactivated Cabin A
        ///     Step 6 in TC-ID: 17.9.9 in 22.9.9 Hide PA Function is configured ‘TIMER’ with reactivated Cabin A
        /// </summary>
        public static void Activate_cabin_A_and_Perform_SoM_to_SR_mode_Level_1(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Turn off power of DMI
        /// Used in:
        ///     Step 4 in TC-ID: 17.9.2 in 22.9.2 Hide PA Function is configured ‘ON’ with reboot DMI
        ///     Step 7 in TC-ID: 17.9.3 in 22.9.3 Hide PA Function is configured ‘OFF’ with reboot DMI
        ///     Step 5 in TC-ID: 17.9.4 in 22.9.4 Hide PA Function is configured ‘STORED’ with reboot DMI
        ///     Step 5 in TC-ID: 17.9.5 in 22.9.6 Hide PA Function is configured ‘TIMER’ with reboot DMI
        /// </summary>
        public static void Turn_off_power_of_DMI(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Turn on power of DMI
        /// Used in:
        ///     Step 5 in TC-ID: 17.9.2 in 22.9.2 Hide PA Function is configured ‘ON’ with reboot DMI
        ///     Step 8 in TC-ID: 17.9.3 in 22.9.3 Hide PA Function is configured ‘OFF’ with reboot DMI
        ///     Step 6 in TC-ID: 17.9.4 in 22.9.4 Hide PA Function is configured ‘STORED’ with reboot DMI
        ///     Step 6 in TC-ID: 17.9.5 in 22.9.6 Hide PA Function is configured ‘TIMER’ with reboot DMI
        /// </summary>
        public static void Turn_on_power_of_DMI(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Power On the system
        /// Used in:
        ///     Step 1 in TC-ID: 17.9.3 in 22.9.3 Hide PA Function is configured ‘OFF’ with reboot DMI
        ///     Step 1 in TC-ID: 17.9.4 in 22.9.4 Hide PA Function is configured ‘STORED’ with reboot DMI
        ///     Step 1 in TC-ID: 17.9.6 in 22.9.5 Hide PA Function is configured ‘ON’ with reactivated Cabin A
        ///     Step 1 in TC-ID: 17.9.5 in 22.9.6 Hide PA Function is configured ‘TIMER’ with reboot DMI
        ///     Step 1 in TC-ID: 17.9.7 in 22.9.7 Hide PA Function is configured ‘OFF’ with reactivated Cabin A
        ///     Step 1 in TC-ID: 17.9.8 in 22.9.8 Hide PA Function is configured ‘STORED’ with reactivated Cabin A
        ///     Step 1 in TC-ID: 17.9.9 in 22.9.9 Hide PA Function is configured ‘TIMER’ with reactivated Cabin A
        /// </summary>
        public static void Power_On_the_system(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Stop the train. Then, deactivate cabin A
        /// Used in:
        ///     Step 5 in TC-ID: 17.9.6 in 22.9.5 Hide PA Function is configured ‘ON’ with reactivated Cabin A
        ///     Step 5 in TC-ID: 17.9.7 in 22.9.7 Hide PA Function is configured ‘OFF’ with reactivated Cabin A
        ///     Step 5 in TC-ID: 17.9.8 in 22.9.8 Hide PA Function is configured ‘STORED’ with reactivated Cabin A
        ///     Step 5 in TC-ID: 17.9.9 in 22.9.9 Hide PA Function is configured ‘TIMER’ with reactivated Cabin A
        /// </summary>
        public static void Stop_the_train_Then_deactivate_cabin_A(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train forward with speed = 40 km/h pass BG2
        /// Used in:
        ///     Step 7 in TC-ID: 17.9.6 in 22.9.5 Hide PA Function is configured ‘ON’ with reactivated Cabin A
        ///     Step 7 in TC-ID: 17.9.7 in 22.9.7 Hide PA Function is configured ‘OFF’ with reactivated Cabin A
        ///     Step 7 in TC-ID: 17.9.8 in 22.9.8 Hide PA Function is configured ‘STORED’ with reactivated Cabin A
        ///     Step 7 in TC-ID: 17.9.9 in 22.9.9 Hide PA Function is configured ‘TIMER’ with reactivated Cabin A
        /// </summary>
        public static void Drive_the_train_forward_with_speed_40_kmh_pass_BG2(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Simulate the communication loss between ETCS onboard and DMI
        /// Used in:
        ///     Step 3 in TC-ID: 17.9.10 (Default Configuration) in 22.9.10 Hide PA Function with the communication loss between ETCS Onboard and DMI
        ///     Step 9 in TC-ID: 17.9.10 (Default Configuration) in 22.9.10 Hide PA Function with the communication loss between ETCS Onboard and DMI
        /// </summary>
        public static void Simulate_the_communication_loss_between_ETCS_onboard_and_DMI(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Deactive and reacitvate the cabin A
        /// Used in:
        ///     Step 4 in TC-ID: 17.9.11 in 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
        ///     Step 9 in TC-ID: 17.9.11 in 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
        ///     Step 15 in TC-ID: 17.9.11 in 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
        /// </summary>
        public static void Deactive_and_reacitvate_the_cabin_A(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Stop the train and then press Hide PA button
        /// Used in:
        ///     Step 8 in TC-ID: 17.9.11 in 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
        ///     Step 14 in TC-ID: 17.9.11 in 22.9.11 Hide PA Function configured ‘STORED’ with re-activate cabin
        /// </summary>
        public static void Stop_the_train_and_then_press_Hide_PA_button(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Driver presses ‘Hide’ button at position top-right of planning area in sub-area D14
        /// Used in:
        ///     Step 8 in TC-ID: 17.10.2 in 22.10.2 Zoom PA Function with Scale Up
        ///     Step 8 in TC-ID: 17.10.3 in 22.10.3 Zoom PA Function with Scale Down
        /// </summary>
        public static void Driver_presses_Hide_button_at_position_top_right_of_planning_area_in_sub_area_D14(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train forward with speed below the permitted speed
        /// Used in:
        ///     Step 4 in TC-ID: 18.1.1.1.1 in 23.1.1.1.1 Concise Visualization
        ///     Step 3 in TC-ID: 18.1.1.1.2 in 23.1.1.1.2 Verbose Visualization
        /// </summary>
        public static void Drive_the_train_forward_with_speed_below_the_permitted_speed(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Receives FS MA and track description from RBC
        /// Used in:
        ///     Step 5 in TC-ID: 18.1.1.1.1 in 23.1.1.1.1 Concise Visualization
        ///     Step 4 in TC-ID: 18.1.1.1.2 in 23.1.1.1.2 Verbose Visualization
        /// </summary>
        public static void Receives_FS_MA_and_track_description_from_RBC(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Send EVC-32 with,MMI_Q_TRACKCOND_UPDATE = 1MMI_N_TRACKCONDITIONS = 1MMI_NID_TRACKCOND = 3MMI_Q_TRACKCOND_STEP = 4
        /// Used in:
        ///     Step 7 in TC-ID: 18.6.1 in 23.6.1 Visualise of the Track Conditions Symbols
        ///     Step 15 in TC-ID: 18.6.2 in 23.6.2 Maximum of Track Conditions in internal memory
        /// </summary>
        public static void
            Send_EVC_32_with_MMI_Q_TRACKCOND_UPDATE_1MMI_N_TRACKCONDITIONS_1MMI_NID_TRACKCOND_3MMI_Q_TRACKCOND_STEP_4(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Deactivate cabin.Then, simulate loss-communication between ETCS onboard and DMI
        /// Used in:
        ///     Step 44 in TC-ID: 18.6.2 in 23.6.2 Maximum of Track Conditions in internal memory
        ///     Step 13 in TC-ID: 18.7 in 23.7 Tunnel stopping area track condition
        /// </summary>
        public static void Deactivate_cabin_Then_simulate_loss_communication_between_ETCS_onboard_and_DMI(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Activate cabin.Then, re-establish communication between ETCS onboard and DMI
        /// Used in:
        ///     Step 45 in TC-ID: 18.6.2 in 23.6.2 Maximum of Track Conditions in internal memory
        ///     Step 14 in TC-ID: 18.7 in 23.7 Tunnel stopping area track condition
        /// </summary>
        public static void Activate_cabin_Then_re_establish_communication_between_ETCS_onboard_and_DMI(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform the following procedure,Enter Driver IDSelect and confirm Level 1. Note: If Level window is display
        /// Used in:
        ///     Step 7 in TC-ID: 20.1 in 25.1 Driver’s Action: Main window
        ///     Step 18 in TC-ID: 7.1 in 27.2 Main window
        /// </summary>
        public static void
            Perform_the_following_procedure_Enter_Driver_IDSelect_and_confirm_Level_1_Note_If_Level_window_is_display(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform the following procedure,Press ‘Maintenance’ button.Enter the Maintenance window by entering the password same as a value in tag ‘PASS_CODE_MTN’ of the configuration file and confirming the password
        /// Used in:
        ///     Step 4 in TC-ID: 20.4 in 25.4 Driver’s Action: Settings window
        ///     Step 3 in TC-ID: 22.6.2 in 27.6.2 Maintenance window
        ///     Step 6 in TC-ID: 22.6.2 in 27.6.2 Maintenance window
        ///     Step 4 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        /// </summary>
        public static void
            Perform_the_following_procedure_Press_Maintenance_button_Enter_the_Maintenance_window_by_entering_the_password_same_as_a_value_in_tag_PASS_CODE_MTN_of_the_configuration_file_and_confirming_the_password(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train forward with constant speed at 40 km/h
        /// Used in:
        ///     Step 2 in TC-ID: 21.1.1 in Sound S1 - Over Speed
        ///     Step 2 in TC-ID: 36.3.1 in 39.3.1 Restrictive Target with Speed Monitoring in Full Supervision Mode
        /// </summary>
        public static void Drive_the_train_forward_with_constant_speed_at_40_kmh(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform the following procedure,Activate Cabin AEnter Driver ID and perform brake testSelect and confirm Level 1
        /// Used in:
        ///     Step 3 in TC-ID: 22.1.1 in 27.1.1 Sub-Level Window: General appearances
        ///     Step 1 in TC-ID: 34.1.2 in 37.1.2 Flexible Train data entry
        /// </summary>
        public static void
            Perform_the_following_procedure_Activate_Cabin_AEnter_Driver_ID_and_perform_brake_testSelect_and_confirm_Level_1(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Slide out an input field
        /// Used in:
        ///     Step 5 in TC-ID: 22.5.1 in 27.5.1 Level Selection Window: General appearance
        ///     Step 11 in TC-ID: 22.6.1 in 27.6.1 Password window
        ///     Step 4 in TC-ID: 22.6.4.1 in 27.6.4.1
        ///     Step 4 in TC-ID: 22.6.6.1 in 27.6.6.1
        ///     Step 8 in TC-ID: 22.8.2.1 in 27.8.2.1 Radio Network ID window: General appearance
        ///     Step 11 in TC-ID: 22.17 in 27.17.1 Driver ID window: General Display
        ///     Step 11 in TC-ID: 22.18 in Train Running Number window
        ///     Step 5 in TC-ID: 22.19 in 27.19 Language Window
        ///     Step 13 in TC-ID: 22.22.3  in 27.22.3 Brake percentage window
        ///     Step 7 in TC-ID: 22.24 in 27.24 Brightness window
        ///     Step 7 in TC-ID: 22.25 in 27.25 Volume window
        ///     Step 4 in TC-ID: 22.27.2 in 27.27.2 ‘Set VBC’ Validation Window
        ///     Step 9 in TC-ID: 22.28.2 in 27.28.2 ‘Remove VBC’ Validation Window
        /// </summary>
        public static void Slide_out_an_input_field(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Slide back into an input field
        /// Used in:
        ///     Step 6 in TC-ID: 22.5.1 in 27.5.1 Level Selection Window: General appearance
        ///     Step 12 in TC-ID: 22.6.1 in 27.6.1 Password window
        ///     Step 5 in TC-ID: 22.6.4.1 in 27.6.4.1
        ///     Step 5 in TC-ID: 22.6.6.1 in 27.6.6.1
        ///     Step 9 in TC-ID: 22.8.2.1 in 27.8.2.1 Radio Network ID window: General appearance
        ///     Step 11 in TC-ID: 22.11 in 27.11 Adhesion Window
        ///     Step 12 in TC-ID: 22.17 in 27.17.1 Driver ID window: General Display
        ///     Step 12 in TC-ID: 22.18 in Train Running Number window
        ///     Step 6 in TC-ID: 22.19 in 27.19 Language Window
        ///     Step 14 in TC-ID: 22.22.3  in 27.22.3 Brake percentage window
        ///     Step 8 in TC-ID: 22.24 in 27.24 Brightness window
        ///     Step 8 in TC-ID: 22.25 in 27.25 Volume window
        ///     Step 5 in TC-ID: 22.27.2 in 27.27.2 ‘Set VBC’ Validation Window
        ///     Step 10 in TC-ID: 22.28.2 in 27.28.2 ‘Remove VBC’ Validation Window
        /// </summary>
        public static void Slide_back_into_an_input_field(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Slide out ‘Close’ button
        /// Used in:
        ///     Step 12 in TC-ID: 22.5.1 in 27.5.1 Level Selection Window: General appearance
        ///     Step 16 in TC-ID: 22.5.3 in 27.5.3 Level Selection Window: Level Inhibition Window
        ///     Step 3 in TC-ID: 22.22.2  in 27.22.2 Brake test window
        /// </summary>
        public static void Slide_out_Close_button(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Slide back into ‘Close’ button
        /// Used in:
        ///     Step 13 in TC-ID: 22.5.1 in 27.5.1 Level Selection Window: General appearance
        ///     Step 17 in TC-ID: 22.5.3 in 27.5.3 Level Selection Window: Level Inhibition Window
        ///     Step 4 in TC-ID: 22.22.2  in 27.22.2 Brake test window
        /// </summary>
        public static void Slide_back_into_Close_button(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Continue to enter the 6th character
        /// Used in:
        ///     Step 8 in TC-ID: 22.6.1 in 27.6.1 Password window
        ///     Step 9 in TC-ID: 22.8.1.1 in 27.8.1.1
        ///     Step 14 in TC-ID: 22.8.1.1 in 27.8.1.1
        ///     Step 13 in TC-ID: 22.9.1 in 27.9.1 SR Speed/Distance window: General appearance
        ///     Step 14 in TC-ID: 7.3.2 in 27.17.3 Entering Characters
        ///     Step 9 in TC-ID: 22.18 in Train Running Number window
        ///     Step 8 in TC-ID: 22.27.1 in 27.27.1 ‘Set VBC’ Data Entry Window
        ///     Step 8 in TC-ID: 22.28.1 in 27.28.1 ‘Remove VBC’ Data Entry Window
        /// </summary>
        public static void Continue_to_enter_the_6th_character(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform the following procedure,Return to the Setting window by pressing Close’ button.Use test script file 22_6_2_a.xml to disable wheel diameter and doppler by sending EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#29) = 0MMI_Q_REQUEST_ENABLE_64 (#30) = 0
        /// Used in:
        ///     Step 4 in TC-ID: 22.6.2 in 27.6.2 Maintenance window
        ///     Step 7 in TC-ID: 22.6.2 in 27.6.2 Maintenance window
        /// </summary>
        public static void
            Perform_the_following_procedure_Return_to_the_Setting_window_by_pressing_Close_button_Use_test_script_file_22_6_2_a_xml_to_disable_wheel_diameter_and_doppler_by_sending_EVC_30_with_MMI_NID_WINDOW_4MMI_Q_REQUEST_ENABLE_64_29_0MMI_Q_REQUEST_ENABLE_64_30_0(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform action step 2-3 for the ‘1’ to ‘9’ buttons.Note: Press the ‘Del’ button to delete an information when entered data is out of input field range is acceptable
        /// Used in:
        ///     Step 4 in TC-ID: 22.6.3.1 in 27.6.3.1 Wheel diameter window: General apearance
        ///     Step 4 in TC-ID: 22.6.5.1 in 27.6.5.1 Radar window: General appearance
        /// </summary>
        public static void
            Perform_action_step_2_3_for_the_1_to_9_buttons_Note_Press_the_Del_button_to_delete_an_information_when_entered_data_is_out_of_input_field_range_is_acceptable(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Slide out the ‘Yes’ button
        /// Used in:
        ///     Step 15 in TC-ID: 22.6.3.1 in 27.6.3.1 Wheel diameter window: General apearance
        ///     Step 13 in TC-ID: 22.6.5.1 in 27.6.5.1 Radar window: General appearance
        ///     Step 20 in TC-ID: 22.8.1.1 in 27.8.1.1
        ///     Step 19 in TC-ID: 22.9.1 in 27.9.1 SR Speed/Distance window: General appearance
        ///     Step 7 in TC-ID: 22.29.2 in 27.29.2 Fixed Train data window: General appearances
        /// </summary>
        public static void Slide_out_the_Yes_button(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Slide back into the ‘Yes’ button
        /// Used in:
        ///     Step 16 in TC-ID: 22.6.3.1 in 27.6.3.1 Wheel diameter window: General apearance
        ///     Step 14 in TC-ID: 22.6.5.1 in 27.6.5.1 Radar window: General appearance
        ///     Step 21 in TC-ID: 22.8.1.1 in 27.8.1.1
        ///     Step 20 in TC-ID: 22.9.1 in 27.9.1 SR Speed/Distance window: General appearance
        ///     Step 8 in TC-ID: 22.29.2 in 27.29.2 Fixed Train data window: General appearances
        /// </summary>
        public static void Slide_back_into_the_Yes_button(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Open the ‘Wheel diameter’ data entry window from the Settings menu
        /// Used in:
        ///     Step 1 in TC-ID: 22.6.3.2.3.2  in 1 Introduction
        ///     Step 1 in TC-ID: 22.6.3.2.5 in 27.6.3.2.5 ‘Wheel diameter’ Data Checks: Technical Range Checks by Variable Range
        /// </summary>
        public static void Open_the_Wheel_diameter_data_entry_window_from_the_Settings_menu(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: This step is to complete the process of ‘Wheel diameter’:- Press the ‘Yes’ button on the ‘Wheel diameter’ window.- Validate the data in the data validation window
        /// Used in:
        ///     Step 4 in TC-ID: 22.6.3.2.3.2  in 1 Introduction
        ///     Step 5 in TC-ID: 22.6.3.2.5 in 27.6.3.2.5 ‘Wheel diameter’ Data Checks: Technical Range Checks by Variable Range
        /// </summary>
        public static void
            This_step_is_to_complete_the_process_of_Wheel_diameter_Press_the_Yes_button_on_the_Wheel_diameter_window_Validate_the_data_in_the_data_validation_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform the following procedure,Press ‘Wheel diameter’ button.Enter and confirm all data in Wheel diameter window.Press ‘Yes’ button
        /// Used in:
        ///     Step 7 in TC-ID: 22.6.4.1 in 27.6.4.1
        ///     Step 9 in TC-ID: 22.6.4.1 in 27.6.4.1
        /// </summary>
        public static void
            Perform_the_following_procedure_Press_Wheel_diameter_button_Enter_and_confirm_all_data_in_Wheel_diameter_window_Press_Yes_button(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Open the ‘Radar’ data entry window from the Settings menu
        /// Used in:
        ///     Step 1 in TC-ID: 22.6.5.2.3.2  in 1 Introduction
        ///     Step 1 in TC-ID: 22.6.5.2.5 in 27.6.5.2.5 ‘Radar’ Data Checks: Technical Range Checks by Variable Range
        /// </summary>
        public static void Open_the_Radar_data_entry_window_from_the_Settings_menu(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: This step is to complete the process of ‘Radar’:- Press the ‘Yes’ button on the ‘Radar’ window.- Validate the data in the data validation window
        /// Used in:
        ///     Step 4 in TC-ID: 22.6.5.2.3.2  in 1 Introduction
        ///     Step 5 in TC-ID: 22.6.5.2.5 in 27.6.5.2.5 ‘Radar’ Data Checks: Technical Range Checks by Variable Range
        /// </summary>
        public static void
            This_step_is_to_complete_the_process_of_Radar_Press_the_Yes_button_on_the_Radar_window_Validate_the_data_in_the_data_validation_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform the following procedure,Press ‘Radar’ button.Enter and confirm all data in Radar window.Press ‘Yes’ button
        /// Used in:
        ///     Step 7 in TC-ID: 22.6.6.1 in 27.6.6.1
        ///     Step 9 in TC-ID: 22.6.6.1 in 27.6.6.1
        /// </summary>
        public static void
            Perform_the_following_procedure_Press_Radar_button_Enter_and_confirm_all_data_in_Radar_window_Press_Yes_button(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Slide out the ‘Next’ button
        /// Used in:
        ///     Step 3 in TC-ID: 22.7.1 in 27.7.1 Data view window for Flexible Train data entry
        ///     Step 3 in TC-ID: 22.7.2 in 27.7.2 Data view window for Fixed Train data entry
        ///     Step 4 in TC-ID: 22.26 in 27.26 System info window
        /// </summary>
        public static void Slide_out_the_Next_button(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Slide back into the ‘Next’ button
        /// Used in:
        ///     Step 4 in TC-ID: 22.7.1 in 27.7.1 Data view window for Flexible Train data entry
        ///     Step 4 in TC-ID: 22.7.2 in 27.7.2 Data view window for Fixed Train data entry
        ///     Step 5 in TC-ID: 22.26 in 27.26 System info window
        /// </summary>
        public static void Slide_back_into_the_Next_button(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform action step 2-5 for ‘Previous’ button
        /// Used in:
        ///     Step 6 in TC-ID: 22.7.1 in 27.7.1 Data view window for Flexible Train data entry
        ///     Step 6 in TC-ID: 22.7.2 in 27.7.2 Data view window for Fixed Train data entry
        /// </summary>
        public static void Perform_action_step_2_5_for_Previous_button(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform action step 3-4 for the ‘1’ to ‘9’ buttons.Note: Press the ‘Del’ button to delete an information when entered data is out of input field range is acceptable
        /// Used in:
        ///     Step 5 in TC-ID: 22.8.1.1 in 27.8.1.1
        ///     Step 5 in TC-ID: 22.9.1 in 27.9.1 SR Speed/Distance window: General appearance
        ///     Step 7 in TC-ID: 22.13.1 in 27.13.1 Set Clock function: General appearance
        ///     Step 5 in TC-ID: 22.22.3  in 27.22.3 Brake percentage window
        ///     Step 4 in TC-ID: 22.27.1 in 27.27.1 ‘Set VBC’ Data Entry Window
        ///     Step 4 in TC-ID: 22.28.1 in 27.28.1 ‘Remove VBC’ Data Entry Window
        /// </summary>
        public static void
            Perform_action_step_3_4_for_the_1_to_9_buttons_Note_Press_the_Del_button_to_delete_an_information_when_entered_data_is_out_of_input_field_range_is_acceptable(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Continue to enter the new value more than 8 characters
        /// Used in:
        ///     Step 10 in TC-ID: 22.8.1.1 in 27.8.1.1
        ///     Step 15 in TC-ID: 22.8.1.1 in 27.8.1.1
        ///     Step 15 in TC-ID: 7.3.2 in 27.17.3 Entering Characters
        ///     Step 9 in TC-ID: 22.27.1 in 27.27.1 ‘Set VBC’ Data Entry Window
        ///     Step 9 in TC-ID: 22.28.1 in 27.28.1 ‘Remove VBC’ Data Entry Window
        /// </summary>
        public static void Continue_to_enter_the_new_value_more_than_8_characters(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform the following procedure,Select and confirm Level 2.Press ‘Enter RBC data’ button
        /// Used in:
        ///     Step 23 in TC-ID: 22.8.1.1 in 27.8.1.1
        ///     Step 25 in TC-ID: 22.8.1.1 in 27.8.1.1
        /// </summary>
        public static void Perform_the_following_procedure_Select_and_confirm_Level_2_Press_Enter_RBC_data_button(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Confirm an entered data by pressing an input field
        /// Used in:
        ///     Step 14 in TC-ID: 22.8.2.1 in 27.8.2.1 Radio Network ID window: General appearance
        ///     Step 25 in TC-ID: 22.13.1 in 27.13.1 Set Clock function: General appearance
        ///     Step 16 in TC-ID: 22.17 in 27.17.1 Driver ID window: General Display
        /// </summary>
        public static void Confirm_an_entered_data_by_pressing_an_input_field(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Restart OTE and RBC simulator.Then, perform SoM until Level 2 is selected and confirmed
        /// Used in:
        ///     Step 16 in TC-ID: 22.8.3.1 in 27.8.3.1 RBC Contact window: General appearance
        ///     Step 19 in TC-ID: 22.8.3.1 in 27.8.3.1 RBC Contact window: General appearance
        ///     Step 21 in TC-ID: 22.8.3.1 in 27.8.3.1 RBC Contact window: General appearance
        /// </summary>
        public static void Restart_OTE_and_RBC_simulator_Then_perform_SoM_until_Level_2_is_selected_and_confirmed(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Open the ‘SR speed / distance’ data entry window from the Special menu
        /// Used in:
        ///     Step 1 in TC-ID: 22.9.9 in 27.9.9 ‘SR speed / distance’ Data Checks: Technical Range Checks by Data Validity
        ///     Step 1 in TC-ID: 22.9.10 in 27.9.10 ‘SR speed / distance’ Data Checks: Technical Range Checks by Variable Range
        /// </summary>
        public static void Open_the_SR_speed_distance_data_entry_window_from_the_Special_menu(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: This step is to complete the process of ‘SR speed / distance’:- Press the ‘Yes’ button on the ‘SR speed / distance’ window.- Validate the data in the data validation window
        /// Used in:
        ///     Step 10 in TC-ID: 22.9.9 in 27.9.9 ‘SR speed / distance’ Data Checks: Technical Range Checks by Data Validity
        ///     Step 6 in TC-ID: 22.9.10 in 27.9.10 ‘SR speed / distance’ Data Checks: Technical Range Checks by Variable Range
        /// </summary>
        public static void
            This_step_is_to_complete_the_process_of_SR_speed_distance_Press_the_Yes_button_on_the_SR_speed_distance_window_Validate_the_data_in_the_data_validation_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Close the Train Running Number window
        /// Used in:
        ///     Step 4 in TC-ID: 22.17 in 27.17.1 Driver ID window: General Display
        ///     Step 18 in TC-ID: 33.1 in 36.1 The relationship between parent and child windows (1)
        /// </summary>
        public static void Close_the_Train_Running_Number_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Close the Driver ID window
        /// Used in:
        ///     Step 22 in TC-ID: 22.17 in 27.17.1 Driver ID window: General Display
        ///     Step 9 in TC-ID: 33.1 in 36.1 The relationship between parent and child windows (1)
        /// </summary>
        public static void Close_the_Driver_ID_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Use the test script file 22_21_a.xml to send EVC-30 with,MMI_Q_REQUEST_ENABLE_64 _#25 = 0MMI_Q_REQUEST_ENABLE_64 _#26 = 0
        /// Used in:
        ///     Step 14 in TC-ID: 22.21 in 27.21 Settings Window
        ///     Step 16 in TC-ID: 22.21 in 27.21 Settings Window
        ///     Step 18 in TC-ID: 22.21 in 27.21 Settings Window
        /// </summary>
        public static void
            Use_the_test_script_file_22_21_a_xml_to_send_EVC_30_with_MMI_Q_REQUEST_ENABLE_64_25_0MMI_Q_REQUEST_ENABLE_64_26_0(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Use the test script file 22_22_1_a.xml to send EVC-30 with,MMI_NID_WINDOW = 4MMI_Q_REQUEST_ENABLE_64 (#31) = 0MMI_Q_REQUEST_ENABLE_64 (#28) = 0
        /// Used in:
        ///     Step 1 in TC-ID: 22.22.1 in 27.22.1 Brake window
        ///     Step 5 in TC-ID: 22.22.1 in 27.22.1 Brake window
        ///     Step 9 in TC-ID: 22.22.1 in 27.22.1 Brake window
        /// </summary>
        public static void
            Use_the_test_script_file_22_22_1_a_xml_to_send_EVC_30_with_MMI_NID_WINDOW_4MMI_Q_REQUEST_ENABLE_64_31_0MMI_Q_REQUEST_ENABLE_64_28_0(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform the following procedure,Press ‘Brake’ percentage button.Enter and confirm all data in brake percentage window.Press ‘Yes’ button
        /// Used in:
        ///     Step 4 in TC-ID: 22.22.4  in 27.22.4 Brake percentage validation window
        ///     Step 6 in TC-ID: 22.22.4  in 27.22.4 Brake percentage validation window
        /// </summary>
        public static void
            Perform_the_following_procedure_Press_Brake_percentage_button_Enter_and_confirm_all_data_in_brake_percentage_window_Press_Yes_button(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Open the ‘Brake percentage’ data entry window from the Special menu
        /// Used in:
        ///     Step 1 in TC-ID: 22.22.5 in 27.22.5 ‘Brake percentage’ Data Checks: Technical Range Checks by Data Validity
        ///     Step 1 in TC-ID: 22.22.6 in 27.22.6 ‘Brake percentage’ Data Checks: Technical Range Checks by Variable Range
        /// </summary>
        public static void Open_the_Brake_percentage_data_entry_window_from_the_Special_menu(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: This step is to complete the process of ‘Brake percentage’:- Press the ‘Yes’ button on the ‘Brake percentage’ window.- Validate the data in the data validation window
        /// Used in:
        ///     Step 9 in TC-ID: 22.22.5 in 27.22.5 ‘Brake percentage’ Data Checks: Technical Range Checks by Data Validity
        ///     Step 5 in TC-ID: 22.22.6 in 27.22.6 ‘Brake percentage’ Data Checks: Technical Range Checks by Variable Range
        /// </summary>
        public static void
            This_step_is_to_complete_the_process_of_Brake_percentage_Press_the_Yes_button_on_the_Brake_percentage_window_Validate_the_data_in_the_data_validation_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Delete the old value and enter the value ‘65536’ for VBC code.Then, confirm an entered data by pressing an input field
        /// Used in:
        ///     Step 10 in TC-ID: 22.27.1 in 27.27.1 ‘Set VBC’ Data Entry Window
        ///     Step 10 in TC-ID: 22.28.1 in 27.28.1 ‘Remove VBC’ Data Entry Window
        /// </summary>
        public static void
            Delete_the_old_value_and_enter_the_value_65536_for_VBC_code_Then_confirm_an_entered_data_by_pressing_an_input_field(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Select and enter the value ‘65536’ for VBC code again
        /// Used in:
        ///     Step 11 in TC-ID: 22.27.1 in 27.27.1 ‘Set VBC’ Data Entry Window
        ///     Step 11 in TC-ID: 22.28.1 in 27.28.1 ‘Remove VBC’ Data Entry Window
        /// </summary>
        public static void Select_and_enter_the_value_65536_for_VBC_code_again(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Confirm an entered data.Then, apply the action step 2-3 for ‘Yes’ button
        /// Used in:
        ///     Step 12 in TC-ID: 22.27.1 in 27.27.1 ‘Set VBC’ Data Entry Window
        ///     Step 12 in TC-ID: 22.28.1 in 27.28.1 ‘Remove VBC’ Data Entry Window
        /// </summary>
        public static void Confirm_an_entered_data_Then_apply_the_action_step_2_3_for_Yes_button(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform action step 13-17 for the Data area of an input field
        /// Used in:
        ///     Step 19 in TC-ID: 22.27.1 in 27.27.1 ‘Set VBC’ Data Entry Window
        ///     Step 19 in TC-ID: 22.28.1 in 27.28.1 ‘Remove VBC’ Data Entry Window
        /// </summary>
        public static void Perform_action_step_13_17_for_the_Data_area_of_an_input_field(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform the following procedure,Enter and confirm value ‘65536’ at input field.Press ‘Yes’ button
        /// Used in:
        ///     Step 1 in TC-ID: 22.27.2 in 27.27.2 ‘Set VBC’ Validation Window
        ///     Step 7 in TC-ID: 22.27.2 in 27.27.2 ‘Set VBC’ Validation Window
        ///     Step 1 in TC-ID: 22.28.2 in 27.28.2 ‘Remove VBC’ Validation Window
        ///     Step 4 in TC-ID: 22.28.2 in 27.28.2 ‘Remove VBC’ Validation Window
        /// </summary>
        public static void
            Perform_the_following_procedure_Enter_and_confirm_value_65536_at_input_field_Press_Yes_button(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: This step is to complete the process of ‘set VBC’:- Press the ‘Yes’ button on the ‘Set VBC’ window.- Validate the data in the data validation window
        /// Used in:
        ///     Step 9 in TC-ID: 22.27.3 in 27.27.3 ‘Set VBC’ Data Checks: Technical Range Checks by Data Validity
        ///     Step 5 in TC-ID: 22.27.9 in 27.27.9 ‘Set VBC’ Data Checks: Technical Range Checks by Variable Range
        /// </summary>
        public static void
            This_step_is_to_complete_the_process_of_set_VBC_Press_the_Yes_button_on_the_Set_VBC_window_Validate_the_data_in_the_data_validation_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: This step is to complete the process of ‘Remove VBC’:- Press the ‘Yes’ button on the ‘Remove VBC’ window.- Validate the data in the data validation window
        /// Used in:
        ///     Step 9 in TC-ID: 22.28.3 in 27.28.3 ‘Remove VBC’ Data Checks: Technical Range Checks by Data Validity
        ///     Step 5 in TC-ID: 22.28.9 in 27.28.9 ‘Remove VBC’ Data Checks: Technical Range Checks by Variable Range
        /// </summary>
        public static void
            This_step_is_to_complete_the_process_of_Remove_VBC_Press_the_Yes_button_on_the_Remove_VBC_window_Validate_the_data_in_the_data_validation_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Open the ‘Train data’ data entry window from the Main menu
        /// Used in:
        ///     Step 1 in TC-ID: 22.29.3 in 27.29.3 ‘Train data’ (Flexible) Data Checks: Technical Range Checks by Data Validity
        ///     Step 1 in TC-ID: 22.29.4 in 27.29.4 ‘Train data’ Data Checks: Technical Range Checks by Variable Range
        /// </summary>
        public static void Open_the_Train_data_data_entry_window_from_the_Main_menu(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: This step is to complete the process of ‘Train data’:- Press the ‘Yes’ button on the ‘Train data’ window.- Validate the data in the data validation window
        /// Used in:
        ///     Step 11 in TC-ID: 22.29.3 in 27.29.3 ‘Train data’ (Flexible) Data Checks: Technical Range Checks by Data Validity
        ///     Step 7 in TC-ID: 22.29.4 in 27.29.4 ‘Train data’ Data Checks: Technical Range Checks by Variable Range
        /// </summary>
        public static void
            This_step_is_to_complete_the_process_of_Train_data_Press_the_Yes_button_on_the_Train_data_window_Validate_the_data_in_the_data_validation_window(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Select ‘Settings’ button and press ‘Set clock’ button
        /// Used in:
        ///     Step 3 in TC-ID: 29.1 in 29.1 UTC time and offset time(by driver)
        ///     Step 4 in TC-ID: 29.2 in 29.2 UTC time and offset time(by using EVC-3)
        ///     Step 6 in TC-ID: 29.2 in 29.2 UTC time and offset time(by using EVC-3)
        ///     Step 4 in TC-ID: 29.3 in 29.3 UTC time and offset time(By VAP acting as NTP server)
        /// </summary>
        public static void Select_Settings_button_and_press_Set_clock_button(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Restart OTE and ATP again until the message “starting up” is displayed in area E5
        /// Used in:
        ///     Step 2 in TC-ID: 25.2 in 30.2 Start-up error with MMI_M_START_REQ = 2, 3 and 4
        ///     Step 4 in TC-ID: 25.2 in 30.2 Start-up error with MMI_M_START_REQ = 2, 3 and 4
        /// </summary>
        public static void Restart_OTE_and_ATP_again_until_the_message_starting_up_is_displayed_in_area_E5(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Simulate the communication loss between DMI and ETCS Onboard
        /// Used in:
        ///     Step 4 in TC-ID: 26.1 in 1 Introduction
        ///     Step 8 in TC-ID: 26.1 in 1 Introduction
        /// </summary>
        public static void Simulate_the_communication_loss_between_DMI_and_ETCS_Onboard(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Re-establish the communication between DMI and ETCS Onboard
        /// Used in:
        ///     Step 6 in TC-ID: 26.1 in 1 Introduction
        ///     Step 9 in TC-ID: 26.1 in 1 Introduction
        /// </summary>
        public static void Re_establish_the_communication_between_DMI_and_ETCS_Onboard(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Select and confirm ‘No’ button
        /// Used in:
        ///     Step 6 in TC-ID: 34.1.1 in 37.1.1 Fixed Train data entry
        ///     Step 6 in TC-ID: 34.1.2 in 37.1.2 Flexible Train data entry
        /// </summary>
        public static void Select_and_confirm_No_button(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Select Train data button
        /// Used in:
        ///     Step 7 in TC-ID: 34.1.1 in 37.1.1 Fixed Train data entry
        ///     Step 9 in TC-ID: 34.1.1 in 37.1.1 Fixed Train data entry
        ///     Step 9 in TC-ID: 34.1.2 in 37.1.2 Flexible Train data entry
        /// </summary>
        public static void Select_Train_data_button(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Repeat action step 3.Then, press ‘Yes’ button
        /// Used in:
        ///     Step 5 in TC-ID: 34.1.2 in 37.1.2 Flexible Train data entry
        ///     Step 7 in TC-ID: 34.1.2 in 37.1.2 Flexible Train data entry
        /// </summary>
        public static void Repeat_action_step_3_Then_press_Yes_button(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Perform the following procedure,Drive the train forward until the brake is appliedStop driving the trainAcknowledge the ‘Brake intervention’ symbol by pressing area E1
        /// Used in:
        ///     Step 2 in TC-ID: 34.1.4 in 37.1.4.1.1 Data entry/validation process when enabling conditions not fullfilled: Level 1
        ///     Step 4 in TC-ID: 34.1.4 in 37.1.4.1.1 Data entry/validation process when enabling conditions not fullfilled: Level 1
        ///     Step 6 in TC-ID: 34.1.4 in 37.1.4.1.1 Data entry/validation process when enabling conditions not fullfilled: Level 1
        ///     Step 8 in TC-ID: 34.1.4 in 37.1.4.1.1 Data entry/validation process when enabling conditions not fullfilled: Level 1
        ///     Step 10 in TC-ID: 34.1.4 in 37.1.4.1.1 Data entry/validation process when enabling conditions not fullfilled: Level 1
        ///     Step 13 in TC-ID: 34.1.4 in 37.1.4.1.1 Data entry/validation process when enabling conditions not fullfilled: Level 1
        ///     Step 15 in TC-ID: 34.1.4 in 37.1.4.1.1 Data entry/validation process when enabling conditions not fullfilled: Level 1
        ///     Step 17 in TC-ID: 34.1.4 in 37.1.4.1.1 Data entry/validation process when enabling conditions not fullfilled: Level 1
        ///     Step 19 in TC-ID: 34.1.4 in 37.1.4.1.1 Data entry/validation process when enabling conditions not fullfilled: Level 1
        ///     Step 2 in TC-ID: 34.1.4.2 in 37.1.4.1.2 Data entry/validation process when enabling conditions not fullfilled: Level 2
        ///     Step 4 in TC-ID: 34.1.4.2 in 37.1.4.1.2 Data entry/validation process when enabling conditions not fullfilled: Level 2
        /// </summary>
        public static void
            Perform_the_following_procedure_Drive_the_train_forward_until_the_brake_is_appliedStop_driving_the_trainAcknowledge_the_Brake_intervention_symbol_by_pressing_area_E1(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Re-validate the step1 by re-starting OTE Simulator and starting the precondition with ETCS level 3
        /// Used in:
        ///     Step 2 in TC-ID: 34.4.2.1 in 37.4.2.1 Text Message “Shunting Refused” in Level 2 and Level 3
        ///     Step 2 in TC-ID: 34.4.2.2 in 37.4.2.2 SH Symbol in Level 2 and Level 3
        ///     Step 2 in TC-ID: 34.4.2.3 in 37.4.2.3 Text Message “Shunting Request Failed” in Level 2 and Level 3
        /// </summary>
        public static void
            Re_validate_the_step1_by_re_starting_OTE_Simulator_and_starting_the_precondition_with_ETCS_level_3(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Confirm entered data by pressing input field
        /// Used in:
        ///     Step 15 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        ///     Step 19 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        ///     Step 23 in TC-ID: 34.7 in 37.7 Dialogue Sequence of Settings window
        /// </summary>
        public static void Confirm_entered_data_by_pressing_input_field(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train forward with speed at 40 km/h
        /// Used in:
        ///     Step 2 in TC-ID: 36.1 in Sound S1 - Driving too fast
        ///     Step 9 in TC-ID: 36.1 in Sound S1 - Driving too fast
        ///     Step 2 in TC-ID: 36.2 in Sound S3 - End of Intervention
        ///     Step 8 in TC-ID: 36.2 in Sound S3 - End of Intervention
        /// </summary>
        public static void Drive_the_train_forward_with_speed_at_40_kmh(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Train runs pass BG1
        /// Used in:
        ///     Step 3 in TC-ID: 36.1 in Sound S1 - Driving too fast
        ///     Step 10 in TC-ID: 36.1 in Sound S1 - Driving too fast
        ///     Step 3 in TC-ID: 36.2 in Sound S3 - End of Intervention
        ///     Step 9 in TC-ID: 36.2 in Sound S3 - End of Intervention
        ///     Step 3 in TC-ID: 36.3.1 in 39.3.1 Restrictive Target with Speed Monitoring in Full Supervision Mode
        ///     Step 3 in TC-ID: 36.3.2 in 39.3.2 Restrictive Target with Movement Authority Changed in Full Supervision Mode
        ///     Step 3 in TC-ID: 36.3.4 in 39.3.4 Restrictive Target with Movement Authority Changed in Limited Supervision Mode
        /// </summary>
        public static void Train_runs_pass_BG1(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Deactivate cabin A and power off the system
        /// Used in:
        ///     Step 7 in TC-ID: 36.1 in Sound S1 - Driving too fast
        ///     Step 6 in TC-ID: 36.2 in Sound S3 - End of Intervention
        /// </summary>
        public static void Deactivate_cabin_A_and_power_off_the_system(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Power on the system and perform SoM to Level 1 in SR mode
        /// Used in:
        ///     Step 8 in TC-ID: 36.1 in Sound S1 - Driving too fast
        ///     Step 7 in TC-ID: 36.2 in Sound S3 - End of Intervention
        /// </summary>
        public static void Power_on_the_system_and_perform_SoM_to_Level_1_in_SR_mode(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Drive the train forward with constant speed at 20 km/h
        /// Used in:
        ///     Step 2 in TC-ID: 36.3.2 in 39.3.2 Restrictive Target with Movement Authority Changed in Full Supervision Mode
        ///     Step 2 in TC-ID: 36.3.3 in 39.3.3 Restrictive Target with Speed Monitoring in Limited Supervision Mode
        ///     Step 2 in TC-ID: 36.3.4 in 39.3.4 Restrictive Target with Movement Authority Changed in Limited Supervision Mode
        /// </summary>
        public static void Drive_the_train_forward_with_constant_speed_at_20_kmh(SignalPool pool)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Description: Train runs pass BG2
        /// Used in:
        ///     Step 4 in TC-ID: 36.3.2 in 39.3.2 Restrictive Target with Movement Authority Changed in Full Supervision Mode
        ///     Step 4 in TC-ID: 36.3.4 in 39.3.4 Restrictive Target with Movement Authority Changed in Limited Supervision Mode
        /// </summary>
        public static void Train_runs_pass_BG2(SignalPool pool)
        {
            throw new NotImplementedException();
        }        
    }
}