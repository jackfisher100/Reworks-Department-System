namespace HostSever.Classes
{
    public class Settings(DatabaseLink db_input)
    {
        private DatabaseLink db = db_input;

        public string getSettings(string key)
        {
            return db.getSettings(key);
        }
    }
}
