using System;
using Rhino.Mocks.Interfaces;

namespace Spextensions.RhinoMocks
{
    public static class ReturnSequenceExtensions
    {
        public static IMethodOptions<T> ReturnSequence<T>(this IMethodOptions<T> methodOptions, ReturnValueSequence<T> sequence)
        {
            return methodOptions.Do((Func<T>)(() =>
                {
                    if (sequence.IsEmpty)
                    {
                        throw new OutOfReturnValuesException();
                    }

                    return sequence.Next();
                }));
        }

        public static IMethodOptions<TResult> ReturnSequence<T, TResult>(
            this IMethodOptions<TResult> methodOptions, ReturnValueSequence<TResult> sequence)
        {
            return methodOptions.Do((Func<T, TResult>)(t =>
                {
                    if (sequence.IsEmpty)
                    {
                        throw new OutOfReturnValuesException();
                    }

                    return sequence.Next();
                }));
        }

        public static IMethodOptions<TResult> ReturnSequence<T, U, TResult>(
            this IMethodOptions<TResult> methodOptions, ReturnValueSequence<TResult> sequence)
        {
            return methodOptions.Do((Func<T, U, TResult>)((t, u) =>
                {
                    if (sequence.IsEmpty)
                    {
                        throw new OutOfReturnValuesException();
                    }

                    return sequence.Next();
                }));
        }
    }
}