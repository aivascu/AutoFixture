namespace AutoFixture.NUnit4.Internal;

internal class AutoTestCaseBuilder
{
    private readonly NUnitTestCaseBuilder _builder = new();

    public TestMethod BuildTestMethod(IMethodInfo method, Test test, AutoTestCaseParameters parameters)
    {
        return _builder.BuildTestMethod(method, test, parameters.GetParameters(method));
    }
}
