namespace AutoFixture.NUnit4.UnitTest.Common;

internal class DelegatingCustomization : ICustomization
{
    internal DelegatingCustomization()
    {
        OnCustomize = f => { };
    }

    public void Customize(IFixture fixture)
    {
        OnCustomize(fixture);
    }

    internal Action<IFixture> OnCustomize { get; set; }
}
