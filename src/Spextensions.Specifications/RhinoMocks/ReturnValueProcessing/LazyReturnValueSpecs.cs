using NUnit.Framework;
using Rhino.Mocks;
using Spextensions.Specifications.RhinoMocks.Dummies;

namespace Spextensions.Specifications.RhinoMocks.ReturnValueProcessing
{
    [TestFixture]
    public class LazyReturnValueSpecs
    {
        private ISomeInterface _mock1;

        [SetUp]
        public void SetUp()
        {
            _mock1 = MockRepository.GenerateMock<ISomeInterface>();
        }

        [Test]
        public void Lazily_returning_constant_value_returns_the_constant_value()
        {
            _mock1.Stub(x => x.Function()).LazyReturnValue(LazyReturnValue.Create(1));

            var actualReturnValue = _mock1.Function();

            Assert.AreEqual(1, actualReturnValue);
        }

        [Test]
        public void Changing_lazily_returned_value_returns_the_new_value()
        {
            var lazyReturnValue = LazyReturnValue.Create(1);

            _mock1.Stub(x => x.Function()).LazyReturnValue(lazyReturnValue);

            lazyReturnValue.Value = 2;

            var actualReturnValue = _mock1.Function();

            Assert.AreEqual(2, actualReturnValue);
        }
    }
}