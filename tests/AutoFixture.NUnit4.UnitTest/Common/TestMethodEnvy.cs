namespace AutoFixture.NUnit4.UnitTest.Common;

internal static class TestMethodEnvy
{
    public static object[] GetArguments(this TestMethod source)
    {
        var value = source.GetType().GetProperty("Arguments",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
            ?.GetValue(source);

        return value as object[];
    }
}
