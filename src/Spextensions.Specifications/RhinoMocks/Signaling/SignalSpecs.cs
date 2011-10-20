using NUnit.Framework;
using Spextensions.NUnit;
using Spextensions.RhinoMocks;

namespace Spextensions.Specifications.RhinoMocks.Signaling
{
    [TestFixture]
    public class SignalSpecs
    {
        private Signal _signal;

        [SetUp]
        public void SetUp()
        {
            _signal = new Signal("");
        }

        [Fact]
        [ExpectedException(typeof(AssertionException))]
        public void Assert_without_prior_signal_throws_ArgumentException()
        {
            _signal.Assert();
        }

        [Fact]
        public void Assert_after_signal_does_not_throw_exception()
        {
            _signal.Send();
            _signal.Assert();
        }

        [Fact]
        public void Assert_for_given_signals_after_multiple_signals_does_not_throw_exception()
        {
            _signal.Send();
            _signal.Send();
            _signal.Assert();
        }

        [Fact]
        [ExpectedException(typeof(AssertionException), ExpectedMessage = "Signal description", MatchType = MessageMatch.Contains)]
        public void AssertException_message_includes_signal_description()
        {
            var signal = new Signal("Signal description");
            signal.Assert();
        }
    }
}