using System;
using System.Collections;
using System.Linq;
using System.Reflection;

namespace AutoFixture.MSTest.DataSources
{
    public class PropertySource : SourceBase
    {
        private readonly PropertyInfo propertyInfo;

        public PropertySource(PropertyInfo propertyInfo)
        {
            this.propertyInfo = propertyInfo ?? throw new ArgumentNullException(nameof(propertyInfo));
        }

        public override IEnumerator GetEnumerator() =>
            this.propertyInfo.GetValue(null) is IEnumerable enumerable
                ? enumerable.GetEnumerator()
                : throw new InvalidOperationException($"Member return type should be {typeof(IEnumerable)}");
    }
}
