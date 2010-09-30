using System;
using NUnit.Framework;
using Rhino.Mocks;
using Spextensions.NUnit;
using Spextensions.RhinoMocks.Dummies;

namespace Spextensions.RhinoMocks
{
    [TestFixture]
    public class ReturnSequenceWithOneValueSpecs
    {
        private ISomeInterface _stub;
        private ReturnValueSequence<int> _sequence;
        const int ExpectedReturnValue = 1;

        [SetUp]
        public void SetUp()
        {
            _stub = MockRepository.GenerateStub<ISomeInterface>();
            _sequence = new ReturnValueSequence<int>(ExpectedReturnValue);
        }

        [Fact]
        public void Returns_given_value_when_expectation_on_method_without_parameters_is_matched()
        {
            _stub.Stub(x => x.Function()).ReturnSequence(_sequence);

            var actualReturnValue = _stub.Function();

            Assert.AreEqual(ExpectedReturnValue, actualReturnValue);
        }

        [Fact]
        public void Returns_given_value_when_expectation_on_method_with_one_parameters_is_matched()
        {
            _stub.Stub(x => x.FunctionWithOneParameter(0)).ReturnSequence<int, int>(_sequence);

            var actualReturnValue = _stub.FunctionWithOneParameter(0);

            Assert.AreEqual(ExpectedReturnValue, actualReturnValue);
        }

        [Fact]
        public void Returns_given_value_when_expectation_on_method_with_two_parameters_is_matched()
        {
            _stub.Stub(x => x.FunctionWithTwoParameters(0, false)).ReturnSequence<int, bool, int>(_sequence);

            var actualReturnValue = _stub.FunctionWithTwoParameters(0, false);

            Assert.AreEqual(ExpectedReturnValue, actualReturnValue);
        }
    }

    public class OutOfReturnValuesException : Exception {}
}