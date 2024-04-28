using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase;

[ApiController]
[Route("File")]
[Produces("application/json")]
public class FileController : ControllerBase
{
    private readonly ILogger<FileController> _logger;
    private Directory root;

    public FileController(ILogger<FileController> logger)
    {
        root = new Directory("root");
        _logger = logger;
    }

    [Route("Create")]
    public string Create([FromBody] JsonObject fileInfo)
    {
        if(fileInfo is null){
            return "File info cannot be null.";
        }

        var path = fileInfo["fileInfo"]?["path"]?.ToString();

        if(string.IsNullOrEmpty(path)){
            return "Path cannot be null.";
        }

        try
        {
            File newFile = KnowledgeBase.File.Create(root.Info.FullName + @"\" + path);
        }
        catch(Exception e)
        {
            return e.Message;
        }
        return "File created";
    }

    [Route("Move")]
    public string Move([FromBody] JsonObject fileInfo)
    {
        if(fileInfo is null){
            return "File info cannot be null.";
        }

        var sourcePath = fileInfo["fileInfo"]?["source"]?.ToString();
        var destPath = fileInfo["fileInfo"]?["destination"]?.ToString();

        if(string.IsNullOrEmpty(sourcePath) || string.IsNullOrEmpty(destPath)){
            return "Source and destination paths cannot be null.";
        }

        File source = new File(root.Info.FullName + @"\" + sourcePath);
        Directory dest = new Directory(root.Info.FullName + @"\" + destPath);
        try
        {
            source.Move(dest);
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return "File moved successfully";
    }

    [Route("Delete")]
    public string Delete([FromBody] JsonObject fileInfo)
    {
        if(fileInfo is null){
            return "File info cannot be null.";
        }

        var path = fileInfo["fileInfo"]?["path"]?.ToString();

        if(string.IsNullOrEmpty(path)){
            return "Path cannot be null.";
        }

        File source = new File(root.Info.FullName + @"\" + path);
        try
        {
            source.Delete();
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return "File deleted successfully";
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
        bool.TryParse(append, out bool appendBool);

        if(string.IsNullOrEmpty(path) || string.IsNullOrEmpty(content)){
            return "Path and content cannot be null.";
        }

        File source = new File(root.Info.FullName + @"\" + path);
        try
        {
            if(appendBool){
                source.Write(content, true);
            }
            else{
                source.Write(content);
            }
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return "File written successfully";
    }

    [Route("Read")]
    public string Read([FromBody] JsonObject fileInfo)
    {
        if(fileInfo is null){
            return "File info cannot be null.";
        }

        var path = fileInfo["fileInfo"]?["path"]?.ToString();
        string content;

        if(string.IsNullOrEmpty(path)){
            return "Path cannot be null.";
        }

        File source = new File(root.Info.FullName + @"\" + path);
        try
        {
            content = source.Read();
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return content;
    }
}