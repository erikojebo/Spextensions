using Rhino.Mocks.Interfaces;

namespace Rhino.Mocks
{
    public static class DebugOutputExtensions
    {
        public static IMethodOptions<T> IgnoreArgumentsAndWriteInvocationInfoToConsole<T>(this IMethodOptions<T> methodOptions)
        {
            return IgnoreArgumentsAndWriteInvocationInfo(methodOptions, new ConsoleInvocationInfoRenderer());
        }

        public static IMethodOptions<T> IgnoreArgumentsAndWriteInvocationInfo<T>(
            this IMethodOptions<T> methodOptions,
            IInvocationInfoRenderer invocationInfoRenderer)
        {
            return methodOptions
                .IgnoreArguments()
                .WhenCalled(mi =>
                    {
                        invocationInfoRenderer.MethodName = mi.Method.Name;
                        invocationInfoRenderer.Render(mi.Arguments);
                    });
        }
    }
}