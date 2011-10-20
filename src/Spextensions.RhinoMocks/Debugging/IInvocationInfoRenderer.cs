namespace Spextensions.RhinoMocks.Debugging
{
    public interface IInvocationInfoRenderer
    {
        void Render(params object[] values);
        string MethodName { get; set; }
    }
}