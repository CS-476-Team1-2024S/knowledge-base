namespace KnowledgeBase.Tests;

[TestClass]
public class DirectoryTests
{
    Directory root;
    Directory insideRoot;
    Directory rootInsideRoot;

    [TestInitialize]
    public void Startup()
    {
        System.IO.Directory.CreateDirectory("root");
        System.IO.Directory.CreateDirectory("root/rootAgain");
        System.IO.Directory.CreateDirectory("root/rootAgain/rootInsideRoot");
        root = new Directory("root");
        insideRoot = new Directory("root/rootAgain");
        rootInsideRoot = new Directory("root/rootAgain/rootInsideRoot");
    }

    [TestMethod]
    public void Directory_Create_ValidInput_ReturnsPath()
    {
        var result = Directory.Create(root.Info.FullName + @"/" + "testDirectory");
        Assert.AreEqual(new DirectoryInfo(root.Info.FullName + @"/" + "testDirectory").ToString(), result.Info.ToString());
    }

    [TestMethod]
    public void Directory_Move_ValidInput_ReturnsTrue()
    {
        rootInsideRoot.Move(root);
        Assert.IsTrue(System.IO.Directory.Exists(root.Info.FullName + @"/" + "rootInsideRoot"));
    }

    [TestMethod]
    public void Directory_Delete_ValidInput_ReturnsFalse()
    {
        insideRoot.Delete();
        Assert.IsFalse(System.IO.Directory.Exists(root.Info.FullName + @"/" + "rootAgain"));
    }

    [TestMethod]
    public void Directory_Scan_ValidInput_ReturnsPathTrue()
    {
        Assert.IsTrue(root.ToString().Contains("rootInsideRoot"));
    }

    [TestCleanup]
    public void Cleanup()
    {
        root.Delete();
    }
}