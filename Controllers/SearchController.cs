using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase.Controllers;

[ApiController]
[Route("Search")]
[Produces("application/json")]
public class SearchController : ControllerBase
{
    static JsonObject JsonResponse(bool success, string message, JsonObject? data = null) => JResponse.Create(success, message, data);
    public SearchController()
    {
    }

    [Route("Query")]
    public JsonObject Query([FromBody] JsonObject searchInfo)
    {
        List<String> searchResults;
        if(searchInfo is null)
            return JsonResponse(false, "Search info cannot be null.");

        var query = searchInfo["searchInfo"]?["query"]?.ToString();

        try
        {
            searchResults = Indexer.SearchTFIDF(query);
        }
        catch(Exception e)
        {
            return JsonResponse(false, e.Message);
        }

        var jsonArray = new JsonArray();
        foreach (var path in searchResults)
        {
            jsonArray.Add(path);
        }
        return JsonResponse(true, "Search complete.", new JsonObject{["FilePaths"] = jsonArray});
    }
}
