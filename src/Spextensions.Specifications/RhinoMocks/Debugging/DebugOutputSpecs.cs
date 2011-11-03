using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Spextensions.Specifications.RhinoMocks.Dummies;

namespace Spextensions.Specifications.RhinoMocks.Debugging
{
    [TestFixture]
    public class DebugOutputSpecs
    {
        private ISomeInterface _mock1;
        private InMemoryInvocationInfoRenderer _invocationInfoRenderer;

        [SetUp]
        public void SetUp()
        {
            _mock1 = MockRepository.GenerateMock<ISomeInterface>();
            _invocationInfoRenderer = new InMemoryInvocationInfoRenderer();
        }

        [Test]
        public void Does_not_write_any_invocation_information_if_method_is_never_called()
        {
            _mock1.Stub(x => x.FunctionWithThreeParameters(0, false, null))
                .IgnoreArgumentsAndWriteInvocationInfo(_invocationInfoRenderer)
                .Return(1);

            CollectionAssert.IsEmpty(_invocationInfoRenderer.InvocationParameterLists);
        }

        [Test]
        public void Adds_single_set_of_parameters_in_correct_order_when_called_with_correct_parameters()
        {
            _mock1.Stub(x => x.FunctionWithThreeParameters(1, true, "string"))
                .IgnoreArgumentsAndWriteInvocationInfo(_invocationInfoRenderer)
                .Return(1);

            _mock1.FunctionWithThreeParameters(1, true, "string");

            Assert.AreEqual(1, _invocationInfoRenderer.InvocationParameterLists.Count());
            CollectionAssert.AreEquivalent(
                new object[] {1, true, "string" }, 
                _invocationInfoRenderer.InvocationParameterLists.First());
        }

        [Test]
        public void Adds_single_set_of_parameters_in_correct_order_when_called_with_incorrect_parameters()
        {
            _mock1.Stub(x => x.FunctionWithThreeParameters(1, true, "string"))
                .IgnoreArgumentsAndWriteInvocationInfo(_invocationInfoRenderer)
                .Return(1);

            _mock1.FunctionWithThreeParameters(0, false, "other string");

            Assert.AreEqual(1, _invocationInfoRenderer.InvocationParameterLists.Count());
            CollectionAssert.AreEquivalent(
                new object[] { 0, false, "other string" },
                _invocationInfoRenderer.InvocationParameterLists.First());
        }
        
        [Test]
        public void Appends_one_set_of_parameters_for_each_invocation()
        {
            _mock1.Stub(x => x.FunctionWithThreeParameters(1, true, "string"))
                .IgnoreArgumentsAndWriteInvocationInfo(_invocationInfoRenderer)
                .Return(1);

            _mock1.FunctionWithThreeParameters(0, false, "other string");
            _mock1.FunctionWithThreeParameters(2, true, "yet another string");


            Assert.AreEqual(2, _invocationInfoRenderer.InvocationParameterLists.Count());

            CollectionAssert.AreEquivalent(
                new object[] { 0, false, "other string" },
                _invocationInfoRenderer.InvocationParameterLists.First());

            CollectionAssert.AreEquivalent(
                new object[] { 2, true, "yet another string" },
                _invocationInfoRenderer.InvocationParameterLists.Last());
        }

        [Test]
        public void String_representation_of_null_is_the_word_null()
        {
            _mock1.Stub(x => x.FunctionWithThreeParameters(1, true, "string"))
                .IgnoreArgumentsAndWriteInvocationInfo(_invocationInfoRenderer)
                .Return(1);

            _mock1.FunctionWithThreeParameters(0, false, null);

            Assert.That(_invocationInfoRenderer.ToString(), Is.StringContaining("0, False, null"));
        }
        
        [Test]
        public void String_representation_is_one_line_of_comma_separated_parameters_for_each_invocation()
        {
            _mock1.Stub(x => x.FunctionWithThreeParameters(1, true, "string"))
                .IgnoreArgumentsAndWriteInvocationInfo(_invocationInfoRenderer)
                .Return(1);

            _mock1.FunctionWithThreeParameters(0, false, "string");
            _mock1.FunctionWithThreeParameters(1, true, "other string");

            var renderedString = _invocationInfoRenderer.ToString();

            Assert.That(renderedString, Is.StringMatching(".*0, False, string.*\n.*1, True, other string"));
        }

        [Test]
        public void String_representation_contains_method_name()
        {
            _mock1.Stub(x => x.FunctionWithThreeParameters(1, true, "string"))
                .IgnoreArgumentsAndWriteInvocationInfo(_invocationInfoRenderer)
                .Return(1);

            _mock1.FunctionWithThreeParameters(0, false, "string");

            Assert.That(_invocationInfoRenderer.ToString(), Is.StringContaining("FunctionWithThreeParameters"));
        }
    }
}