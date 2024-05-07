using System.Text.Json;
using System.Text;
using System.Text.Json.Nodes;
namespace KnowledgeBase
{
    public class Directory : FileSystemEntity
    {
        public DirectoryInfo Info { get; set; }
        public Directory(string? path)
        {
            if(string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path),"Path cannot be null.");
            if(System.IO.Directory.Exists(path))
                Info = new(path);
            else
                throw new ArgumentException($"{path} doesn't exist.");
        }
        public static Directory Create(string? path)
        {
           if(string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path),"Path cannot be null.");
            if (System.IO.Directory.Exists(path))
                throw new ArgumentException($"{path} already exists.");
            System.IO.Directory.CreateDirectory(path);

            IncrementChangeCount();

            return new Directory(path);
        }
        public void Move(Directory dest)
        {
            this.Info.MoveTo(Path.Combine(dest.Info.FullName, this.Info.Name));

            IncrementChangeCount();
        }
        public void Delete()
        {
            if(this.Info.Name == "Root")
                throw new ArgumentException("Cannot delete Root directory.");
            this.Info.Delete(true);

            IncrementChangeCount();
        }
        
        public static DirectoryNode BuildTree(DirectoryInfo directoryInfo)
        {
            var node = new DirectoryNode(directoryInfo.Name);
            foreach (FileInfo file in directoryInfo.GetFiles())
            {
                node.AddFile(file.Name);
            }
            foreach (DirectoryInfo subDir in directoryInfo.GetDirectories())
            {
                node.AddSubdirectory(BuildTree(subDir));
            }
            return node;
        }
        public JsonObject ToJSON()
        {
            DirectoryNode root = BuildTree(this.Info);

            var options = new JsonSerializerOptions { WriteIndented = false };
            string json = JsonSerializer.Serialize(root, options);
            JsonObject obj = JsonSerializer.Deserialize<JsonObject>(json);
            return obj;
        }
        public override string ToString()
        {
            return ToStringHelper(this.Info);
        }
        private static string ToStringHelper(DirectoryInfo root, int level = 0)
        {
            StringBuilder builder = new();

            builder.AppendLine(string.Concat(Enumerable.Repeat("    ", level)) + "¬" + root.Name);

            foreach (var sub in root.EnumerateDirectories())
                builder.Append(ToStringHelper(sub, level + 1));
            foreach (var file in root.EnumerateFiles())
                builder.AppendLine(string.Concat(Enumerable.Repeat("    ", level + 1)) + "¬" + file.Name);

            return builder.ToString();
        }
    }
}