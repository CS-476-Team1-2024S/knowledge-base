using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase.Controllers;

[ApiController]
[Route("User")]
[Produces("application/json")]
public class UserController : ControllerBase
{
    static JsonObject JsonResponse(bool success, string message, JsonObject? data = null) => JResponse.Create(success, message, data);
    public UserController()
    {
        UserDB.LoadFromFile();
    }
    ~UserController()
    {
        UserDB.SaveToFile();
    }

    [Route("Add")]
    public JsonObject Add([FromBody] JsonObject userInfo)
    {
        if(userInfo is null)
            return JsonResponse(false,"User info cannot be null");

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
            return JsonResponse(false,e.Message);
        }

        try
        {
            UserDB.AddUser(username, password, accessLevel);
        }
        catch (Exception e)
        {
            return JsonResponse(false,e.Message);
        }

        return JsonResponse(true,"User added.");
    }

    [Route("Login")]
    public JsonObject Login([FromBody] JsonObject userInfo)
    {

        if(userInfo is null)
            return JsonResponse(false,"User info cannot be null");

        string? username = userInfo["userInfo"]?["username"]?.ToString();
        string? password = userInfo["userInfo"]?["password"]?.ToString();

        try
        {
            User user = new(username,password);
        }
        catch(Exception e)
        {
            return JsonResponse(false,e.Message);
        }
        string? token = UserDB.Login(username, password);

        if(token == null)
            return JsonResponse(false,"Username/Password incorrect.");
        return JsonResponse(true,"Login successful.", new JsonObject{ ["Token"] = token });
    }
    [Route("Logout")]
    public JsonObject Logout([FromBody] JsonObject userInfo)
    {
        if(userInfo is null)
            return JsonResponse(false,"User info cannot be null");

        string? token = userInfo["userInfo"]?["token"]?.ToString();

        try
        {
            UserDB.Logout(token);
        }
        catch (Exception e)
        {
            return JsonResponse(false,e.Message);
        }
        return JsonResponse(true,"Login successful.", new JsonObject{ ["Token"] = token });
    }

    [Route("Remove")]
    public JsonObject Remove([FromBody] JsonObject userInfo)
    {
        if(userInfo is null)
            return JsonResponse(false,"User info cannot be null");

        string? username = userInfo["userInfo"]?["username"]?.ToString();
        string? password = userInfo["userInfo"]?["password"]?.ToString();

        try
        {
            User user = new(username,password);
        }
        catch(Exception e)
        {
            return JsonResponse(false,e.Message);
        }

        try
        {
            UserDB.RemoveUser(username);
        }
        catch (Exception e)
        {
            return JsonResponse(false,e.Message);
        }
        
        return JsonResponse(true,"User removed.");
    }
}// Add messages where appropriate
