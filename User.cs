using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace KnowledgeBase
{
    public class User
    {
        private string username;
        private string password;
        private int accessLevel;

        public string Username
        {
            get => username;
            set
            {
                if(string.IsNullOrWhiteSpace(value) || value.Length < 6)
                    throw new Exception("Username must be at least 6 characters.");
                if(!value.IsAlphaNumeric())
                    throw new Exception("Username can only contain alphanumeric characters and underscores.");
                username = value;
            }
        }
        public string Password
        {
            get => password;
            set
            {
                if(string.IsNullOrWhiteSpace(value) || value.Length < 6)
                    throw new Exception("Password must be at least 6 characters.");
                 if(!value.IsAlphaNumeric())
                     throw new Exception("Password can only contain alphanumeric characters and underscores.");
                password = value;
            }
        }
        public int AccessLevel
        {
            get => accessLevel;
            set
            {
                if(value < 0)
                    throw new Exception("Not a valid access level.");
                accessLevel = value;
            }
        }
        public User(string? username, string? password, int accessLevel = 0)
        {
            Username = username;
            Password = password;
            AccessLevel = accessLevel;
        }

        public override string ToString()
        {
            return $"{Username},{Password},{AccessLevel}"; // Use hashed values in real scenarios.
        }
    }
}