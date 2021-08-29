using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using AutoFixture.MSTest.DataSources;
using AutoFixture.MSTest.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoFixture.MSTest
{
    public abstract class BaseAutoDataAttribute : Attribute, ITestDataSource
    {
        protected BaseAutoDataAttribute(Func<IFixture> fixtureFactory)
        {
            this.FixtureFactory = fixtureFactory ?? throw new ArgumentNullException(nameof(fixtureFactory));
        }

        /// <summary>
        /// Gets the fixture factory.
        /// </summary>
        public Func<IFixture> FixtureFactory { get; protected set; }

        /// <summary>
        /// Gets data for calling test method.
        /// </summary>
        public string? DisplayName { get; set; }

        /// <inheritdoc />
        public abstract IEnumerable<object[]> GetData(MethodInfo methodInfo);

        /// <inheritdoc />
        public string? GetDisplayName(MethodInfo methodInfo, object[]? data)
        {
            if (!string.IsNullOrWhiteSpace(this.DisplayName)) return this.DisplayName;
            if (data is null) return null;
            return string.Format(
                CultureInfo.CurrentCulture, Messages.DataDrivenDisplayName,
                methodInfo.Name, string.Join(',', data.AsEnumerable()));
        }

        /// <summary>
        /// Creates an auto test case source instance.
        /// </summary>
        /// <param name="fixtureFactory">The <see cref="IFixture" /> instance factory.</param>
        /// <param name="source">The test case source.</param>
        /// <returns>A <see cref="ITestCaseSource" /> instance.</returns>
        protected virtual ITestCaseSource CreateAutoSource(Func<IFixture> fixtureFactory, ITestCaseSource source)
        {
            return new AutoSource(fixtureFactory, source);
        }
    }
}