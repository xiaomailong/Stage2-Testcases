using System;
using System.Collections.Generic;
using CL345;
using static Testcase.Telegrams.EVCtoDMI.Variables;

namespace Testcase.Telegrams.EVCtoDMI
{
    public static class EVC20_MMISelectLevel
    {
        private static SignalPool _pool;
        private static MMI_Q_LEVEL_NTC_ID[] _qLevelNtcId;
        private static MMI_M_CURRENT_LEVEL[] _mCurrentLevel;
        private static MMI_M_LEVEL_FLAG[] _mLevelFlag;
        private static MMI_M_INHIBITED_LEVEL[] _mInhibitedLevel;
        private static MMI_M_INHIBIT_ENABLE[] _mInhibitEnable;
        private static MMI_M_LEVEL_NTC_ID[] _mLevelNtcId;
        private static byte _qCloseEnable;

        /// <summary>
        /// Initialise an instance of EVC-20 MMI Select Level telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // Set as dynamic
            _pool.SITR.SMDCtrl.ETCS1.SelectLevel.Value = 0x8;

            // Set default values
            _pool.SITR.ETCS1.SelectLevel.MmiMPacket.Value = 20; // Packet ID
        }

        private static void SetAliasK()
        {
            if (!(Equals(_qLevelNtcId.Length, _mCurrentLevel.Length) &&
                  Equals(_mCurrentLevel.Length, _mLevelFlag.Length) &&
                  Equals(_mLevelFlag.Length, _mInhibitedLevel.Length) &&
                  Equals(_mInhibitedLevel.Length, _mInhibitEnable.Length) &&
                  Equals(_mInhibitEnable.Length, _mLevelNtcId.Length) &&
                  Equals(_mLevelNtcId.Length, _mCurrentLevel.Length)))
            {
                throw new Exception("Number of Level does not match!");
            }

            var _nLevels = (ushort) _qLevelNtcId.Length;
            _pool.SITR.ETCS1.SelectLevel.MmiNLevels.Value = _nLevels;

            for (var k = 0; k < _nLevels; k++)
            {
                
            }
        }

        public static void Send()
        {
            if (TrainSetCaptions.Count > 9)
                throw new ArgumentOutOfRangeException();
            if (DataElements.Count > 9)
                throw new ArgumentOutOfRangeException();
            if (TrainSetCaptions.Count != DataElements.Count)
                throw new Exception("Number of RBC elements and number of captions do not match!");

            ushort totalSizeCounter = 160;

            // Set number of trainset captions
            _pool.SITR.ETCS1.CurrentTrainData.MmiNTrainset.Value = (ushort)TrainSetCaptions.Count;

            // Populate the array of trainset captions
            for (var trainsetIndex = 0; trainsetIndex < TrainSetCaptions.Count; trainsetIndex++)
            {
                var charArray = TrainSetCaptions[trainsetIndex].ToCharArray();
                if (charArray.Length > 12)
                    throw new ArgumentOutOfRangeException();

                // Set length of char array
                _pool.SITR.Client.Write(
                    $"ETCS1_CurrentTrainData_EVC06CurrentTrainDataSub1{trainsetIndex}_MmiNCaptionTrainset",
                    charArray.Length);

                totalSizeCounter += 16;

                for (var charIndex = 0; charIndex < charArray.Length; charIndex++)
                {
                    var character = charArray[charIndex];

                    // Trainset caption text character
                    if (charIndex < 10)
                    {
                        _pool.SITR.Client.Write(
                            $"ETCS1_CurrentTrainData_EVC06CurrentTrainDataSub1{trainsetIndex}_EVC06CurrentTrainDataSub110{charIndex}_MmiXCaptionTrainset",
                            character);
                    }
                    else
                    {
                        _pool.SITR.Client.Write(
                            $"ETCS1_CurrentTrainData_EVC06CurrentTrainDataSub1{trainsetIndex}_EVC06CurrentTrainDataSub11{charIndex}_MmiXCaptionTrainset",
                            character);
                    }
                    totalSizeCounter += 8;
                }
            }

            // Set number of train data elements
            _pool.SITR.ETCS1.CurrentTrainData.MmiNDataElements.Value = (ushort)DataElements.Count;

            totalSizeCounter = PopulateDataElements($"ETCS1_CurrentTrainData_EVC06CurrentTrainDataSub2",
                totalSizeCounter, DataElements, _pool);

            // Set the total length of the packet
            _pool.SITR.ETCS1.CurrentTrainData.MmiLPacket.Value = totalSizeCounter;

            _pool.SITR.SMDCtrl.ETCS1.SelectLevel.Value = 0x09;
        }

        public static MMI_Q_LEVEL_NTC_ID[] MMI_Q_LEVEL_NTC_ID
        {
            set
            {

            }
        }

        public static MMI_M_CURRENT_LEVEL[] MMI_M_CURRENT_LEVEL
        {
            set
            {

            }
        }

        public static MMI_M_LEVEL_FLAG[] MMI_M_LEVEL_FLAG
        {
            set
            {

            }
        }

        public static MMI_M_INHIBITED_LEVEL[] MMI_M_INHIBITED_LEVEL
        {
            set
            {

            }
        }

        public static MMI_M_INHIBIT_ENABLE[] MMI_M_INHIBIT_ENABLE
        {
            set
            {

            }
        }

        public static MMI_M_LEVEL_NTC_ID[] MMI_M_LEVEL_NTC_ID
        {
            set
            {

            }
        }

        /// <summary>
        /// Enabling close button in EVC-14, EVC-20 and EVC-22.
        /// 
        /// Bits:
        /// 0 = "disable/enable close button"
        /// 1..7 = "Spare"
        ///
        /// Note: Bit0 = 0 -> disable close button, Bit0 = 1 -> enable close button
        /// </summary>
        public static byte MMI_Q_CLOSE_ENABLE
        {
            set
            {
                _qCloseEnable = value;
                _qCloseEnable <<= 7;
                _pool.SITR.ETCS1.SelectLevel.MmiQCloseEnable.Value = _qCloseEnable;               
            }
        }

        public static List<string> TrainSetCaptions { get; set; }

        public static List<DataElement> DataElements { get; set; }
    }

    /// <summary>
    /// Last level used enum
    /// </summary>
    public enum MMI_M_CURRENT_LEVEL : byte
    {
        NotLatestUsedLevel = 0,
        LatestUsedLevel = 1
    }
}

