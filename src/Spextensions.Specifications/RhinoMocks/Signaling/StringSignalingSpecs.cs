using NUnit.Framework;
using Rhino.Mocks;

namespace Spextensions.Specifications.RhinoMocks.Signaling
{
    [TestFixture]
    public class StringSignalingSpecs
    {
        [TestFixture]
        public class SignalingSpecs
        {
            private ISomeInterface _mock1;
            private ISomeOtherInterface _mock2;
            private SignalState _signals;
            private ISomeInterface _stub1;

            [SetUp]
            public void SetUp()
            {
                _mock1 = MockRepository.GenerateMock<ISomeInterface>();
                _stub1 = MockRepository.GenerateStub<ISomeInterface>();
                _mock2 = MockRepository.GenerateMock<ISomeOtherInterface>();
                _signals = new SignalState();
            }

            [Fact]
            public void No_exception_when_signal_is_given_before_matched_expectation()
            {
                _mock1.Expect(x => x.Method1())
                    .Signal("given signal", _signals);

                _mock2.Expect(x => x.Method2())
                    .AssertSignal("given signal", _signals);

                _mock1.Method1();
                _mock2.Method2();
            }

            [Fact]
            [ExpectedException(typeof(SignalAssertionException))]
            public void Throws_SignalAssertionException_when_no_signal_is_given_before_matched_expectation()
            {
                _mock2.Expect(x => x.Method2())
                    .AssertSignal("given signal", _signals);

                _mock2.Method2();
            }

            [Fact]
            [ExpectedException(typeof(SignalAssertionException))]
            public void Throws_SignalAssertionException_when_only_some_other_signal_is_given_before_matched_expectation()
            {
                _mock1.Expect(x => x.Method1())
                    .Signal("other signal", _signals);

                _mock2.Expect(x => x.Method2())
                    .AssertSignal("expected signal", _signals);

                _mock1.Method1();
                _mock2.Method2();
            }

            [Fact]
            [ExpectedException(typeof(SignalAssertionException))]
            public void Throws_SignalAssertionException_when_expected_signal_is_setup_but_signaling_call_is_never_executed()
            {
                _mock1.Expect(x => x.Method1())
                    .Signal("given signal", _signals);

                _mock2.Expect(x => x.Method2())
                    .AssertSignal("given signal", _signals);

                _mock2.Method2();
            }

            [Fact]
            [ExpectedException(typeof(SignalAssertionException))]
            public void Throws_SignalAssertionException_when_expected_signal_is_setup_but_signaling_call_is_made_with_wrong_argument()
            {
                _mock1.Expect(x => x.Method1("expected argument"))
                    .Signal("given signal", _signals);

                _mock2.Expect(x => x.Method2())
                    .AssertSignal("given signal", _signals);

                _mock1.Method1("wrong argument");
                _mock2.Method2();
            }

            [Fact]
            public void No_exception_is_thrown_when_stub_signals_expected_signal_before_matched_call()
            {
                _stub1.Stub(x => x.Method1())
                    .Signal("given signal", _signals);

                _mock2.Expect(x => x.Method2())
                    .AssertSignal("given signal", _signals);

                _stub1.Method1();
                _mock2.Method2();
            }

            [Fact]
            [ExpectedException(typeof(SignalAssertionException))]
            public void Throws_SignalAssertionException_when_stub_signal_is_set_up_but_signaling_call_is_never_made()
            {
                _stub1.Stub(x => x.Method1())
                    .Signal("given signal", _signals);

                _mock2.Expect(x => x.Method2())
                    .AssertSignal("given signal", _signals);

                _mock2.Method2();
            }

            [Fact]
            [ExpectedException(typeof(SignalAssertionException))]
            public void Throws_SignalAssertionException_when_stub_signal_is_set_up_but_signaling_call_is_made_after_matching_call()
            {
                _stub1.Stub(x => x.Method1())
                    .Signal("given signal", _signals);

                _mock2.Expect(x => x.Method2())
                    .AssertSignal("given signal", _signals);

                _mock2.Method2();
                _stub1.Method1();
            }

            [Fact]
            [ExpectedException(typeof(SignalAssertionException))]
            public void Throws_SignalAssertionException_when_stub_signal_is_set_up_but_signaling_call_is_made_with_wrong_argument()
            {
                _stub1.Stub(x => x.Method1("expected argument"))
                    .Signal("given signal", _signals);

                _mock2.Expect(x => x.Method2())
                    .AssertSignal("given signal", _signals);

                _stub1.Method1("wrong argument");
                _mock2.Method2();
            }

            [Fact]
            [ExpectedException(typeof(SignalAssertionException))]
            public void Throws_SignalAssertionException_when_two_signals_are_expected_but_only_one_is_signaled()
            {
                _stub1.Stub(x => x.Method1())
                    .Signal("given signal", _signals);
                _mock1.Stub(x => x.Method1())
                    .Signal("other given signal", _signals);

                _mock2.Expect(x => x.Method2())
                    .AssertSignal("given signal", _signals)
                    .AssertSignal("other given signal", _signals);

                _mock1.Method1();
                _mock2.Method2();
            }

            [Fact]
            public void No_exception_when_two_signals_are_expected_and_both_are_signaled_before_matching_call()
            {
                _stub1.Stub(x => x.Method1())
                    .Signal("given signal", _signals);
                _mock1.Stub(x => x.Method1())
                    .Signal("other given signal", _signals);

                _mock2.Expect(x => x.Method2())
                    .AssertSignal("given signal", _signals)
                    .AssertSignal("other given signal", _signals);

                _stub1.Method1();
                _mock1.Method1();
                _mock2.Method2();
            }

            [Fact]
            public void No_expectation_when_signal_set_for_property_setter_is_signaled_before_matching_call()
            {
                _mock1.Expect(x => x.Property)
                    .SetPropertyWithArgument("expected value")
                    .Signal("given signal", _signals);

                _mock2.Expect(x => x.Method2())
                    .AssertSignal("given signal", _signals);

                _mock1.Property = "expected value";
                _mock2.Method2();
            }

            [Fact]
            [ExpectedException(typeof(SignalAssertionException))]
            public void Throws_SignalAssertionException_when_signal_is_set_for_property_setter_but_is_called_with_wrong_argument()
            {
                _mock1.Expect(x => x.Property)
                    .SetPropertyWithArgument("expected value")
                    .Signal("given signal", _signals);

                _mock2.Expect(x => x.Method2())
                    .AssertSignal("given signal", _signals);

                _mock1.Property = "wrong value";
                _mock2.Method2();
            }

            [Fact]
            [ExpectedException(typeof(SignalAssertionException), ExpectedMessage = "test signal name", MatchType = MessageMatch.Contains)]
            public void SignalAssertionException_message_includes_signal_name()
            {
                _mock1.Expect(x => x.Property)
                    .SetPropertyWithArgument("expected value")
                    .Signal("test signal name", _signals);

                _mock2.Expect(x => x.Method2())
                    .AssertSignal("test signal name", _signals);

                _mock1.Property = "wrong value";
                _mock2.Method2();
            }

            public interface ISomeInterface
            {
                string Property { get; set; }
                void Method1();
                void Method1(string argument);
            }

            public interface ISomeOtherInterface
            {
                void Method2();
            }
        }
    }
}