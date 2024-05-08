// using System.Text;
// using System.Text.Json.Nodes;

// namespace KnowledgeBase.Tests;

// [TestClass]
// public class UserDBTests
// {
//     private string readFile(FileInfo Info)
//     {
//         using FileStream fs = new(Info.FullName, FileMode.Open);
//         byte[] bytes = new byte[fs.Length];
//         fs.Read(bytes);
//         return Encoding.UTF8.GetString(bytes);
//     }

//     [TestInitialize]
//     public void Initialize()
//     {
//         UserDB.LoadFromFile();
//     }

//     [TestMethod]
//     public void UserDB_AddUser_ValidInput_ReturnsUsername()
//     {
//         UserDB.AddUser("testUser", "testUser", 1);
//         UserDB.SaveToFile();
//         var file = JsonObject.Parse(readFile(new("Users.db")));
//         var username = file?["testUser"]?["Username"]?.ToString();
//         Assert.AreEqual("testUser", username);
//     }

//     [TestMethod]
//     public void UserDB_GetUser_ValidInput_ReturnsUserInfo()
//     {
//         Assert.AreEqual("testUser,testUser,1", UserDB.GetUser("testUser")?.ToString());
//     }

//     [TestMethod]
//     public void UserDB_VerifyUser_ValidInput_ReturnsTrue()
//     {
//         Assert.IsTrue(UserDB.VerifyUser("testUser", "testUser"));
//     }

//     [TestMethod]
//     public void UserDB_RemoveUser_ValidInput_ReturnsNull()
//     {
//         UserDB.RemoveUser("testUser");
//         UserDB.SaveToFile();
//         var file = JsonObject.Parse(readFile(new("Users.db")));
//         var username = file?["testUser"]?["Username"]?.ToString();
//         Assert.IsNull(username);
//     }


//     [TestCleanup]
//     public void Cleanup()
//     {
//         System.IO.File.Delete("Users.db");
//     }
// }