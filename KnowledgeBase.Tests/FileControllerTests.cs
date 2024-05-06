using System.Text.Json.Nodes;
using KnowledgeBase.Controllers;

namespace KnowledgeBase.Tests;

[TestClass]
public class FileControllerTests
{
    private DirectoryInfo root;
    private FileController fileController;

    [TestInitialize]
    public void Startup()
    {
        root = System.IO.Directory.CreateDirectory("root");
        fileController = new FileController();
    }

    [TestMethod]
    public void FileController_Create_ValidInput_ReturnsSuccessful()
    {
        var result = fileController.Create(new JsonObject
        {
            ["fileInfo"] = new JsonObject
            {
                ["path"] = "testFile.txt"
            }
        });
        Assert.AreEqual("File created successfully.", result);
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    public void FileController_Create_NullInput_ReturnsInvalid(string path)
    {
        var result = fileController.Create(new JsonObject
        {
            ["fileInfo"] = new JsonObject
            {
                ["path"] = path
            }
        });
        Assert.AreEqual("Path cannot be null.", result);
    }

    [TestMethod]
    public void FileController_Move_ValidInput_ReturnsSuccessful()
    {
        System.IO.Directory.CreateDirectory("root/test");
        var result = fileController.Move(new JsonObject
        {
            ["fileInfo"] = new JsonObject
            {
                ["source"] = "testFile.txt",
                ["destination"] = "test"
            }
        });
        Assert.AreEqual("File moved successfully.", result);
    }

    [TestMethod]
    [DataRow(null, null)]
    [DataRow("", "")]
    public void FileController_Move_NullInputs_ReturnsInvalid(string src, string dest)
    {
        var result = fileController.Move(new JsonObject
        {
            ["fileInfo"] = new JsonObject
            {
                ["source"] = src,
                ["destination"] = dest
            }
        });
        Assert.AreEqual("Source and destination paths cannot be null.", result);
    }

    [TestMethod]
    public void FileController_Delete_ValidInput_ReturnsSuccessful()
    {
        var result = fileController.Delete(new JsonObject
        {
            ["fileInfo"] = new JsonObject
            {
                ["path"] = "test/testFile.txt"
            }
        });
        Assert.AreEqual("File deleted successfully.", result);
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    public void FileController_Delete_NullInput_ReturnsInvalid(string path)
    {
        var result = fileController.Delete(new JsonObject
        {
            ["fileInfo"] = new JsonObject
            {
                ["path"] = path
            }
        });
        Assert.AreEqual("Path cannot be null.", result);
    }

    [TestMethod]
    public void FileController_Write_ValidInput_ReturnsSuccessful()
    {
        System.IO.File.Create("root/testFile.txt").Close();
        var result = fileController.Write(new JsonObject
        {
            ["fileInfo"] = new JsonObject
            {
                ["path"] = "testFile.txt",
                ["content"] = "Hello, World!",
                ["append"] = "false"
            }
        });
        Assert.AreEqual("File written successfully.", result);
    }

    [TestMethod]
    [DataRow(null, null, null)]
    [DataRow("", "", "")]
    public void FileController_Write_NullInputs_ReturnsInvalid(string path, string content, string append)
    {
        var result = fileController.Write(new JsonObject
        {
            ["fileInfo"] = new JsonObject
            {
                ["path"] = path,
                ["content"] = content,
                ["append"] = append
            }
        });
        Assert.AreEqual("Path and content cannot be null.", result);
    }

    [TestMethod]
    public void FileController_Read_ValidInput_ReturnsSuccessful()
    {
        var result = fileController.Read(new JsonObject
        {
            ["fileInfo"] = new JsonObject
            {
                ["path"] = "testFile.txt"
            }
        });
        Assert.AreEqual("Hello, World!", result);
    }

    [TestMethod]
    [DataRow(null)]
    [DataRow("")]
    public void FileController_Read_NullInput_ReturnsInvalid(string path)
    {
        var result = fileController.Read(new JsonObject
        {
            ["fileInfo"] = new JsonObject
            {
                ["path"] = path
            }
        });
        Assert.AreEqual("Path cannot be null.", result);
        Cleanup();
    }

    public void Cleanup()
    {
        root.Delete(true);
    }
}