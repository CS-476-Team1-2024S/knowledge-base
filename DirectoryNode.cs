namespace KnowledgeBase
{
    public class DirectoryNode
    {
        public string Name { get; set; }
        public List<string> Files { get; set; } = [];
        public List<DirectoryNode> Subdirectories { get; set; } = [];

        public DirectoryNode(string name)
        {
            Name = name;
        }
        public void AddFile(string fileName)
        {
            Files.Add(fileName);
        }
        public void AddSubdirectory(DirectoryNode subdirectory)
        {
            Subdirectories.Add(subdirectory);
        }
    }
}