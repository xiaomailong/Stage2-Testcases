#region usings

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
using Testcase.Telegrams.EVCtoDMI;
using Testcase.Telegrams.DMItoEVC;

#endregion

namespace Testcase.XML
{
    /// <summary>
    /// Values of 5.10.a.xml file
    /// </summary>
    static class XML_5_10_a
    {
        private static SignalPool _pool;

        public static void Send(SignalPool pool)
        {
            _pool = pool;
            

            // Step 2
            // Assume flags are initially on and setting them disabled changes the state on the DMI
            EVC30_MMIRequestEnable.SendBlank(); 

            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.ExitShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EOA |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Adhesion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.UseShortNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.GeographicalPosition |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EndOfDataEntryNTC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Reserved |
                                                                EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableBrakePercentage) &
                                                                ~(EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                                  EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                                  EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                                  EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                                  EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber |
                                                                  EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                                  EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                                  EVC30_MMIRequestEnable.EnabledRequests.MaintainShunting);
            EVC30_MMIRequestEnable.Send();

            _pool.WaitForVerification("Check that the following buttons are displayed with a border with Dark-Grey text:" + Environment.NewLine + Environment.NewLine +
                                      @"1. The ‘Start’ button." + Environment.NewLine +
                                      @"2. The ‘Driver ID’ button." + Environment.NewLine +
                                      @"3. The ‘Train data’ button." + Environment.NewLine +
                                      @"4. The ‘Level’ button." + Environment.NewLine +
                                      @"5. The ‘Train running number’ button." + Environment.NewLine +
                                      @"6. The ‘Shunting’ button." + Environment.NewLine +
                                      @"7. The ‘Non - Leading’ button." + Environment.NewLine +
                                      @"8. The ‘Maintain Shunting’ button");


            // Step 3
            DMITestCases.DmiActions.ShowInstruction(_pool, "Press ‘Exit’ button then select Override menu");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                                EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ExitShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                                EVC30_MMIRequestEnable.EnabledRequests.MaintainShunting | 
                                                                EVC30_MMIRequestEnable.EnabledRequests.Adhesion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.UseShortNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.GeographicalPosition |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EndOfDataEntryNTC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Reserved |
                                                                EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableBrakePercentage) &
                                                               ~EVC30_MMIRequestEnable.EnabledRequests.EOA;
            EVC30_MMIRequestEnable.Send();

            _pool.WaitForVerification("Check that the following button is displayed with a border with Dark-Grey text:" + Environment.NewLine + Environment.NewLine +
                                      @"1. The ‘EOA’ button.");


