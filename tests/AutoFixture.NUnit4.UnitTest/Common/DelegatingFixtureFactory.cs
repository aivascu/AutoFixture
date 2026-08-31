namespace AutoFixture.NUnit4.UnitTest.Common;

internal class DelegatingFixtureFactory
{
    public DelegatingFixtureFactory()
    {
    }

    public DelegatingFixtureFactory(Func<IFixture> factory)
    {
        OnFixtureCreated = factory;
    }

    public Func<IFixture> OnFixtureCreated { get; set; }

    public bool Invoked { get; private set; }

    public void Reset()
    {
        Invoked = false;
    }

    public IFixture Invoke()
    {
        Invoked = true;
        return OnFixtureCreated?.Invoke();
    }

    public static implicit operator Func<IFixture>(DelegatingFixtureFactory factory)
    {
        return factory.Invoke;
    }
}
