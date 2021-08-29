using System.Collections.Generic;

namespace AutoFixture.MSTest
{
    internal class CustomizationBuilder
    {
        private readonly List<ICustomization> customizations = new();

        public CustomizationBuilder Append(ICustomization customization)
        {
            this.customizations.Add(customization);
            return this;
        }

        public CustomizationBuilder Insert(int index, ICustomization customization)
        {
            this.customizations.Insert(index, customization);
            return this;
        }

        public ICustomization ToCustomization()
        {
            return new CompositeCustomization(this.customizations);
        }
    }
}