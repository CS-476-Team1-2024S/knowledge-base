using System.Text;
namespace KnowledgeBase
{
    public class Directory
    {
        public DirectoryInfo Info { get; set; }
        public Directory(string path)
        {
            // Will create a new directory if it does not exist
            Info = System.IO.Directory.CreateDirectory(path);
        }
        public static void Move(Directory src, Directory dest)
        {
            src.Info.MoveTo(Path.Combine(dest.Info.FullName, src.Info.Name));
        }
        public static void Delete(Directory dir)
        {

            dir.Info.Delete(true);
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