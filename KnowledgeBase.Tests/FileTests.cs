namespace KnowledgeBase.Tests;

[TestClass]
public class FileTests
{
    Directory root;
    Directory insideRoot;
    File testFile;

    [TestInitialize]
    public void Setup()
    {
        System.IO.Directory.CreateDirectory("root");
        System.IO.Directory.CreateDirectory("root/insideRoot");
        root = new Directory("root");
        insideRoot = new Directory("root/insideRoot");
        testFile = File.Create("root/insideRoot/test.txt");
    }

    [TestMethod]
    public void File_Create_ValidInput_ReturnsPath()
    {
        var result = File.Create(root.Info.FullName + @"/" + "testOne.txt");
        Assert.AreEqual(new FileInfo(root.Info.FullName + @"/" + "testOne.txt").ToString(), result.Info.ToString());
    }

    [TestMethod]
    public void File_Move_ValidInput_ReturnsTrue()
    {
        testFile.Move(insideRoot);
        Assert.IsTrue(System.IO.File.Exists(insideRoot.Info.FullName + @"/" + "test.txt"));
    }

    [TestMethod]
    public void File_Delete_ValidInput_ReturnsFalse()
    {
        testFile.Delete();
        Assert.IsFalse(System.IO.File.Exists(insideRoot.Info.FullName + @"/" + "test.txt"));
    }

    [TestMethod]
    public void File_WriteRead_ValidInput_ReturnsWritten()
    {
        testFile.Write("Hello, World!");
        Assert.AreEqual("Hello, World!", testFile.Read());
    }

    [TestCleanup]
    public void Cleanup()
    {
        root.Delete();
    }
}