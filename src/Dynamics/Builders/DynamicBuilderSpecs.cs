using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Spextensions.Dynamics.Builders.Exceptions;

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

        [Test]
        public void Adds_child_when_calling_singular_collection_name_method_with_child_instance()
        {
            SomeClass actual = _builder
                .SubclassObject(new PropertyValueCollector())
                .Build();

            Assert.AreEqual(1, actual.SubclassObjects.Count);
        }

        [Test]
        public void Adds_child_when_calling_singular_collection_name_method_with_property_value_collector()
        {
            var expectedChild = new SubclassObject();

            SomeClass actual = _builder
                .SubclassObject(expectedChild)
                .Build();

            var actualChild = actual.SubclassObjects.FirstOrDefault();

            Assert.AreEqual(1, actual.SubclassObjects.Count);
            Assert.AreSame(expectedChild, actualChild);
        }

        [Ignore]
        [Test]
        public void Adds_child_with_properties_set_on_added_instance_when_adding_plural_s_results_in_collection_name()
        {
            dynamic collector = new PropertyValueCollector();

            SomeClass actual = _builder
                .SubclassObject(collector.SomeString("string").SomeDouble(1.23))
                .Build();

            var actualChild = actual.SubclassObjects.FirstOrDefault();

            Assert.AreEqual(1, actual.SubclassObjects.Count);
            Assert.AreEqual("string", actualChild.SomeString);
            Assert.AreEqual(1.23, actualChild.SomeDouble);
        }

        private class SomeClass
        {
            public SomeClass()
            {
                SubclassObjects = new List<SubclassObject>();
            }

            public int SomeInt { get; set; }
            public IList<SubclassObject> SubclassObjects { get; set; }
        }

        private class SubclassObject
        {
            public string SomeString { get; set; }
            public string SomeDouble { get; set; }
        }
    }
}