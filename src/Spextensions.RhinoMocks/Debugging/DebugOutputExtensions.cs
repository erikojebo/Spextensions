using Rhino.Mocks.Interfaces;

namespace Spextensions.RhinoMocks.Debugging
{
    public static class DebugOutputExtensions
    {
        public static IMethodOptions<T> Signal<T>(this IMethodOptions<T> methodOptions, Signal signal)
        {
            return methodOptions.WhenCalled(mi => signal.Send());
        }
    }
}