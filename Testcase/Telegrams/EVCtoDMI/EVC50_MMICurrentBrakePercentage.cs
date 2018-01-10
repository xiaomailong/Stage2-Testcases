using CL345;

namespace Testcase.Telegrams.EVCtoDMI
{
    /// <summary>
    /// This packet shall be sent when stored brake percentage data shall be presented via the MMI. 
    /// This packet is used in relation with packets EVC-51, EVC-150 and EVC-151. 
    /// The purpose of those packets is to allow the driver to modify the brake percentage besides the Train Data Entry procedure.
    /// </summary>
    public static class EVC50_MMICurrentBrakePercentage
    {
        private static SignalPool _pool;

        /// <summary>
        /// Initialise EVC-50 MMI_Current_Brake_Percentage telegram.
        /// </summary>
        /// <param name="pool"></param>
        public static void Initialise(SignalPool pool)
        {
            _pool = pool;

            // Set default values
            _pool.SITR.ETCS1.CurrentBrakePercentage.MmiMPacket.Value = 50; // Packet ID
            _pool.SITR.ETCS1.CurrentBrakePercentage.MmiLPacket.Value = 56; // Packet length
        }

        /// <summary>
        /// Send EVC50 Current Brake Percentage.
        /// </summary>
        public static void Send()
        {
            _pool.SITR.SMDCtrl.ETCS1.CurrentBrakePercentage.Value = 1;
        }

        /// <summary>
        /// Brake percentage given by recent Train Data Entry procedure (original ones, used for procedure 'Brake Percentage Entry' and 'Brake Percentage View').
        /// 
        /// Values:
        /// 0..9="Reserved"
        /// 10..250="Brake percentage"
        /// 251..255 = "Reserved"
        /// 
        /// </summary>
        public static byte MMI_M_BP_ORIG
        {
            set => _pool.SITR.ETCS1.CurrentBrakePercentage.MmiMBpOrig.Value = value;
        }

        /// <summary>
        /// Currently used brake percentage/entered brake percentage  (used for procedure 'Brake Percentage Entry' and 'Brake Percentage View'), 
        /// on transmission of  data to ETC this variable contains the BP entered by the driver. 
        /// On transmission of data to the MMI this variable contains the currently used BP.
        /// 
        /// Values:
        /// 0..9="Reserved"
        /// 10..250="Brake percentage"
        /// 251="Technical Range Check failed"
        /// 252..254="Reserved"
        /// 255 = "Original value exceeded (will be displayed as '++++' in grey, Data Field 'Current BP')"
        /// 
        /// Note: All special values concerning cross/range checks are only used in packet EVC-50.
        /// </summary>
        public static byte MMI_M_BP_CURRENT
        {
            set => _pool.SITR.ETCS1.CurrentBrakePercentage.MmiMBpCurrent.Value = value;
        }

        /// <summary>
        /// Last measured brake percentage 
        /// (used for procedure 'Brake Percentage Entry' and 'Brake Percentage View', the value is a result of an automatic brake deceleration measurement of an ATP)
        /// 
        /// Values:
        /// 0..9="Reserved"
        /// 10..250="Brake percentage"
        /// 251..254 = "Reserved"
        /// 255 = "No last measured brake percentage available, i.e. this will be displayed as '_ _ _ _' in grey in Data Field 'Last measured BP'."
        /// 
        /// Note: All special values are only used in packet EVC-50.
        /// </summary>
        public static byte MMI_M_BP_MEASURED
        {
            set => _pool.SITR.ETCS1.CurrentBrakePercentage.MmiMBpMeasured.Value = value;
        }
    }
}