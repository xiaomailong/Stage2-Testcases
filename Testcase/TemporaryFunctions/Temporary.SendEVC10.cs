using System;

namespace Testcase.TemporaryFunctions
{
    internal static partial class Temporary
    {
        /// <summary>
        ///     Sends EVC-10 telegram with echoed train data. Reads all existing Current train data and bit-inverses them.
        /// </summary>
        /// <param name="soMLevel1"></param>
        public static void SendEVC10_MMIEchoedTrainData(SoM_Level1 soMLevel1)
        {
            // Packet ID
            soMLevel1.SITR.ETCS1.EchoedTrainData.MmiMPacket.Value = 10;

            // TODO is this what the VSIS says? Bit inverting the array length will break it
            ushort evc6MmiNTrainset = soMLevel1.SITR.ETCS1.CurrentTrainData.MmiNTrainset.Value;
            soMLevel1.SITR.ETCS1.EchoedTrainData.MmiNTrainsetsR.Value = Convert.ToUInt16(~evc6MmiNTrainset);

            // Dynamic fields 1st dimension
            ushort numberOfCaptionTrainset = 0; // To be used for Packet length

            for (var k = 0; k < evc6MmiNTrainset; k++)
            {
                // Bit-inverted Trainset caption text length
                ushort evc6MmiNCaptionTrainset = Convert.ToUInt16(soMLevel1.SITR.Client.Read(
                    "ETCS1_CurrentTrainData_EVC06CurrentTrainDataSub1" + k +
                    "_MmiNCaptionTrainset"));

                soMLevel1.SITR.Client.Write(
                    "ETCS1_EchoedTrainData_EVC10EchoedTrainDataSub1" + k + "_MmiNCaptionTrainsetR",
                    Convert.ToUInt16(~evc6MmiNCaptionTrainset));

                numberOfCaptionTrainset +=
                    evc6MmiNCaptionTrainset; // Total number of CaptionTrainset for the whole telegram

                // Dynamic fields 2nd dimension
                for (var l = 0; l < evc6MmiNCaptionTrainset; l++)
                    // Bit-inverted Trainset caption text
                    if (l < 10)
                    {
                        ushort evc6MmiXCaptionTrainset = Convert.ToUInt16(soMLevel1.SITR.Client.Read(
                            "ETCS1_CurrentTrainData_EVC06CurrentTrainDataSub1" + k +
                            "_EVC06CurrentTrainDataSub110" + l + "_MmiXCaptionTrainset"));

                        soMLevel1.SITR.Client.Write("ETCS1_EchoedTrainData_EVC10EchoedTrainDataSub1" + k +
                                                    "_EVC10EchoedTrainDataSub110" + l +
                                                    "_MmiXCaptionTrainsetR", Convert.ToChar(~evc6MmiXCaptionTrainset));
                    }

                    else
                    {
                        ushort evc6MmiXCaptionTrainset = Convert.ToUInt16(soMLevel1.SITR.Client.Read(
                            "ETCS1_CurrentTrainData_EVC06CurrentTrainDataSub1" + k +
                            "_EVC06CurrentTrainDataSub11" + l + "_MmiXCaptionTrainset"));

                        soMLevel1.SITR.Client.Write("ETCS1_EchoedTrainData_EVC10EchoedTrainDataSub1" + k +
                                                    "_EVC10EchoedTrainDataSub11" + l +
                                                    "_MmiXCaptionTrainsetR", Convert.ToChar(~evc6MmiXCaptionTrainset));
                    }
            }

            // EVC10_alias_1
            soMLevel1.SITR.ETCS1.EchoedTrainData.EVC10alias1.Value =
                (byte) ~soMLevel1.SITR.ETCS1.CurrentTrainData.EVC6alias1.Value;

            // Bit-inverted Loading gauge type of train 
            soMLevel1.SITR.ETCS1.EchoedTrainData.MmiNidKeyLoadGaugeR.Value =
                (byte) ~soMLevel1.SITR.ETCS1.CurrentTrainData.MmiNidKeyLoadGauge.Value;

            // Bit-inverted Train equipped with airtight system
            soMLevel1.SITR.ETCS1.EchoedTrainData.MmiMAirtightR.Value =
                (byte) ~soMLevel1.SITR.ETCS1.CurrentTrainData.MmiMAirtight.Value;

            // Bit-inverted Axle load category 
            soMLevel1.SITR.ETCS1.EchoedTrainData.MmiNidKeyAxleLoadR.Value =
                (byte) ~soMLevel1.SITR.ETCS1.CurrentTrainData.MmiNidKeyAxleLoad.Value;

            // Bit-inverted Max train speed
            soMLevel1.SITR.ETCS1.EchoedTrainData.MmiVMaxtrainR.Value =
                (byte) ~soMLevel1.SITR.ETCS1.CurrentTrainData.MmiVMaxtrain.Value;

            // Bit-inverted Max train length
            soMLevel1.SITR.ETCS1.EchoedTrainData.MmiLTrainR.Value =
                (byte) ~soMLevel1.SITR.ETCS1.CurrentTrainData.MmiLTrain.Value;

            // Bit-inverted Brake percentage
            soMLevel1.SITR.ETCS1.EchoedTrainData.MmiMBrakePercR.Value =
                (byte) ~soMLevel1.SITR.ETCS1.CurrentTrainData.MmiMBrakePerc.Value;

            // Bit-inverted Train category
            soMLevel1.SITR.ETCS1.EchoedTrainData.MmiNidKeyTrainCatR.Value =
                (byte) ~soMLevel1.SITR.ETCS1.CurrentTrainData.MmiNidKeyTrainCat.Value;

            // Bit-inverted Train data enabled
            soMLevel1.SITR.ETCS1.EchoedTrainData.MmiMDataEnableR.Value =
                (byte) ~soMLevel1.SITR.ETCS1.CurrentTrainData.MmiMDataEnable.Value;

            // Packet length
            soMLevel1.SITR.ETCS1.EchoedTrainData.MmiLPacket.Value =
                Convert.ToUInt16(144 + evc6MmiNTrainset * 16 + numberOfCaptionTrainset * 8);

            soMLevel1.SITR.SMDCtrl.ETCS1.EchoedTrainData.Value = 0x09;
        }
    }
}