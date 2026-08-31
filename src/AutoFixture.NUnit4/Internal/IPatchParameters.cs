namespace AutoFixture.NUnit4.Internal;

internal interface IPatchParameters
{
    void Patch(TestCaseParameters parameters, IMethodInfo method, int autoDataStartIndex);
}
