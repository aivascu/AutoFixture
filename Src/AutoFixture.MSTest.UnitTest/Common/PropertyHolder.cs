namespace AutoFixture.MSTest.UnitTest.TestClasses
{
    public record PropertyHolder<T>
    {
        public T Value { get; set; }
    }
}
