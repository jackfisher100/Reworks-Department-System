using System.Data.SqlClient;

namespace HostSever.Model
{

	public class BayInfo
	{
		public int? BayNo { get; set; }
		public string? Barcode { get; set; }
		public DateTime? TimeOfEntry { get; set; }
		public string? WorkerID { get; set; }
		public bool? AndonOn { get; set; }
		public int? BreakPeriod { get; set; }
		public int? Prediction { get; set; }


		public BayInfo(SqlDataReader reader)
		{
			Barcode = (string)reader["Barcode"];
			try { TimeOfEntry = (DateTime?)reader["TimeOfEntry"]; } catch { }
			try { BayNo = (int)reader["BayNo"]; } catch { }
			try { WorkerID = (string)reader["WorkerID"]; } catch { }
			try { Prediction = (int)reader["Prediction"]; } catch { }
		}

		public BayInfo(Car car)
		{
			Barcode = car.Barcode;
			TimeOfEntry = car.TimeOfEntry;
			BayNo = car.BayNo.Value;
			WorkerID = car.WorkerID;
			Prediction = car.Prediction;
		}
	}
}
