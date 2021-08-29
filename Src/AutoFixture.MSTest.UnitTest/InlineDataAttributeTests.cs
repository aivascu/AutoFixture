using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoFixture.MSTest.UnitTest
{
    [TestClass]
    public class InlineDataAttributeTests
    {
        [TestMethod]
        public void IsAttribute()
        {
            var sut = new InlineAutoDataAttribute();

            Assert.IsInstanceOfType(sut, typeof(Attribute));
        }

        [TestMethod]
        public void IsDataSource()
        {
            var sut = new InlineAutoDataAttribute();

            Assert.IsInstanceOfType(sut, typeof(ITestDataSource));
        }

        [TestMethod]
        public void CanBeAppliedToMethodsOnly()
        {
            var usage = (AttributeUsageAttribute)Attribute.GetCustomAttribute(
                typeof(InlineAutoDataAttribute),
                typeof(AttributeUsageAttribute));

            Assert.AreEqual(AttributeTargets.Method, usage?.ValidOn);
        }

        [TestMethod]
        public void DoesNotAllowMultipleInstances()
        {
            var usage = (AttributeUsageAttribute)Attribute.GetCustomAttribute(
                typeof(InlineAutoDataAttribute),
                typeof(AttributeUsageAttribute));

            Assert.IsFalse(usage?.AllowMultiple);
        }
    }
}
