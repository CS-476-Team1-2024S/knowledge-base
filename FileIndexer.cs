using System.Text.RegularExpressions;

namespace KnowledgeBase
{
    class FileIndexer
    {
        private static Dictionary<string, Dictionary<string, int>> wordCounts = [];
        public static void IndexDirectory(Directory root)
        {
            foreach (var sub in root.Info.EnumerateDirectories())
                IndexDirectory(new(sub.FullName));
            foreach (var file in root.Info.EnumerateFiles())
                IndexFile(new(file.FullName));
        }
        public static void IndexFile(File file)
        {
            string content = file.Read();
            string[] words = Regex.Split(content.ToLower(), @"\W+");


            foreach (string word in words)
            {
                if (word == "") continue; // skip empty entries from split

                wordCounts.TryGetValue(word, out Dictionary<string, int>? value);

                if(value == null)
                    wordCounts[word] = [];
                
                if(wordCounts[word].ContainsKey(file.Info.FullName))
                    wordCounts[word][file.Info.FullName]++;
                else
                    wordCounts[word][file.Info.FullName] = 1;
                
            }
        }
        public static List<KeyValuePair<string,int>> SearchAndRank(string query)
        {
            Dictionary<string, int> fileScores = [];

            string[] queryWords = query.ToLower().Split(new char[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string word in queryWords)
            {
                if (wordCounts.TryGetValue(word, out Dictionary<string, int>? value))
                {
                    if(value == null) continue;
                    foreach (var entry in value)
                    {
                        string filePath = entry.Key;
                        int score = entry.Value;
                        if (fileScores.ContainsKey(filePath))
                            fileScores[filePath] += score;
                        else
                            fileScores[filePath] = score;
                    }
                }
            }
            var rankedFiles = fileScores.OrderByDescending(pair => pair.Value)
                                        .ToList();
            foreach(var rankedFile in rankedFiles)
            {
                Console.WriteLine($"{rankedFile.Key} : {rankedFile.Value}");
            }
            return rankedFiles;
        }
    }
}