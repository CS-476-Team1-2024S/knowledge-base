using System.Text.Json.Nodes;
namespace KnowledgeBase
{
    public static class JResponse
    {
        public static readonly JsonObject noData = [];
        public static JsonObject Create(bool success, string content, JsonObject? data = null)
        {
            data ??= noData; // Use noData if data is null
            return new JsonObject
            {
                ["Success"] = success,
                ["Content"] = content,
                ["Data"] = data
            };
        }
    }
}