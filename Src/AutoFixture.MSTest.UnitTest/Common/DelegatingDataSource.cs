using System.Collections;
using System.Collections.Generic;

namespace AutoFixture.MSTest.UnitTest.TestClasses
{
    public class DelegatingDataSource : IEnumerable
    {
        private readonly IEnumerable data;

        public DelegatingDataSource(IEnumerable<object[]> data)
        {
            this.data = data;
        }

        public IEnumerator GetEnumerator()
        {
            return this.data.GetEnumerator();
        }
    }
}
