using System;

namespace Spextensions.RhinoMocks.Debugging
{
    public class ConsoleInvocationInfoRenderer : InvocationInfoRenderer
    {
        protected override void OnRender()
        {
            Console.WriteLine(CreateInfoStringForLastParameterList());
        }
    }
}