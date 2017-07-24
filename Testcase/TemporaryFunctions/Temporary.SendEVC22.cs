using System;
using System.Linq;

namespace Testcase.TemporaryFunctions
{
    internal static partial class Temporary
    {
        /// <summary>
        /// Send_EVC22_MMI_Current_Rbc_Data sends RBC Data to the DMI
        /// </summary>
        public static void Send_EVC22_MMI_Current_Rbc(SoM_Level1 soMLevel1, uint mmiNidRbc, uint[] mmiNidRadio,
            byte mmiNidWindow,
            byte mmiQCloseEnable,
            byte mmiMButtons, string[] captionNetworks, byte[] mmiNidData, byte[] mmiQDataCheck,
            string[] textDataElements)
        {
            soMLevel1.SITR.ETCS1.CurrentRbcData.MmiMPacket.Value = 22; // Packet Id
            soMLevel1.SITR.ETCS1.CurrentRbcData.MmiNidRbc.Value = mmiNidRbc; // RBC Id
            soMLevel1.SITR.ETCS1.CurrentRbcData.MmiNidRadio.Value = mmiNidRadio; // RBC phone number
            soMLevel1.SITR.ETCS1.CurrentRbcData.MmiNidWindow.Value = mmiNidWindow; // ETCS Window Id
            soMLevel1.SITR.ETCS1.CurrentRbcData.MmiQCloseEnable.Value = mmiQCloseEnable; // Close button enable?
            soMLevel1.SITR.ETCS1.CurrentRbcData.MmiMButtons.Value = mmiMButtons; // Buttons available

            //Networks information
            ushort numberOfNetworks = Convert.ToUInt16(captionNetworks.Length);

            // Limit the number of networks to 10 (range : 0 - 9 according to VSIS 2.8)
            if (numberOfNetworks <= 10)
            {
                soMLevel1.SITR.ETCS1.CurrentRbcData.MmiNNetworks.Value = numberOfNetworks; // Number of networks
            }
            else
            {
                soMLevel1.TraceError(
                    "{0} networks were attempted to be displayed. Only 10 are allowed, the rest have been discarded!!");
                numberOfNetworks = 10;
                soMLevel1.SITR.ETCS1.CurrentRbcData.MmiNNetworks.Value = numberOfNetworks; // Number of networks
            }

            ushort totalNumberofCaptionsNetwork = 0; // To be used for packet length

            //For all networks
            for (int k = 0; k < numberOfNetworks; k++)
            {
                char[] networkCaptionChars = captionNetworks[k].ToArray();
                ushort numberNetworkCaptionChars = Convert.ToUInt16(networkCaptionChars.Length);
                totalNumberofCaptionsNetwork +=
                    numberNetworkCaptionChars; // Total number of CaptionXNetworks chars for the whole telegram

                // Limit number of caption characters to 16
                if (numberNetworkCaptionChars > 16)
                {
                    Array.Resize(ref networkCaptionChars, 16);
                }

                // Write individual network chars
                soMLevel1.SITR.Client.Write(
                    "ETCS1_CurrentTrainData_EVC22CurrentRbcDataSub1" + k + "_MmiNCaptionNetwork",
                    numberNetworkCaptionChars);

                // Dynamic fields 2nd dimension
                for (int l = 0; l < numberNetworkCaptionChars; l++)
                {
                    // Network caption text character
                    if (l < 10)
                    {
                        soMLevel1.SITR.Client.Write(
                            "ETCS1_CurrentTrainData_EVC22CurrentRbcDataSub1" + k + "_EVC22CurrentRbcDataSub110" + l +
                            "_MmiXCaptionNetwork",
                            networkCaptionChars[l]);
                    }
                    else
                    {
                        soMLevel1.SITR.Client.Write(
                            "ETCS1_CurrentTrainData_EVC22CurrentRbcDataSub1" + k + "_EVC22CurrentRbcDataSub11" + l +
                            "_MmiXCaptionNetwork",
                            networkCaptionChars[l]);
                    }
                }
            }

            ushort numberOfDataElements = Convert.ToUInt16(textDataElements.Length);

            // Limit the number of data elements to 9 (range : 0 - 8 according to VSIS 2.8)
            if (numberOfDataElements <= 9)
            {
                soMLevel1.SITR.ETCS1.CurrentRbcData.MmiNDataElements.Value =
                    numberOfDataElements; // Number of data elements to enter
            }
            else
            {
                soMLevel1.TraceError(
                    "{0} networks were attempted to be displayed. Only 9 are allowed, the rest have been discarded!!");
                numberOfDataElements = 9;
                soMLevel1.SITR.ETCS1.CurrentRbcData.MmiNDataElements.Value =
                    numberOfDataElements; // Number of data elements to enter
            }

            ushort totalNumberOfDataElementsText = 0; // To be used for packet length

            // For all data elements
            for (int k = 0; k < numberOfDataElements; k++)
            {
                soMLevel1.SITR.Client.Write("ETCS1_CurrentRbcData_EVC22CurrentRbcDataSub2" + k + "_MmiNidData",
                    mmiNidData[k]); // Data entry element Id
                soMLevel1.SITR.Client.Write("ETCS1_CurrentRbcData_EVC22CurrentRbcDataSub2" + k + "_MmiQDataCheck",
                    mmiQDataCheck[k]); // Data Check Result for data element  

                char[] dataElementsChars = textDataElements[k].ToArray();
                ushort numberDataElementsChars = Convert.ToUInt16(dataElementsChars.Length);
                totalNumberOfDataElementsText +=
                    numberDataElementsChars; // Total number of XTexts chars for the whole telegram

                // Limit number of caption characters to 16
                if (numberDataElementsChars > 16)
                {
                    Array.Resize(ref dataElementsChars, 16);
                }

                // Write individual data element chars
                soMLevel1.SITR.Client.Write("ETCS1_CurrentRbcData_EVC22CurrentRbcDataSub2" + k + "_MmiNText",
                    numberDataElementsChars);

                // Dynamic fields 2nd dimension
                for (int l = 0; l < numberDataElementsChars; l++)
                {
                    // Data element text character
                    if (l < 10)
                    {
                        soMLevel1.SITR.Client.Write(
                            "ETCS1_CurrentRbcData_EVC22CurrentRbcDataSub2" + k + "_EVC22CurrentRbcDataSub210" + l +
                            "_MmiXText",
                            dataElementsChars[l]);
                    }
                    else
                    {
                        soMLevel1.SITR.Client.Write(
                            "ETCS1_CurrentRbcData_EVC22CurrentRbcDataSub2" + k + "_EVC22CurrentRbcDataSub21" + l +
                            "_MmiXText",
                            dataElementsChars[l]);
                    }
                }
            }

            // Packet length
            soMLevel1.SITR.ETCS1.CurrentTrainData.MmiLPacket.Value = Convert.ToUInt16(
                192 + numberOfNetworks * 16 + totalNumberofCaptionsNetwork * 8
                + numberOfDataElements * 32 + totalNumberOfDataElementsText * 8);
        }
    }
}