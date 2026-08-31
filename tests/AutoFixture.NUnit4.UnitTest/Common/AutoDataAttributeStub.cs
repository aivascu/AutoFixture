namespace AutoFixture.NUnit4.UnitTest.Common;

/// <summary>
/// A stub of <see cref="AutoDataAttribute"/> for the benefit of unit testing.
/// </summary>
public class AutoDataAttributeStub : AutoDataAttribute
{
    public AutoDataAttributeStub(Func<IFixture> fixtureFactory)
        : base(fixtureFactory)
    {
    }
}
