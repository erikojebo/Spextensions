using NUnit.Framework;
using Rhino.Mocks.Interfaces;

namespace Spextensions.RhinoMocks
{
    public static class SignalingExtensions
    {
        public static IMethodOptions<T> Signal<T>(this IMethodOptions<T> methodOptions, string signalName, SignalState signals)
        {
            signals.Called = true;
            return methodOptions;
        }

        public static IMethodOptions<T> AssertSignal<T>(this IMethodOptions<T> methodOptions, string signalName, SignalState signals)
        {
            if (!signals.Called)
                throw new AssertionException("");

            return methodOptions;
        }
    }
}