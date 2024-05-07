using System.Text.Json;

namespace KnowledgeBase
{
    public static class UserDB
    {
        private static readonly File file = new("Users.db");
        private static Dictionary<string, User> usernamePassword = [];
        private static Dictionary<string, User> tokenUsername = [];
        public static void AddUser(string? username, string? password, int accessLevel)
        {
            if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(username), "Username/Password cannot be null.");
            if (usernamePassword.ContainsKey(username))
                throw new ArgumentException("Username already exists.");
            usernamePassword[username] = new(username,password,accessLevel);
        }
        public static bool RemoveUser(string? username)
        {
            if(string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username), "Username cannot be null.");
            return usernamePassword.Remove(username);
        }
        public static User? GetUser(string username)
        {
            usernamePassword.TryGetValue(username, out User? value);
            return value;
        }
        public static string? Login(string? username, string? password)
        {
            if(string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(username), "Username/Password cannot be null.");
            if (usernamePassword.TryGetValue(username, out User? value))
            {
                if(value.Password == password) // Password comparison; use hashed comparison in real scenarios.
                {
                    string token = Utilities.GenerateUUID();
                    tokenUsername[token] = value;
                    return token;
                }
            }
            return null;
        }
        public static void Logout(string? token)
        {
            token ??= "";
            tokenUsername.TryGetValue(token, out User? value);
            if(value == null)
                throw new ArgumentException("Invalid Token");
            tokenUsername.Remove(token);
        }
        public static string? VerifyToken(string? token)
        {
            if(token == null)
                return null;
            if (tokenUsername.TryGetValue(token, out User? value))
            {
                return value.Username;
            }
            return null;
        }
        public static void SaveToFile()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(usernamePassword, options);
            file.Write(json);
        }
        public static void LoadFromFile()
        {
            if (System.IO.File.Exists(file.Info.FullName))
            {
                string json = file.Read();
                try
                {
                    var deserializedDb = JsonSerializer.Deserialize<Dictionary<string, User>>(json);
                    if (deserializedDb != null)
                    {
                        usernamePassword = deserializedDb;
                    }
                }
                catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}