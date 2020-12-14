using ImplementationDiscovery.Extensions.UnitTest.Interfaces;

namespace ImplementationDiscovery.Extensions.UnitTest.Implementations
{
    public class BasicImplementation : IBasicInterface
    {
        public string A { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}