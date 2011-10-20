using Spextensions.RhinoMocks.Exceptions;

namespace Spextensions.RhinoMocks
{
    public class Signal
    {
        private readonly string _description;
        private bool _wasSent;

        public Signal(string description)
        {
            _description = description;
        }

        public void Send()
        {
            _wasSent = true;
        }

        public void Assert()
        {
            if (!_wasSent)
            {
                var message = string.Format("The signal '{0}' not been signaled", _description);

                throw new SignalAssertionException(message);
            }
        }
    }
}