using AutoFixture.Kernel;

namespace AutoFixture.MSTest
{
    internal static class FixtureExtensions
    {
        public static object Resolve(this IFixture source, object request)
        {
            return new SpecimenContext(source).Resolve(request);
        }
    }
}