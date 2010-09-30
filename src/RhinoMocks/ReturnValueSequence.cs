using System;

namespace Spextensions.RhinoMocks
{
    public class ReturnValueSequence<T>
    {
        private readonly T[] _values;

        public ReturnValueSequence(params T[] values)
        {
            _values = values;
        }

        public bool IsEmpty
        {
            get { return _values.Length == 0; }
        }

        public T Next()
        {
            return _values[0];
        }
    }
}