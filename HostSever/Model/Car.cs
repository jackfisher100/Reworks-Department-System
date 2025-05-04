using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace HostSever.Model
{




    public class Car
    {
        public int? EntryID { get; set; }
        public string? Barcode { get; set; }
        public DateTime? TimeOfEntry { get; set; }
        public DateTime? TimeOfExit { get; set; }
        public int? BayNo { get; set; }
        public string? WorkerID { get; set; }
        public string? ManagerID { get; set; }
        public string? Issue { get; set; }
        public int? Prediction { get; set; }
        public int? Dwell { get; set; }

        public Car()
        {
        }

        public Car(SqlDataReader reader)
        {
			EntryID = (int)reader["EntryID"];
			Barcode = (string)reader["Barcode"];
			try { TimeOfEntry = (DateTime?)reader["TimeOfEntry"]; } catch { }
			try { TimeOfExit = (DateTime?)reader["TimeOfExit"]; } catch { }
			try { BayNo = (int)reader["BayNo"]; } catch { }
			try { WorkerID = (string)reader["WorkerID"]; } catch { }
			try { ManagerID = (string)reader["ManagerID"]; } catch { }
			try { Issue = (string)reader["Issue"]; } catch { }
			try { Prediction = (int)reader["Prediction"]; } catch { }
			try { Dwell = (int)reader["Dwell"]; } catch { }
		}


    }
}
