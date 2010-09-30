using System;
using NUnit.Framework;
using Rhino.Mocks.Interfaces;

namespace Spextensions.RhinoMocks
{
    public static class AssertionExtensions
    {
        public static IMethodOptions<T> AssertThat<T>(this IMethodOptions<T> methodOptions,
            Func<bool> condition, string message = "condition not met")
        {
            return methodOptions.WhenCalled(mi => Assert(condition, message));
        }

        private static void Assert(Func<bool> condition, string message)
        {
            if (!condition())
            {
                throw new AssertionException(message);
            }
        }
    }
}