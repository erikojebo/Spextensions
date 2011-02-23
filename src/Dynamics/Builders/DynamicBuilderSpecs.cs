using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Is = Rhino.Mocks.Constraints.Is;

namespace Spextensions.Dynamics.Builders
{
    [TestFixture]
    public class DynamicBuilderSpecs
    {
        private dynamic _builder;

        [SetUp]
        public void SetUp()
        {
            _builder = new DynamicBuilder<SomeClass>();            
        }

        [Test]
        public void Sets_property_with_given_name_to_given_value()
        {
            var actual = _builder
                .SomeInt(1)
                .Build();

            Assert.AreEqual(1, actual.SomeInt);
        }

        [Test]
        public void Throws_when_trying_to_set_non_existing_property()
        {
            Assert.Throws<MissingPropertyException>(() => _builder.NonExistingProperty(1));
        }

        private class SomeClass
        {
            public int SomeInt { get; set; }
        }
    }
}