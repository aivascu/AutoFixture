namespace AutoFixture.NUnit4.Internal;

internal static class CustomizationExtensions
{
    public static ICustomization Aggregate(this IEnumerable<ICustomization> customizations)
    {
        return new CompositeCustomization(customizations);
    }

    public static IEnumerable<ICustomization> GetCustomizations(this IParameterInfo source)
    {
        return source.GetCustomAttributes<Attribute>(false)
            .OfType<IParameterCustomizationSource>()
            .OrderBy(x => x, new CustomizeAttributeComparer())
            .Select(x => x.GetCustomization(source.ParameterInfo));
    }

    public static object Resolve(this IFixture source, IParameterInfo parameterInfo)
    {
        return new SpecimenContext(source).Resolve(parameterInfo.ParameterInfo);
    }
}
