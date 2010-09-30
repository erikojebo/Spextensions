using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using Spextensions.NUnit;
using Is = Rhino.Mocks.Constraints.Is;

namespace Spextensions.RhinoMocks
{
    [TestFixture]
    public class ReturnQueueSpecs
    {
        [Fact]
        public void Empty_queue_throws_exception_when_expectation_is_matched()
        {
            var queue = new ReturnValueQueue();

        }
    }

    public class ReturnValueQueue {}
}