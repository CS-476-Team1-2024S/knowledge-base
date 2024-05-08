namespace KnowledgeBase
{
    public abstract class FileSystemEntity
    {
        // First time you try to search will index
        private static int changeCount = 1;

        protected static void IncrementChangeCount()
        {
            changeCount++;
        }
        public static void ResetChangeCount()
        {
            changeCount = 0;
        }
        public static int ChangeCount => changeCount;
    }
}