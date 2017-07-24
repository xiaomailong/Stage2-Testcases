using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BT_CSB_Tools.Annotations;
using CL345;

namespace Testcase.Telegrams
{
    static class EVC8_MMIDriverMessage
    {
        private static SignalPool _pool;
        private static MMI_Q_TEXT_CLASS _mmiQTextClass;
        private static ushort _mmiQTextCriteria;

        public static void Initialise(SignalPool pool)
        {
            _pool = pool;


            // set as dynamic
            _pool.SITR.SMDCtrl.ETCS1.DriverMessage.Value = 0x8;

            // Set default values
            _pool.SITR.ETCS1.DriverMessage.MmiMPacket.Value = 8; // Packet ID
            _pool.SITR.ETCS1.DriverMessage.MmiNText.Value = 0x0; // Number of customs text characters. i.e. 0

            // TODO this needs to change when plain text string is implemented
            _pool.SITR.ETCS1.DriverMessage.MmiLPacket.Value = 80; // Packet length
        }

        private static void SetAlias()
        {
            var mmiQTextClass = (byte) _mmiQTextClass;
            _pool.SITR.ETCS1.DriverMessage.EVC8alias1.Value = (byte) (mmiQTextClass << 7 | _mmiQTextCriteria);
        }

        public static void Send()
        {
            _pool.SITR.SMDCtrl.ETCS1.DriverMessage.Value = 1;
        }

        /// <summary>
        /// The class of the text/symbol
        /// </summary>
        public static MMI_Q_TEXT_CLASS MMI_Q_TEXT_CLASS
        {
            set
            {
                _mmiQTextClass = value;
                SetAlias();
            }
        }

        /// <summary>
        /// Criteria for handling of text/symbol
        /// Values:
        /// 0 = "Add text/symbol with ack prompt, to be kept after ack"
        /// 1 = "Add text/symbol with ack prompt, to be removed after ack"
        /// 2 = "Add text with ack/nak prompt, to be removed after ack/nak"
        /// 3 = "Add informative text/symbol"
        /// 4 = "Remove text/symbol. Text/symbol to be removed is defined by MMI_I_TEXT."
        /// 5 = "Text still incomplete. Another instance of EVC-8 follows."
        /// </summary>
        public static ushort MMI_Q_TEXT_CRITERIA
        {
            set
            {
                _mmiQTextCriteria = value;
                SetAlias();
            }
        }

        /// <summary>
        /// The identifier of the transmitted text. Used to identify the text for addressing, acknowledgment and removing
        /// Values:
        /// 0 = "Spare"
        /// 1..255 = "Valid value"
        /// </summary>
        public static ushort MMI_I_TEXT
        {
            set => _pool.SITR.ETCS1.DriverMessage.MmiIText.Value = (byte) value;
        }

