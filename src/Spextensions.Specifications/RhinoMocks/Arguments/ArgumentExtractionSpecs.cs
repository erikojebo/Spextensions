using NUnit.Framework;
using Rhino.Mocks;
using Spextensions.Specifications.RhinoMocks.Dummies;

namespace Spextensions.Specifications.RhinoMocks.Arguments
{
    [TestFixture]
    public class ArgumentExtractionSpecs
    {
        private ISomeInterface _mock1;

        [SetUp]
        public void SetUp()
        {
            _mock1 = MockRepository.GenerateMock<ISomeInterface>();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNotFoundException), ExpectedMessage = "Double", MatchType = MessageMatch.Contains)]
        public void Throws_ArgumentNotFoundException_for_method_without_parameters()
        {
            double argumentValue;

            _mock1.Stub(x => x.Function())
                .CaptureFirstArgumentOfType(typeof(double), x => argumentValue = (double)x)
                .Return(0);

            _mock1.Function();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNotFoundException))]
        public void Throws_ArgumentNotFoundException_if_no_parameters_exist_with_given_type()
        {
            double argumentValue;

            _mock1.Stub(x => x.FunctionWithOneParameter(1))
                .CaptureFirstArgumentOfType(typeof(double), x => argumentValue = (double)x)
                .Return(0);

            _mock1.FunctionWithOneParameter(1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNotFoundException), ExpectedMessage = "Double", MatchType = MessageMatch.Contains)]
        public void Throws_ArgumentNotFoundException_with_message_containing_type_name_if_no_parameters_exist_with_given_type()
        {
            double argumentValue;

            _mock1.Stub(x => x.FunctionWithOneParameter(1))
                .CaptureFirstMatchingArgument(new ArgumentConstraint<double>(), x => argumentValue = x)
                .Return(0);

            _mock1.FunctionWithOneParameter(1);
        }

        [Test]
        public void Captures_argument_for_method_with_single_parameter_which_is_of_correct_type()
        {
            int argumentValue = -1;

            _mock1.Stub(x => x.FunctionWithOneParameter(1))
                .CaptureFirstMatchingArgument(new ArgumentConstraint<int>(), x => argumentValue = x)
                .Return(0);

            _mock1.FunctionWithOneParameter(1);

            Assert.AreEqual(1, argumentValue);
        }

        [Test]
        public void Captures_argument_of_correct_type_for_method_with_two_parameter()
        {
            string argumentValue = null;

            _mock1.Stub(x => x.FunctionWithTwoParametersOfSameType(null, null))
                .IgnoreArguments()
                .CaptureFirstMatchingArgument(new ArgumentConstraint<string>(), x => argumentValue = x)
                .Return(0);

            _mock1.FunctionWithTwoParametersOfSameType("string 1", "string 2");

            Assert.AreEqual("string 1", argumentValue);
        }

        [Test]
        public void Captures_argument_with_given_match_index_for_method_with_two_parameters()
        {
            string argumentValue = null;

            _mock1.Stub(x => x.FunctionWithTwoParametersOfSameType(null, null))
                .IgnoreArguments()
                .CaptureFirstMatchingArgument(new ArgumentConstraint<string>(matchIndex: 1), x => argumentValue = x)
                .Return(0);

            _mock1.FunctionWithTwoParametersOfSameType("string 1", "string 2");

            Assert.AreEqual("string 2", argumentValue);
        }

        [Test]
        public void Captures_argument_with_given_match_index_for_method_with_multiple_parameters_of_different_type()
        {
            string argumentValue = null;

            _mock1.Stub(x => x.FunctionWithFourParameters(0, false, null, null))
                .IgnoreArguments()
                .CaptureFirstMatchingArgument(new ArgumentConstraint<string>(matchIndex: 1), x => argumentValue = x)
                .Return(0);

            _mock1.FunctionWithFourParameters(1, true, "string 1", "string 2");

            Assert.AreEqual("string 2", argumentValue);
        }
    }
}