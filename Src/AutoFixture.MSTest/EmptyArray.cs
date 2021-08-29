namespace AutoFixture.MSTest
{
    internal static class EmptyArray<T>
    {
        public static T[] Value => new T[0];
    }
}