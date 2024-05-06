using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace KnowledgeBase.Controllers;

[ApiController]
[Route("Directory")]
[Produces("application/json")]
public class DirectoryController : ControllerBase
{
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
    public string Create([FromBody] JsonObject directoryInfo)
    {
        if(directoryInfo is null){
            return "Directory info cannot be null.";
        }

        string? path = directoryInfo["directoryInfo"]?["path"]?.ToString();

        try
        {
            Directory newDir = Directory.Create(Path.Combine(System.IO.Directory.GetCurrentDirectory(), path) );
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return "Directory created successfully.";
    }

    [Route("Move")]
    public string Move([FromBody] JsonObject directoryInfo)
    {
        if(directoryInfo is null){
            return "Directory info cannot be null.";
        }

        var sourcePath = directoryInfo["directoryInfo"]?["source"]?.ToString();
        var destPath = directoryInfo["directoryInfo"]?["destination"]?.ToString();

        try
        {
            Directory source = new(Path.Combine(System.IO.Directory.GetCurrentDirectory(), sourcePath));
            Directory dest = new(Path.Combine(System.IO.Directory.GetCurrentDirectory(), destPath));
            source.Move(dest);
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return "Directory moved successfully.";
    }

    [Route("Delete")]
    public string Delete([FromBody] JsonObject directoryInfo)
    {
        if(directoryInfo is null){
            return "Directory info cannot be null.";
        }

        var path = directoryInfo["directoryInfo"]?["path"]?.ToString();
        
        try
        {
            Directory source = new(Path.Combine(System.IO.Directory.GetCurrentDirectory(), path));
            source.Delete();
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return "Directory deleted successfully.";
    }

    [Route("Scan")]
    public string Scan([FromBody] JsonObject directoryInfo)
    {
        if(directoryInfo is null){
            return "Directory info cannot be null.";
        }

        var path = directoryInfo["directoryInfo"]?["path"]?.ToString();
        
        try
        {
            Directory source = new(Path.Combine(System.IO.Directory.GetCurrentDirectory(), path));
            return source.ToJSON();
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
}
