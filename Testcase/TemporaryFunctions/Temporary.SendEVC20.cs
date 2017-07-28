using System;

namespace Testcase.TemporaryFunctions
{
    internal static partial class Temporary
    {
        /// <summary>
        ///     SendEVC20_MMI_Select_Level
        ///     Sends ETCS and NTC levels and related additional status information.
        ///     <param name="mmiMCurrentLevel[k]">Last used level</param>
        ///     <param name="mmiMLevelFlag[k]">Marker to indicate if a level button is enabled or disabled.</param>
        ///     <param name="mmiMInhibitedLevel[k]">Inhibit status</param>
        ///     <param name="mmiMInhibitEnable[k]">Inhibit enabled</param>
        ///     <param name="mmiMLevelNtcId[k]">Identity of level or NTC</param>
        ///     <param name="mmiQCloseEnable">Close Button Enable</param>
        public static void SendEVC20_MMISelectLevel(SoM_Level1 soMLevel1, bool[] mmiQLevelNtcId,
            bool[] mmiMCurrentLevel,
            bool[] mmiMLevelFlag,
            bool[] mmiMInhibitedLevel, bool[] mmiMInhibitEnable, uint[] mmiMLevelNtcId,
            bool mmiQCloseEnable)
        {
            soMLevel1.SITR.ETCS1.SelectLevel.MmiMPacket.Value = 20; // Packet Id

            var numberOfLevels = (ushort) mmiQLevelNtcId.Length;
            soMLevel1.SITR.ETCS1.SelectLevel.MmiNLevels.Value = numberOfLevels; // Number of levels

            // Dynamic fields
            for (var k = 0; k < numberOfLevels; k++)
            {
                // Implementing EVC20_alias_1[k]
                uint uintMmiQLevelNtcId = Convert.ToUInt32(mmiQLevelNtcId[k]);
                uintMmiQLevelNtcId <<= 7;

                uint uintMmiMCurrentLevel = Convert.ToUInt32(mmiMCurrentLevel[k]);
                uintMmiMCurrentLevel <<= 6;

                uint uintMmiMLevelFlag = Convert.ToUInt32(mmiMLevelFlag[k]);
                uintMmiMLevelFlag <<= 5;

                uint uintMmiMInhibitedLevel = Convert.ToUInt32(mmiMInhibitedLevel[k]);
                uintMmiMInhibitedLevel <<= 4;

                uint uintMmiMInhibitEnable = Convert.ToUInt32(mmiMInhibitEnable[k]);
                uintMmiMInhibitEnable <<= 3;


                byte evc20Alias1 = Convert.ToByte(uintMmiQLevelNtcId | uintMmiMCurrentLevel |
                                                  uintMmiMLevelFlag | uintMmiMInhibitedLevel |
                                                  uintMmiMInhibitEnable);

                if (k < 10)
                {
                    soMLevel1.SITR.Client.Write("ETCS1_SelectLevel_EVC20SelectLevelSub0" + k + "_EVC20alias1",
                        evc20Alias1);
                    soMLevel1.SITR.Client.Write("ETCS1_SelectLevel_EVC20SelectLevelSub0" + k + "_MmiMLevelNtcId",
                        Convert.ToByte(mmiMLevelNtcId[k]));
                }
                else
                {
                    soMLevel1.SITR.Client.Write("ETCS1_SelectLevel_EVC20SelectLevelSub" + k + "_EVC20alias1",
                        evc20Alias1);
                    soMLevel1.SITR.Client.Write("ETCS1_SelectLevel_EVC20SelectLevelSub" + k + "_MmiMLevelNtcId",
                        Convert.ToByte(mmiMLevelNtcId[k]));
                }
            }

            uint uintMmiQCloseEnable = Convert.ToUInt32(mmiQCloseEnable);
            uintMmiQCloseEnable <<= 7;

            soMLevel1.SITR.ETCS1.SelectLevel.MmiQCloseEnable.Value =
                Convert.ToByte(uintMmiQCloseEnable); // Close Button enable?
            soMLevel1.SITR.ETCS1.SelectLevel.MmiLPacket.Value =
                Convert.ToUInt16(56 + numberOfLevels * 16); // Packet length

            soMLevel1.SITR.SMDCtrl.ETCS1.SelectLevel.Value = 0x9;
        }

        /// <summary>
        ///     Send standard EVC-20 telegram with Levels 0-3, CBTC, and AWS/TPWS selectable. Level 1 is preselected.
        /// </summary>
        /// <param name="soMLevel1"></param>
        public static void SendEVC20_MMISelectLevel_AllLevels(SoM_Level1 soMLevel1)
        {
            bool[] paramEvc20MmiQLevelNtcId = {true, true, true, true, false, false};
            bool[] paramEvc20MmiMCurrentLevel = {false, true, false, false, false, false};
            bool[] paramEvc20MmiMLevelFlag = {true, true, true, true, true, true};
            bool[] paramEvc20MmiMInhibitedLevel = {false, false, false, false, false, false};
            bool[] paramEvc20MmiMInhibitEnable = {true, true, true, true, true, true};
            uint[] paramEvc20MmiMLevelNtcId = {0, 1, 2, 3, 50, 20}; // 50 = CBTC, 20 = AWS/TPWS

            SendEVC20_MMISelectLevel(soMLevel1, paramEvc20MmiQLevelNtcId, paramEvc20MmiMCurrentLevel,
                paramEvc20MmiMLevelFlag, paramEvc20MmiMInhibitedLevel,
                paramEvc20MmiMInhibitEnable, paramEvc20MmiMLevelNtcId,
                true);
        }

        /// <summary>
        ///     Sends EVC-20 telegram to cancel previous MMI_Select_Level presentation
        /// </summary>
        /// <param name="soMLevel1"></param>
        public static void SendEVC20_MMISelectLevel_Cancel(SoM_Level1 soMLevel1)
        {
            soMLevel1.SITR.ETCS1.SelectLevel.MmiMPacket.Value = 20; // Packet Id
            soMLevel1.SITR.ETCS1.SelectLevel.MmiNLevels.Value =
                0; // No levels - Cancel presentation of previous MMI_Select_Level
            soMLevel1.SITR.ETCS1.SelectLevel.MmiQCloseEnable.Value = 0x08; // Close enabled
            soMLevel1.SITR.ETCS1.SelectLevel.MmiLPacket.Value = 56; // Packet length
        }
    }
}