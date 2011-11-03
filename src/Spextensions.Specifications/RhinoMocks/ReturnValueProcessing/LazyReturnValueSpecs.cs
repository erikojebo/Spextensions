using NUnit.Framework;
using Rhino.Mocks;
using Spextensions.Specifications.RhinoMocks.Dummies;

namespace Spextensions.Specifications.RhinoMocks.ReturnValueProcessing
{
    [TestFixture]
    public class LazyReturnValueSpecs
    {
        private ISomeInterface _stub;
        private LazyReturnValue<int> _returnValue;

        [SetUp]
        public void SetUp()
        {
            _stub = MockRepository.GenerateStub<ISomeInterface>();

            _returnValue = LazyReturnValue.Create(1);
            _stub.Stub(x => x.Function()).LazyReturnValue(_returnValue);

        }

        [Test]
        public void Lazily_returning_constant_value_returns_the_constant_value()
        {
            var actualReturnValue = _stub.Function();

            Assert.AreEqual(1, actualReturnValue);
        }

        [Test]
        public void Changing_lazily_returned_value_returns_the_new_value()
        {
            _returnValue.Value = 2;

            var actualReturnValue = _stub.Function();

            Assert.AreEqual(2, actualReturnValue);
        }
    }
}