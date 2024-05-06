using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase.Controllers;

[ApiController]
[Route("User")]
[Produces("application/json")]
public class UserController : ControllerBase
{

    public UserController()
    {
        UserDB.LoadFromFile();
    }

    [Route("Add")]
    public string Add([FromBody] JsonObject userInfo)
    {
        if(userInfo is null){
            return "User info cannot be null.";
        }

        string? username = userInfo["userInfo"]?["username"]?.ToString();
        string? password = userInfo["userInfo"]?["password"]?.ToString();
        if(!int.TryParse(userInfo["userInfo"]?["password"]?.ToString(), out int accessLevel))
        {
            accessLevel = -1;
        }

        try
        {
            User user = new(username,password, accessLevel);
        }
        catch(Exception e)
        {
            return e.Message;
        }

        try
        {
            UserDB.AddUser(username, password, accessLevel);
            UserDB.SaveToFile();
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return "User added successfully.";
    }

    [Route("Login")]
    public string Login([FromBody] JsonObject userInfo)
    {

        if(userInfo is null){
            return "User info cannot be null.";
        }

        string? username = userInfo["userInfo"]?["username"]?.ToString();
        string? password = userInfo["userInfo"]?["password"]?.ToString();

        try
        {
            User user = new(username,password);
        }
        catch(Exception e)
        {
            return e.Message;
        }
        string? token = UserDB.Login(username, password);

        return token ?? "Username/Password is incorrect";
    }

    [Route("Remove")]
    public string Remove([FromBody] JsonObject userInfo)
    {
        if(userInfo is null){
            return "User info cannot be null.";
        }

        string? username = userInfo["userInfo"]?["username"]?.ToString();
        string? password = userInfo["userInfo"]?["password"]?.ToString();

        try
        {
            User user = new(username,password);
        }
        catch(Exception e)
        {
            return e.Message;
        }

        try
        {
            UserDB.RemoveUser(username);
        }
        catch (Exception e)
        {
            return e.Message;
        }
        
        return "User was removed successfully.";
    }
}
