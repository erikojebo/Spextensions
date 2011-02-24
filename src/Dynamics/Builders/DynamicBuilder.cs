using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Spextensions.Dynamics.Builders.Exceptions;
using Spextensions.Dynamics.Infrastructure;

namespace Spextensions.Dynamics.Builders
{
    public class DynamicBuilder<T> : HookableDynamicObject
        where T : class, new()
    {
        protected readonly T Entity = new T();
        private PropertyInfo _collectionProperty;

        public override InvocationResult MethodMissing(string methodName, object[] arguments)
        {
            var value = arguments[0];

            if (MatchesCollectionProperty(methodName))
            {
                return AddChild(value);
            }

            return SetOrdinaryProperty(methodName, value);
        }

        private bool MatchesCollectionProperty(string methodName)
        {
            var propertyName = methodName + "s";
            _collectionProperty = typeof(T).GetProperty(propertyName);

            return _collectionProperty != null;
        }

        private InvocationResult AddChild(object value)
        {
            var propertyType = _collectionProperty.PropertyType;

            var collectionInterfaces = propertyType.GetInterfaces()
                .Where(x => x.IsGenericType)
                .Where(x => x.GetGenericTypeDefinition() == typeof(ICollection<>));

            var childTypes = collectionInterfaces.Select(x => x.GetGenericArguments()[0]);

            if (childTypes.Any())
            {
                var childType = childTypes.First();
                object childInstance = GetChildInstance(childType, value);

                var addMethod = collectionInterfaces.First().GetMethod("Add");
                var collection = _collectionProperty.GetValue(Entity, null);
                addMethod.Invoke(collection, new[] { childInstance });
            }

            return new SuccessfulInvocationResult(this);
        }

        private object GetChildInstance(Type childType, object value)
        {
            // If the argument to the method was an instance of the child type, use the
            // parameter value as the child to add
            if (childType.IsAssignableFrom(value.GetType()))
            {
                return value;
            }

            // Otherwise, create a new instance of the child type
            return Activator.CreateInstance(childType);
        }

        private InvocationResult SetOrdinaryProperty(string propertyName, object value)
        {
            var property = typeof(T).GetProperty(propertyName);

            var propertyExists = property != null;

            if (!propertyExists)
            {
                throw new MissingPropertyException(propertyName, value);
            }

            property.SetValue(Entity, value, null);
            return new SuccessfulInvocationResult(this);
        }

        public T Build()
        {
            return Entity;
        }
    }
}