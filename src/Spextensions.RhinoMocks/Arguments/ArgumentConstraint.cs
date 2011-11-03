using System.Linq;
using Rhino.Mocks;

namespace Rhino.Mocks
{
    public class ArgumentConstraint<T>
    {
        private readonly int _matchIndex;

        public ArgumentConstraint() : this(0) {}

        public ArgumentConstraint(int matchIndex)
        {
            _matchIndex = matchIndex;
        }

        public T FindMatch(object[] arguments)
        {
            var argumentsOfExpectedType = arguments.OfType<T>();

            if (argumentsOfExpectedType.Count() <= _matchIndex)
            {
                var message = string.Format("No matching argument of type '{0}' was found", typeof(T).Name);

                if (_matchIndex > 0)
                {
                    message += string.Format(" (attempted to find match with index: {0})", _matchIndex);
                }

                throw new ArgumentNotFoundException(message);
            }

            var matches = argumentsOfExpectedType.ElementAt(_matchIndex);

            return matches;
        }
    }
}