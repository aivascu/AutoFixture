namespace AutoFixture.NUnit4.Internal;

internal class AutoTestCaseParameters
{
    private readonly Func<IFixture> _fixtureFactory;
    private readonly IReadOnlyList<object> _arguments;

    public AutoTestCaseParameters(Func<IFixture> fixtureFactory, IReadOnlyList<object> arguments)
    {
        _fixtureFactory = fixtureFactory ?? throw new ArgumentNullException(nameof(fixtureFactory));
        _arguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
    }

    public string Category { get; set; }

    public IPatchParameters Patcher { get; set; } = new FixedNameArgumentsPatcher();

    public TestCaseParameters GetParameters(IMethodInfo method)
    {
        var parameters = CreateParameters(method);

        if (!string.IsNullOrWhiteSpace(Category))
        {
            parameters.Properties.Add("Category", Category);
        }

        return parameters;
    }

    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes",
        Justification = "This method is always expected to return an instance of the TestCaseParameters class.")]
    private TestCaseParameters CreateParameters(IMethodInfo method)
    {
        var methodParameters = method.GetParameters();

        var missingParameters = methodParameters
            .Skip(_arguments.Count).ToList();

        if (missingParameters.Count == 0)
            return new TestCaseParameters(_arguments.ToArray());

        try
        {
            var fixture = _fixtureFactory.Invoke();

            missingParameters
                .SelectMany(x => x.GetCustomizations())
                .Aggregate().Customize(fixture);

            var missingValues = missingParameters.Select(fixture.Resolve);
            var arguments = _arguments.Concat(missingValues).ToArray();

            var parameters = new TestCaseParameters(arguments);
            Patcher.Patch(parameters, method, _arguments.Count);
            return parameters;
        }
        catch (Exception e)
        {
            return new TestCaseParameters(e);
        }
    }
}
