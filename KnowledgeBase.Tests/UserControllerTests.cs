using System.Text.Json.Nodes;
using KnowledgeBase.Controllers;

namespace KnowledgeBase.Tests;

[TestClass]
public class UserControllerTests
{
    private UserController userController = new UserController();

    [TestMethod]
    public void UserController_Add_ValidInput_ReturnsSuccessful()
    {
        var result = userController.Add(new JsonObject
        {
            ["userInfo"] = new JsonObject
            {
                ["username"] = "testUser",
                ["password"] = "testUser",
                ["accessLevel"] = "1"
            }
        });
        Assert.AreEqual("User added successfully", result);
    }

    [TestMethod]
    [DataRow(null, null, null)]
    [DataRow("", "", "")]
    public void UserController_Add_NullInputs_ReturnsInvalid(string username, string password, string accessLevel)
    {
        var result = userController.Add(new JsonObject
        {
            ["userInfo"] = new JsonObject
            {
                ["username"] = username,
                ["password"] = password,
                ["accessLevel"] = accessLevel
            }
        });
        Assert.AreEqual("Username, password, or access level cannot be null.", result);
    }

    [TestMethod]
    public void UserController_Login_ValidInput_ReturnsSuccessful()
    {
        var result = userController.Login(new JsonObject
        {
            ["userInfo"] = new JsonObject
            {
                ["username"] = "testUser",
                ["password"] = "testUser"
            }
        });
        Assert.AreEqual("User logged in successfully", result);
    }

    [TestMethod]
    public void UserController_Login_ValidInput_ReturnsIncorrect()
    {
        var result = userController.Login(new JsonObject
        {
            ["userInfo"] = new JsonObject
            {
                ["username"] = "realUser",
                ["password"] = "realUser"
            }
        });
        Assert.AreEqual("Username or password is incorrect.", result);
    }

    [TestMethod]
    [DataRow(null, null)]
    [DataRow("", "")]
    public void UserController_Login_NullInputs_ReturnsIncorrect(string username, string password)
    {
        var result = userController.Login(new JsonObject
        {
            ["userInfo"] = new JsonObject
            {
                ["username"] = username,
                ["password"] = password
            }
        });
        Assert.AreEqual("Username or password cannot be null.", result);
    }

    [TestMethod]
    public void UserController_Remove_ValidInput_ReturnsSuccessful()
    {
        var result = userController.Remove(new JsonObject
        {
            ["userInfo"] = new JsonObject
            {
                ["username"] = "testUser",
                ["password"] = "testUser",
                ["accessLevel"] = "1"
            }
        });
        Assert.AreEqual("User was removed", result);
    }

    [TestMethod]
    [DataRow(null, null, null)]
    [DataRow("", "", "")]
    public void UserController_Remove_NullInputs_ReturnsSuccessful(string username, string password, string accessLevel)
    {
        var result = userController.Remove(new JsonObject
        {
            ["userInfo"] = new JsonObject
            {
                ["username"] = username,
                ["password"] = password,
                ["accessLevel"] = accessLevel
            }
        });
        Assert.AreEqual("Username, password, or access level cannot be null.", result);
    }

    [TestCleanup]
    public void Cleanup()
    {
        //Remove Users.db in build folder after tests
        System.IO.File.Delete("Users.db");
    }
}