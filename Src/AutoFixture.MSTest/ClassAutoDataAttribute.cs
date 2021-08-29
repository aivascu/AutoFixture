using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoFixture.MSTest.DataSources;

namespace AutoFixture.MSTest
{
    public class ClassAutoDataAttribute : BaseAutoDataAttribute
    {
        private readonly ITestCaseSource source;

        public ClassAutoDataAttribute(Type type, params object[] arguments)
            : this(() => new Fixture(), type, arguments)
        {
        }

        protected ClassAutoDataAttribute(Func<IFixture> fixtureFactory, Type type, object[] arguments)
            : base(fixtureFactory)
        {
            this.Type = type ?? throw new ArgumentNullException(nameof(type));
            this.source = new AutoSource(fixtureFactory, new ClassSource(type, arguments));
        }

        /// <summary>
        /// The type of the data source.
        /// </summary>
        public Type Type { get; }

        public override IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            return this.source.GetTestCases(methodInfo).Select(x => x.ToArray());
        }
    }
}