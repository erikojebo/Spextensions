using Rhino.Mocks.Interfaces;

namespace Rhino.Mocks
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