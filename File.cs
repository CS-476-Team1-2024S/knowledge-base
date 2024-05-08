using System.Text;
namespace KnowledgeBase
{
    public class File : FileSystemEntity
    {
        public FileInfo Info { get; set; }
        public File(string path)
        {
            if(string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path),"Path cannot be null.");
            if(System.IO.File.Exists(path))
                Info = new(path);
            else
            {
                throw new ArgumentException($"{path} doesn't exist.");
            }
        }
        public static File Create(string path)
        {
            if(string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path),"Path cannot be null.");
            if(System.IO.File.Exists(path))
                throw new ArgumentException($"{path} already exists.");
            System.IO.File.Create(path).Close();

            IncrementChangeCount();

            return new File(path);
        }
        public void Move(Directory dest)
        {
            this.Info.MoveTo(Path.Combine(dest.Info.FullName, this.Info.Name));

            IncrementChangeCount();
        }
        public void Delete()
        {
            this.Info.Delete();

            IncrementChangeCount();
        }
        public void Write(string content, bool append = false)
        {
            if(string.IsNullOrEmpty(content))
                throw new ArgumentNullException(nameof(content),"Content cannot be empty or null.");
            byte[] bytes = Encoding.UTF8.GetBytes(content);
            FileMode fm = append ? FileMode.Append : FileMode.Create;
            using FileStream fs = new(this.Info.FullName, fm);
            fs.Write(bytes);

            IncrementChangeCount();
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