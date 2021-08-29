using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoFixture.MSTest.DataSources;

namespace AutoFixture.MSTest
{
    [AttributeUsage(AttributeTargets.Method)]
    public class AutoDataAttribute : BaseAutoDataAttribute
    {
        private readonly ITestCaseSource source;

        public AutoDataAttribute()
            : this(() => new Fixture())
        {
        }

        protected AutoDataAttribute(Func<IFixture> fixtureFactory)
            : base(fixtureFactory)
        {
            this.source = new AutoSource(fixtureFactory, new EmptySource());
        }

        /// <inheritdoc />
        public override IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            return this.source.GetTestCases(methodInfo).Select(x => x.ToArray());
        }
    }
}
