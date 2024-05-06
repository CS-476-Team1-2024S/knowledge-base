using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase.Controllers;

[ApiController]
[Route("File")]
[Produces("application/json")]
public class FileController : ControllerBase
{
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
    public string Create([FromBody] JsonObject fileInfo)
    {
        if(fileInfo is null){
            return "File info cannot be null.";
        }

        var path = fileInfo["fileInfo"]?["path"]?.ToString();
        try
        {
            File newFile = KnowledgeBase.File.Create(Path.Combine(System.IO.Directory.GetCurrentDirectory(), path));
        }
        catch(Exception e)
        {
            return e.Message;
        }
        
        return "File created successfully.";
    }

    [Route("Move")]
    public string Move([FromBody] JsonObject fileInfo)
    {
        if(fileInfo is null){
            return "File info cannot be null.";
        }

        var sourcePath = fileInfo["fileInfo"]?["source"]?.ToString();
        var destPath = fileInfo["fileInfo"]?["destination"]?.ToString();

        try
        {
            File source = new(Path.Combine(System.IO.Directory.GetCurrentDirectory(), sourcePath));
            Directory dest = new(Path.Combine(System.IO.Directory.GetCurrentDirectory(), destPath));
            source.Move(dest);
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return "File moved successfully.";
    }

    [Route("Delete")]
    public string Delete([FromBody] JsonObject fileInfo)
    {
        if(fileInfo is null){
            return "File info cannot be null.";
        }

        var path = fileInfo["fileInfo"]?["path"]?.ToString();
        
        try
        {
            File source = new(Path.Combine(System.IO.Directory.GetCurrentDirectory(), path));
            source.Delete();
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return "File deleted successfully.";
    }

    [Route("Write")]
    public string Write([FromBody] JsonObject fileInfo)
    {
        if(fileInfo is null){
            return "File info cannot be null.";
        }

        var path = fileInfo["fileInfo"]?["path"]?.ToString();
        var content = fileInfo["fileInfo"]?["content"]?.ToString();
        var append = fileInfo["fileInfo"]?["append"]?.ToString();
        if(!bool.TryParse(append, out bool appendBool))
        {
            return "Append is an improper value";
        }

        try
        {
            File source = new(Path.Combine(System.IO.Directory.GetCurrentDirectory(), path));
            source.Write(content, appendBool );
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return "File written successfully.";
    }

    [Route("Read")]
    public string Read([FromBody] JsonObject fileInfo)
    {
        if(fileInfo is null){
            return "File info cannot be null.";
        }

        var path = fileInfo["fileInfo"]?["path"]?.ToString();
        string content;

        try
        {
            File source = new(Path.Combine(System.IO.Directory.GetCurrentDirectory(), path));
            content = source.Read();
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return content;
    }
}
