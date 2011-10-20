using NUnit.Framework;
using Rhino.Mocks;
using Spextensions.RhinoMocks.Arguments;
using Spextensions.RhinoMocks.Exceptions;
using Spextensions.Specifications.RhinoMocks.Dummies;

namespace Spextensions.Specifications.RhinoMocks.Arguments
{
    [TestFixture]
    public class ArgumentConstraintSpecs
    {
        private ArgumentConstraint<string> _constraint;

        [SetUp]
        public void SetUp()
        {
            _constraint = new ArgumentConstraint<string>();
        }

        [Test]
        [ExpectedException(typeof(ArgumentNotFoundException))]
        public void Throws_ArgumentNotFoundException_if_no_parameters_are_supplied()
        {
            _constraint.FindMatch(new object[] { });
        }

        [Test]
        [ExpectedException(typeof(ArgumentNotFoundException))]
        public void Throws_ArgumentNotFoundException_if_no_parameters_exist_with_given_type()
        {
            _constraint.FindMatch(new object[] { 1, 2d});
        }

        [Test]
        [ExpectedException(typeof(ArgumentNotFoundException), ExpectedMessage = "String", MatchType = MessageMatch.Contains)]
        public void Throws_ArgumentNotFoundException_with_message_containing_type_name()
        {
            _constraint.FindMatch(new object[] {});
        }

        [Test]
        [ExpectedException(typeof(ArgumentNotFoundException))]
        public void Throws_ArgumentNotFoundException_if_match_index_is_too_large()
        {
            var constraint = new ArgumentConstraint<string>(2);
            constraint.FindMatch(new object[] { "0", "1" });            
        }

        [Test]
        [ExpectedException(typeof(ArgumentNotFoundException), ExpectedMessage = "index: 2", MatchType = MessageMatch.Contains)]
        public void Throws_ArgumentNotFoundException_containing_match_index_if_match_index_is_too_large()
        {
            var constraint = new ArgumentConstraint<string>(2);
            constraint.FindMatch(new object[] { "0", "1" });            
        }

        [Test]
        public void Captures_first_argument_of_matching_type_if_no_match_index_is_supplied()
        {
            var constraint = new ArgumentConstraint<string>();
            var match = constraint.FindMatch(new object[] { "0", "1" });

            Assert.AreEqual("0", match);
        }

        [Test]
        public void Captures_argument_with_given_match_index()
        {
            var constraint = new ArgumentConstraint<string>(1);
            var match = constraint.FindMatch(new object[] { "0", 0d, "1", 1d, "2", 2d });

            Assert.AreEqual("1", match);
        }
    }
}