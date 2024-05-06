using System.Text.Json;
using System.Text;
namespace KnowledgeBase
{
    public class Directory
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
        
        // // Returns tuple of type List<String> of all directory and file paths
        // public (List<String>,List<String>) Scan(bool relative = false)
        // {
        //     DirectoryInfo directoryInfo = this.Info;
        //     List<String> directories = [];
        //     List<String> files = [];

        //     FileInfo[] fileInfos = directoryInfo.GetFiles("*", SearchOption.AllDirectories);
        //     foreach(FileInfo fileInfo in fileInfos )
        //     {
        //         files.Add(fileInfo.FullName);
        //     }

        //     DirectoryInfo[] directoryInfos = directoryInfo.GetDirectories("*", SearchOption.AllDirectories);
        //     foreach (DirectoryInfo dir in directoryInfos)
        //     {
        //         directories.Add(dir.FullName);
        //     }
        //     if(!relative)
        //         return (directories, files);

        //     // Get relative paths to the root
        //     List<string> relativeDirectories = [];
        //     List<string> relativeFiles = [];
        //     foreach (string path in directories)
        //     {
        //         string relativePath = path[(directoryInfo.FullName.Length - directoryInfo.Name.Length)..];
        //         relativeDirectories.Add(relativePath.TrimStart(Path.DirectorySeparatorChar));
        //     }
        //     foreach (string path in files)
        //     {
        //         string relativePath = path[(directoryInfo.FullName.Length - directoryInfo.Name.Length)..];
        //         relativeFiles.Add(relativePath.TrimStart(Path.DirectorySeparatorChar));
        //     }
        //     return (relativeDirectories, relativeFiles);
        // }
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
        public string ToJSON()
        {
            DirectoryNode root = BuildTree(this.Info);

            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(root, options);
            return json;
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