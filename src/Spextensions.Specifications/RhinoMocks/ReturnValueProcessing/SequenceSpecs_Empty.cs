using NUnit.Framework;
using Spextensions.NUnit;
using Rhino.Mocks;

namespace Spextensions.Specifications.RhinoMocks.ReturnValueProcessing
{
    [TestFixture]
    public class SequenceSpecs_Empty
    {
        private Sequence<int> _emptySequence;

        [SetUp]
        public void SetUp()
        {
            _emptySequence = new Sequence<int>();
        }

        [Fact]
        [ExpectedException(typeof(OutOfReturnValuesException))]
        public void Throws_OutOfArgumentsException_when_asked_for_next_value()
        {
            _emptySequence.Next();
        }

        [Fact]
        public void Is_empty()
        {
            Assert.IsTrue(_emptySequence.IsEmpty);
        }
    }
}