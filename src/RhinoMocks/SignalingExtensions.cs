using NUnit.Framework;
using Rhino.Mocks.Interfaces;

namespace Spextensions.RhinoMocks
{
    public static class SignalingExtensions
    {
        public static IMethodOptions<T> Signal<T>(this IMethodOptions<T> methodOptions, string signalName, SignalState signals)
        {
            signals.Signal = signalName;
            return methodOptions;
        }

        public static IMethodOptions<T> AssertSignal<T>(this IMethodOptions<T> methodOptions, string signalName, SignalState signals)
        {
            if (signals.Signal != signalName)
                throw new AssertionException("");

            return methodOptions;
        }
    }
}