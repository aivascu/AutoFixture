using System.Collections.Generic;
using System.Reflection;

namespace AutoFixture.MSTest.DataSources
{
    public class EmptySource : ITestCaseSource
    {
        public IEnumerable<IEnumerable<object>> GetTestCases(MethodInfo method)
        {
            yield break;
        }
    }
}
