using NUnit.Framework;
using Rhino.Mocks;
using Spextensions.NUnit;
using Spextensions.Specifications.RhinoMocks.Dummies;

namespace Spextensions.Specifications.RhinoMocks.ReturnValueProcessing
{
    [TestFixture]
    public class ReturnSequenceSpecs
    {
        private ISomeInterface _stub;
        private IValueProvider<int> _stubValueProvider;
        private const int ExpectedReturnValue = 1;

        [SetUp]
        public void SetUp()
        {
            _stub = MockRepository.GenerateStub<ISomeInterface>();
            _stubValueProvider = MockRepository.GenerateStub<IValueProvider<int>>();
            _stubValueProvider.Stub(x => x.Next()).Return(ExpectedReturnValue);
        }

        [Fact]
        public void Returns_next_value_from_value_provider_when_expectation_on_method_without_arguments_is_matched()
        {
            _stub.Stub(x => x.Function()).ReturnSequence(_stubValueProvider);

            var actualValue = _stub.Function();

            Assert.AreEqual(ExpectedReturnValue, actualValue);
        }

        [Fact]
        public void Returns_next_value_from_value_provider_when_expectation_on_method_with_one_argument_is_matched()
        {
            _stub.Stub(x => x.FunctionWithOneParameter(0)).ReturnSequence(_stubValueProvider);

            var actualValue = _stub.FunctionWithOneParameter(0);

            Assert.AreEqual(ExpectedReturnValue, actualValue);
        }

        [Fact]
        public void Returns_next_value_from_value_provider_when_expectation_on_method_with_two_arguments_is_matched()
        {
            _stub.Stub(x => x.FunctionWithTwoParameters(0, false)).ReturnSequence(_stubValueProvider);

            var actualValue = _stub.FunctionWithTwoParameters(0, false);

            Assert.AreEqual(ExpectedReturnValue, actualValue);
        }

        [Fact]
        public void Returns_next_value_from_value_provider_when_expectation_on_method_with_three_arguments_is_matched()
        {
            _stub.Stub(x => x.FunctionWithThreeParameters(0, false, "")).ReturnSequence(_stubValueProvider);

            var actualValue = _stub.FunctionWithThreeParameters(0, false, "");

            Assert.AreEqual(ExpectedReturnValue, actualValue);
        }

        [Fact]
        public void Returns_specified_return_values_in_expected_order()
        {
            var valueProvider = new Sequence<int>(1, 2, 3);

            _stub.Stub(x => x.Function()).ReturnSequence(valueProvider);

            Assert.AreEqual(1, _stub.Function());
            Assert.AreEqual(2, _stub.Function());
            Assert.AreEqual(3, _stub.Function());
        }
    }
}