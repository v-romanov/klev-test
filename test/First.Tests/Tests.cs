namespace First.Tests;

[TestClass]
public sealed class FirstTest
{
    [TestMethod]
    [DataRow("a3b3", "aaabbb")]
    [DataRow("a", "a")]
    [DataRow("b2", "bb")]
    [DataRow("b3d", "bbbd")]
    [DataRow("pogchamp", "pogchamp")]
    [DataRow("f10", "ffffffffff")]
    [DataRow("f2d4c", "ffddddc")]
    public void TestMethod1(string expected, string input)
    {
        Assert.AreEqual(expected, LatinCompress.Comporess(input));
        Assert.AreEqual(input, LatinCompress.Decompress(expected));
    }
}