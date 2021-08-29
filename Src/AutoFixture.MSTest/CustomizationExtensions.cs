using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutoFixture.MSTest
{
    internal static class CustomizationExtensions
    {
        public static IEnumerable<ICustomization> GetCustomizations(this ParameterInfo source)
        {
            return source
                .GetCustomAttributes<Attribute>(false)
                .OfType<IParameterCustomizationSource>()
                .OrderBy(x => x, new CustomizeAttributeComparer())
                .Select(x => x.GetCustomization(source));
        }
    }
}