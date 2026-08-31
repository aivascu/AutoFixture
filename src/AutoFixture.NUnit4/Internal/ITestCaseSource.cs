namespace AutoFixture.NUnit4.Internal;

/// <summary>
/// Represents a source of test cases.
/// </summary>
internal interface ITestCaseSource
{
    /// <summary>
    /// Gets the test cases.
    /// </summary>
    /// <param name="method">The method to get test cases for.</param>
    /// <returns>An enumerator of test cases.</returns>
    IEnumerable<IReadOnlyList<object>> GetTestCases(IMethodInfo method);
}
