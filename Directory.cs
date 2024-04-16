using System.Text;
namespace KnowledgeBase
{
    public class Directory
    {
        public DirectoryInfo Info { get; set; }
        public Directory(string path)
        {
            if(System.IO.Directory.Exists(path))
                Info = System.IO.Directory.CreateDirectory(path);
            else
                throw new ArgumentException($"{path} does not exist.");
        }
        public static Directory Create(string path)
        {
            if(System.IO.Directory.Exists(path))
                throw new ArgumentException($"{path} already exists.");
            System.IO.Directory.CreateDirectory(path);
            return new Directory(path);
        }
        public void Move(Directory dest)
        {
            this.Info.MoveTo(Path.Combine(dest.Info.FullName, this.Info.Name));
        }
        public void Delete()
        {

            this.Info.Delete(true);
        }
        public override string ToString()
        {
            return Scan(this.Info);
        }
        private static string Scan(DirectoryInfo root, int level = 0)
        {
            StringBuilder builder = new();

            builder.AppendLine(string.Concat(Enumerable.Repeat("    ", level)) + "¬" + root.Name);

            foreach (var sub in root.EnumerateDirectories())
                builder.Append(Scan(sub, level + 1));
            foreach (var file in root.EnumerateFiles())
                builder.AppendLine(string.Concat(Enumerable.Repeat("    ", level + 1)) + "¬" + file.Name);

            return builder.ToString();
        }
    }
}