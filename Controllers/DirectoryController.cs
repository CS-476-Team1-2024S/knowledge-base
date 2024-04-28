using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase;

[ApiController]
[Route("Directory")]
[Produces("application/json")]
public class DirectoryController : ControllerBase
{
    private readonly ILogger<DirectoryController> _logger;
    private Directory root;

    public DirectoryController(ILogger<DirectoryController> logger)
    {
        root = new Directory("root");
        _logger = logger;
    }

    [Route("Create")]
    public string Create([FromBody] JsonObject directoryInfo)
    {
        if(directoryInfo is null){
            return "Directory info cannot be null.";
        }

        var path = directoryInfo["directoryInfo"]?["path"]?.ToString();

        if(string.IsNullOrEmpty(path)){
            return "Path cannot be null.";
        }

        try
        {
            root = Directory.Create(root.Info.FullName + @"/" + path);
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return "Directory created successfully";
    }

    [Route("Move")]
    public string Move([FromBody] JsonObject directoryInfo)
    {
        if(directoryInfo is null){
            return "Directory info cannot be null.";
        }

        var sourcePath = directoryInfo["directoryInfo"]?["source"]?.ToString();
        var destPath = directoryInfo["directoryInfo"]?["destination"]?.ToString();

        if(string.IsNullOrEmpty(sourcePath) || string.IsNullOrEmpty(destPath)){
            return "Source and destination paths cannot be null.";
        }

        try
        {
            Directory source = new Directory(root.Info.FullName + @"/" + sourcePath);
            Directory dest = new Directory(root.Info.FullName + @"/" + destPath);
            source.Move(dest);
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return "Directory moved successfully";
    }

    [Route("Delete")]
    public string Delete([FromBody] JsonObject directoryInfo)
    {
        if(directoryInfo is null){
            return "Directory info cannot be null.";
        }

        var path = directoryInfo["directoryInfo"]?["path"]?.ToString();

        if(string.IsNullOrEmpty(path)){
            return "Path cannot be null.";
        }

        try
        {
            Directory source = new Directory(root.Info.FullName + @"/" + path);
            source.Delete();
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return "Directory deleted successfully";
    }

    [Route("Scan")]
    public string Scan()
    {
        string directories;
        try
        {
            directories = root.ToString();
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return directories;
    }
}
