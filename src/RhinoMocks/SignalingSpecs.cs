using NUnit.Framework;
using Rhino.Mocks;

namespace Spextensions.RhinoMocks
{
    [TestFixture]
    public class SignalingSpecs
    {
        private ISomeInterface _mock1;
        private ISomeOtherInterface _mock2;
        private SignalState _signals;

        [SetUp]
        public void SetUp()
        {
            _mock1 = MockRepository.GenerateMock<ISomeInterface>();
            _mock2 = MockRepository.GenerateMock<ISomeOtherInterface>();
            _signals = new SignalState();
        }

        [Test]
        public void No_exception_when_signal_is_given_before_matched_expectation()
        {
            _mock1.Expect(x => x.Method1())
                .Signal("given signal", _signals);

            _mock2.Expect(x => x.Method2())
                .AssertSignal("given signal", _signals);

            _mock1.Method1();
            _mock2.Method2();
        }

        [Test]
        [ExpectedException(typeof(AssertionException))]
        public void AssertionException_when_no_signal_is_given_before_matched_expectation()
        {
            _mock2.Expect(x => x.Method2())
                .AssertSignal("given signal", _signals);

            _mock2.Method2();
        }

        public interface ISomeInterface
        {
            void Method1();
        }

        public interface ISomeOtherInterface
        {
            void Method2();            
        }
    }
}