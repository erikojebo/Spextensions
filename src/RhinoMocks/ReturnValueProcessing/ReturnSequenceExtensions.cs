using System;
using Rhino.Mocks.Interfaces;
using Spextensions.RhinoMocks.Exceptions;

namespace Spextensions.RhinoMocks
{
    public static class ReturnSequenceExtensions
    {
        public static IMethodOptions<T> ReturnSequence<T>(this IMethodOptions<T> methodOptions, IValueProvider<T> sequence)
        {
            return methodOptions.Do((Func<T>)(sequence.Next));
        }

        public static IMethodOptions<TResult> ReturnSequence<TArg, TResult>(
            this IMethodOptions<TResult> methodOptions, IValueProvider<TResult> sequence)
        {
            return methodOptions.Do((Func<TArg, TResult>)(t => sequence.Next()));
        }

        public static IMethodOptions<TResult> ReturnSequence<TArg1, TArg2, TResult>(
            this IMethodOptions<TResult> methodOptions, IValueProvider<TResult> sequence)
        {
            return methodOptions.Do((Func<TArg1, TArg2, TResult>)((t, u) => sequence.Next()));
        }

        public static IMethodOptions<TResult> ReturnSequence<TArg1, TArg2, TArg3, TResult>(
            this IMethodOptions<TResult> methodOptions, IValueProvider<TResult> sequence)
        {
            return methodOptions.Do((Func<TArg1, TArg2, TArg3, TResult>)((t, u, v) => sequence.Next()));
        }
    }
}