using NUnit.Framework;
using Rhino.Mocks;
using Spextensions.NUnit;
using Spextensions.RhinoMocks.Dummies;

namespace Spextensions.RhinoMocks
{
    [TestFixture]
    public class SignalingSpecs
    {
        private ISomeInterface _mock1;
        private ISomeOtherInterface _mock2;
        private Signal _signal1;
        private Signal _signal2;
        private ISomeInterface _stub1;

        [SetUp]
        public void SetUp()
        {
            _mock1 = MockRepository.GenerateMock<ISomeInterface>();
            _stub1 = MockRepository.GenerateStub<ISomeInterface>();
            _mock2 = MockRepository.GenerateMock<ISomeOtherInterface>();
            _signal1 = new Signal("signal description 1");
            _signal2 = new Signal("signal description 2");
        }

        [Fact]
        public void No_exception_when_signal_is_given_before_matched_expectation()
        {
            _mock1.Expect(x => x.Method1())
                .Signal(_signal1);

            _mock2.Expect(x => x.Method2())
                .AssertSignal(_signal1);

            _mock1.Method1();
            _mock2.Method2();
        }

        [Fact]
        [ExpectedException(typeof(AssertionException))]
        public void Throws_AssertionException_when_no_signal_is_given_before_matched_expectation()
        {
            _mock2.Expect(x => x.Method2())
                .AssertSignal(_signal1);

            _mock2.Method2();
        }

        [Fact]
        [ExpectedException(typeof(AssertionException))]
        public void Throws_AssertionException_when_only_some_other_signal_is_given_before_matched_expectation()
        {
            _mock1.Expect(x => x.Method1())
                .Signal(_signal2);

            _mock2.Expect(x => x.Method2())
                .AssertSignal(_signal1);

            _mock1.Method1();
            _mock2.Method2();
        }

        [Fact]
        [ExpectedException(typeof(AssertionException))]
        public void Throws_AssertionException_when_expected_signal_is_setup_but_signaling_call_is_never_executed()
        {
            _mock1.Expect(x => x.Method1())
                .Signal(_signal1);

            _mock2.Expect(x => x.Method2())
                .AssertSignal(_signal1);

            _mock2.Method2();
        }

        [Fact]
        [ExpectedException(typeof(AssertionException))]
        public void Throws_AssertionException_when_expected_signal_is_setup_but_signaling_call_is_made_with_wrong_argument()
        {
            _mock1.Expect(x => x.Method1("expected argument"))
                .Signal(_signal1);

            _mock2.Expect(x => x.Method2())
                .AssertSignal(_signal1);

            _mock1.Method1("wrong argument");
            _mock2.Method2();
        }

        [Fact]
        public void No_exception_is_thrown_when_stub_signals_expected_signal_before_matched_call()
        {
            _stub1.Stub(x => x.Method1())
                .Signal(_signal1);

            _mock2.Expect(x => x.Method2())
                .AssertSignal(_signal1);

            _stub1.Method1();
            _mock2.Method2();
        }
        
        [Fact]
        [ExpectedException(typeof(AssertionException))]
        public void Throws_AssertionException_when_stub_signal_is_set_up_but_signaling_call_is_never_made()
        {
            _stub1.Stub(x => x.Method1())
                .Signal(_signal1);

            _mock2.Expect(x => x.Method2())
                .AssertSignal(_signal1);

            _mock2.Method2();
        }
        
        [Fact]
        [ExpectedException(typeof(AssertionException))]
        public void Throws_AssertionException_when_stub_signal_is_set_up_but_signaling_call_is_made_after_matching_call()
        {
            _stub1.Stub(x => x.Method1())
                .Signal(_signal1);

            _mock2.Expect(x => x.Method2())
                .AssertSignal(_signal1);

            _mock2.Method2();
            _stub1.Method1();
        }
        
        [Fact]
        [ExpectedException(typeof(AssertionException))]
        public void Throws_AssertionException_when_stub_signal_is_set_up_but_signaling_call_is_made_with_wrong_argument()
        {
            _stub1.Stub(x => x.Method1("expected argument"))
                .Signal(_signal1);

            _mock2.Expect(x => x.Method2())
                .AssertSignal(_signal1);

            _stub1.Method1("wrong argument");
            _mock2.Method2();
        }
        
        [Fact]
        [ExpectedException(typeof(AssertionException))]
        public void Throws_AssertionException_when_two_signals_are_expected_but_only_one_is_signaled()
        {
            _stub1.Stub(x => x.Method1())
                .Signal(_signal1);
            _mock1.Stub(x => x.Method1())
                .Signal(_signal2);

            _mock2.Expect(x => x.Method2())
                .AssertSignal(_signal1)
                .AssertSignal(_signal2);

            _mock1.Method1();
            _mock2.Method2();
        }
        
        [Fact]
        public void No_exception_when_two_signals_are_expected_and_both_are_signaled_before_matching_call()
        {
            _stub1.Stub(x => x.Method1())
                .Signal(_signal1);
            _mock1.Stub(x => x.Method1())
                .Signal(_signal2);

            _mock2.Expect(x => x.Method2())
                .AssertSignal(_signal1)
                .AssertSignal(_signal2);

            _stub1.Method1();
            _mock1.Method1();
            _mock2.Method2();
        }

        [Fact]
        public void No_expectation_when_signal_set_for_property_setter_is_signaled_before_matching_call()
        {
            _mock1.Expect(x => x.Property)
                .SetPropertyWithArgument("expected value")
                .Signal(_signal1);

            _mock2.Expect(x => x.Method2())
                .AssertSignal(_signal1);

            _mock1.Property = "expected value";
            _mock2.Method2();
        }

        [Fact]
        [ExpectedException(typeof(AssertionException))]
        public void Throws_ArgumentException_when_signal_is_set_for_property_setter_but_is_called_with_wrong_argument()
        {
            _mock1.Expect(x => x.Property)
                .SetPropertyWithArgument("expected value")
                .Signal(_signal1);

            _mock2.Expect(x => x.Method2())
                .AssertSignal(_signal1);

            _mock1.Property = "wrong value";
            _mock2.Method2();
        }
    }
}