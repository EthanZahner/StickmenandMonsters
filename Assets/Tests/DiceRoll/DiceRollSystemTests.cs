using NUnit.Framework;

public class DiceRollSystemTests
{
    [SetUp]
    public void SetUp()
    {
        IRandomNumberGenerator.Register(new MockFixedRNG(7));
    }

    [TearDown]
    public void TearDown()
    {
        IRandomNumberGenerator.Reset();
    }

    [Test]
    public void Roll_Passes()
    {
        var sut = new DiceRollSystem();
        var diceRoll = new DiceRoll(2, 10, 4);
        Assert.AreEqual(18, sut.Roll(diceRoll));
    }
}