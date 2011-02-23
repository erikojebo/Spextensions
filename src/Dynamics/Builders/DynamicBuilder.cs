using Spextensions.Dynamics.Infrastructure;

namespace Spextensions.Dynamics.Builders
{
    public class DynamicBuilder<T> : HookableDynamicObject
        where T : class, new()
    {
        protected readonly T Entity = new T();

        public override InvocationResult MethodMissing(string methodName, object[] arguments)
        {
            var property = typeof(T).GetProperty(methodName);

            var propertyExists = property != null;

            if (!propertyExists)
            {
                throw new MissingPropertyException(methodName, arguments[0]);
            }

            property.SetValue(Entity, arguments[0], null);
            return new SuccessfulInvocationResult(this);
        }

        public T Build()
        {
            return Entity;
        }
    }
}