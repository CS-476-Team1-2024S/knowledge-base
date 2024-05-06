using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase.Controllers;

[ApiController]
[Route("Search")]
[Produces("application/json")]
public class SearchController : ControllerBase
{
    private Directory root;
    private FileIndexer index;

    public SearchController()
    {
        index = new FileIndexer();
        try{
            root = new Directory("root");
        }
        catch(Exception e){
            Console.WriteLine(e.Message + "\nCreating root directory.");
            System.IO.Directory.CreateDirectory("root");
        }
        finally{
            root = new Directory("root");
        }
    }

    [Route("Query")]
    public string Query([FromBody] JsonObject searchInfo)
    {
        List<String> searchResults;
        if(searchInfo is null){
            return "File info cannot be null.";
        }

        var query = searchInfo["searchInfo"]?["query"]?.ToString();

        if(string.IsNullOrEmpty(query)){
            return "Query cannot be null.";
        }

        try
        {
            index.IndexDirectory(root);
            searchResults = index.SearchTFIDF(query);
        }
        catch(Exception e)
        {
            return e.Message;
        }
        
        return string.Join(",", searchResults);
    }
}
