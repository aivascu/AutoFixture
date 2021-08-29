using System;
using System.Collections;
using System.Linq;
using AutoFixture.Kernel;

namespace AutoFixture.MSTest.DataSources
{
    public class ClassSource : SourceBase
    {
        private readonly Type type;
        private readonly object[] arguments;
        private readonly IMethodQuery query;

        public ClassSource(Type type, object[] arguments)
            : this(new ModestConstructorQuery(), type, arguments)
        {
        }

        public ClassSource(IMethodQuery query, Type type, object[] arguments)
        {
            this.query = query ?? throw new ArgumentNullException(nameof(query));
            this.type = type ?? throw new ArgumentNullException(nameof(type));
            this.arguments = arguments;
        }

        public override IEnumerator GetEnumerator()
        {
            var constructor = this.query.SelectMethods(this.type).FirstOrDefault();
            if (constructor is null)
            {
                throw new InvalidOperationException(
                    $"Type {this.type} does not contain any public non-static constructors.");
            }

            if (constructor.Invoke(this.arguments) is not IEnumerable enumerable)
            {
                throw new InvalidOperationException($"Source type must implement the {typeof(IEnumerable)} interface.");
            }

            return enumerable.GetEnumerator();
        }
    }
}