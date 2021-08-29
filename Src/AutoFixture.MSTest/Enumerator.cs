using System.Collections;
using System.Linq;

namespace AutoFixture.MSTest
{
    internal static class Enumerator
    {
        public static IEnumerator Empty => Enumerable.Empty<object>().GetEnumerator();
    }
}