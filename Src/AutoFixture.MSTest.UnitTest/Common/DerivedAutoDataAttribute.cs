using System;

namespace AutoFixture.MSTest.UnitTest.Common
{
    internal class DerivedAutoDataAttribute : AutoDataAttribute
    {
        public DerivedAutoDataAttribute(Func<IFixture> factory)
            : base(factory)
        {
        }
    }
}
