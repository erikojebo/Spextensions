using System.Collections.Generic;
using Spextensions.RhinoMocks.Exceptions;

namespace Spextensions.RhinoMocks
{
    public class Sequence<T>
    {
        private Queue<T> _queue;

        public Sequence(params T[] values)
        {
            _queue = new Queue<T>(values);
        }

        public bool IsEmpty
        {
            get { return _queue.Count == 0; }
        }

        public T Next()
        {
            if (IsEmpty)
            {
                throw new OutOfReturnValuesException();
            }

            return _queue.Dequeue();
        }
    }
}