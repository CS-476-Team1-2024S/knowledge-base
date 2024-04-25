using Microsoft.AspNetCore.Mvc;

namespace KnowledgeBase;

[ApiController]
[Route("User")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    [Route("Add/{username}/{password}/{accessLevel}")]
    public string Set(string username, string password, int accessLevel)
    {
        Response.Headers.Add("Access-Control-Allow-Origin", "*");

        try
        {
            UserDB.AddUser(username, password, accessLevel);
            UserDB.SaveToFile();
        }

        catch (Exception e)
        {
            return e.Message;
        }
        return "User added successfully";
    }

    [Route("Login/{username}/{password}")]
    public string Get(string username, string password)
    {
        Response.Headers.Add("Access-Control-Allow-Origin", "*");
        var verified = false;
        try
        {
            UserDB.LoadFromFile();
            verified = UserDB.VerifyUser(username, password);
        }

        catch (Exception e)
        {
            return e.Message;
        }
        if (verified)
        {
            return "User logged in successfully";
        }
        else
        {
            return "Username or password is incorrect.";
        }
    }

    [Route("Remove/{username}")]
    public string Set(string username)
    {
        Response.Headers.Add("Access-Control-Allow-Origin", "*");
        try
        {
            UserDB.LoadFromFile();
            UserDB.RemoveUser(username);
        }

        catch (Exception e)
        {
            return e.Message;
        }
            return "User was removed";
    }
}
