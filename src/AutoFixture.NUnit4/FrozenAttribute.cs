namespace AutoFixture.NUnit4;

/// <summary>
/// An attribute that can be applied to parameters in an <see cref="AutoDataAttribute"/>-driven
/// TestCase to indicate that the parameter value should be frozen so that the same instance is
/// returned every time the <see cref="IFixture"/> creates an instance of that type.
/// </summary>
[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false)]
public sealed class FrozenAttribute : CustomizeAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FrozenAttribute"/> class.
    /// </summary>
    /// <remarks>
    /// The <see cref="Matching"/> criteria used to determine
    /// which requests will be satisfied by the frozen parameter value
    /// is <see cref="Matching.ExactType"/>.
    /// </remarks>
    public FrozenAttribute()
        : this(Matching.ExactType)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FrozenAttribute"/> class.
    /// </summary>
    /// <param name="by">
    /// The <see cref="Matching"/> criteria used to determine
    /// which requests will be satisfied by the frozen parameter value.
    /// </param>
    public FrozenAttribute(Matching by)
    {
        By = by;
    }

    /// <summary>
    /// Gets the <see cref="Matching"/> criteria used to determine
    /// which requests will be satisfied by the frozen parameter value.
    /// </summary>
    public Matching By { get; }

    /// <summary>
    /// Gets a <see cref="FreezeOnMatchCustomization"/> configured
    /// to match requests based on the <see cref="Type"/> and optionally
    /// the name of the parameter.
    /// </summary>
    /// <param name="parameter">
    /// The parameter for which the customization is requested.
    /// </param>
    /// <returns>
    /// A <see cref="FreezeOnMatchCustomization"/> configured
    /// to match requests based on the <see cref="Type"/> and optionally
    /// the name of the parameter.
    /// </returns>
    public override ICustomization GetCustomization(ParameterInfo parameter)
    {
        if (parameter is null) throw new ArgumentNullException(nameof(parameter));

        var matcher = new ParameterMatcherBuilder(parameter).SetFlags(By).Build();
        return new FreezeOnMatchCustomization(parameter, matcher);
    }
}
