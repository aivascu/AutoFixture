using System.Collections;

namespace AutoFixture.NUnit4.Internal;

internal class StaticMethodSource : TestCaseSourceBase
{
    public StaticMethodSource(MethodInfo method, object[] parameters)
    {
        Method = method ?? throw new ArgumentNullException(nameof(method));
        Parameters = parameters;
    }

    public MethodInfo Method { get; }

    public object[] Parameters { get; }

    protected override IEnumerator GetEnumerator()
    {
        return GetMethodData(Method, Parameters).GetEnumerator();
    }

    private static IEnumerable GetMethodData(MethodInfo method, object[] parameters)
    {
        if (method.Invoke(null, parameters) is IEnumerable enumerable)
        {
            return enumerable;
        }

        return Enumerable.Empty<object>();
    }
}
