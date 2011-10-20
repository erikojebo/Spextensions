using System;

namespace Spextensions.RhinoMocks.Exceptions
{
    public class AssertionException : SpextensionException
    {
        public AssertionException(string message) : base(message) {}
    }
}