namespace Rhino.Mocks
{
    public interface IValueProvider<out T>
    {
        T Next();
    }
}