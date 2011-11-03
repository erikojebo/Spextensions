using System;
using System.Linq;
using Rhino.Mocks.Interfaces;

namespace Rhino.Mocks
{
    public static class ArgumentCaptureExtensions
    {
        public static IMethodOptions<TReturnValue> CaptureFirstArgumentOfType<TReturnValue>(
            this IMethodOptions<TReturnValue> methodOptions, Type argumentType, Action<object> setter)
        {
            return methodOptions
                .WhenCalled(mi =>
                    {
                        var argumentsOfExpectedType = mi.Arguments.Where(x => x.GetType() == argumentType);

                        if (!argumentsOfExpectedType.Any())
                        {
                            var message = string.Format("No argument of type '{0}' was found", argumentType.Name);
                            throw new ArgumentNotFoundException(message);    
                        }

                        setter(argumentsOfExpectedType.First());
                    });
        }

        public static IMethodOptions<TReturnValue> CaptureFirstMatchingArgument<TReturnValue, TArgument>(
            this IMethodOptions<TReturnValue> methodOptions,
            ArgumentConstraint<TArgument> constraint,
            Action<TArgument> setter)
        {
            return methodOptions
                .WhenCalled(mi =>
                    {
                        var matchingArgument = constraint.FindMatch(mi.Arguments);

                        setter(matchingArgument);
                    });
        }
    }
}