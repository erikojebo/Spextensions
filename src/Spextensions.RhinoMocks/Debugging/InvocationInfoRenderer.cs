using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spextensions.RhinoMocks.Debugging
{
    public abstract class InvocationInfoRenderer : IInvocationInfoRenderer
    {
        private readonly List<object[]> _invocationParameterLists = new List<object[]>();

        public IEnumerable<object[]> InvocationParameterLists
        {
            get { return _invocationParameterLists; }
        }

        public void Render(params object[] values)
        {
            _invocationParameterLists.Add(values);

            OnRender();
        }

        public string MethodName { get; set; }

        protected virtual void OnRender() {}

        protected string CreateInfoStringForLastParameterList()
        {
            return CreateInfoString(InvocationParameterLists.Last());
        }

        protected string CreateInfoString(object[] invocationParameterList)
        {
            var nullReplacedParameterList = invocationParameterList.Select(ValueOrNullString);

            var parameterList = string.Join(", ", nullReplacedParameterList);
            var message = string.Format(
                "Stubbed/expected method '{0}' was called with the following parameters: "
                + parameterList, MethodName);

            return message;
        }

        private object ValueOrNullString(object value)
        {
            if (value == null)
            {
                return "null";
            }

            return value;
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            foreach (var invocationParameterList in InvocationParameterLists)
            {
                stringBuilder.AppendLine(CreateInfoString(invocationParameterList));
            }

            return stringBuilder.ToString();
        }
    }
}