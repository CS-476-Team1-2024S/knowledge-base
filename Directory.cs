using System.ComponentModel;
using System.Text;
namespace KnowledgeBase
{
    public class Directory
    {
        public DirectoryInfo Info { get; set; }
        public Directory(string path)
        {
            if(System.IO.Directory.Exists(path))
                Info = new(path);
            else
                throw new ArgumentException($"{path} doesn't exist.");
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
        
        // Format in json in Controller
        public List<String> Scan()
        {
            DirectoryInfo directoryInfo = this.Info;
            List<String> paths = [];

            FileInfo[] files = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
            foreach (FileInfo fileInfo in files)
            {
                paths.Add(fileInfo.FullName);
            }

            // Get all directories in the directory and its subdirectories
            DirectoryInfo[] directories = directoryInfo.GetDirectories("*", SearchOption.AllDirectories);
            foreach (DirectoryInfo dir in directories)
            {
                paths.Add(dir.FullName);
            }
            List<string> relativePaths = [];
            foreach (string path in paths)
            {
                string relativePath = path[(directoryInfo.FullName.Length - directoryInfo.Name.Length)..];
                relativePaths.Add(relativePath.TrimStart(Path.DirectorySeparatorChar));
            }

            return relativePaths;
        }
        public override string ToString()
        {
            return Print(this.Info);
        }
        private static string Print(DirectoryInfo root, int level = 0)
        {
            StringBuilder builder = new();

            builder.AppendLine(string.Concat(Enumerable.Repeat("    ", level)) + "¬" + root.Name);

            foreach (var sub in root.EnumerateDirectories())
                builder.Append(Print(sub, level + 1));
            foreach (var file in root.EnumerateFiles())
                builder.AppendLine(string.Concat(Enumerable.Repeat("    ", level + 1)) + "¬" + file.Name);

            return builder.ToString();
        }
    }
}