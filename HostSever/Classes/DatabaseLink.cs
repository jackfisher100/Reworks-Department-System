using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using HostSever.Model;

namespace HostSever.Classes
{
	public class DatabaseLink
	{
		private static string _connectionString = "Data Source=JACK-DELL-WINDO\\SQLEXPRESS;Initial Catalog=Bentley;Integrated Security=True;TrustServerCertificate=True;MultipleActiveResultSets=True;";
		SqlConnection con;

		public DatabaseLink()
		{
		}

		public DatabaseLink(string connectionString)
		{
			// Establish connection
			_connectionString = connectionString;
		}

		// Destructor
		~DatabaseLink()
		{
			// Close Connection
			con.Close();
		}

		public void InsertCar(Car car)
		{
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				string query = "INSERT INTO Cars (Barcode, TimeOfEntry, BayNo, WorkerID, ManagerID, Issue, Prediction, Dwell) VALUES (@Barcode, @TimeOfEntry, @BayNo, @WorkerID, @ManagerID, @Issue, @Prediction, @Dwell)";
				using (SqlCommand cmd = new SqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("@Barcode", car.Barcode);
					cmd.Parameters.AddWithValue("@TimeOfEntry", car.TimeOfEntry);
					cmd.Parameters.AddWithValue("@BayNo", car.BayNo);
					cmd.Parameters.AddWithValue("@WorkerID", car.WorkerID);
					cmd.Parameters.AddWithValue("@ManagerID", car.ManagerID);
					cmd.Parameters.AddWithValue("@Issue", car.Issue);
					cmd.Parameters.AddWithValue("@Prediction", car.Prediction);
					cmd.Parameters.AddWithValue("@Dwell", car.Dwell);

					cmd.ExecuteNonQuery();
				}
			}
		}

