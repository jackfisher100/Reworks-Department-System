using HostSever.Model;
using System.Text.Json;

namespace HostSever.Classes
{
    public class Manager(DatabaseLink db_input)
    {
        private DatabaseLink db = db_input;


        public async Task<string> updatePredicted(HttpContext context)
        {
            StreamReader stream = new StreamReader(context.Request.Body);
            string body = await stream.ReadToEndAsync();

            UpdatePredictedCar car;
            try
            {
                car = JsonSerializer.Deserialize<UpdatePredictedCar>(body)!;
            }
            catch (JsonException)
            {
                Console.WriteLine("Error: <updatePredicted> Issue with Json");
                return "Error: Issue with Json";
            }


            if (db.updatePredictedTime(car.CarBarcode, car.NewPredicted))
            {
                Console.WriteLine("Updated predicted time");
                return "Updated predicted time";
            }
            else
            {
                Console.WriteLine("Something went wrong with updating the predicted time");
                return "Unable to update time";
            }

        }

        public async Task<string> updateNotes(HttpContext context)
        {
            StreamReader stream = new StreamReader(context.Request.Body);
            string body = await stream.ReadToEndAsync();

            UpdateNotesCar car;
            try
            {
                car = JsonSerializer.Deserialize<UpdateNotesCar>(body)!;
            }
            catch (JsonException)
            {
                Console.WriteLine("Error: <updateNotes> Issue with Json");
                return "Error: Issue with Json";
            }


            // Get the old notes from the db

            string OldNotes = db.getCarNotes(car.CarBarcode);

            string newNotes = OldNotes + "\n\n" + car.NewNotes;

            if (db.addNotes(car.CarBarcode, newNotes))
            {
                Console.WriteLine("Updated notes");
                return "Updated notes";
            }
            else
            {
                Console.WriteLine("Something went wrong when updating notes");
                return "Unable to update notes";
            }

        }

    }






}
