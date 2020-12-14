namespace ImplementationDiscovery.Extensions.UnitTest.Interfaces
{
    public interface IInterfaceWithGeneric<T, U>
    {
        T Invoke(U type);
    }
}