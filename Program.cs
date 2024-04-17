namespace KnowledgeBase
{
    class Program {
        static void Main()
        {
            string path = Path.Combine(System.IO.Directory.GetCurrentDirectory(),"Root");
            Directory root = new(path);
            // File f = File.Create(path+"\\Dir3"+"\\Info4.txt");
            // f.Write("Hello there!",true);

            // Console.WriteLine(root.ToString());
            // f.Move(new(path+"\\Dir2"));
            // f.Write("I've been moved!",true);
            // Directory dir3 = new(path+"\\Dir3");
            // dir3.Move(new(path+"\\Dir2"));

            // Console.WriteLine(root.ToString());

            // UserDB.AddUser("TechnoBro03","password1234",1);

            // Console.WriteLine(UserDB.VerifyUser("TechnoBro03","password1234"));
            // UserDB.LoadFromFile();
            // Console.WriteLine(UserDB.VerifyUser("TechnoBro03","password1234"));
            // UserDB.SaveToFile();
            
            FileIndexer.IndexDirectory(root);
            FileIndexer.SearchAndRank("i");
        }
    }
}