using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.Json;

namespace KnowledgeBase.Controllers;

[ApiController]
[Route("Directory")]
[Produces("application/json")]

public class DirectoryController : ControllerBase
{
    static JsonObject JsonResponse(bool success, string message, JsonObject? data = null) => JResponse.Create(success, message, data);

    private Directory root;

    public DirectoryController()
    {
        try{
            root = new Directory(Path.Combine(System.IO.Directory.GetCurrentDirectory(),"root"));
        }
        catch(Exception e){
            Console.WriteLine(e.Message + "\nCreating root directory.");
            root = Directory.Create(Path.Combine(System.IO.Directory.GetCurrentDirectory(),"root"));
        }
    }

    [Route("Create")]
    public JsonObject Create([FromBody] JsonObject directoryInfo)
    {
        if(directoryInfo is null)
            return JsonResponse(false,"Directory Info cannot be null.");

        string? path = directoryInfo["directoryInfo"]?["path"]?.ToString();
        string? token = directoryInfo["directoryInfo"]?["token"]?.ToString();

        if (UserDB.VerifyToken(token) == null)
            return JsonResponse(false,"Invalid token.");

        try
        {
            Directory newDir = Directory.Create(Path.Combine(System.IO.Directory.GetCurrentDirectory(), path) );
        }
        catch (Exception e)
        {
            return JsonResponse(false,e.Message);
        }

        return JsonResponse(true,"Directory created.");
    }

    [Route("Move")]
    public JsonObject Move([FromBody] JsonObject directoryInfo)
    {
        if(directoryInfo is null)
            return JsonResponse(false,"Directory Info cannot be null.");
        

        var sourcePath = directoryInfo["directoryInfo"]?["source"]?.ToString();
        var destPath = directoryInfo["directoryInfo"]?["destination"]?.ToString();
        string? token = directoryInfo["directoryInfo"]?["token"]?.ToString();

        if (UserDB.VerifyToken(token) == null)
            return JsonResponse(false,"Invalid token.");

        try
        {
            Directory source = new(Path.Combine(System.IO.Directory.GetCurrentDirectory(), sourcePath));
            Directory dest = new(Path.Combine(System.IO.Directory.GetCurrentDirectory(), destPath));
            source.Move(dest);
        }
        catch (Exception e)
        {
             return JsonResponse(false,e.Message);
        }

        return JsonResponse(true,"Directory moved.");
    }

    [Route("Delete")]
    public JsonObject Delete([FromBody] JsonObject directoryInfo)
    {
        if(directoryInfo is null)
            return JsonResponse(false,"Directory Info cannot be null.");

        var path = directoryInfo["directoryInfo"]?["path"]?.ToString();
        string? token = directoryInfo["directoryInfo"]?["token"]?.ToString();

        if (UserDB.VerifyToken(token) == null)
            return JsonResponse(false,"Invalid token.");
        
        try
        {
            Directory source = new(Path.Combine(System.IO.Directory.GetCurrentDirectory(), path));
            source.Delete();
        }
        catch (Exception e)
        {
            return JsonResponse(false,e.Message);
        }

        return JsonResponse(true,"Directory deleted.");
    }

    [Route("Scan")]
    public JsonObject Scan([FromBody] JsonObject directoryInfo)
    {
        if(directoryInfo is null)
            return JsonResponse(false,"Directory Info cannot be null.");

        var path = directoryInfo["directoryInfo"]?["path"]?.ToString();
        
        try
        {
            Directory source = new(Path.Combine(System.IO.Directory.GetCurrentDirectory(), path));
            return JsonResponse(true,"Scan complete.",source.ToJSON());
        }
        catch (Exception e)
        {
            return JsonResponse(false,e.Message);
        }
    }
}
