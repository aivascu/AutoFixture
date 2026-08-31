using System.Collections;

namespace AutoFixture.NUnit4.Internal;

internal class StaticFieldSource : TestCaseSourceBase
{
    public StaticFieldSource(FieldInfo field)
    {
        Field = field ?? throw new ArgumentNullException(nameof(field));
    }

    public FieldInfo Field { get; }

    protected override IEnumerator GetEnumerator()
    {
        return GetFieldData(Field).GetEnumerator();
    }

    private static IEnumerable GetFieldData(FieldInfo field)
    {
        return field.GetValue(null) is IEnumerable enumerable
            ? enumerable
            : Enumerable.Empty<object>();
    }
}
