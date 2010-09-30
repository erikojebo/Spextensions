namespace Spextensions.RhinoMocks.Dummies
{
    public interface ISomeInterface
    {
        string Property { get; set; }
        void Method();
        void MethodWithParameter(string argument);
        int Function();
        int FunctionWithOneParameter(int a);
        int FunctionWithTwoParameters(int a, bool b);
    }
}