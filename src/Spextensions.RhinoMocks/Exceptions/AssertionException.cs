using System;

namespace Spextensions.RhinoMocks.Exceptions
{
    public class AssertionException : Exception
    {
        public AssertionException(string message) : base(message) {}
    }
}