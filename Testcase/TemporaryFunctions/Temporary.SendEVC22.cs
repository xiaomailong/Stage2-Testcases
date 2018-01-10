using System.Collections.Generic;
using Testcase.Telegrams.EVCtoDMI;

namespace Testcase.TemporaryFunctions
{
    internal static partial class Temporary
    {
        /// <summary>
        /// Send_EVC22_MMI_Current_Rbc_Data sends RBC Data to the DMI
        /// </summary>
        public static void Send_EVC22_MMI_Current_Rbc(SoM_Level1 soMLevel1, uint rbcId, ulong mmiNidRadio,
            ushort mmiNidWindow,
            Variables.MMI_Q_CLOSE_ENABLE mmiQCloseEnable,
            EVC22_MMICurrentRBC.EVC22BUTTONS mmiMButtons, string[] textDataElements)
        {
            // TODO what is the NID_C?
            EVC22_MMICurrentRBC.NID_C = 0;
            EVC22_MMICurrentRBC.NID_RBC = rbcId;
            EVC22_MMICurrentRBC.MMI_NID_RADIO = mmiNidRadio; // RBC phone number
            EVC22_MMICurrentRBC.MMI_NID_WINDOW = mmiNidWindow; // ETCS Window Id
            EVC22_MMICurrentRBC.MMI_Q_CLOSE_ENABLE = mmiQCloseEnable; // Close button enable?
            EVC22_MMICurrentRBC.MMI_M_BUTTONS = mmiMButtons; // Buttons available

            EVC22_MMICurrentRBC.NetworkCaptions = new List<string>(textDataElements);
            EVC22_MMICurrentRBC.DataElements = new List<Variables.DataElement>();

            EVC22_MMICurrentRBC.Send();
        }
    }
}