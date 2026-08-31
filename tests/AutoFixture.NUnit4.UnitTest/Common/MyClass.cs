namespace AutoFixture.NUnit4.UnitTest.Common;

public class MyClass
{
    public T Echo<T>(T item)
    {
        return item;
    }
}
