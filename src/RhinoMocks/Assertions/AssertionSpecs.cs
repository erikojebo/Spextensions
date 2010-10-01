using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Spextensions.NUnit;
using Spextensions.RhinoMocks.Dummies;
using Is = Rhino.Mocks.Constraints.Is;

namespace Spextensions.RhinoMocks
{
    [TestFixture]
    public class AssertionSpecs
    {
        private ISomeInterface _mock;

        [SetUp]
        public void SetUp()
        {
            _mock = MockRepository.GenerateMock<ISomeInterface>();
        }

        [Fact]
        [ExpectedException(typeof(AssertionException))]
        public void AssertThat_throws_AssertionException_when_condition_is_not_met_at_match()
        {
            _mock.Expect(x => x.Method())
                .AssertThat(() => false);

            _mock.Method();
        }
        
        [Fact]
        public void AssertThat_does_not_throw_AssertionException_when_condition_is_met_at_match()
        {
            _mock.Expect(x => x.Method())
                .AssertThat(() => true);

            _mock.Method();
        }
        
        [Fact]
        public void AssertThat_does_not_throw_AssertionException_when_condition_is_met_after_setup_but_before_match()
        {
            bool condition = false;

            _mock.Expect(x => x.Method())
                .AssertThat(() => condition);

            condition = true;

            _mock.Method();
        }

        [Fact]
        [ExpectedException(typeof(AssertionException), ExpectedMessage = "expected message", MatchType = MessageMatch.Exact)]
        public void AssertThat_throws_AssertionException_containing_given_message()
        {
            _mock.Expect(x => x.Method())
                .AssertThat(() => false, "expected message");

            _mock.Method();
        }
    }
}