using System.Dynamic;

namespace Spextensions.Dynamics.Infrastructure
{
    public class HookableDynamicObject : DynamicObject
    {
        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var invocationResult = MethodMissing(binder.Name, args);

            result = invocationResult.ReturnValue;
            
            return invocationResult.WasInvocationSuccessful;
        }

        public virtual InvocationResult MethodMissing(string methodName, object[] arguments)
        {
            return new FailedInvocationResult();
        }
    }
}