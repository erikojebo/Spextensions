using Rhino.Mocks.Interfaces;

namespace Spextensions.RhinoMocks
{
    public static class SignalingExtensions
    {
        public static IMethodOptions<T> Signal<T>(this IMethodOptions<T> methodOptions, string signalName, SignalState signals)
        {
            return methodOptions.WhenCalled(mi => signals.Signal(signalName));
        }

        public static IMethodOptions<T> AssertSignal<T>(this IMethodOptions<T> methodOptions, string signalName, SignalState signals)
        {
            return methodOptions.WhenCalled(mi => signals.AssertSignal(signalName));
        }
    }
}