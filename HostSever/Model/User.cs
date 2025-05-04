using System.Data.SqlClient;

namespace HostSever.Model
{
    public class User
    {
        public int? UserID { get; set; }
        public string? BentleyID { get; set; }
        public string? NameOfUser { get; set; }
        public bool? Manager { get; set; }

		public User() { }

		public User(SqlDataReader row)
		{
			UserID = (int)row["UserID"];
			BentleyID = (string)row["BentleyID"];
			NameOfUser = (string)row["NameOfUser"];
			Manager = (bool)row["Manager"];
		}

	}


}
