using System.Text.Json;

namespace KnowledgeBase
{
    public static class UserDB
    {
        private static readonly File file = new("Users.db");
        private static Dictionary<string, User> db = [];
        public static void AddUser(string username, string password, int accessLevel)
        {
            if (db.ContainsKey(username))
                throw new ArgumentException("User already exists");
            db[username] = new(username,password,accessLevel);
        }
        public static void RemoveUser(string username)
        {
            db.Remove(username);
        }
        public static User? GetUser(string username)
        {
            db.TryGetValue(username, out User? value);
            return value;
        }
        public static bool VerifyUser(string username, string password)
        {
            if (db.TryGetValue(username, out User? value))
            {
                return value.Password == password; // Password comparison; use hashed comparison in real scenarios.
            }
            return false;
        }
        public static void SaveToFile()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(db, options);
            file.Write(json);
        }
        public static void LoadFromFile()
        {
            if (System.IO.File.Exists(file.Info.FullName))
            {
                string json = file.Read();
                try
                {
                    db = JsonSerializer.Deserialize<Dictionary<string, User>>(json);
                }
                catch {}
            }
        }
    }
}