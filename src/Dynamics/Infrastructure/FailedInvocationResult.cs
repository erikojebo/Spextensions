namespace Spextensions.Dynamics.Infrastructure
{
    public class FailedInvocationResult : InvocationResult
    {
        public FailedInvocationResult() : base(false, null) {}
    }
}