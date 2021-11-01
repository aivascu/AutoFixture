using AutoFixture.Kernel;

namespace AutoFixture.MSTest.UnitTest.Common
{
    internal class FixedParameterBuilder<T> : FilteringSpecimenBuilder
    {
        public FixedParameterBuilder(string name, T value)
            : base(new FixedBuilder(value), new ParameterSpecification(typeof(T), name))
        {
        }
    }
}
