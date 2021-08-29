using System;
using System.Collections;
using System.Reflection;

namespace AutoFixture.MSTest.DataSources
{
    public class MethodSource : SourceBase
    {
        public MethodSource(MethodInfo methodInfo, object[]? parameters)
        {
            this.MethodInfo = methodInfo ?? throw new ArgumentNullException(nameof(methodInfo));
            this.Parameters = parameters;
        }

        public MethodInfo MethodInfo { get; }
        public object[]? Parameters { get; }

        public override IEnumerator GetEnumerator()
        {
            var result = this.MethodInfo.Invoke(null, this.Parameters);

            if (result is not IEnumerable enumerable)
                throw new InvalidOperationException($"Member return type should be {typeof(IEnumerable)}");

            return enumerable.GetEnumerator();

        }
    }
}