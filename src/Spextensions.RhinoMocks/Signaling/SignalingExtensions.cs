using Rhino.Mocks.Interfaces;

namespace Spextensions.RhinoMocks
{
    public static class SignalingExtensions
    {
        public static IMethodOptions<T> Signal<T>(this IMethodOptions<T> methodOptions, Signal signal)
        {
            return methodOptions.WhenCalled(mi => signal.Send());
        }

        public static IMethodOptions<T> AssertSignal<T>(this IMethodOptions<T> methodOptions, Signal signal)
        {
            return methodOptions.WhenCalled(mi => signal.Assert());
        }
    }
}