using System;
using System.Collections.Generic;
using CL345;
using Testcase.Telegrams.EVCtoDMI;
using System.Windows.Forms;


namespace Testcase.DMITestCases
{
    /// <summary>
    /// These are the generic methods used to perform actions on the DMI
    /// </summary>
    public static partial class DmiActions
    {
        /// <summary>
        /// Forces DMI into completed SoM, L0, UN Mode. Displays Default window.
        /// No user input required.
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Complete_SoM_L0_UN(SignalPool pool)
        {
            // Set train running number, cab 1 active, and other defaults
            ((TestcaseBase)pool).StartUp();

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
            // Set train running number, cab 1 active, and other defaults
            ((TestcaseBase)pool).StartUp();

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
            // Set train running number, cab 1 active, and other defaults
            ((TestcaseBase)pool).StartUp();

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
            // Set train running number, cab 1 active, and other defaults
            ((TestcaseBase) pool).StartUp();

            // Set driver ID
            Set_Driver_ID(pool, "1234");

            // Set to level 1 and SR mode
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Level = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_LEVEL.L1;
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;

            // Enable standard buttons including Start, and display Default window.
            Finished_SoM_Default_Window(pool);
        }

        /// <summary>
        /// Enable standard buttons including Start, and display Default window.
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Finished_SoM_Default_Window(SignalPool pool)
        {
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.Start | Variables.standardFlags;
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Default;
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
            EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
            EVC14_MMICurrentDriverID.Send();
        }

        /// <summary>
        /// Set Driver ID string
        /// </summary>
        /// <param name="pool">Signal pool</param>
        /// <param name="vbcCode"></param>
        public static void Set_VBC_Code(SignalPool pool, string vbcCode)
        {
            EVC18_MMISetVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC18_MMISetVBC.MMI_N_VBC = 1;
            EVC18_MMISetVBC.NID_VBCMK = 0;
            EVC18_MMISetVBC.SetVBCCode();
            EVC18_MMISetVBC.MMI_Q_DATA_CHECK = Variables.Q_DATA_CHECK.All_checks_passed;
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
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                EVC30_MMIRequestEnable.EnabledRequests.Start | Variables.standardFlags;
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Default;
            EVC30_MMIRequestEnable.Send();
        }

