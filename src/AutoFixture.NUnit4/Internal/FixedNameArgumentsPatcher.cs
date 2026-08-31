namespace AutoFixture.NUnit4.Internal;

internal class FixedNameArgumentsPatcher : IPatchParameters
{
    public void Patch(TestCaseParameters parameters, IMethodInfo method, int autoDataStartIndex)
    {
        if (parameters is null) throw new ArgumentNullException(nameof(parameters));
        if (method is null) throw new ArgumentNullException(nameof(method));

        EnsureOriginalArgumentsArrayIsNotShared(parameters);

        var methodParameters = method.GetParameters();
        for (var index = autoDataStartIndex; index < parameters.OriginalArguments.Length; index++)
        {
            parameters.OriginalArguments[index] = new TypeNameRenderer(methodParameters[index].ParameterType);
        }
    }

    /// <summary>
    /// Before NUnit 3.5 the Arguments and OriginalArguments properties are referencing the same array, so
    /// we cannot safely update the OriginalArguments without touching the Arguments value.
    /// This method fixes that by making the OriginalArguments array a standalone copy.
    /// <para>
    /// When running in NUnit4.5 and later the method is supposed to do nothing.
    /// </para>
    /// </summary>
    private static void EnsureOriginalArgumentsArrayIsNotShared(TestCaseParameters parameters)
    {
        if (!ReferenceEquals(parameters.Arguments, parameters.OriginalArguments)) return;

        var clonedArguments = new object[parameters.OriginalArguments.Length];
        Array.Copy(parameters.OriginalArguments, clonedArguments, parameters.OriginalArguments.Length);

        // Unfortunately the property has a private setter, so can be updated via reflection only.
        // Should use the type where the property is declared as otherwise the private setter is not available.
        var property = typeof(TestParameters).GetTypeInfo()
            .GetProperty(nameof(TestCaseParameters.OriginalArguments));
        property.SetValue(parameters, clonedArguments, null);
    }

    private class TypeNameRenderer
    {
        private Type Type { get; }

        public TypeNameRenderer(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public override string ToString() => $"auto<{Type.Name}>";
    }
}
