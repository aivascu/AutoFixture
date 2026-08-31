using AutoFixture.NUnit4.Internal;

namespace AutoFixture.NUnit4.UnitTest.Internal;

public class ParameterMatcherBuilderTests
{
    [Test]
    public void MatchesExactType()
    {
        // Arrange
        var method = this.GetType().GetMethod(nameof(this.SampleTest))!;
        var parameters = method.GetParameters();
        var parameterA = parameters[0];
        var sut = new ParameterMatcherBuilder(parameterA);
        var specification = sut.SetFlags(Matching.ExactType).Build();

        // Act
        var matchesExactRequest = specification.IsSatisfiedBy(parameterA);
        var matchesExactType = specification.IsSatisfiedBy(typeof(NoopImplementer));
        var matchesBaseType = specification.IsSatisfiedBy(typeof(NoopBaseType));
        var matchesInterfaceA = specification.IsSatisfiedBy(typeof(IInterfaceA));
        var matchesInterfaceB = specification.IsSatisfiedBy(typeof(IInterfaceB));

        // Assert
        Assert.That(matchesExactRequest, Is.True);
        Assert.That(matchesExactType, Is.True);
        Assert.That(matchesBaseType, Is.False);
        Assert.That(matchesInterfaceA, Is.False);
        Assert.That(matchesInterfaceB, Is.False);
    }

    public void SampleTest(NoopImplementer noopArgument)
    {
    }

    public interface IInterfaceA
    {
        object DoA(object obj);
    }

    public interface IInterfaceB
    {
        object DoB(object obj);
    }

    public class NoopBaseType : IInterfaceA, IInterfaceB
    {
        public virtual object DoA(object obj) => new();
        public virtual object DoB(object obj) => new();
    }

    public class NoopImplementer : NoopBaseType
    {
        public override object DoA(object obj) => new();
        public override object DoB(object obj) => new();
    }

    public class TypeWithMatchingProperty<T>
    {
        public T NoopArgument { get; set; }
    }
}
