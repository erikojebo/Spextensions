namespace Rhino.Mocks
{
    public class LazyReturnValue<T>
    {
        public LazyReturnValue(T value)
        {
            Value = value;
        }

        public T Value { get; set; }
    }

    public class LazyReturnValue
    {
        private LazyReturnValue() {}

        public static LazyReturnValue<T> Create<T>(T value)
        {
            return new LazyReturnValue<T>(value);
        }
    }
}