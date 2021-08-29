using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutoFixture.MSTest.DataSources
{
    public class MemberSource : ITestCaseSource
    {
        private readonly MemberInfo memberInfo;
        private readonly object[]? arguments;

        public MemberSource(MemberInfo memberInfo, object[]? arguments)
        {
            this.memberInfo = memberInfo ?? throw new ArgumentNullException(nameof(memberInfo));
            this.arguments = arguments;
        }

        public IEnumerable<IEnumerable<object>> GetTestCases(MethodInfo method)
        {
            ITestCaseSource source = this.memberInfo switch
            {
                FieldInfo fieldInfo => new FieldSource(fieldInfo),
                PropertyInfo propertyInfo => new PropertySource(propertyInfo),
                MethodInfo methodInfo => new MethodSource(methodInfo, this.arguments),
                _ => throw new InvalidOperationException("The member needs to be a field, property or method.")
            };
            return source.GetTestCases(method);
        }
    }
}