namespace AutoFixture.NUnit4.Internal;

internal class StaticMemberSource : ITestCaseSource
{
    private const BindingFlags Binding = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

    private readonly ITestCaseSource _source;

    public StaticMemberSource(Type type, string name, object[] parameters)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Parameters = parameters;
        _source = GetSource(Type, Name, Parameters);
    }

    public Type Type { get; }

    public string Name { get; }

    public object[] Parameters { get; }

    public IEnumerable<IReadOnlyList<object>> GetTestCases(IMethodInfo method)
    {
        return _source.GetTestCases(method);
    }

    private static ITestCaseSource GetSource(Type type, string name, object[] parameters)
    {
        return type.GetMember(name, Binding).Single() switch
        {
            MethodInfo methodSource => new StaticMethodSource(methodSource, parameters),
            PropertyInfo propertySource => new StaticPropertySource(propertySource),
            FieldInfo fieldSource => new StaticFieldSource(fieldSource),
            _ => new NullSource()
        };
    }
}
