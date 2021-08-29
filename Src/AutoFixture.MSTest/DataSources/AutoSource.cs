using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutoFixture.MSTest.DataSources
{
    public class AutoSource : ITestCaseSource
    {
        public AutoSource(Func<IFixture> createFixture, ITestCaseSource source)
        {
            this.CreateFixture = createFixture ?? throw new ArgumentNullException(nameof(createFixture));
            this.Source = source ?? throw new ArgumentNullException(nameof(source));
        }

        public Func<IFixture> CreateFixture { get; }

        public ITestCaseSource Source { get; }

        public IEnumerable<IEnumerable<object>> GetTestCases(MethodInfo method)
        {
            var parameters = method.GetParameters();
            if (parameters.Length == 0) yield break;

            IFixture fixture;
            using var enumerator = this.Source.GetTestCases(method).GetEnumerator();

            if (!enumerator.MoveNext())
            {
                fixture = this.CreateFixture();
                GetParameterCustomization(parameters).Customize(fixture);
                yield return parameters.Select(fixture.Resolve);
                yield break;
            }

            do
            {
                fixture = this.CreateFixture();
                var values = enumerator?.Current?.ToList() ?? new List<object>();
                var missingParameters = parameters.Skip(values.Count).ToList();

                GetParameterCustomization(missingParameters).Customize(fixture);

                var missingValues = missingParameters.Select(fixture.Resolve);

                yield return values.Concat(missingValues);
            } while (enumerator.MoveNext());
        }

        private static ICustomization GetParameterCustomization(IEnumerable<ParameterInfo> parameters)
            => parameters
                .SelectMany(x => x.GetCustomizations())
                .Aggregate(new CustomizationBuilder(), (b, x) => b.Append(x))
                .ToCustomization();
    }
}
