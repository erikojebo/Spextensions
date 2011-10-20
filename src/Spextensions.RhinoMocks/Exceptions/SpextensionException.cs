using System;

namespace Spextensions.RhinoMocks.Exceptions
{
    public class SpextensionException : Exception
    {
        public SpextensionException() {}
        
        public SpextensionException(string message) : base(message) {}
    }
}