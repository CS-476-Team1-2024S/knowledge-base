using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
namespace KnowledgeBase
{
    public static class Extensions
    {
        public static string Hash256(this string value)
        {
            return SHA256.HashData(value.ToByteArray()).FromByteArray();
        }
        public static byte[] ToByteArray(this string value, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8; // Use UTF8 by default
            return encoding.GetBytes(value);
        }
        public static string FromByteArray(this byte[] value, Encoding? encoding = null)
        {
            encoding ??= Encoding.UTF8; // Use UTF8 by default
            return encoding.GetString(value);
        }
        public static bool IsAlphaNumeric(this string value)
        {
            return Regex.IsMatch(value, @"^[a-zA-Z0-9_]*$");
        }
    }
}