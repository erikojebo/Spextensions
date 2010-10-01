using System;

namespace Spextensions.RhinoMocks
{
    public interface IValueProvider<out T>
    {
        T Next();
    }
}