using System.Data.SqlClient;

namespace HostSever.Model
{
    public class Andon
    {
        public int ID { get; set; }
        public int BayNumber { get; set; }
        public string BentleyID { get; set; }
        public bool AndonOn { get; set; }
        public DateTime TimeLogged { get; set; }


		public Andon(SqlDataReader reader)
		{
			ID = (int)reader["ID"];
			BayNumber = (int)reader["BayNumber"];
			BentleyID = (string)reader["BentleyID"];
			AndonOn = (bool)reader["AndonOn"];
			TimeLogged = (DateTime)reader["TimeLogged"];
		}

	}
}
