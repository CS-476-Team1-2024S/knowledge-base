using System.Text;
namespace KnowledgeBase
{
    class Directory
    {
        public DirectoryInfo Info { get; set; }
        public Directory(string path)
        {
            Info = new(path);
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
        // Functions: Create files/Directories
    }
}