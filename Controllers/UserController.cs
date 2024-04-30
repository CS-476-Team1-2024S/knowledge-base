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

        var username = userInfo["userInfo"]?["username"]?.ToString();
        var password = userInfo["userInfo"]?["password"]?.ToString();
        var accessLevel = userInfo["userInfo"]?["accessLevel"]?.ToString();
        int accessLevelInt;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(accessLevel)){
            return "Username, password, or access level cannot be null.";
        }
        else{
            accessLevelInt = Int32.Parse(accessLevel);
        }

        try
        {
            UserDB.AddUser(username, password, accessLevelInt);
            UserDB.SaveToFile();
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return "User added successfully";
    }

    [Route("Login")]
    public string Login([FromBody] JsonObject userInfo)
    {
        bool verified;

        if(userInfo is null){
            return "User info cannot be null.";
        }

        var username = userInfo["userInfo"]?["username"]?.ToString();
        var password = userInfo["userInfo"]?["password"]?.ToString();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password)){
            return "Username or password cannot be null.";
        }

        try
        {
            verified = UserDB.VerifyUser(username, password);
        }
        catch (Exception e)
        {
            return e.Message;
        }

        if (verified){
            return "User logged in successfully";
        }
        else{
            return "Username or password is incorrect.";
        }
    }

    [Route("Remove")]
    public string Remove([FromBody] JsonObject userInfo)
    {
        if(userInfo is null){
            return "User info cannot be null.";
        }

        var username = userInfo["userInfo"]?["username"]?.ToString();
        var password = userInfo["userInfo"]?["password"]?.ToString();
        var accessLevel = userInfo["userInfo"]?["accessLevel"]?.ToString();
        int accessLevelInt;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(accessLevel) ){
            return "Username, password, or access level cannot be null.";
        }
        else{
            accessLevelInt = Int32.Parse(accessLevel);
        }

        try
        {
            UserDB.RemoveUser(username); //Update to include password and accessLevel
        }
        catch (Exception e)
        {
            return e.Message;
        }
        
        return "User was removed";
    }
}
