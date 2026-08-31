namespace AutoFixture.NUnit4.Internal;

internal class ReflectedSource : ITestCaseSource
{
    private readonly ITestCaseSource _source;

    public ReflectedSource(Type type, string memberName, object[] parameters)
    {
        Type = type;
        MemberName = memberName;
        Parameters = parameters;
        _source = GetSource(Type, MemberName, Parameters);
    }

    public Type Type { get; }

    public string MemberName { get; }

    public object[] Parameters { get; }

    public IEnumerable<IReadOnlyList<object>> GetTestCases(IMethodInfo method)
    {
        return _source.GetTestCases(method);
    }

    private static ITestCaseSource GetSource(Type type, string name, object[] parameters)
    {
        if (type is not null && name is not null)
            return new StaticMemberSource(type, name, parameters);

        if (type is not null)
            return new ClassTestCaseSource(type);

        return new NullSource();
    }
}
