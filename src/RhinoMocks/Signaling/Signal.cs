using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Spextensions.RhinoMocks
{
    public class Signal
    {
        private readonly string _description;
        private readonly List<string> _signals = new List<string>();
        private bool _wasSent;

        public Signal(string description)
        {
            _description = description;
        }

        public void Send(string signalName)
        {
            _signals.Add(signalName);
        }

        public void Send()
        {
            _wasSent = true;
        }

        public void Assert(string signalName)
        {
            bool isSignaled = _signals.Any(s => s == signalName);

            if (!isSignaled)
            {
                ThrowAssertionExceptionFor(signalName);
            }
        }

        public void Assert()
        {
            if (!_wasSent)
            {
                throw new AssertionException(_description);
            }
        }

        private void ThrowAssertionExceptionFor(string signalName)
        {
            var errorMessage = string.Format("The signal '{0}' has not been signaled", signalName);

            throw new AssertionException(errorMessage);
        }
    }
}