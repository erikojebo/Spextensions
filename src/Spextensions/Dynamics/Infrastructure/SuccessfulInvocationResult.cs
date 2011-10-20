namespace Spextensions.Dynamics.Infrastructure
{
    public class SuccessfulInvocationResult : InvocationResult
    {
        public SuccessfulInvocationResult(object returnValue) : base(true, returnValue) {}
    }
}