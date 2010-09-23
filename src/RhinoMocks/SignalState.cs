using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Spextensions.RhinoMocks
{
    public class SignalState
    {
        private readonly List<string> _signals = new List<string>();

        public void Signal(string signalName)
        {
            _signals.Add(signalName);
        }

        public void AssertSignal(string signalName)
        {
            bool isSignaled = _signals.Any(s => s == signalName);

            Assert.IsTrue(isSignaled, "The given signal has not been signaled");
        }
    }
}