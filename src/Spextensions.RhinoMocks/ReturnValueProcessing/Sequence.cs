using System.Collections.Generic;

namespace Rhino.Mocks
{
    public class Sequence<T> : IValueProvider<T>
    {
        private readonly Queue<T> _queue;

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