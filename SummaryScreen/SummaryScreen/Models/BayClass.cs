using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI;

namespace SummaryScreen.Models
{
    public class BayClass
    {
        public int bayNo { get; set; }
        public string? barcode { get; set; }
        public DateTime? timeOfEntry { get; set; }
        public string? workerID { get; set; }
        public bool andonOn { get; set; }
		public int? breakPeriod { get; set; }
        public int? prediction { get; set; }
        public int? breakTime { get; set; }
        

        public bool _andonOn;

        public string timeInBay { get { if (timeOfEntry != null) { return "Time spent: " + FormatTimeSpan(DateTime.Now - timeOfEntry.Value); } else { return ""; } } }
        public string BarcodeString { get { if (barcode != null) { return "VIN: " + getVIN(barcode); } else { return ""; } } }

        public Visibility progressVisible { get { if (prediction != null) return Visibility.Visible; else return Visibility.Collapsed; } }
        public int? predictionSecs { get { return prediction * 60; } }
        public int? currentSecs { get { if (timeOfEntry != null) { return ((int)(DateTime.Now - timeOfEntry.Value).TotalSeconds) - breakPeriod; } else { return null; } } }

        public SolidColorBrush? Background { get; set; }

        public static SolidColorBrush BackgroundAndonOff = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 220, 220, 220));
        public static SolidColorBrush Yellow = new SolidColorBrush(Colors.Yellow);
        public static SolidColorBrush Red = new SolidColorBrush(Colors.Red);


        private string getVIN(string vin)
        {
            if (vin.Length < 5) return vin;
            else return barcode.Substring(barcode.Length - 5);
        }


        private string FormatTimeSpan(TimeSpan timeSpan)
        {
            if (timeSpan.TotalDays >= 1)
            {
                return string.Format("{0}d {1:D2}:{2:D2}:{3:D2}", (int)timeSpan.TotalDays, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            }
            else
            {
                return string.Format("{0:D2}:{1:D2}:{2:D2}", (int)timeSpan.TotalHours, timeSpan.Minutes, timeSpan.Seconds);
            }
        }




    }
}
