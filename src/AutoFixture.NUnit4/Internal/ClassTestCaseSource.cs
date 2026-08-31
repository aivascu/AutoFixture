using System.Collections;

namespace AutoFixture.NUnit4.Internal;

internal class ClassTestCaseSource : TestCaseSourceBase
{
    public ClassTestCaseSource(Type type)
    {
        Type = type ?? throw new ArgumentNullException(nameof(type));
        TestCases = GetInstance(type);
    }

    public Type Type { get; }

    private IEnumerable TestCases { get; }

    protected override IEnumerator GetEnumerator()
    {
        return TestCases.GetEnumerator();
    }

    private static IEnumerable GetInstance(Type type)
    {
        var constructor = type.GetConstructor(Type.EmptyTypes);
        var instance = constructor?.Invoke(null);
        return instance is IEnumerable enumerable
            ? enumerable
            : Enumerable.Empty<object>();
    }
}
