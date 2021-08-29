using System.Collections.Generic;

namespace AutoFixture.MSTest
{
    internal class CustomizeAttributeComparer : Comparer<IParameterCustomizationSource>
    {
        public override int Compare(IParameterCustomizationSource? x, IParameterCustomizationSource? y)
        {
            var xFrozen = x is FrozenAttribute;
            var yFrozen = y is FrozenAttribute;

            if (xFrozen && !yFrozen) return 1;
            if (yFrozen && !xFrozen) return -1;
            return 0;
        }
    }
}