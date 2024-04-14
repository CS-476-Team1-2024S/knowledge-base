using System.Text;
namespace KnowledgeBase
{
    class File
    {
        public FileInfo Info { get; set; }
        public File(string path)
        {
            // Will create a new file if it does not exist 
            if(!System.IO.File.Exists(path))
                using (System.IO.File.Create(path));
            Info = new(path);
        }
        public static void Move(File src, Directory dest)
        {
            src.Info.MoveTo(Path.Combine(dest.Info.FullName, src.Info.Name));
        }
        public static void Delete(File file)
        {
            file.Info.Delete();
        }
        public static void Write(File file, string content, bool append = false)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            FileMode fm = append ? FileMode.Append : FileMode.Create;
            using FileStream fs = new(file.Info.FullName,fm);
            fs.Write(bytes);
        }
        public static string Read(File file)
        {
            using FileStream fs = new(file.Info.FullName, FileMode.Open);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes);
            return Encoding.UTF8.GetString(bytes);
        }
    }
}