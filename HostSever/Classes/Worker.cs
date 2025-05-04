using HostSever.Model;
using System.Text.Json;


namespace HostSever.Classes
{


    public class Worker(DatabaseLink db_input)
    {

        private DatabaseLink db = db_input;

        public async Task<string> insertCar(HttpContext context)
        {
            StreamReader stream = new StreamReader(context.Request.Body);
            string body = await stream.ReadToEndAsync();

            Car car;
            try
            {
                car = JsonSerializer.Deserialize<Car>(body)!;
            }
            catch (JsonException)
            {
                Console.WriteLine("Error: <insertCar> Issue with Json");
                return "Error: Issue with Json";
            }


            db.insertCar(car);
            return "OK";
        }

        public async Task<String> checkinCar(HttpContext context)
        {
            StreamReader stream = new StreamReader(context.Request.Body);
            string body = await stream.ReadToEndAsync();
            Car car;
            try
            {
                car = JsonSerializer.Deserialize<Car>(body)!;
            }
            catch
            {
                Console.WriteLine("Error: <CheckinCar> Issue with Json");
                return "Error: Issue with Json";
            }


            db.checkinCar(car);
            return "OK";    
			

		}


		public async Task<string> checkoutCar(HttpContext context)
        {
            StreamReader stream = new StreamReader(context.Request.Body);
            string body = await stream.ReadToEndAsync();

            Car checkoutCar;
            try
            {
                checkoutCar= JsonSerializer.Deserialize<Car>(body)!;
            }
            catch
            {
                Console.WriteLine("Error: <CheckoutCar> Issue with Json");
                return "Error: Issue with Json";
            }

            db.checkoutCar(checkoutCar);
			return "OK";

		}


        public async Task<string> login(string BentleyID)
        {

            return db.login(BentleyID);
        }

    }
}
