using System.Collections.Generic;
using System.Linq;

namespace Rhino.Mocks
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

            if (!isSignaled)
            {
                var message = string.Format("The signal '{0}' not been signaled", signalName);

                throw new SignalAssertionException(message);
            }
        }
    }
}