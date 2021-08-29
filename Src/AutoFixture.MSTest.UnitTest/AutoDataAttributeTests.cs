using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture.Kernel;
using AutoFixture.MSTest.UnitTest.Common;
using AutoFixture.MSTest.UnitTest.TestClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoFixture.MSTest.UnitTest
{
    [TestClass]
    public class AutoDataAttributeTests
    {
        [TestMethod]
        public void IsAttribute()
        {
            var sut = new AutoDataAttribute();

            Assert.IsInstanceOfType(sut, typeof(Attribute));
        }

        [TestMethod]
        public void IsDataSource()
        {
            var sut = new AutoDataAttribute();

            Assert.IsInstanceOfType(sut, typeof(ITestDataSource));
        }

        [TestMethod]
        public void CanBeAppliedToMethodsOnly()
        {
            var usage = (AttributeUsageAttribute)Attribute.GetCustomAttribute(
                typeof(AutoDataAttribute),
                typeof(AttributeUsageAttribute));

            Assert.AreEqual(AttributeTargets.Method, usage?.ValidOn);
        }

        [TestMethod]
        public void DoesNotAllowMultipleInstances()
        {
            var usage = (AttributeUsageAttribute)Attribute.GetCustomAttribute(
                typeof(AutoDataAttribute),
                typeof(AttributeUsageAttribute));

            Assert.IsFalse(usage?.AllowMultiple);
        }

        [TestMethod]
        public void InitializesFixtureFactory()
        {
            Func<IFixture> factory = () => new DelegatingFixture();
            var sut = new DerivedAutoDataAttribute(factory);

            Assert.AreEqual(factory, sut.FixtureFactory);
        }

        [TestMethod]
        public void ReturnsExpectedDisplayName()
        {
            var sut = new AutoDataAttribute();
            var methodName = nameof(SampleTestClass.TestWithPrimitiveTypes);
            var method = typeof(SampleTestClass)
                .GetMethod(methodName);
            var data = new object[] { "hello world", 42, 100_000m };
            var expected = $"{methodName} (hello world,42,100000)";

            var actual = sut.GetDisplayName(method, data);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DisplayNamePropertyOverridesDisplayName()
        {
            var sut = new AutoDataAttribute { DisplayName = "Custom test name" };
            var method = typeof(SampleTestClass)
                .GetMethod(nameof(SampleTestClass.TestWithPrimitiveTypes));
            var data = new object[] { "hello world", 42, 100_000m };

            var actual = sut.GetDisplayName(method, data);

            Assert.AreEqual("Custom test name", actual);
        }

        [TestMethod]
        public void ReturnsSingleTestCase()
        {
            var sut = new AutoDataAttribute();
            var method = typeof(SampleTestClass)
                .GetMethod(nameof(SampleTestClass.TestWithPrimitiveTypes));

            var data = sut.GetData(method).ToList();

            Assert.AreEqual(1, data.Count);
        }

        [TestMethod]
        public void CanCreateInstances()
        {
            var sut = new AutoDataAttribute();
            var method = typeof(SampleTestClass)
                .GetMethod(nameof(SampleTestClass.TestWithPrimitiveTypes));

            var data = sut.GetData(method).First();

            Assert.AreEqual(3, data.Length);
            CollectionAssert.AllItemsAreNotNull(data);
        }

        [TestMethod]
        public void CreatesSimpleData()
        {
            var builder = new CompositeSpecimenBuilder(
                new FixedParameterBuilder<string>("a", "value"),
                new FixedParameterBuilder<int>("b", 7),
                new FixedParameterBuilder<decimal>("c", 123.99m));
            var sut = new DerivedAutoDataAttribute(
                () => new DelegatingFixture { OnCreate = (r, c) => builder.Create(r, c) });
            var method = typeof(SampleTestClass)
                .GetMethod(nameof(SampleTestClass.TestWithPrimitiveTypes));
            object[] expected = { "value", 7, 123.99m };

            var actual = sut.GetData(method).Single();

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void CreatesCustomData()
        {
            var builder = new CompositeSpecimenBuilder(
                new FixedParameterBuilder<PropertyHolder<string>>("a",
                    new PropertyHolder<string> { Value = "ddd" }),
                new FixedParameterBuilder<PropertyHolder<int>>("b",
                    new PropertyHolder<int> { Value = 201 }));
            var sut = new DerivedAutoDataAttribute(
                () => new DelegatingFixture { OnCreate = (r, c) => builder.Create(r, c) });
            var method = typeof(SampleTestClass)
                .GetMethod(nameof(SampleTestClass.TestWithCustomTypes));
            object[] expected =
            {
                new PropertyHolder<string> { Value = "ddd" }, new PropertyHolder<int> { Value = 201 }
            };

            var actual = sut.GetData(method).Single();

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void FreezesValues()
        {
            var builder = new CompositeSpecimenBuilder(
                    new FixedParameterBuilder<float>("a", 3.5f),
                    new FixedParameterBuilder<string>("b", "value"),
                    new FixedParameterBuilder<int>("c", 7))
                .ToCustomization();
            var sut = new DerivedAutoDataAttribute(
                () => new Fixture().Customize(builder));
            var method = typeof(SampleTestClass)
                .GetMethod(nameof(SampleTestClass.TestWithMixedFrozenArgs));
            object[] expected = { 3.5f, "value", 7, "value", 7 };

            var actual = sut.GetData(method).Single();

            CollectionAssert.AreEquivalent(expected, actual);
        }

        [TestMethod]
        public void MatchesFrozenValuesByInterface()
        {
            var builder = new CompositeSpecimenBuilder(
                    (ISpecimenBuilder)new FixedParameterBuilder<ConcretePropertyHolder<int>>(
                        "a", new ConcretePropertyHolder<int> { Value = 3 }))
                .ToCustomization();
            var sut = new DerivedAutoDataAttribute(
                () => new Fixture().Customize(builder));
            var method = typeof(SampleTestClass)
                .GetMethod(nameof(SampleTestClass.TestWithFrozenInterfaceInstance));

            var actual = sut.GetData(method).Single();

            Assert.AreSame(actual[0], actual[1]);
        }

        public void CreatesGenericCollections()
        {
            var builder = new CompositeSpecimenBuilder(
                new FixedParameterBuilder<IEnumerable<int>>("a", new[] { 1, 20 }),
                new FixedParameterBuilder<string[]>("b", new[] { "a", "d", "c" }),
                new FixedParameterBuilder<List<decimal>>("c", new List<decimal> { 201.30m, 29.2m }));
            var sut = new DerivedAutoDataAttribute(
                () => new Fixture().Customize(builder.ToCustomization()));
        }
    }
}
