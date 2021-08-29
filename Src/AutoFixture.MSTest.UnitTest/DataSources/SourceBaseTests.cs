using System.Linq;
using AutoFixture.MSTest.UnitTest.Common;
using AutoFixture.MSTest.UnitTest.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoFixture.MSTest.UnitTest.DataSources
{
    [TestClass]
    public class SourceBaseTests
    {
        [TestMethod]
        public void ReturnsTestCases()
        {
            var testCases = new[]
            {
                new object[] { "a", 1, 3.4m },
                new object[] { "b", 2, 4.1m }
            };
            var sut = new DerivedSourceBase
            {
                OnGetEnumerator = () => testCases.GetEnumerator()
            };

            var method = typeof(SampleTestClass)
                .GetMethod(nameof(SampleTestClass.TestWithPrimitiveTypes));

            var actual = sut.GetTestCases(method)
                .Select(x => x.ToArray()).ToArray();

            testCases.Zip(actual)
                .ForEach(y => CollectionAssert.AreEquivalent(y.First, y.Second));
        }
    }
}