		// This function allows cars to be rechecked in, even if they have already been checked in
		public void CheckinCar(string barcode, int bayNo, string workerID)
		{
			int entryID = GetMostRecentEntryCar(barcode);

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("UPDATE Cars SET TimeOfEntry = @timeOfEntry, BayNo = @BayNo, WorkerID = @WorkerID WHERE Barcode = @carBarcode AND EntryID = @EntryID", con))
				{
					cmd.Parameters.AddWithValue("@timeOfEntry", DateTime.Now);
					cmd.Parameters.AddWithValue("@BayNo", bayNo);
					cmd.Parameters.AddWithValue("@WorkerID", workerID);
					cmd.Parameters.AddWithValue("@carBarcode", barcode);
					cmd.Parameters.AddWithValue("@EntryID", entryID);

					cmd.ExecuteNonQuery();
				}
			}
		}

		public bool CheckoutCar(string barcode, int dwell)
		{
			Car testCar = GetCheckinCar(barcode);
			if (testCar == null)
			{
				// Car has not been checked in yet
				return false;
			}

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("UPDATE Cars SET TimeOfExit = @timeOfExit, Dwell = @Dwell WHERE Barcode = @carBarcode AND EntryID = @EntryID", con))
				{
					cmd.Parameters.AddWithValue("@timeOfExit", DateTime.Now);
					cmd.Parameters.AddWithValue("@Dwell", dwell);
					cmd.Parameters.AddWithValue("@carBarcode", barcode);
					cmd.Parameters.AddWithValue("@EntryID", testCar.EntryID);

					cmd.ExecuteNonQuery();
				}
			}
			return true;
		}

		public void UpdatePredicted(string barcode, string managerID, int updatedPrediction)
		{
			int entryID = GetMostRecentEntryCar(barcode);
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("UPDATE Cars SET Prediction = @updatedPrediction, ManagerID = @managerID WHERE Barcode = @carBarcode AND EntryID = @EntryID", con))
				{
					cmd.Parameters.AddWithValue("@updatedPrediction", updatedPrediction);
					cmd.Parameters.AddWithValue("@managerID", managerID);
					cmd.Parameters.AddWithValue("@carBarcode", barcode);
					cmd.Parameters.AddWithValue("@EntryID", entryID);

					cmd.ExecuteNonQuery();
				}
			}
		}

		public int? getPredicted(string Barcode)
		{
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("SELECT Prediction FROM Cars WHERE Barcode = @barcode AND TimeOfExit IS NULL", con))
				{
					cmd.Parameters.AddWithValue("@barcode", Barcode);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							// Crashes as a car is checked in and the prediction value is then null
							// Add in a check for null
							return reader.GetInt32(0);
						}
					}
				}
			}
			return null;
		}

		public bool UpdateUser(User user)
		{
			bool newUser = user.UserID == null;

			if (newUser)
			{
				if (CheckUser(user.BentleyID) != null) // If trying to add someone with an already existing BentleyID
				{
					return false;
				}
			}
			else if (CheckUser(user.BentleyID, user.UserID) != null) return false;

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				string commandString;
				if (newUser) commandString = "INSERT INTO Users (BentleyID, NameOfUser, Manager) VALUES	(@bentleyID, @nameOfUser, @manager)";
				else commandString = "UPDATE Users SET BentleyID = @bentleyID, NameOfUser=@nameOfUser, Manager = @manager WHERE UserID = @userID"; 

				using (SqlCommand cmd = new SqlCommand(commandString, con))
				{
					cmd.Parameters.AddWithValue("@bentleyID", user.BentleyID);
					cmd.Parameters.AddWithValue("@nameOfUser", user.NameOfUser);
					cmd.Parameters.AddWithValue("@manager", user.Manager);
					if (!newUser) cmd.Parameters.AddWithValue("@userID", user.UserID);


					cmd.ExecuteNonQuery();
				}
			}
			return true;
		}

		public List<User> getAllUsers()
		{
			List<User> result = new List<User>();

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("Select * From Users", con))
				{
					using (SqlDataReader reader = cmd.ExecuteReader()){
						while (reader.Read())
						{
							result.Add(new User(reader));
						}
					}
				}
			}

			return result;
		}

		public void AddNotes(string barcode, string notes)
		{
			int? entryID = GetMostRecentEntryCar(barcode);
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("UPDATE Cars SET Issue = @notes WHERE Barcode = @carBarcode AND EntryID = @EntryID", con))
				{
					cmd.Parameters.AddWithValue("@notes", notes);
					cmd.Parameters.AddWithValue("@carBarcode", barcode);
					cmd.Parameters.AddWithValue("@EntryID", entryID);

					cmd.ExecuteNonQuery();
				}
			}
		}

		public void InsertBayInfo(int bayNumber, string bentleyID, bool andonOn)
		{
			List<int> currentAndon = GetCurrentAndon();

			if (andonOn && currentAndon.Contains(bayNumber)) return;

			if (!andonOn && !currentAndon.Contains(bayNumber)) return;

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				string query = "INSERT INTO BayInfo (BayNumber, BentleyID, AndonOn, TimeLogged) VALUES (@BayNumber, @IDLogin, @AndonOn, @time)";
				using (SqlCommand cmd = new SqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("@BayNumber", bayNumber);
					cmd.Parameters.AddWithValue("@IDLogin", bentleyID);
					cmd.Parameters.AddWithValue("@AndonOn", andonOn);
					cmd.Parameters.AddWithValue("@time", DateTime.Now);
					cmd.ExecuteNonQuery();
				}
			}
		}

		public void InsertBreakInfo(int bayNumber, string BentleyID, string Barcode, bool breakOn)
		{
			List<int> currentlyOnBreak= GetOpenBreaks();

			if (breakOn && currentlyOnBreak.Contains(bayNumber)) return;

			if (!breakOn && !currentlyOnBreak.Contains(bayNumber)) return;

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				string query = "INSERT INTO BreakInfo (BayNumber, BentleyID, BreakOn, Barcode, TimeLogged) VALUES (@BayNumber, @IDLogin, @BreakOn, @Barcode, @time)";
				using (SqlCommand cmd = new SqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("@BayNumber", bayNumber);
					cmd.Parameters.AddWithValue("@IDLogin", BentleyID);
					cmd.Parameters.AddWithValue("@BreakOn", breakOn);
					cmd.Parameters.AddWithValue("@Barcode", Barcode);
					cmd.Parameters.AddWithValue("@time", DateTime.Now);
					cmd.ExecuteNonQuery();
				}
			}
		}

		public string GetCarNotes(string barcode)
		{
			string result = "";
			int? entryID = GetMostRecentEntryCar(barcode);
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				string query = "SELECT Issue FROM Cars WHERE Barcode = @carBarcode AND EntryID = @EntryID";
				using (SqlCommand cmd = new SqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("@carBarcode", barcode);
					cmd.Parameters.AddWithValue("@EntryID", entryID);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							result += reader["Issue"];
						}
					}
				}

			}
			return result;
		}

		public List<Car> GetAllOpenCars()
		{
			List<Car> cars = new List<Car>();

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("SELECT * FROM Cars WHERE TimeOfExit IS NULL AND TimeOfEntry IS NOT NULL", con))
				{
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							Car car = new Car(reader);
							cars.Add(car);
						}
					}
				}
			}
			return cars;
		}

		public List<Car> GetAllCars()
		{
			List<Car> result = new List<Car>();
			string query = "SELECT * FROM Cars";

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand(query, con))
				{
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							Car car = new Car(reader);
							result.Add(car);
						}
					}
				}
			}
			return result;
		}

		public string GetSettings(string key)
		{
			string result = "";

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("SELECT SettingValue FROM Settings WHERE SettingKey = @key", con))
				{
					cmd.Parameters.AddWithValue("@key", key);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							result += reader["SettingValue"];
						}
					}
				}
			}

			return result;
		}

		// Checks to see if any other user (not EntryID) has the same BentleyID
		public User? CheckUser(string workerID, int? userID = null)
		{
			User? result = null;

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				string query;
				if (userID == null) query = "SELECT * FROM Users WHERE BentleyID = @bentleyID";
				else query = "SELECT * FROM Users WHERE BentleyID = @bentleyID AND NOT UserID = @userID";
				using (SqlCommand cmd = new SqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("@bentleyID", workerID);
                    if ( userID != null) cmd.Parameters.AddWithValue("@userID", userID);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							result = new User(reader);
							if (reader.Read())
							{
								Console.WriteLine("Error: <Login> More than 1 user with the same BentleyID. = "+workerID);
								
							}

							return result;
						}
					}
				}
			}

			return result;
		}

		public User? Login(string workerID, bool manager)
		{
			User? result = null;

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("SELECT * FROM Users WHERE BentleyID = @bentleyID AND Manager = @manager", con))
				{
					cmd.Parameters.AddWithValue("@bentleyID", workerID);
					cmd.Parameters.AddWithValue("@manager", manager);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							result = new User(reader);
							if (reader.Read())
							{
								Console.WriteLine("Error: <Login> More than 1 user with the same BentleyID");
								return null;
							}

							return result;
						}
					}
				}
			}

			return result;
		}

		public int GetTodaysCarsCount()
		{
			int carCount = -1;
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Cars WHERE CAST(TimeOfExit AS DATE) = CAST(GETDATE() AS DATE);", con))
				{
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							carCount = reader.GetInt32(0);
						}
					}
				}
			}
			return carCount;
		}

		public List<int> GetCurrentAndon()
		{
			List<Andon> result = new List<Andon>();

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("SELECT * FROM BayInfo WHERE CAST(TimeLogged AS DATE) = CAST(GETDATE() AS DATE);", con))
				{
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							Andon andon = new Andon(reader);
							result.Add(andon);
						}
					}
				}
			}

			result = result.OrderBy(a => a.TimeLogged).ToList();
			List<int> baysWithAndonOn = new List<int>();

			foreach (Andon a in result)
			{
				if (a.AndonOn)
				{
					baysWithAndonOn.Add(a.BayNumber);
				}
				else
				{
					baysWithAndonOn.Remove(a.BayNumber);
				}
			}

			return baysWithAndonOn;
		}

		public List<BreakInfo> GetTodaysBreaks()
		{
			List<BreakInfo> result = new List<BreakInfo>();

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("SELECT * FROM BreakInfo WHERE CAST(TimeLogged AS DATE) = CAST(GETDATE() AS DATE);", con))
				{
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							BreakInfo andon = new BreakInfo(reader);
							result.Add(andon);
						}
					}
				}
			}
			return result;
		}

		public List<int> GetOpenBreaks()
		{
			List<BreakInfo> result = GetTodaysBreaks();

			result = result.OrderBy(a => a.TimeLogged).ToList();
			List<int> baysWithBreakOn = new List<int>();

			foreach (BreakInfo a in result)
			{
				if (a.BreakOn)
				{
					baysWithBreakOn.Add(a.BayNumber);
				}
				else
				{
					baysWithBreakOn.Remove(a.BayNumber);
				}
			}

			return baysWithBreakOn;
		}

		public int GetTotalBreak(int BayNumber, string Barcode)
		{
			List<BreakInfo> breaks = new List<BreakInfo>();

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("SELECT * FROM BreakInfo WHERE BayNumber = @bayNumber AND Barcode = @barcode AND CAST(TimeLogged AS DATE) BETWEEN CAST(DATEADD(DAY, -1, GETDATE()) AS DATE) AND CAST(GETDATE() AS DATE);", con))
				{

					cmd.Parameters.AddWithValue("@bayNumber", BayNumber);
					cmd.Parameters.AddWithValue("@barcode", Barcode);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{
							BreakInfo info = new BreakInfo(reader);
							breaks.Add(info);
						}
					}
				}
			}

			breaks = breaks.OrderBy(a => a.TimeLogged).ToList();
			int totalBreakTime = 0;
			DateTime breakOpen = new DateTime();
			bool breakClosed = true;
			foreach (BreakInfo b in breaks)
			{
				if (b.BreakOn)
				{
					breakOpen = b.TimeLogged;
					breakClosed = false;
				}
				else
				{
					totalBreakTime += (int)(b.TimeLogged - breakOpen).TotalSeconds;
					breakClosed = true;
				}
			}

			if (breakClosed == false) totalBreakTime += (int)(DateTime.Now - breakOpen).TotalSeconds;

			return totalBreakTime;
		}

		private void NewCar(string barcode)
		{
			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("INSERT INTO Cars (Barcode) VALUES (@Barcode)", con))
				{
					cmd.Parameters.AddWithValue("@Barcode", barcode);
					cmd.ExecuteNonQuery();
				}
			}
		}

		private int GetMostRecentEntryCar(string barcode)
		{
			Car? car = GetCheckinCar(barcode);

			if (car == null)
			{
				car = GetNoCheckinCar(barcode);

				if (car == null)
				{
					NewCar(barcode);
					car = GetNoCheckinCar(barcode);
				}
			}

			return car.EntryID.Value;
		}

		private Car GetNoCheckinCar(string barcode)
		{
			Car? car = null;

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("SELECT * FROM Cars WHERE Barcode = @barcode AND TimeOfEntry IS NULL AND TimeOfExit IS NULL", con))
				{
					cmd.Parameters.AddWithValue("@barcode", barcode);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							car = new Car(reader);
						}
					}
				}
			}

			return car;
		}

		private Car GetCheckinCar(string barcode)
		{
			Car? car = null;

			using (SqlConnection con = new SqlConnection(_connectionString))
			{
				con.Open();
				using (SqlCommand cmd = new SqlCommand("SELECT * FROM Cars WHERE Barcode = @barcode AND TimeOfEntry IS NOT NULL AND TimeOfExit IS NULL", con))
				{
					cmd.Parameters.AddWithValue("@barcode", barcode);

					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							car = new Car(reader);
						}
					}
				}
			}

			return car;
		}


        


	}
}
