using Rhino.Mocks.Interfaces;

namespace Rhino.Mocks
{
    public static class ReturnSequenceExtensions
    {
        public static IMethodOptions<T> ReturnSequence<T>(this IMethodOptions<T> methodOptions, IValueProvider<T> sequence)
        {
            // RhinoMocks requires that a return value is set, but the actual return value is 
            // determined in the WhenCalled callback. So just set the return value to the default
            // value for the given type, so that RhinoMocks stops complaining
            return methodOptions.WhenCalled(mi => mi.ReturnValue = sequence.Next())
                .Return(default(T));
        }
    }
}