namespace KnowledgeBase
{
    class Program {
        static void Main()
        {
            string path = Path.Combine(System.IO.Directory.GetCurrentDirectory());
            Directory root = new(path);


            Console.WriteLine(root.ToString());
        }
    }
}