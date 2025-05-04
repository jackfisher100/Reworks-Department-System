using HostSever.Model;

using System.Data.SqlClient;



namespace HostSever
{


    public class Test
    {

        public static User GetUser(int num)
        {
            return new User
            {
                UserID = num,
                BentleyId = "456",
                Name = "John",
                Manager = true
            };
        }

        public static User GetUser(string num)
        {
            return new User
            {
                UserID = 123,
                BentleyId = num,
                Name = "John",
                Manager = true
            };
        }
    }
}
