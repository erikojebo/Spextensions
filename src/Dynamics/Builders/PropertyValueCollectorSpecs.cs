using NUnit.Framework;

namespace Spextensions.Dynamics.Builders
{
    [TestFixture]
    public class PropertyValueCollectorSpecs
    {
        private dynamic _collector;

        [SetUp]
        public void SetUp()
        {
            _collector = new PropertyValueCollector();
        }

        [Test]
        public void Stores_properties_and_values_set_on_object()
        {
            _collector.Property1("string")
                .Property2(1.23);

            Assert.AreEqual(2, _collector.PropertyValues.Count);
        }

        [Test]
        public void Overwrites_property_value_if_set_multiple_times()
        {
            _collector.Property(1);
            _collector.Property(2);

            Assert.AreEqual(2, _collector.PropertyValues["Property"]);
        }
    }
}