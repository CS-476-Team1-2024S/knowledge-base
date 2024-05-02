using System.ComponentModel;
using System.Text.RegularExpressions;

namespace KnowledgeBase
{
    class FileIndexer
    {
        private Dictionary<string, Dictionary<string, int>> wordCounts = [];
        private Dictionary<string, int> documentFrequency = [];
        private Dictionary<string, int> totalWordsPerDocument = [];
        private int totalDocuments;
        public void IndexDirectory(Directory root)
        {
            foreach (var sub in root.Info.EnumerateDirectories())
                IndexDirectory(new(sub.FullName));
            foreach (var file in root.Info.EnumerateFiles())
                IndexFile(new(file.FullName));
        }
        public void IndexFile(File file)
        {
            string content = file.Read();
            string[] words = Regex.Split(content.ToLower(), @"\W+");

            foreach (string word in words)
            {
                if (word == "") continue; // skip empty entries from split

                wordCounts.TryGetValue(word, out Dictionary<string, int>? value); // Get KVP associated with word

                if(value == null) // If it DNE, create an empty KVP
                    wordCounts[word] = [];
                
                if(wordCounts[word].ContainsKey(file.Info.FullName)) // If word has been seen before in this document
                    wordCounts[word][file.Info.FullName]++; // Increment count
                else // If word hasn't been seen before in the document
                {
                    wordCounts[word][file.Info.FullName] = 1; // count = 1
                    if(documentFrequency.ContainsKey(word)) // Track number of documents that contain this word
                        documentFrequency[word]++;
                    else
                        documentFrequency[word] = 1;
                }
            }
            totalWordsPerDocument[file.Info.FullName] = words.Length;
            totalDocuments++;
        }
        // Term Frequency-Inverse Document Frequency
        public List<string> SearchTFIDF(string query)
        {
            Dictionary<string, double> fileScores = [];

            string[] queryWords = query.ToLower().Split(new char[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();

            foreach (string word in queryWords)
            {
                if (wordCounts.TryGetValue(word, out Dictionary<string, int>? value))
                {   
                    if(value == null) continue;
                    double idf = Math.Log((double)totalDocuments / documentFrequency[word]); // Calculate Inverse Document Frequency
                    foreach (var entry in value)
                    {
                        string filePath = entry.Key;
                        double tf = (double)entry.Value / totalWordsPerDocument[filePath]; // Calculate Term Frequency
                        double tfidf = tf * idf;

                        if (fileScores.ContainsKey(filePath)) // Add it to that document "score"
                            fileScores[filePath] += tfidf;
                        else
                            fileScores[filePath] = tfidf;
                    }
                }
            }
            var rankedFiles = fileScores.OrderByDescending(pair => pair.Value).Select(pair => pair.Key).ToList(); // Rank documents by score
            return rankedFiles;
        }
    }
}