namespace KnowledgeBase
{
    class File
    {
        public FileInfo Info { get; set; }
        public File(string path)
        {
            Info = new(path);
        }

        // Function: Read, Write, Create, ?Track changes?
    }
}