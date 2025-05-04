using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SummaryScreen.Models;

public class Car
{
    public int? EntryID { get; set; }
    public string? Barcode { get; set; }
    public DateTime? TimeOfEntry { get; set; }
    public DateTime? TimeOfExit { get; set; }
    public int BayNo { get; set; }
    public string? WorkerID { get; set; }
    public string? ManagerID { get; set; }
    public string? Issue { get; set; }
    public int? Prediction { get; set; }
    public int? Dwell { get; set; }

}

