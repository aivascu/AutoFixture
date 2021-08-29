using System.Collections.Generic;

namespace AutoFixture.MSTest.UnitTest.TestClasses
{
    public class SampleTestClass
    {
        public void TestWithPrimitiveTypes(string a, int b, decimal c)
        {
        }

        public void TestWithCustomTypes(PropertyHolder<string> a, PropertyHolder<int> b)
        {
        }

        public void TestWithMixedFrozenArgs(float a, [Frozen] string b, [Frozen] int c, string d, int e)
        {
        }

        public void TestWithFrozenInterfaceInstance(
            [Frozen(Matching.ImplementedInterfaces)] ConcretePropertyHolder<int> a,
            IPropertyHolder<int> b)
        {
        }

        public void TestWithGenericCollectionArgs(IEnumerable<int> a, string[] b, List<decimal> c)
        {
        }
    }
}
