namespace OwonBinfileReader.Tests;

[TestClass]
public sealed class SIHelperTests
{
    [TestMethod]
    public void GetNormalizedValue_Returns_Correct_Small_Results()
    {
        Assert.AreEqual(1, SIHelper.GetNormalizedValue("1V", "V"));
        Assert.AreEqual(0.2, SIHelper.GetNormalizedValue("2dm", "m"));
        Assert.AreEqual(0.03, SIHelper.GetNormalizedValue("3cm", "m"));
        Assert.AreEqual(0.004, SIHelper.GetNormalizedValue("4mV", "V"));
        Assert.AreEqual(0.000003, SIHelper.GetNormalizedValue("3uV", "V"));
        Assert.AreEqual(0.000004, SIHelper.GetNormalizedValue("4µV", "V"));
        Assert.AreEqual(0.000000005, SIHelper.GetNormalizedValue("5ns", "s"));
        Assert.AreEqual(0.000000000006, SIHelper.GetNormalizedValue("6pm", "m"));
        Assert.AreEqual(0.000000000000007, SIHelper.GetNormalizedValue("7fm", "m"));
    }

    [TestMethod]
    public void GetNormalizedValue_Returns_Correct_Big_Results()
    {
        Assert.AreEqual(1, SIHelper.GetNormalizedValue("1V", "V"));
        Assert.AreEqual(20, SIHelper.GetNormalizedValue("2dam", "m"));
        Assert.AreEqual(300, SIHelper.GetNormalizedValue("3hm", "m"));
        Assert.AreEqual(4000, SIHelper.GetNormalizedValue("4kV", "V"));
        Assert.AreEqual(4000, SIHelper.GetNormalizedValue("4KV", "V"));
        Assert.AreEqual(3000000, SIHelper.GetNormalizedValue("3MV", "V"));
        Assert.AreEqual(4000000000, SIHelper.GetNormalizedValue("4GV", "V"));
        Assert.AreEqual(5000000000000, SIHelper.GetNormalizedValue("5Ts", "s"));
        Assert.AreEqual(6000000000000000, SIHelper.GetNormalizedValue("6Pm", "m"));
    }

    [TestMethod]
    public void GetNormalizedValue_Returns_Correct_UniteLess_Results()
    {
        Assert.AreEqual(123, SIHelper.GetNormalizedValue("123X", string.Empty));
        Assert.AreEqual(1.45, SIHelper.GetNormalizedValue("145cX", string.Empty));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GetNormalizedValue_Throws_On_Invalid_Unit()
        => Assert.AreEqual(1, SIHelper.GetNormalizedValue("1xm", "m"));

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GetNormalizedValue_Throws_On_Empty_Unit()
        => Assert.AreEqual(1, SIHelper.GetNormalizedValue("1", "m"));

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void GetNormalizedValue_Throws_On_Empty_Value()
        => Assert.AreEqual(1, SIHelper.GetNormalizedValue("m", "m"));
    [TestMethod]

    [ExpectedException(typeof(ArgumentException))]
    public void GetNormalizedValue_Throws_On_Empty_Argument()
        => Assert.AreEqual(1, SIHelper.GetNormalizedValue(string.Empty, "m"));

    [ExpectedException(typeof(ArgumentException))]
    public void GetNormalizedValue_Throws_On_Null_Argument()
        => Assert.AreEqual(1, SIHelper.GetNormalizedValue(null, "m"));
}
