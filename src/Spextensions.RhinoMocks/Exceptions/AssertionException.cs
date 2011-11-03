using System;

namespace Rhino.Mocks
{
    public class AssertionException : SpextensionException
    {
        public AssertionException(string message) : base(message) {}
    }
}