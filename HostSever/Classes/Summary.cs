using HostSever.Model;
using System.Text.Json;




namespace HostSever.Classes
{
    public class Summary(DatabaseLink db_input)
    {
        private DatabaseLink db = db_input;


        public string allBays(HttpContext context)
        {
            List<Car> cars = db.GetAllOpenCars();

            string result = JsonSerializer.Serialize(cars);

            return result;
        }


        public int todaysStats(HttpContext context)
        {
            int carsToday = db.getTodaysCarsCount();
            return carsToday;
        } 

    }
    





    
}
