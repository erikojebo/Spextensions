using System;

namespace Rhino.Mocks
{
    public class SpextensionException : Exception
    {
        public SpextensionException() {}
        
        public SpextensionException(string message) : base(message) {}
    }
}