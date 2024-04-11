namespace KnowledgeBase
{
    class Utilities
    {
        public static string GenerateUUID()
        {
            return Guid.NewGuid().ToString();
        }
    }
}