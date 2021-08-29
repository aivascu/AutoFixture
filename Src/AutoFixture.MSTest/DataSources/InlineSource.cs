using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AutoFixture.MSTest.DataSources
{
    public class InlineSource : SourceBase
    {
        public InlineSource(IEnumerable<object> arguments)
        {
            this.Arguments = arguments ?? Enumerable.Empty<object>();
        }

        public IEnumerable<object> Arguments { get; }

        public override IEnumerator GetEnumerator()
        {
            return this.Arguments.GetEnumerator();
        }
    }
}
