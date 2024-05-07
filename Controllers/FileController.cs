using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase.Controllers;

[ApiController]
[Route("File")]
[Produces("application/json")]
public class FileController : ControllerBase
{
    static JsonObject JsonResponse(bool success, string message, JsonObject? data = null) => JResponse.Create(success, message, data);
    private Directory root;

    public FileController()
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
    public JsonObject Create([FromBody] JsonObject fileInfo)
    {
        if(fileInfo is null)
            return JsonResponse(false, "File info cannot be null.");
        
        var path = fileInfo["fileInfo"]?["path"]?.ToString();
        string? token = fileInfo["fileInfo"]?["token"]?.ToString();

        if (UserDB.VerifyToken(token) == null)
            return JsonResponse(false,"Invalid token.");
        
        try
        {
            File newFile = KnowledgeBase.File.Create(Path.Combine(System.IO.Directory.GetCurrentDirectory(), path));
        }
        catch(Exception e)
        {
            return JsonResponse(false,e.Message);
        }
        
        return JsonResponse(true,"File created.");
    }

    [Route("Move")]
    public JsonObject Move([FromBody] JsonObject fileInfo)
    {
        if(fileInfo is null)
            return JsonResponse(false, "File info cannot be null.");

        var sourcePath = fileInfo["fileInfo"]?["source"]?.ToString();
        var destPath = fileInfo["fileInfo"]?["destination"]?.ToString();
        string? token = fileInfo["fileInfo"]?["token"]?.ToString();

        if (UserDB.VerifyToken(token) == null)
            return JsonResponse(false,"Invalid token.");

        try
        {
            File source = new(Path.Combine(System.IO.Directory.GetCurrentDirectory(), sourcePath));
            Directory dest = new(Path.Combine(System.IO.Directory.GetCurrentDirectory(), destPath));
            source.Move(dest);
        }
        catch (Exception e)
        {
            return JsonResponse(false,e.Message);
        }

        return JsonResponse(true,"File moved.");
    }

    [Route("Delete")]
    public JsonObject Delete([FromBody] JsonObject fileInfo)
    {
        if(fileInfo is null)
            return JsonResponse(false, "File info cannot be null.");

        var path = fileInfo["fileInfo"]?["path"]?.ToString();
        string? token = fileInfo["fileInfo"]?["token"]?.ToString();

        if (UserDB.VerifyToken(token) == null)
            return JsonResponse(false,"Invalid token.");
        
        try
        {
            File source = new(Path.Combine(System.IO.Directory.GetCurrentDirectory(), path));
            source.Delete();
        }
        catch (Exception e)
        {
            return JsonResponse(false,e.Message);
        }

        return JsonResponse(true,"File deleted.");
    }

    [Route("Write")]
    public JsonObject Write([FromBody] JsonObject fileInfo)
    {
        if(fileInfo is null)
            return JsonResponse(false, "File info cannot be null.");

        var path = fileInfo["fileInfo"]?["path"]?.ToString();
        var content = fileInfo["fileInfo"]?["content"]?.ToString();
        var append = fileInfo["fileInfo"]?["append"]?.ToString();
        string? token = fileInfo["fileInfoInfo"]?["token"]?.ToString();

        if (UserDB.VerifyToken(token) == null)
            return JsonResponse(false,"Invalid token.");

        if(!bool.TryParse(append, out bool appendBool))
            return JsonResponse(false,"Append is an improper value");

        try
        {
            File source = new(Path.Combine(System.IO.Directory.GetCurrentDirectory(), path));
            source.Write(content, appendBool );
        }
        catch (Exception e)
        {
            return JsonResponse(false,e.Message);
        }

        return JsonResponse(true,"File written to.");
    }

    [Route("Read")]
    public JsonObject Read([FromBody] JsonObject fileInfo)
    {
        if(fileInfo is null)
            return JsonResponse(false, "File info cannot be null.");

        var path = fileInfo["fileInfo"]?["path"]?.ToString();
        string content;

        try
        {
            File source = new(Path.Combine(System.IO.Directory.GetCurrentDirectory(), path));
            content = source.Read();
        }
        catch (Exception e)
        {
            return JsonResponse(false,e.Message);
        }

        return JsonResponse(false,"", new JsonObject { ["FileContent"] = content});
    }
}
