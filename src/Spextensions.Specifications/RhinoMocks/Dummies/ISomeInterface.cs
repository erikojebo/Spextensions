namespace Spextensions.Specifications.RhinoMocks.Dummies
{
    public interface ISomeInterface
    {
        string Property { get; set; }
        void Method();
        void MethodWithParameter(string argument);
        int Function();
        int FunctionWithOneParameter(int a);
        int FunctionWithTwoParameters(int a, bool b);
        int FunctionWithThreeParameters(int a, bool b, string s);
        int FunctionWithTwoParametersOfSameType(string first, string second);
        int FunctionWithFourParameters(int a, bool b, string s, string s2);
    }
}