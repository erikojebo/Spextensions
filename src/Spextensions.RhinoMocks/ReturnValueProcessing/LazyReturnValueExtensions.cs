using Rhino.Mocks.Interfaces;

namespace Rhino.Mocks
{
    public static class LazyReturnValueExtensions
    {
        public static IMethodOptions<T> LazyReturnValue<T>(this IMethodOptions<T> methodOptions, 
            LazyReturnValue<T> lazyReturnValue)
        {
            // RhinoMocks requires that a return value is set, but the actual return value is 
            // determined in the WhenCalled callback. So just set the return value to the default
            // value for the given type, so that RhinoMocks stops complaining
            return methodOptions
                .WhenCalled(mi => mi.ReturnValue = lazyReturnValue.Value)
                .Return(default(T));
        }
    }
}