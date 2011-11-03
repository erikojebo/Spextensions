namespace Rhino.Mocks
{
    public class ArgumentNotFoundException : SpextensionException
    {
        public ArgumentNotFoundException(string message) : base(message) {}
    }
}