        /// <summary>
        /// Description: Set SR distance/speed window
        /// Used in:
        ///     Step 6 in TC-ID: 14.1 in 19.1
        /// <param name="pool">Signal pool</param>
        public static void Display_SR_speed_distance_window(SignalPool pool, uint lStff, ushort vStff)
        {
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = Variables.standardFlags;
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.SR_speed_distance;
            EVC30_MMIRequestEnable.Send();

            EVC11_MMICurrentSRRules.MMI_L_STFF = lStff;
            EVC11_MMICurrentSRRules.MMI_V_STFF = vStff;
            EVC11_MMICurrentSRRules.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC11_MMICurrentSRRules.Send();
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
        public static void Send_EVC6_MMICurrentTrainData(Variables.MMI_M_DATA_ENABLE mmiMDataEnable, ushort mmiLTrain,
            ushort mmiVMaxTrain, Variables.MMI_NID_KEY mmiNidKeyTrainCat, byte mmiMBrakePerc,
            Variables.MMI_NID_KEY mmiNidKeyAxleLoad,
            byte mmiMAirtight, Variables.MMI_NID_KEY_Load_Gauge mmiNidKeyLoadGauge,
            EVC6_MMICurrentTrainData.MMI_M_BUTTONS_CURRENT_TRAIN_DATA mmiMButtons,
            ushort mmiMTrainsetId, ushort mmiMAltDem, string[] trainSetCaptions,
            Variables.DataElement[] dataElements = null)
        {
            EVC6_MMICurrentTrainData.MMI_M_DATA_ENABLE = mmiMDataEnable; // Train data enabled
            EVC6_MMICurrentTrainData.MMI_L_TRAIN = mmiLTrain; // Train length
            EVC6_MMICurrentTrainData.MMI_V_MAXTRAIN = mmiVMaxTrain; // Max train speed
            EVC6_MMICurrentTrainData.MMI_NID_KEY_TRAIN_CAT = mmiNidKeyTrainCat; // Train category
            EVC6_MMICurrentTrainData.MMI_M_BRAKE_PERC = mmiMBrakePerc; // Brake percentage
            EVC6_MMICurrentTrainData.MMI_NID_KEY_AXLE_LOAD = mmiNidKeyAxleLoad; // Axle load category
            EVC6_MMICurrentTrainData.MMI_M_AIRTIGHT = mmiMAirtight; // Train equipped with airtight system
            EVC6_MMICurrentTrainData.MMI_NID_KEY_LOAD_GAUGE = mmiNidKeyLoadGauge; // Loading gauge type of train 
            EVC6_MMICurrentTrainData.MMI_M_BUTTONS = mmiMButtons; // Button available

            EVC6_MMICurrentTrainData.MMI_M_TRAINSET_ID = mmiMTrainsetId;
            EVC6_MMICurrentTrainData.MMI_M_ALT_DEM = mmiMAltDem;

            EVC6_MMICurrentTrainData.TrainSetCaptions = new List<string>(trainSetCaptions);

            if (dataElements == null)
            {
                EVC6_MMICurrentTrainData.DataElements = new List<Variables.DataElement>();
            }
            else
            {
                EVC6_MMICurrentTrainData.DataElements = new List<Variables.DataElement>(dataElements);
            }

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
            EVC6_MMICurrentTrainData.MMI_M_DATA_ENABLE =
                Variables.MMI_M_DATA_ENABLE.TrainSetID; // "Train Set ID" data enabled
            EVC6_MMICurrentTrainData.MMI_L_TRAIN = 0; // Train length
            EVC6_MMICurrentTrainData.MMI_V_MAXTRAIN = 0; // Max train speed
            EVC6_MMICurrentTrainData.MMI_NID_KEY_TRAIN_CAT = Variables.MMI_NID_KEY.NoDedicatedKey; // Train category
            EVC6_MMICurrentTrainData.MMI_M_BRAKE_PERC = 0; // Brake percentage
            EVC6_MMICurrentTrainData.MMI_NID_KEY_AXLE_LOAD = Variables.MMI_NID_KEY.NoDedicatedKey; // Axle load category
            EVC6_MMICurrentTrainData.MMI_M_AIRTIGHT = 0; // Train equipped with airtight system
            EVC6_MMICurrentTrainData.MMI_NID_KEY_LOAD_GAUGE =
                Variables.MMI_NID_KEY_Load_Gauge.NoDedicatedKey; // Loading gauge type of train 
            EVC6_MMICurrentTrainData.MMI_M_BUTTONS =
                EVC6_MMICurrentTrainData.MMI_M_BUTTONS_CURRENT_TRAIN_DATA.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC6_MMICurrentTrainData.MMI_M_TRAINSET_ID = mmiMTrainsetId; // Preselected Trainset ID
            EVC6_MMICurrentTrainData.MMI_M_ALT_DEM = 0; // No alternative train data available

            EVC6_MMICurrentTrainData.TrainSetCaptions = new List<string>(fixedTrainsetCaptions);
            EVC6_MMICurrentTrainData.DataElements = new List<Variables.DataElement>(); // No train data elements

            EVC6_MMICurrentTrainData.Send();
        }

        public static void Send_EVC10_MMIEchoedTrainData(SignalPool pool, Variables.MMI_M_DATA_ENABLE mmiMDataEnable,
            ushort mmiLTrain,
            ushort mmiVMaxTrain, Variables.MMI_NID_KEY mmiNidKeyTrainCat,
            byte mmiMBrakePerc, Variables.MMI_NID_KEY mmiNidKeyAxleLoad,
            byte mmiMAirtight, Variables.MMI_NID_KEY mmiNidKeyLoadGauge,
            string[] trainSetCaptions)
        {
            // EVC-10 inverts all the integral values except the alias
            EVC10_MMIEchoedTrainData.MMI_M_DATA_ENABLE_ = (ushort) mmiMDataEnable; // Train data enabled
            EVC10_MMIEchoedTrainData.MMI_L_TRAIN_ = (ushort) mmiLTrain; // Train length
            EVC10_MMIEchoedTrainData.MMI_V_MAXTRAIN_ = (ushort) mmiVMaxTrain; // Max train speed
            EVC10_MMIEchoedTrainData.MMI_NID_KEY_TRAIN_CAT_ = (byte) mmiNidKeyTrainCat; // Train category
            EVC10_MMIEchoedTrainData.MMI_M_BRAKE_PERC_ = (byte) mmiMBrakePerc; // Brake percentage
            EVC10_MMIEchoedTrainData.MMI_NID_KEY_AXLE_LOAD_R = (byte) mmiNidKeyAxleLoad; // Axle load category
            EVC10_MMIEchoedTrainData.MMI_M_AIRTIGHT_R = (byte) mmiMAirtight; // Train equipped with airtight system
            EVC10_MMIEchoedTrainData.MMI_NID_KEY_LOAD_GAUGE_ =
                (byte) mmiNidKeyLoadGauge; // Loading gauge type of train 
            EVC10_MMIEchoedTrainData.EVC10_alias_1 =
                pool.SITR.ETCS1.CurrentTrainData.EVC6alias1.Value; // Alias variable for bit mapping
            EVC10_MMIEchoedTrainData.MMI_N_TRAINSETS_ = pool.SITR.ETCS1.CurrentTrainData.MmiNTrainset.Value;
            EVC10_MMIEchoedTrainData.TrainSetCaptions = new List<string>(trainSetCaptions);

            EVC10_MMIEchoedTrainData.Send();
        }

        /*public static void Send_EVC10_MMIEchoedTrainData(SignalPool pool)
        {
            // EVC-10 inverts all the integral values except the alias
            EVC10_MMIEchoedTrainData.MMI_M_DATA_ENABLE_ = (ushort)EVC6_MMICurrentTrainData.MMI_M_DATA_ENABLE;                   // Train data enabled
            EVC10_MMIEchoedTrainData.MMI_L_TRAIN_ = (ushort)EVC6_MMICurrentTrainData.MMI_L_TRAIN;                               // Train length
            EVC10_MMIEchoedTrainData.MMI_V_MAXTRAIN_ = (ushort)EVC6_MMICurrentTrainData.MMI_V_MAXTRAIN;                         // Max train speed
            EVC10_MMIEchoedTrainData.MMI_NID_KEY_TRAIN_CAT_ = (byte)EVC6_MMICurrentTrainData.MMI_NID_KEY_TRAIN_CAT;             // Train category
            EVC10_MMIEchoedTrainData.MMI_M_BRAKE_PERC_ = (byte)EVC6_MMICurrentTrainData.MMI_M_BRAKE_PERC;                       // Brake percentage
            EVC10_MMIEchoedTrainData.MMI_NID_KEY_AXLE_LOAD_R = (byte)EVC6_MMICurrentTrainData.MMI_NID_KEY_AXLE_LOAD;            // Axle load category
            EVC10_MMIEchoedTrainData.MMI_M_AIRTIGHT_R = (byte)EVC6_MMICurrentTrainData.MMI_M_AIRTIGHT;                          // Train equipped with airtight system
            EVC10_MMIEchoedTrainData.MMI_NID_KEY_LOAD_GAUGE_ = (byte)EVC6_MMICurrentTrainData.MMI_NID_KEY_LOAD_GAUGE;           // Loading gauge type of train 
            EVC10_MMIEchoedTrainData.EVC10_alias_1 = pool.SITR.ETCS1.CurrentTrainData.EVC6alias1.Value;                         // Alias variable for bit mapping
            EVC10_MMIEchoedTrainData.MMI_N_TRAINSETS_ = (ushort)pool.SITR.ETCS1.CurrentTrainData.MmiNTrainset.Value;
            EVC10_MMIEchoedTrainData.TrainSetCaptions = EVC6_MMICurrentTrainData.TrainSetCaptions;

            EVC10_MMIEchoedTrainData.Send();
        }*/

        /// <summary>
        ///     Sends EVC-10 telegram with Fixed Data Entry for up to 9 trainset strings.
        /// </summary>
        /// <param name="pool">Signal pool</param>
        /// <param name="fixedTrainsetCaptions"> Array of strings for trainset captions</param>
        /// <param name="mmiMTrainsetId">Index of trainset to be pre-selected on DMI</param>
        public static void Send_EVC10_MMIEchoedTrainData_FixedDataEntry(SignalPool pool, string[] fixedTrainsetCaptions)
        {
            EVC10_MMIEchoedTrainData.MMI_M_DATA_ENABLE_ =
                (ushort) EVC6_MMICurrentTrainData.MMI_M_DATA_ENABLE; // Train data enabled
            EVC10_MMIEchoedTrainData.MMI_L_TRAIN_ = EVC6_MMICurrentTrainData.MMI_L_TRAIN; // Train length
            EVC10_MMIEchoedTrainData.MMI_V_MAXTRAIN_ = EVC6_MMICurrentTrainData.MMI_V_MAXTRAIN; // Max train speed
            EVC10_MMIEchoedTrainData.MMI_NID_KEY_TRAIN_CAT_ =
                (byte) EVC6_MMICurrentTrainData.MMI_NID_KEY_TRAIN_CAT; // Train category
            EVC10_MMIEchoedTrainData.MMI_M_BRAKE_PERC_ =
                (byte) EVC6_MMICurrentTrainData.MMI_M_BRAKE_PERC; // Brake percentage
            EVC10_MMIEchoedTrainData.MMI_NID_KEY_AXLE_LOAD_R =
                (byte) EVC6_MMICurrentTrainData.MMI_NID_KEY_AXLE_LOAD; // Axle load category
            EVC10_MMIEchoedTrainData.MMI_M_AIRTIGHT_R =
                (byte) EVC6_MMICurrentTrainData.MMI_M_AIRTIGHT; // Train equipped with airtight system
            EVC10_MMIEchoedTrainData.MMI_NID_KEY_LOAD_GAUGE_ =
                (byte) EVC6_MMICurrentTrainData.MMI_NID_KEY_LOAD_GAUGE; // Loading gauge type of train 
            EVC10_MMIEchoedTrainData.EVC10_alias_1 =
                pool.SITR.ETCS1.CurrentTrainData.EVC6alias1.Value; // Alias variable for bit mapping
            EVC10_MMIEchoedTrainData.MMI_N_TRAINSETS_ = pool.SITR.ETCS1.CurrentTrainData.MmiNTrainset.Value;
            EVC10_MMIEchoedTrainData.TrainSetCaptions = new List<string>(fixedTrainsetCaptions);

            EVC10_MMIEchoedTrainData.Send();
        }

        /// <summary>
        /// Send standard EVC-20 telegram with Levels 0-3, CBTC, and AWS/TPWS selectable. Level 1 is preselected.
        /// </summary>
        public static void Send_EVC20_MMISelectLevel_AllLevels(SignalPool pool, bool closeEnable = true)
        {
            EVC20_MMISelectLevel.MMI_Q_LEVEL_NTC_ID = Variables.paramEvc20MmiQLevelNtcId;
            EVC20_MMISelectLevel.MMI_M_CURRENT_LEVEL = Variables.paramEvc20MmiMCurrentLevel;
            EVC20_MMISelectLevel.MMI_M_LEVEL_FLAG = Variables.paramEvc20MmiMLevelFlag;
            EVC20_MMISelectLevel.MMI_M_INHIBITED_LEVEL = Variables.paramEvc20MmiMInhibitedLevel;
            EVC20_MMISelectLevel.MMI_M_INHIBIT_ENABLE = Variables.paramEvc20MmiMInhibitEnable;
            EVC20_MMISelectLevel.MMI_M_LEVEL_NTC_ID = Variables.paramEvc20MmiMLevelNtcId;
            EVC20_MMISelectLevel.MMI_Q_CLOSE_ENABLE =
                closeEnable ? Variables.MMI_Q_CLOSE_ENABLE.Enabled : Variables.MMI_Q_CLOSE_ENABLE.Disabled;
            EVC20_MMISelectLevel.Send();
        }

        /// <summary>
        /// Send_EVC22_MMI_Current_Rbc_Data sends RBC Data to the DMI
        /// </summary>
        public static void Send_EVC22_MMI_Current_RBC(SignalPool pool, uint rbcId, ulong mmiNidRadio,
            ushort mmiNidWindow,
            bool closeEnable, EVC22_MMICurrentRBC.EVC22BUTTONS mmiMButtons,
            string[] textDataElements)
        {
            EVC22_MMICurrentRBC.NID_C = Variables.NidC;
            EVC22_MMICurrentRBC.NID_RBC = rbcId;
            EVC22_MMICurrentRBC.MMI_NID_RADIO = mmiNidRadio; // RBC phone number
            EVC22_MMICurrentRBC.MMI_NID_WINDOW = mmiNidWindow; // ETCS Window Id
            EVC22_MMICurrentRBC.MMI_Q_CLOSE_ENABLE =
                closeEnable
                    ? Variables.MMI_Q_CLOSE_ENABLE.Enabled
                    : Variables.MMI_Q_CLOSE_ENABLE.Disabled; // Close button enable?
            EVC22_MMICurrentRBC.MMI_M_BUTTONS = mmiMButtons; // Buttons available

            EVC22_MMICurrentRBC.NetworkCaptions = new List<string>(textDataElements);
            EVC22_MMICurrentRBC.DataElements = new List<Variables.DataElement>();

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

            var textLabel = new Label() {Left = 50, Top = 20, Text = text};
            var textBox = new TextBox() {Left = 50, Top = 50, Width = 400};
            var confirmation = new Button()
            {
                Text = "Ok",
                Left = 350,
                Width = 100,
                Top = 70,
                DialogResult = DialogResult.OK
            };

            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        /// <summary>
        /// Description: This function allows to stop sending periodically any EVC-1 forcing the DMI to go to "ATP-Down" state
        /// MMI_gen 244-- 	If the ETCS-MMI is in “active” state and [EVC-1] is lost*, the MMI shall enter “ATP-Down” state...
        /// Used in:
        ///     Step 6 in TC-ID: 1.6 in 6.6
        /// </summary>
        public static void Force_Loss_Communication(SignalPool pool)
        {
            EVC1_MMIDynamic.ForceComunicationLoss(pool);
            pool.Wait_Realtime(1000);
        }

        public static void Restablish_Communication(SignalPool pool)
        {
            EVC1_MMIDynamic.UnforceCommunicationLoss(pool);
            pool.Wait_Realtime(1000);
        }

        /// <summary>
        /// Description: Activate cabin 1
        /// Used in:
        ///     Step 1 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void Activate_Cabin_1(SignalPool pool)
        {
            RigControl.SetMCSState(pool, 1, RigControl.CabState.Forward);

            EVC2_MMIStatus.TrainRunningNumber = 1;
            EVC2_MMIStatus.MMI_M_ACTIVE_CABIN = Variables.MMI_M_ACTIVE_CABIN.Cabin1Active;
            EVC2_MMIStatus.MMI_M_ADHESION = 0x0;
            EVC2_MMIStatus.MMI_M_OVERRIDE_EOA = false;
            EVC2_MMIStatus.Send();
            pool.Wait_Realtime(5000);
        }

        /// <summary>
        /// Description: Activate cabin 2
        /// Used in:
        ///     Step 12 in TC-ID: 15.1.1 in 20.1.1
        /// </summary>
        public static void Activate_Cabin_2(SignalPool pool)
        {
            RigControl.SetMCSState(pool, 2, RigControl.CabState.Forward);

            EVC2_MMIStatus.TrainRunningNumber = 1;
            EVC2_MMIStatus.MMI_M_ACTIVE_CABIN = Variables.MMI_M_ACTIVE_CABIN.Cabin2Active;
            EVC2_MMIStatus.MMI_M_ADHESION = 0x0;
            EVC2_MMIStatus.MMI_M_OVERRIDE_EOA = false;
            EVC2_MMIStatus.Send();
            pool.Wait_Realtime(2000);
        }

        /// <summary>
        /// Description: Deactivate cabins
        /// Used in:
        ///     Step 12 in TC-ID: 15.1.1 in 20.1.1
        ///     Step 10 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        public static void Deactivate_Cabin(SignalPool pool)
        {
            RigControl.SetMCSState(pool, 1, RigControl.CabState.Shutdown);

            EVC2_MMIStatus.MMI_M_ACTIVE_CABIN = Variables.MMI_M_ACTIVE_CABIN.NoCabinActive;
            EVC2_MMIStatus.Send();
            pool.Wait_Realtime(5000);
        }

        /// <summary>
        /// Description: ETCS requests driver to allow the brake test
        /// Used in:
        ///     Step 2 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        public static void Request_Brake_Test(SignalPool pool, ushort mmiIText = 1)
        {
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 2;
            EVC8_MMIDriverMessage.MMI_I_TEXT = mmiIText;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 514;
            EVC8_MMIDriverMessage.Send();
        }

        /// <summary>
        /// Description: ETCS perform the brake test
        /// Used in:
        ///     Step 2 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        public static void Perform_Brake_Test(SignalPool pool, ushort mmiIText = 1)
        {
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.AuxiliaryInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = mmiIText;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 516;
            EVC8_MMIDriverMessage.Send();
        }

        /// <summary>
        /// Description: ETCS perform the brake test
        /// Used in:
        ///     Step 2 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        public static void Display_Brake_Test_Successful(SignalPool pool, ushort mmiIText = 1)
        {
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.AuxiliaryInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.MMI_I_TEXT = mmiIText;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 524;
            EVC8_MMIDriverMessage.Send();
        }

        /// <summary>
        /// Description: ETCS perform the brake test
        /// Used in:
        ///     Step 2 in TC-ID: 15.1.3 in 20.1.3
        /// </summary>
        public static void Delete_Brake_Test_Successful(SignalPool pool, ushort mmiIText = 1)
        {
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.AuxiliaryInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.MMI_I_TEXT = mmiIText;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 524;
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

        public static void Show_RBC_Connection_Lost_Symbol(SignalPool pool)
        {
            EVC8_MMIDriverMessage.MMI_I_TEXT = 12;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 282;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
            EVC8_MMIDriverMessage.Send();
        }

        public static void Remove_RBC_Connection_Lost_Symbol(SignalPool pool)
        {
            EVC8_MMIDriverMessage.MMI_I_TEXT = 12;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 282;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 4;
            EVC8_MMIDriverMessage.Send();
        }

        public static void Show_RBC_Connection_Established_Symbol(SignalPool pool)
        {
            EVC8_MMIDriverMessage.MMI_I_TEXT = 15;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 568;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 3;
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
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 257; // "#3 LE07/LE11/LE13/LE15 (Ack Transition to Level #4)"
            EVC8_MMIDriverMessage.PlainTextMessage = "0";
            EVC8_MMIDriverMessage.Send();
        }

        /// <summary>
        /// Description: Level 1 announcement ack request sent to the driver
        /// Used in:
        ///     Step 1 in TC-ID: 15.1.4 in 20.1.4
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_L1_Announcement_Ack(SignalPool pool)
        {
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 257; // "#3 LE07/LE11/LE13/LE15 (Ack Transition to Level #4)"
            EVC8_MMIDriverMessage.PlainTextMessage = "1";
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
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 263; // "#3 MO10 (Ack Staff Responsible Mode)"
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
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;
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
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 264; // "#3 MO17 (Ack Unfitted Mode)"
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
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 259; // "#3 MO08 (Ack On Sight Mode)"
            EVC8_MMIDriverMessage.Send();
        }

        /// <summary>
        /// Description: LS mode sent to be displayed on the DMI
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Send_LS_Mode(SignalPool pool)
        {
            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode =
                EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.LimitedSupervision;
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
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 709; // "#3 MO22 (Ack for Limited Supervision)"
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
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 266; // "#3 MO05 (Ack Train Trip)" 
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
        /// Description: RV mode acknowledgement request sent to the driver
        /// Used in:
        ///     Step 2 in TC-ID: 14.1 in 19.1
        /// </summary>
        public static void Send_RV_Mode_Ack(SignalPool pool)
        {
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CLASS = MMI_Q_TEXT_CLASS.ImportantInformation;
            EVC8_MMIDriverMessage.MMI_Q_TEXT_CRITERIA = 1;
            EVC8_MMIDriverMessage.MMI_I_TEXT = 1;
            EVC8_MMIDriverMessage.MMI_Q_TEXT = 262; // "#3 MO15 (Ack Reversing Mode)"
            EVC8_MMIDriverMessage.Send();
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
            EVC30_MMIRequestEnable.MMI_NID_WINDOW =
                EVC30_MMIRequestEnable.WindowID.Main; //EVC30_MMIRequestEnable.WindowID.No_window_specified

            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                Variables.standardFlags | EVC30_MMIRequestEnable.EnabledRequests.Start;
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
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Main;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = Variables.standardFlags;
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
            EVC14_MMICurrentDriverID.MMI_Q_CLOSE_ENABLE = Variables.MMI_Q_CLOSE_ENABLE.Enabled;
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
            Send_EVC6_MMICurrentTrainData_FixedDataEntry(pool, new[] {"FLU", "RLU", "Rescue"}, 15);

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
        public static void Enable_Fixed_Train_Data_Validation(SignalPool pool,
            Variables.Fixed_Trainset_Captions trainsetSelected)
        {
            Variables.DataElement[] dataElements = new Variables.DataElement[1]
            {
                new Variables.DataElement
                {
                    Identifier = 6,
                    QDataCheck = 0,
                    EchoText = Enum.GetName(typeof(Variables.Fixed_Trainset_Captions), trainsetSelected)
                }
            };

            DmiActions.Send_EVC6_MMICurrentTrainData(Variables.MMI_M_DATA_ENABLE.TrainSetID, 0, 0,
                Variables.MMI_NID_KEY.NoDedicatedKey, 0, Variables.MMI_NID_KEY.NoDedicatedKey, 0,
                Variables.MMI_NID_KEY_Load_Gauge.NoDedicatedKey,
                EVC6_MMICurrentTrainData.MMI_M_BUTTONS_CURRENT_TRAIN_DATA.BTN_YES_DATA_ENTRY_COMPLETE,
                Convert.ToUInt16((byte) (trainsetSelected)), 0, new string[] { }, dataElements);
        }

        /// <summary>
        /// Description: Finishes the Train Data Entry process in order to move to Train Data Validation
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="trainsetSelected"></param>
        public static void Complete_Fixed_Train_Data_Entry(SignalPool pool,
            Variables.Fixed_Trainset_Captions trainsetSelected)
        {
            Variables.DataElement[] dataElements = new Variables.DataElement[8]
            {
                new Variables.DataElement {Identifier = 6, QDataCheck = 0, EchoText = ""},
                new Variables.DataElement {Identifier = 9, QDataCheck = 0, EchoText = ""},
                new Variables.DataElement {Identifier = 10, QDataCheck = 0, EchoText = ""},
                new Variables.DataElement {Identifier = 11, QDataCheck = 0, EchoText = ""},
                new Variables.DataElement {Identifier = 12, QDataCheck = 0, EchoText = ""},
                new Variables.DataElement {Identifier = 13, QDataCheck = 0, EchoText = ""},
                new Variables.DataElement {Identifier = 7, QDataCheck = 0, EchoText = ""},
                new Variables.DataElement {Identifier = 8, QDataCheck = 0, EchoText = ""}
            };

            DmiActions.Send_EVC6_MMICurrentTrainData(Variables.MMI_M_DATA_ENABLE.NONE, 0, 0,
                Variables.MMI_NID_KEY.NoDedicatedKey, 0, Variables.MMI_NID_KEY.NoDedicatedKey, 0,
                Variables.MMI_NID_KEY_Load_Gauge.NoDedicatedKey,
                EVC6_MMICurrentTrainData.MMI_M_BUTTONS_CURRENT_TRAIN_DATA.BTN_YES_DATA_ENTRY_COMPLETE,
                Convert.ToUInt16((byte) (trainsetSelected)), 0, new string[] { }, dataElements);
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
            (pool, 0, 0, 9, true, EVC22_MMICurrentRBC.EVC22BUTTONS.NoButton,
                new[] {"Network1", "Network2", "Network3"});
        }

        /// <summary>
        /// Description: RBC Data sent to be displayed on th DMI
        /// Used in:
        ///     Step 1 in TC-ID: 15.2.2 in 20.2.2
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Display_RBC_Contact_Window(SignalPool pool)
        {
            Send_EVC22_MMI_Current_RBC(pool, 2, 0x12345FFFFFFFFFFF, 5, true, EVC22_MMICurrentRBC.EVC22BUTTONS.BTN_ENTER,
                new[] {"Network1"});
        }


        /// <summary>
        /// Description: RBC Data sent to be displayed on th DMI
        /// Used in:
        ///     Step 1 in TC-ID: 22.27.1 in 27.27.1
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Display_Set_VBC_Window(SignalPool pool)
        {
            EVC18_MMISetVBC.MMI_M_BUTTONS = Variables.MMI_M_BUTTONS_VBC.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC18_MMISetVBC.MMI_N_VBC = 0;
            EVC18_MMISetVBC.Send();
        }

        /// <summary>
        /// Description: RBC Data sent to be displayed on th DMI
        /// Used in:
        ///     Step 1 in TC-ID: 22.27.1 in 27.27.1
        /// </summary>
        /// <param name="pool">Signal pool</param>
        public static void Display_Data_View_Window(SignalPool pool)
        {
            EVC13_MMIDataView.MMI_X_DRIVER_ID = "1234";
            EVC13_MMIDataView.MMI_NID_OPERATION = 0xFFFFFFFF;
            EVC13_MMIDataView.MMI_M_DATA_ENABLE = Variables.MMI_M_DATA_ENABLE.TrainSetID;
            EVC13_MMIDataView.MMI_L_TRAIN = 0;
            EVC13_MMIDataView.MMI_V_MAXTRAIN = 0;
            EVC13_MMIDataView.MMI_M_BRAKE_PERC = 0;
            EVC13_MMIDataView.MMI_NID_KEY_AXLE_LOAD = Variables.MMI_NID_KEY.NoDedicatedKey;
            EVC13_MMIDataView.MMI_NID_RADIO = 0x12345678FFFFFFFF;
            EVC13_MMIDataView.MMI_NID_RBC = 2;
            EVC13_MMIDataView.MMI_M_AIRTIGHT = 0;
            EVC13_MMIDataView.MMI_NID_KEY_LOAD_GAUGE = Variables.MMI_NID_KEY.NoDedicatedKey;
            EVC13_MMIDataView.MMI_M_VBC_CODE = null;
            EVC13_MMIDataView.Trainset_Caption = "FLU";
            EVC13_MMIDataView.Network_Caption = "Network1";
            EVC13_MMIDataView.MMI_NID_KEY_TRAIN_CAT = Variables.MMI_NID_KEY.NoDedicatedKey;
            EVC13_MMIDataView.Send();
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
            EVC16_CurrentTrainNumber.Send();
            ;
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
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Override;
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH =
                Variables.standardFlags | EVC30_MMIRequestEnable.EnabledRequests.EOA;
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
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Default;
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
            string lssma_string = DmiActions.ShowDialog(@"Perform the following actions: " + Environment.NewLine +
                                                        Environment.NewLine +
                                                        "1. Enter a LSSMA value (integer lower than 601)." +
                                                        Environment.NewLine, "LSSMA entering");

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
            EVC1_MMIDynamic.MMI_V_TRAIN = 0; // Set speed to zero
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
            EVC6_MMICurrentTrainData.MMI_M_DATA_ENABLE =
                Variables.MMI_M_DATA_ENABLE.TrainSetID; // "Train Set ID" data enabled
            EVC6_MMICurrentTrainData.MMI_L_TRAIN = 0; // Train length
            EVC6_MMICurrentTrainData.MMI_V_MAXTRAIN = 0; // Max train speed
            EVC6_MMICurrentTrainData.MMI_NID_KEY_TRAIN_CAT = Variables.MMI_NID_KEY.NoDedicatedKey; // Train category
            EVC6_MMICurrentTrainData.MMI_M_BRAKE_PERC = 0; // Brake percentage
            EVC6_MMICurrentTrainData.MMI_NID_KEY_AXLE_LOAD = Variables.MMI_NID_KEY.NoDedicatedKey; // Axle load category
            EVC6_MMICurrentTrainData.MMI_M_AIRTIGHT = 0; // Train equipped with airtight system
            EVC6_MMICurrentTrainData.MMI_NID_KEY_LOAD_GAUGE =
                Variables.MMI_NID_KEY_Load_Gauge.NoDedicatedKey; // Loading gauge type of train 
            EVC6_MMICurrentTrainData.MMI_M_BUTTONS =
                EVC6_MMICurrentTrainData.MMI_M_BUTTONS_CURRENT_TRAIN_DATA.BTN_YES_DATA_ENTRY_COMPLETE;
            EVC6_MMICurrentTrainData.MMI_M_TRAINSET_ID = 15; // Preselected Trainset ID
            EVC6_MMICurrentTrainData.MMI_M_ALT_DEM = 0; // No alternative train data available
            EVC6_MMICurrentTrainData.TrainSetCaptions = new List<string>(Variables.paramEvc6FixedTrainsetCaptions);
            EVC6_MMICurrentTrainData.DataElements = new List<Variables.DataElement>(); // No train data elements
            EVC6_MMICurrentTrainData.SetWithoutSending();

            // Send EVC 10
            Send_EVC10_MMIEchoedTrainData_FixedDataEntry(pool, Variables.paramEvc6FixedTrainsetCaptions);
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
            ((TestcaseBase)pool).StartUp();

            Set_Driver_ID(pool, "1234");
            Send_SB_Mode(pool);
            ShowInstruction(pool, "Enter and confirm Driver ID");

            Request_Brake_Test(pool);
            ShowInstruction(pool, "Perform Brake Test");
            Perform_Brake_Test(pool, 2);
            pool.Wait_Realtime(5000);
            Display_Brake_Test_Successful(pool, 3);

            Display_Level_Window(pool);
            Delete_Brake_Test_Successful(pool, 3);

            ShowInstruction(pool, "Select and enter Level 1");

            Display_Main_Window_with_Start_button_not_enabled(pool);
            ShowInstruction(pool, @"Press ‘Train data’ button");

            Display_Fixed_Train_Data_Window(pool);
            ShowInstruction(pool, @"Enter FLU and confirm value in each input field.");

            Enable_Fixed_Train_Data_Validation(pool, Variables.Fixed_Trainset_Captions.FLU);
            ShowInstruction(pool, @"Press ‘Yes’ button.");

            Complete_Fixed_Train_Data_Entry(pool, Variables.Fixed_Trainset_Captions.FLU);
            ShowInstruction(pool, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                  Environment.NewLine +
                                  "1. Press ‘Yes’ button." + Environment.NewLine +
                                  "2. Confirmed the selected value by pressing the input field." + Environment.NewLine +
                                  "3. Press OK on THIS window.");

            Display_Train_data_validation_Window(pool);
            ShowInstruction(pool, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                  Environment.NewLine +
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
        /// Description: Perform SoM in SR mode, Level 2
        /// Used in:
        ///     Step 1 in TC-ID: 1.9 in 6.9 Performance of ETCS-DMI: Data handling
        ///     Step 1 in TC-ID: 1.10 in 6.10 Performance of ETCS-DMI Data Processing
        ///     Step 3 in TC-ID: 18.1.1.1.1 in 23.1.1.1.1 Concise Visualization
        ///     Step 2 in TC-ID: 18.1.1.1.2 in 23.1.1.1.2 Verbose Visualization 
        /// </summary>
        public static void Perform_SoM_in_SR_mode_Level_2(SignalPool pool)
        {
            ((TestcaseBase)pool).StartUp();

            Set_Driver_ID(pool, "1234");
            Send_SB_Mode(pool);
            ShowInstruction(pool, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                  Environment.NewLine +
                                  "1. Enter and confirm Driver ID." + Environment.NewLine +
                                  "2. Press OK on THIS window.");

            Request_Brake_Test(pool);
            ShowInstruction(pool, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                  Environment.NewLine +
                                  "1. Perform Brake Test" + Environment.NewLine +
                                  "2. Press OK on THIS window.");
            Perform_Brake_Test(pool, 2);
            pool.Wait_Realtime(5000);
            Display_Brake_Test_Successful(pool, 3);

            Display_Level_Window(pool);
            Delete_Brake_Test_Successful(pool, 3);
            ShowInstruction(pool, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                  Environment.NewLine +
                                  "1. Select and enter Level 2" + Environment.NewLine +
                                  "2. Press OK on THIS window.");

            Display_RBC_Contact_Window(pool);
            ShowInstruction(pool, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                  Environment.NewLine +
                                  "1. Select and enter 'Contact last RBC'" + Environment.NewLine +
                                  "2. Press OK on THIS window.");

            Show_RBC_Connection_Lost_Symbol(pool);
            pool.Wait_Realtime(5000);
            Remove_RBC_Connection_Lost_Symbol(pool);
            pool.Wait_Realtime(5000);
            Show_RBC_Connection_Established_Symbol(pool);

            Display_Main_Window_with_Start_button_not_enabled(pool);
            ShowInstruction(pool, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                  Environment.NewLine +
                                  "1. Press ‘Train data’ button." + Environment.NewLine +
                                  "2. Press OK on THIS window.");

            Display_Fixed_Train_Data_Window(pool);
            ShowInstruction(pool, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                  Environment.NewLine +
                                  "1. Enter FLU and confirm value in each input field." + Environment.NewLine +
                                  "2. Press OK on THIS window.");

            Enable_Fixed_Train_Data_Validation(pool, Variables.Fixed_Trainset_Captions.FLU);
            ShowInstruction(pool, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                  Environment.NewLine +
                                  "1. Press ‘Yes’ button." + Environment.NewLine +
                                  "2. Press OK on THIS window.");

            Complete_Fixed_Train_Data_Entry(pool, Variables.Fixed_Trainset_Captions.FLU);
            Display_Train_data_validation_Window(pool);
            ShowInstruction(pool, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                  Environment.NewLine +
                                  "1. Press ‘Yes’ button." + Environment.NewLine +
                                  "2. Confirmed the selected value by pressing the input field." + Environment.NewLine +
                                  "3. Press OK on THIS window.");

            Display_Train_data_validation_Window(pool);
            ShowInstruction(pool, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                  Environment.NewLine +
                                  "1. Press ‘Yes’ button." + Environment.NewLine +
                                  "2. Confirmed the selected value by pressing the input field.");

            Display_TRN_Window(pool);
            ShowInstruction(pool, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                  Environment.NewLine +
                                  "1. Enter and confirm Train Running Number." + Environment.NewLine +
                                  "2. Press OK on THIS window.");

            Display_Main_Window_with_Start_button_enabled(pool);
            ShowInstruction(pool, @"Perform the following actions on the DMI: " + Environment.NewLine +
                                  Environment.NewLine +
                                  "1. Press ‘Start’ button." + Environment.NewLine +
                                  "2. Press OK on THIS window.");

            Send_SR_Mode_Ack(pool);
            ShowInstruction(pool, @"Perform the following action after pressing OK: " + Environment.NewLine +
                                  Environment.NewLine +
                                  "1. Press DMI Sub Area C1.");

            Send_SR_Mode(pool);
            Send_L2(pool);
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
        /// Description: Open the Settings window
        /// Used in:
        ///     Step 1 in TC-ID: 22.27 in 27.27
        /// </summary>
        public static void Open_the_Settings_window(SignalPool pool)
        {
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = Variables.standardFlags;
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Settings;
            EVC30_MMIRequestEnable.Send();
        }

        /// <summary>
        /// Description: Open the Special window
        /// Used in:
        ///     Step 6 in TC-ID: 14.1 in 19.1
        /// </summary>
        public static void Open_the_Special_window(SignalPool pool)
        {
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = Variables.standardFlags;
            EVC30_MMIRequestEnable.MMI_NID_WINDOW = EVC30_MMIRequestEnable.WindowID.Special;
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
            Perform_the_following_procedure_Enter_and_confirm_all_data_in_Train_data_window_Press_Yes_button(
                SignalPool pool)
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
            Perform_the_following_procedure_Press_Train_data_button_Enter_and_confirm_all_data_in_Train_data_window_Press_Yes_button(
                SignalPool pool)
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
            pool.TraceInfo("Drive train forward passing BG1 - " +
                      "not valid in static testing");
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
            pool.TraceInfo("Drive train forward passing BG1 - " +
                      "not valid in static testing");
        }

        /// <summary>
        /// Description: Force the train into TR mode by moving the train forward to position of EOA
        /// Used in:
        ///     Step 2 in TC-ID: 12.3.10 in 17.3.10 Speed Pointer: Colour of speed pointer in TR mode and PT mode
        ///     Step 6 in TC-ID: 15.1.1 in 20.1.1 Mode Symbols in Sub-Area B7 for SB, SR, FS, TR, PT, SH, NL and SF mode
        /// </summary>
        public static void Force_train_forward_overpassing_EOA(SignalPool pool)
        {
            pool.TraceInfo("Drive train forward passing EOA - " +
                      "not valid in static testing");
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
            pool.TraceInfo("Drive train forward - " +
                      "not valid in static testing");
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
        public static void Drive_the_train_forward_pass_BG0_with_MA_and_Track_descriptionPkt_12_21_and_27(
            SignalPool pool)
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
        public static void Stop_the_train_when_the_track_condition_symbol_has_been_removed_from_sub_area_B3(
            SignalPool pool)
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
        /// Description: This step is to complete the process of ‘Train data’:- Press the ‘Yes’ button on the ‘Train data’ window.- Validate the data in the data validation window
        /// Used in:
        ///     Step 11 in TC-ID: 22.29.3 in 27.29.3 ‘Train data’ (Flexible) Data Checks: Technical Range Checks by Data Validity
        ///     Step 7 in TC-ID: 22.29.4 in 27.29.4 ‘Train data’ Data Checks: Technical Range Checks by Variable Range
        /// </summary>
        public static void
            This_step_is_to_complete_the_process_of_Train_data_Press_the_Yes_button_on_the_Train_data_window_Validate_the_data_in_the_data_validation_window(
                SignalPool pool)
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
    }
}