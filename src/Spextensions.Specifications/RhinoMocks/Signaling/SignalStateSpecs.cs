using NUnit.Framework;

namespace Rhino.Mocks
{
    [TestFixture]
    public class SignalStateSpecs
    {
        private SignalState _signals;

        [SetUp]
        public void SetUp()
        {
            _signals = new SignalState();
        }

        [Fact]
        [ExpectedException(typeof(SignalAssertionException))]
        public void Assert_without_prior_signal_throws_SignalAssertionException()
        {
            _signals.AssertSignal("signal that has not been signaled");
        }

        [Fact]
        public void Assert_after_signal_does_not_throw_exception()
        {
            _signals.Signal("expected signal");
            _signals.AssertSignal("expected signal");
        }

        [Fact]
        public void Assert_for_given_signals_after_multiple_signals_does_not_throw_exception()
        {
            _signals.Signal("expected signal");
            _signals.Signal("other expected signal");
            _signals.AssertSignal("expected signal");
            _signals.AssertSignal("other expected signal");
        }

        [Fact]
        [ExpectedException(typeof(SignalAssertionException))]
        public void Assert_after_only_signal_with_different_name_throws_SignalAssertionException()
        {
            _signals.Signal("other signal");
            _signals.AssertSignal("expected signal");
        }
    }
}