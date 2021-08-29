using System.Collections.Generic;
using System.Reflection;

namespace AutoFixture.MSTest
{
    public interface ITestCaseSource
    {
        IEnumerable<IEnumerable<object>> GetTestCases(MethodInfo method);
    }
}