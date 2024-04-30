using System.Text;
namespace KnowledgeBase
{
    public class File
    {
        public FileInfo Info { get; set; }
        public File(string path)
        {
            // Will create a new file if it does not exist 
            if(System.IO.File.Exists(path))
                Info = new(path);
            else
            {
                try
                {
                    System.IO.File.Create(path).Close();
                    Info = new(path);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        public static File Create(string path)
        {
            if(System.IO.File.Exists(path))
                throw new ArgumentException($"{path} already exists.");
            System.IO.File.Create(path).Close();
            return new File(path);
        }
        public void Move(Directory dest)
        {
            this.Info.MoveTo(Path.Combine(dest.Info.FullName, this.Info.Name));
        }
        public void Delete()
        {
            this.Info.Delete();
        }
        public void Write(string content, bool append = false)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            FileMode fm = append ? FileMode.Append : FileMode.Create;
            using FileStream fs = new(this.Info.FullName, fm);
            fs.Write(bytes);
        }
        public string Read()
        {
            using FileStream fs = new(this.Info.FullName, FileMode.Open);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}