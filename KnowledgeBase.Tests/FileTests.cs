namespace KnowledgeBase.Tests;

[TestClass]
public class FileTests
{
    Directory root = new Directory("root");
    Directory insideRoot = new Directory("root/insideRoot");
    File testFile = new File("root/insideRoot/test.txt");

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