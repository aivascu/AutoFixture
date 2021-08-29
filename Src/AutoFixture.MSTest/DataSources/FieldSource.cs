using System;
using System.Collections;
using System.Reflection;

namespace AutoFixture.MSTest.DataSources
{
    public class FieldSource : SourceBase
    {
        private readonly FieldInfo fieldInfo;

        public FieldSource(FieldInfo fieldInfo)
        {
            this.fieldInfo = fieldInfo ?? throw new ArgumentNullException(nameof(fieldInfo));
        }

        public override IEnumerator GetEnumerator() =>
            this.fieldInfo.GetValue(null) is IEnumerable enumerable
                ? enumerable.GetEnumerator()
                : throw new InvalidOperationException($"Member return type should be {typeof(IEnumerable)}");
    }
}
