namespace AutoFixture.MSTest.UnitTest.TestClasses
{
    public interface IPropertyHolder<out T>
    {
        T Value { get; }
    }

    public class ConcretePropertyHolder<T> : IPropertyHolder<T>
    {
        public T Value { get; set; }
    }

    public abstract class AbstractPropertyHolder<T>
    {
        public virtual T Value { get; set; }
    }

    public class DerivedPropertyHolder<T> : AbstractPropertyHolder<T>
    {
    }
}