            // Step 4
            DMITestCases.DmiActions.ShowInstruction(_pool, "Press the ‘Exit’ button and select the Special menu");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                                EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ExitShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                                EVC30_MMIRequestEnable.EnabledRequests.MaintainShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EOA |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.UseShortNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.GeographicalPosition |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EndOfDataEntryNTC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Reserved |
                                                                EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableBrakePercentage) &
                                                               ~(EVC30_MMIRequestEnable.EnabledRequests.Adhesion |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity);
            EVC30_MMIRequestEnable.Send();

            _pool.WaitForVerification("Check that the following buttons are displayed with a border with Dark-Grey text:" + Environment.NewLine + Environment.NewLine +
                                      @"1. The ‘Adhesion’ button." + Environment.NewLine +
                                      @"2. The ‘SR speed/distance’ button." + Environment.NewLine +
                                      @"3. The ‘Train Integrity’ button.");

            // Step 5
            DMITestCases.DmiActions.ShowInstruction(_pool, "Press the ‘Exit’ button and select the Settings menu");

            // The spec indicates 9 bits to set but only tests 8 buttons: assume the first 6 are correct and Set Clock (#25) is SetLocalTimeDateAndOffset
            // Bit #32 would be in the next word so if MMI_Q_REQUEST_ENABLE_LOW is available and the only button to be set it could be a bool as suggested
            // but not implemented
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                                EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ExitShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                                EVC30_MMIRequestEnable.EnabledRequests.MaintainShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EOA |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Adhesion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.UseShortNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.GeographicalPosition |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EndOfDataEntryNTC | 
                                                                EVC30_MMIRequestEnable.EnabledRequests.Reserved |
                                                                EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableBrakePercentage) &
                                                               ~(EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |                                                               
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset);

            // Other signals need setting 
            // from VSIS looks like the other buttons are controlled by MMI_Q_REQUEST_ENABLE_LOW
            //EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_LOW = true;          // System info enabled

            EVC30_MMIRequestEnable.Send();

            _pool.WaitForVerification("Check that the following buttons are displayed with a border with Dark-Grey text:" + Environment.NewLine + Environment.NewLine +
                                      @"1. The ‘Language’ button." + Environment.NewLine +
                                      @"2. The ‘Volume’ button." + Environment.NewLine +
                                      @"3. The ‘Brightness’ button." + Environment.NewLine +
                                      @"4. The ‘System version’ button." + Environment.NewLine +
                                      @"5. The ‘Set VBC’ button." + Environment.NewLine +
                                      @"6. The ‘Remove VBC’ button." + Environment.NewLine +
                                      @"7. The ‘Set Clock’ button." + Environment.NewLine +
                                      @"8. The ‘System info’ button");

            // Step 6
            // De-activate and activate cabin
            // More to do??
            EVC2_MMIStatus.MMI_M_ACTIVE_CABIN = Variables.MMI_M_ACTIVE_CABIN.NoCabinActive;
            EVC2_MMIStatus.Send();

             EVC2_MMIStatus.MMI_M_ACTIVE_CABIN = Variables.MMI_M_ACTIVE_CABIN.Cabin1Active;
            EVC2_MMIStatus.Send();

            DMITestCases.DmiActions.Set_Driver_ID(_pool, "1234");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;
            DMITestCases.DmiExpectedResults.SB_Mode_displayed(_pool);


            // Step 7
            DMITestCases.DmiActions.ShowInstruction(_pool, "Press Setting menu and select Maintenance button and password");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                                EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ExitShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                                EVC30_MMIRequestEnable.EnabledRequests.MaintainShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EOA |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Adhesion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.UseShortNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.GeographicalPosition |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EndOfDataEntryNTC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Reserved |
                                                                EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest)  |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableBrakePercentage &
                                                               ~(EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler);
            EVC30_MMIRequestEnable.Send();

            _pool.WaitForVerification("Check that the following buttons are displayed with a border with Dark-Grey text:" + Environment.NewLine + Environment.NewLine +
                                      @"1. The ‘Wheel diameter’ button." + Environment.NewLine +
                                      @"2. The ‘Radar’ button.");       // aka Enable Doppler

            // Step 8
            // De-activate and activate cabin
            // More to do????
            EVC2_MMIStatus.MMI_M_ACTIVE_CABIN = Variables.MMI_M_ACTIVE_CABIN.NoCabinActive;
            EVC2_MMIStatus.Send();

            EVC2_MMIStatus.MMI_M_ACTIVE_CABIN = Variables.MMI_M_ACTIVE_CABIN.Cabin1Active;
            EVC2_MMIStatus.Send();

            DMITestCases.DmiActions.Set_Driver_ID(_pool, "1234");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;

            DMITestCases.DmiExpectedResults.SB_Mode_displayed(_pool);

            // Step 9
            DMITestCases.DmiActions.ShowInstruction(_pool, "Enter Driver ID. Skip the brake test. Select Level 1 then shunting mode");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.Shunting;
            DMITestCases.DmiExpectedResults.SH_Mode_displayed(_pool);

            // Step 10
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                                EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                                EVC30_MMIRequestEnable.EnabledRequests.MaintainShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EOA |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Adhesion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.UseShortNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.GeographicalPosition |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EndOfDataEntryNTC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Reserved |
                                                                EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableBrakePercentage) &
                                                               ~EVC30_MMIRequestEnable.EnabledRequests.ExitShunting;
            EVC30_MMIRequestEnable.Send();

            _pool.WaitForVerification("Check that the following button is displayed with a border with Dark-Grey text:" + Environment.NewLine + Environment.NewLine +
                                      @"1. The ‘Exit Shunting’ button.");

            // Step 11
            // More to do????
            EVC2_MMIStatus.MMI_M_ACTIVE_CABIN = Variables.MMI_M_ACTIVE_CABIN.NoCabinActive;
            EVC2_MMIStatus.Send();

            EVC2_MMIStatus.MMI_M_ACTIVE_CABIN = Variables.MMI_M_ACTIVE_CABIN.Cabin1Active;
            EVC2_MMIStatus.Send();

            DMITestCases.DmiActions.Set_Driver_ID(_pool, "1234");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;
            DMITestCases.DmiExpectedResults.SB_Mode_displayed(_pool);


            // Step 12
            DMITestCases.DmiActions.Complete_SoM_L1_SR(_pool); 

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StaffResponsible;
            DMITestCases.DmiExpectedResults.SR_Mode_displayed(_pool);


            // Step 13
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 40;

            DMITestCases.DmiActions.ShowInstruction(_pool, "Select Settings menu");

            // this relies on ETCS data so packet required...

            _pool.WaitForVerification("Check that the following buttons are displayed with a border with Dark-Grey text:" + Environment.NewLine + Environment.NewLine +
                                      @"1. The ‘Lock screen for cleaning’ button." + Environment.NewLine +
                                      @"2. The ‘Brake’ button." + Environment.NewLine +
                                      @"3. The ‘National’ button." + Environment.NewLine +
                                      @"4. The ‘Maintenance’ button.");

            // Step 14
            // Don't know what to do here...
            //DMITestCases.DmiActions.Pass_BG1_with_Pkt_12_21_and_27(this);

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.FullSupervision;
            EVC102_MMIStatusReport.Check_MMI_M_MODE_READBACK = EVC102_MMIStatusReport.MMI_M_MODE_READBACK.FullSupervision;
            DMITestCases.DmiExpectedResults.Driver_symbol_displayed(_pool, "Full Supervision mode", "MO11", "B7", false);


            // Step 15
            // Pass BG2 with pkt 79 Geographical position  ???
            // rob's telegram
            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                                EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ExitShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                                EVC30_MMIRequestEnable.EnabledRequests.MaintainShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EOA |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Adhesion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.UseShortNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EndOfDataEntryNTC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Reserved |
                                                                EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableBrakePercentage) &
                                                               ~EVC30_MMIRequestEnable.EnabledRequests.GeographicalPosition;
            EVC30_MMIRequestEnable.Send();

            _pool.WaitForVerification("Check that the following button is displayed with a border with Dark-Grey text:" + Environment.NewLine + Environment.NewLine +
                                      @"1. The ‘Geograhical position’ button.");


            // Step 16
            // Stop the train
            EVC1_MMIDynamic.MMI_V_TRAIN_KMH = 0;

            // Train is at a standstill
            //_pool.WaitForVerification("Check the following:" + Environment.NewLine + Environment.NewLine +
            //                          "1. DMI displays speed = 0 km/h.");


            // Step 17
            // More to do????
            EVC2_MMIStatus.MMI_M_ACTIVE_CABIN = Variables.MMI_M_ACTIVE_CABIN.NoCabinActive;
            EVC2_MMIStatus.Send();

            EVC2_MMIStatus.MMI_M_ACTIVE_CABIN = Variables.MMI_M_ACTIVE_CABIN.Cabin1Active;
            EVC2_MMIStatus.Send();

            DMITestCases.DmiActions.Set_Driver_ID(_pool, "1234");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;
            DMITestCases.DmiExpectedResults.SB_Mode_displayed(_pool);

            // Step 18
            DMITestCases.DmiActions.ShowInstruction(_pool, "Enter Driver ID and perform brake test. Select and confirm Level 2");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                                EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ExitShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                                EVC30_MMIRequestEnable.EnabledRequests.MaintainShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EOA |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Adhesion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.GeographicalPosition |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EndOfDataEntryNTC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Reserved |
                                                                EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableBrakePercentage) &
                                                               ~(EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.UseShortNumber |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                                                                 EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID);
            EVC30_MMIRequestEnable.Send();

            _pool.WaitForVerification("Check that the following buttons are displayed with a border with Dark-Grey text:" + Environment.NewLine + Environment.NewLine +
                                      @"1. The ‘Contract last window’ button." + Environment.NewLine +
                                      @"2. The ‘Use short number’ button." + Environment.NewLine +
                                      @"3. The ‘Enter RBC data’ button." + Environment.NewLine +
                                      @"4. The ‘Radio Network ID’ button.");

            // Step 19
            // More to do????
            EVC2_MMIStatus.MMI_M_ACTIVE_CABIN = Variables.MMI_M_ACTIVE_CABIN.NoCabinActive;
            EVC2_MMIStatus.Send();

            EVC2_MMIStatus.MMI_M_ACTIVE_CABIN = Variables.MMI_M_ACTIVE_CABIN.Cabin1Active;
            EVC2_MMIStatus.Send();

            DMITestCases.DmiActions.Set_Driver_ID(_pool, "1234");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;
            DMITestCases.DmiExpectedResults.SB_Mode_displayed(_pool);


            // Step 20
            DMITestCases.DmiActions.ShowInstruction(_pool, "Activate Cabin A. Enter Driver ID and perform brake test. Select and confirm Level STM PLZB. Enter train data and confirm entry");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                                EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ExitShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                                EVC30_MMIRequestEnable.EnabledRequests.MaintainShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EOA |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Adhesion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.UseShortNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.GeographicalPosition |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Reserved |
                                                                EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableBrakePercentage) &
                                                               ~EVC30_MMIRequestEnable.EnabledRequests.EndOfDataEntryNTC;
            EVC30_MMIRequestEnable.Send();

            _pool.WaitForVerification("Check that the following button is displayed with a border with Dark-Grey text:" + Environment.NewLine + Environment.NewLine +
                                      @"1. The ‘End of data entry’ button.");

            // Step 21
            // More to do????
            EVC2_MMIStatus.MMI_M_ACTIVE_CABIN = Variables.MMI_M_ACTIVE_CABIN.NoCabinActive;
            EVC2_MMIStatus.Send();

            EVC2_MMIStatus.MMI_M_ACTIVE_CABIN = Variables.MMI_M_ACTIVE_CABIN.Cabin1Active;
            EVC2_MMIStatus.Send();

            DMITestCases.DmiActions.Set_Driver_ID(_pool, "1234");

            EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_Mode = EVC7_MMIEtcsMiscOutSignals.MMI_OBU_TR_M_MODE.StandBy;
            DMITestCases.DmiExpectedResults.SB_Mode_displayed(_pool);

            // Step 22
            DMITestCases.DmiActions.ShowInstruction(_pool, @"Press the ‘Close’ button in the Main window. " + Environment.NewLine +
                                                           @"Press the ‘Settings’ button. Press the ‘Brake’ button");

            EVC30_MMIRequestEnable.SendBlank();
            EVC30_MMIRequestEnable.MMI_Q_REQUEST_ENABLE_HIGH = (EVC30_MMIRequestEnable.EnabledRequests.Start |
                                                                EVC30_MMIRequestEnable.EnabledRequests.DriverID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Level |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainRunningNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Shunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ExitShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.NonLeading |
                                                                EVC30_MMIRequestEnable.EnabledRequests.MaintainShunting |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EOA |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Adhesion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SRSpeedDistance |
                                                                EVC30_MMIRequestEnable.EnabledRequests.TrainIntegrity |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Language |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Volume |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Brightness |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SystemVersion |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RemoveVBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.ContactLastRBC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.UseShortNumber |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnterRBCData |
                                                                EVC30_MMIRequestEnable.EnabledRequests.RadioNetworkID |
                                                                EVC30_MMIRequestEnable.EnabledRequests.GeographicalPosition |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EndOfDataEntryNTC |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalTimeDateAndOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.SetLocalOffset |
                                                                EVC30_MMIRequestEnable.EnabledRequests.Reserved |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableWheelDiameter |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableDoppler |
                                                                EVC30_MMIRequestEnable.EnabledRequests.EnableBrakePercentage) &
                                                               ~EVC30_MMIRequestEnable.EnabledRequests.StartBrakeTest;
            EVC30_MMIRequestEnable.Send();

            _pool.WaitForVerification("Check that the following button is displayed with a border with Dark-Grey text:" + Environment.NewLine + Environment.NewLine +
                                      @"1. The ‘Brake test’ button.");
        }
    }
}