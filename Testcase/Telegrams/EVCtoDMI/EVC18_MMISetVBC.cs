#region usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CL345;

#endregion

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This packet is sent sporadically from ETC when the 'Set VBC' procedure is ongoing
    /// and is intended to support the following use cases:
    /// 1. Prompt the driver to enter a VBC code
    /// 2. Display/change echo text after data checks have been performed by ETC;
    ///     this also includes control over the allowed driver actions in case some data check has failed
    /// It also gives the ETC the ability to control the status/type of the "Yes" button,
    ///     if specified by functional requirements for ETC and DMI.
    /// 
    /// Note: Parameter 'MMI_N_VBC' distinguishes between use case 1 and 2
    /// </summary>
    public static class EVC18_MMISetVBC
    {
        private static SignalPool _pool;

        /// <summary>
        /// Initialise EVC-18 MMI_Set_VBC telegram
        /// VBC = Virtual Balise Cover
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // Set default values
            _pool.SITR.ETCS1.SetVbc.MmiMPacket.Value = 18; // Packet ID
            VBCList = new List<VBCElement>();
        }

        /// <summary>
        /// Send EVC-18 MMI_Set_VBC telegram
        /// </summary>
        public static void Send()
        {
            ushort numberOfVbc = (ushort)VBCList.Count;

            // Initial packet size
            ushort totalSizeCounter = 64;
        }

        /// <summary>
        /// Number of VBC Identifiers
        /// 
        /// Values:
        /// 0 = Prompt driver to enter VBC code
        /// 1 = Display/change echo text after data checks have been performed by ETC
        /// </summary>
        public static ushort MMI_N_VBC
        {
            get => _pool.SITR.ETCS1.SetVbc.MmiNVbc.Value;

            set => _pool.SITR.ETCS1.SetVbc.MmiNVbc.Value = value;
        }

        /// <summary>
        /// Intended to be used to distinguish between:
        ///     'BTN_YES_DATA_ENTRY_COMPLETE',
        ///     'BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE',
        ///     'no button' (here this shall be interpreted as 'Yes button disabled').
        /// The 'BTN_SETTINGS' shall be used as special value to indicate that DMI shall
        /// close the 'Set VBC' window and return to the parent 'settings' window.
        /// Other buttons are not in scope of packet EVC-18
        /// </summary>
        public static EVC18BUTTONS MMI_M_BUTTONS
        {
            set => _pool.SITR.ETCS1.SetVbc.MmiMButtons.Value = (byte)value;
        }

        /// <summary>
        /// List of VBCs
        /// </summary>
        public static List<VBCElement> VBCList { get; set; }
    }

    /// <summary>
    /// VBC data to be displayed or verified by the EVC.
    /// </summary>
    public class VBCElement
    {
        public static int Identifier { get; set; }      // VBC Identifier
        public static byte QDataCheck { get; set; }     // Result of data check
        public static string EchoText { get; set; }     // Echo text of data
    }

    public enum EVC18BUTTONS : byte
    {
        BTN_YES_DATA_ENTRY_COMPLETE = 36,
        BTN_YES_DATA_ENTRY_COMPLETE_DELAY_TYPE = 37,
        NoButton = 255
    }
}
