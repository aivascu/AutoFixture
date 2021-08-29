using System.Linq;
using AutoFixture.Kernel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoFixture.MSTest.UnitTest
{
    [TestClass]
    public class Scenario
    {
        [TestMethod, InlineAutoData(5)]
        public void TestMethod1(int v, int a)
        {
            Assert.AreEqual(5, v);
            Assert.IsTrue(a > 0);
        }

        [TestMethod, AutoData]
        public void TestMethod2(int v, int a)
        {
            Assert.IsTrue(v > 0);
            Assert.IsTrue(a > 0);
        }

        [TestMethod, AutoData]
        public void CanFreezeValues([Frozen]int v, int a)
        {
            Assert.AreEqual(v, a);
        }
    }
}