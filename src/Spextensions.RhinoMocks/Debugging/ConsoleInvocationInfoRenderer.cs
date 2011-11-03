using System;
using Rhino.Mocks;

namespace Rhino.Mocks
{
    public class ConsoleInvocationInfoRenderer : InvocationInfoRenderer
    {
        protected override void OnRender()
        {
            Console.WriteLine(CreateInfoStringForLastParameterList());
        }
    }
}