namespace KnowledgeBase
{
    class Program {
        static void Main()
        {
            string path = Path.Combine(System.IO.Directory.GetCurrentDirectory(),"Root");
            Directory root = new(path);

            Console.WriteLine(root.ToString());

            File file = new(path+"\\Info4.txt");
            File.Write(file, "This is me writing to a file");

            Console.WriteLine(root.ToString());
            
            File.Move(file, new(path+"\\Dir3"));

            Console.WriteLine(root.ToString());
            
        }
    }
}