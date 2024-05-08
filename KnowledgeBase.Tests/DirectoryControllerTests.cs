// using System.Text.Json.Nodes;
// using KnowledgeBase.Controllers;

// namespace KnowledgeBase.Tests;

// [TestClass]
// public class DirectoryControllerTests
// {
//     private DirectoryInfo root;
//     private DirectoryInfo testDirectoryTwo;
//     private DirectoryController directoryController;

//     [TestInitialize]
//     public void Startup()
//     {
//          root = System.IO.Directory.CreateDirectory("root");
//          testDirectoryTwo = System.IO.Directory.CreateDirectory("root/testDirectoryTwo");
//          directoryController = new DirectoryController();
//     }

//     [TestMethod]
//     public void DirectoryController_Create_ValidInput_ReturnsSuccessful()
//     {
//         var result = directoryController.Create(new JsonObject
//         {
//             ["directoryInfo"] = new JsonObject
//             {
//                 ["path"] = "testDirectory"
//             }
//         });
//         Assert.AreEqual("Directory created successfully.", result);
//     }

//     [TestMethod]
//     [DataRow(null)]
//     [DataRow("")]
//     public void DirectoryController_Create_NullInput_ReturnsInvalid(string path)
//     {
//         var result = directoryController.Create(new JsonObject
//         {
//             ["directoryInfo"] = new JsonObject
//             {
//                 ["path"] = path
//             }
//         });
//         Assert.AreEqual("Path cannot be null.", result);
//     }

//     [TestMethod]
//     public void DirectoryController_Move_ValidInput_ReturnsSuccessful()
//     {
//         System.IO.Directory.CreateDirectory("root/testDirectory");
//         var result = directoryController.Move(new JsonObject
//         {
//             ["directoryInfo"] = new JsonObject
//             {
//                 ["source"] = "testDirectoryTwo",
//                 ["destination"] = "testDirectory"
//             }
//         });
//         Assert.AreEqual("Directory moved successfully.", result);
//     }

//     [TestMethod]
//     [DataRow(null, null)]
//     [DataRow("", "")]
//     public void DirectoryController_Move_NullInputs_ReturnsInvalid(string src, string dest)
//     {
//         var result = directoryController.Move(new JsonObject
//         {
//             ["directoryInfo"] = new JsonObject
//             {
//                 ["source"] = src,
//                 ["destination"] = dest
//             }
//         });
//         Assert.AreEqual("Source and destination paths cannot be null.", result);
//     }

//     [TestMethod]
//     public void DirectoryController_Delete_ValidInput_ReturnsSuccessful()
//     {
//         var result = directoryController.Delete(new JsonObject
//         {
//             ["directoryInfo"] = new JsonObject
//             {
//                 ["path"] = "testDirectoryTwo"
//             }
//         });
//         Assert.AreEqual("Directory deleted successfully.", result);
//     }

//     [TestMethod]
//     [DataRow(null)]
//     [DataRow("")]
//     public void DirectoryController_Delete_NullInput_ReturnsInvalid(string path)
//     {
//         var result = directoryController.Delete(new JsonObject
//         {
//             ["directoryInfo"] = new JsonObject
//             {
//                 ["path"] = path
//             }
//         });
//         Assert.AreEqual("Path cannot be null.", result);
//     }

//     [TestCleanup]
//     public void Cleanup()
//     {
//         root.Delete(true);
//     }
// }