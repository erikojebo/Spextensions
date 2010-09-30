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
    public class EmptyReturnSequenceSpecs
    {
        private ISomeInterface _stub;
        private ReturnValueSequence<int> _emptySequence;

        [SetUp]
        public void SetUp()
        {
            _stub = MockRepository.GenerateStub<ISomeInterface>();
            _emptySequence = new ReturnValueSequence<int>();
        }

        [Fact]
        [ExpectedException(typeof(OutOfReturnValuesException))]
        public void Throws_OutOfReturnValuesException_when_expectation_is_matched()
        {
            _stub.Stub(x => x.Function()).ReturnSequence(_emptySequence);

            _stub.Function();
        }
    }
}