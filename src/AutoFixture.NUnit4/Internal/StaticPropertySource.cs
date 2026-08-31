using System.Collections;

namespace AutoFixture.NUnit4.Internal;

internal class StaticPropertySource : TestCaseSourceBase
{
    public StaticPropertySource(PropertyInfo property)
    {
        Property = property ?? throw new ArgumentNullException(nameof(property));
    }

    public PropertyInfo Property { get; }

    protected override IEnumerator GetEnumerator()
    {
        return GetPropertyData(Property).GetEnumerator();
    }

    private static IEnumerable GetPropertyData(PropertyInfo property)
    {
        return property.GetValue(null) is IEnumerable enumerable
            ? enumerable
            : Enumerable.Empty<object>();
    }
}
