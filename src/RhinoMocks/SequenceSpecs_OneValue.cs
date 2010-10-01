using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Spextensions.NUnit;
using Spextensions.RhinoMocks.Exceptions;
using Is = Rhino.Mocks.Constraints.Is;

namespace Spextensions.RhinoMocks
{
    [TestFixture]
    public class SequenceSpecs_OneValue
    {
        private Sequence<int> _sequenceWithOneValue;

        [SetUp]
        public void SetUp()
        {
            _sequenceWithOneValue = new Sequence<int>(1);
        }

        [Fact]
        [ExpectedException(typeof(OutOfReturnValuesException))]
        public void Throws_OutOfArgumentsException_when_asked_for_next_value_twice()
        {
            _sequenceWithOneValue.Next();
            _sequenceWithOneValue.Next();
        }

        [Fact]
        public void Is_empty_after_being_asked_for_next_value_once()
        {
            _sequenceWithOneValue.Next();

            Assert.IsTrue(_sequenceWithOneValue.IsEmpty);
        }

        [Fact]
        public void Is_not_empty()
        {
            Assert.IsFalse(_sequenceWithOneValue.IsEmpty);
        }

        [Fact]
        public void Returns_value_when_asked_for_next_value_once()
        {
            var actualValue = _sequenceWithOneValue.Next();

            Assert.AreEqual(1, actualValue);
        }
    }
}