        /// <summary>
        /// Predefined texts to be displayed by the MMI.
        /// Values:
        /// 0 = "Level crossing not protected"
        /// 1 = "Acknowledgement"
        /// 2..255 = "Reserved for application specific coded text messages from wayside packet #76."
        /// 256 = "#1 (plain text only)"
        /// 257 = "#3 LE07/LE11/LE13/LE15 (Ack Transition to Level #4)"
        /// 258 = "#3 LE09 (Ack Transition to NTC #2)"
        /// 259 = "#3 MO08 (Ack On Sight Mode)"
        /// 260 = "#3 ST01 (Brake intervention)"
        /// 261 = "Spare"
        /// 262 = "#3 MO15 (Ack Reversing Mode)"
        /// 263 = "#3 MO10 (Ack Staff Responsible Mode)"
        /// 264 = "#3 MO17 (Ack Unfitted Mode)"
        /// 265 = "#3 MO02 (Ack Shunting ordered by Trackside)"
        /// 266 = "#3 MO05 (Ack Train Trip)"
        /// 267 = "Balise read error"
        /// 268 = "Communication error"
        /// 269 = "Runaway movement"
        /// 270..272 = "Spare"
        /// 273 = "Unauthorized passing of EOA / LOA"
        /// 274 = "Entering FS"
        /// 275 = "Entering OS"
        /// 276 = "#3 LE06/LE10/LE12/LE14 (Transition to Level #4)"
        /// 277 = "#3 LE08 (Transition to NTC #2)"
        /// 278 = "Emergency Brake Failure"
        /// 279 = "Apply brakes"
        /// 280 = " Emergency stop"
        /// 281 = "Spare"
        /// 282 = "#3 ST04 (Connection Lost/Set-Up failed)"
        /// 283..285 = "Spare"
        /// 286 = "#3 ST06 (Reversing is possible)"
        /// 287..289 = "Spare"
        /// 290 = "SH refused"
        /// 291 = "Spare"
        /// 292 = "SH request failed"
        /// 293..295 = "Spare"
        /// 296 = "Trackside not compatible"
        /// 297 = "Spare"
        /// 298 = "#3 DR02 (Confirm Track Ahead Free)"
        /// 299 = "Train is rejected"
        /// 300 = "No MA received at level transition"
        /// 301..304 = "Spare"
        /// 305 = "Train divided"
        /// 306..309 = "Spare"
        /// 310 = "Train data changed"
        /// 311..314 = "Spare"
        /// 315 = "SR distance exceeded"
        /// 316 = "SR stop order"
        /// 317..319 = "Spare"
        /// 320 = "RV distance exceeded"
        /// 321 = "ETCS Isolated"
        /// 322..513 = "Spare"
        /// 514 = "Perform Brake Test!"
        /// 515 = "Unable to start Brake Test"
        /// 516 = "Brake Test in Progress"
        /// 517 = "Brake Test failed, perform new Test!"
        /// 518..519 = "Spare"
        /// 520 = "LZB Partial Block Mode"
        /// 521 = "Override LZB Partial Block Mode"
        /// 522 = "Restriction #1 km/h in Release Speed Area"
        /// 523 = "Spare"
        /// 524 = "Brake Test successful"
        /// 525 = "Brake Test timeout in #1 Hours"
        /// 526 = "Brake Test Timeout"
        /// 527 = "Brake Test aborted, perform new Test?"
        /// 528 = "Apply Brakes!"
        /// 529..530 = "Spare"
        /// 531 = "BTM Test in Progress"
        /// 532 = "BTM Test Failure"
        /// 533 = "BTM Test Timeout"
        /// 534 = "BTM Test Timeout in #1 hours"
        /// 535 = "ATP Restart required in #1 Hours"
        /// 536 = "Restart ATP!"
        /// 537..539 = "Spare"
        /// 540 = "No Level available Onboard"
        /// 541..542 = "Spare"
        /// 543 = "#2 failed"
        /// 544 = "Spare"
        /// 545 = "#3 LE02A (Confirm LZB NTC)"
        /// 546..551 = "Spare"
        /// 552 = "Announced level(s) not supported Onboard"
        /// 553 = "Spare"
        /// 554 = "Reactivate the Cabin!"
        /// 555 = "#3 MO20 (Ack SN Mode)"
        /// 556..559 = "Spare"
        /// 560 = "Trackside malfunction"
        /// 561..562 = "Spare"
        /// 563 = "Trackside Level(s) not supported Onboard"
        /// 564 = "Confirm change of inhibit Level #1"
        /// 565 = "Confirm change of inhibit STM #2"
        /// 566..567 = "Spare"
        /// 568 = "#3 ST03 (Connection established)"
        /// 569 = "Radio network registration failed"
        /// 570 = "Shunting rejected due to #2 Trip"
        /// 571 = "Spare"
        /// 572 = "No Track Description"
        /// 573 = "#2 needs data"
        /// 574 = "Cabin Reactivation required in #1 hours"
        /// 575..579 = "Spare"
        /// 580 = "Procedure Brake Percentage Entry terminated by ATP"
        /// 581 = "Procedure Wheel Diameter Entry terminated by ATP"
        /// 582 = "Procedure Doppler Radar Entry terminated by ATP"
        /// 583 = "Doppler error"
        /// 584..605 = "Spare"
        /// 606 = "SH Stop Order"
        /// 607..608 = "Spare"
        /// 609 = "#3 Symbol ST100 (Network registered via one modem)"
        /// 610 = "#3 Symbol ST102 (Network registered via two modems)"
        /// 613 = "#3 Symbol ST103 (Connection Up) "
        /// 614 = "#3 Symbol ST03B (Connection Up with two RBCs)"
        /// 615 = "#3 Symbol ST03C (Connection Lost/Set-Up failed)"
        /// 616..620 = "Spare"
        /// 621 = "Unable to start Brake Test, vehicle not ready"
        /// 622 = "Unblock EB"
        /// 623 = "Spare"
        /// 624 = "ETCS Failure"
        /// 625 = "Tachometer error"
        /// 626 = "SDU error"
        /// 627 = "Speed Sensor failure"
        /// 628 = "ETCS Service Brake not available"
        /// 629 = "ETCS Traction Cut-off not available"
        /// 630 = "ETCS Isolation Switch failure"
        /// 631 = "#2 Isolation input not recognized"
        /// 632 = "Coasting input not recognised"
        /// 633 = "Brake Bypass failure"
        /// 634 = "Special brake input failure"
        /// 635 = "Juridical Recording not available"
        /// 636 = "Euroloop not available"
        /// 637 = "TIMS not available"
        /// 638 = "Degraded Radio service"
        /// 639 = "No Radio connection possible"
        /// 640..699 = "Spare"
        /// 700 = "#2 brake demand"
        /// 701 = "Route unsuitable – axle load category"
        /// 702 = "Route unsuitable – loading gauge"
        /// 703 = "Route unsuitable – traction system"
        /// 704 = "#2 is not available"
        /// 705 = "New power-up required in #1 hours"
        /// 706 = "No valid authentication key"
        /// 707 = "Spare"
        /// 708 = "Spare"
        /// 709 = "#3 MO22 (Acknowledgement for Limited Supervision)"
        /// 710 = "#3 (Train divided)"
        /// 711 = "NL-input signal is withdrawn"
        /// 712 = "Wheel data settings were successfully changed"
        /// 713 = "Doppler radar settings were successfully changed"
        /// 714 = "Brake percentage was successfully changed"
        /// 715 = "No Country Selection in LZB PB Mode"
        /// 716 = "#3 Symbol ST05 (hour glass)"
        /// </summary>
        public static ushort MMI_Q_TEXT
        {
            set => _pool.SITR.ETCS1.DriverMessage.MmiQText.Value = value;
        }

        /// <summary>
        /// Plain text message
        /// </summary>
        public static string PlainTextMessage
        {
            set
            {
                var charArray = value.ToCharArray();
                if (charArray.Length < 0)
                    throw new NotImplementedException();
                _pool.SITR.ETCS1.DriverMessage.MmiNText.Value = (ushort) charArray.Length;

                // TODO populate the char array
            }
        }
    }

    public enum MMI_Q_TEXT_CLASS : byte
    {
        AuxiliaryInformation = 0,
        ImportantInformation = 1
    }
}