using System.Collections;

namespace AutoFixture.NUnit4.Internal;

internal class NullSource : ITestCaseSource, IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        return Enumerable.Empty<object>().GetEnumerator();
    }

    public IEnumerable<IReadOnlyList<object>> GetTestCases(IMethodInfo methodInfo)
    {
        return Enumerable.Empty<object[]>();
    }
}
