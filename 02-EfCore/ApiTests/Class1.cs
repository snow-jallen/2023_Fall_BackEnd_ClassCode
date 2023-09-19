using NUnit.Framework;

namespace ApiTests;

[TestFixture]
public class Class1
{
    [Test]
    public void Pass()
    {
        Assert.Fail("What?!");
    }

    [Test]
    public void Pass2() { Assert.Pass("Hooray!"); }
}