using System.Data.SqlClient;

namespace HostSever.Model
{
	public class BreakInfo
	{
		public int ID { get; set; }
		public int BayNumber { get; set; }
		public string BentleyID { get; set; }
		public bool BreakOn { get; set; }
		public DateTime TimeLogged { get; set; }
		public string Barcode { get; set; }


		public BreakInfo(SqlDataReader reader)
		{
			ID = (int)reader["ID"];
			BayNumber = (int)reader["BayNumber"];
			BentleyID = (string)reader["BentleyID"];
			BreakOn = (bool)reader["BreakOn"];
			TimeLogged = (DateTime)reader["TimeLogged"];
			Barcode = (string)reader["Barcode"];
		}

	}
}

