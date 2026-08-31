using System.Collections;

namespace AutoFixture.NUnit4.Internal;

/// <summary>
/// Base class for test case sources.
/// </summary>
internal abstract class TestCaseSourceBase : ITestCaseSource
{
    /// <summary>Gets the test cases.</summary>
    /// <returns>An enumerator of test cases.</returns>
    protected abstract IEnumerator GetEnumerator();

    /// <inheritdoc />
    public IEnumerable<IReadOnlyList<object>> GetTestCases(IMethodInfo method)
    {
        if (method is null) throw new ArgumentNullException(nameof(method));

        var parameters = method.GetParameters();
        if (parameters.Length == 0)
        {
            // If the method has no parameters, a single test run is enough.
            yield return Array.Empty<object>();
            yield break;
        }

        var enumerator = GetEnumerator()
            ?? throw new InvalidOperationException($"No data could be found for {method.Name}.");

        while (enumerator.MoveNext())
        {
            var value = enumerator.Current;

            if (parameters[0].ParameterType.IsInstanceOfType(value))
            {
                yield return new[] { value };
            }
            else if (value is IEnumerable values)
            {
                yield return values.OfType<object>().ToList();
            }
            else
            {
                yield return Array.Empty<object>();
            }
        }
    }
}
