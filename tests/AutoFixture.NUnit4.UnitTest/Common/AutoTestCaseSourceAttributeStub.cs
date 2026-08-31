namespace AutoFixture.NUnit4.UnitTest.Common;

/// <summary>
/// A stub of <see cref="AutoTestCaseSourceAttribute"/> for the benefit of unit testing.
/// </summary>
public class AutoTestCaseSourceAttributeStub : AutoTestCaseSourceAttribute
{
    public AutoTestCaseSourceAttributeStub(string sourceName, params object[] parameters)
        : base(sourceName, parameters)
    {
    }

    public AutoTestCaseSourceAttributeStub(Type sourceType)
        : base(sourceType)
    {
    }

    public AutoTestCaseSourceAttributeStub(Type sourceType, string sourceName, params object[] parameters)
        : base(sourceType, sourceName, parameters)
    {
    }

    public AutoTestCaseSourceAttributeStub(
        Func<IFixture> fixtureFactory, Type sourceType,
        string sourceName, params object[] parameters)
        : base(fixtureFactory, sourceType, sourceName, parameters)
    {
    }

    public AutoTestCaseSourceAttributeStub(Func<IFixture> fixtureFactory)
        : base(fixtureFactory, null, null, null)
    {
    }
}
