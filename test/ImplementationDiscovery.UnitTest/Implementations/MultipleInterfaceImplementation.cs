using ImplementationDiscovery.Extensions.UnitTest.Interfaces;

namespace ImplementationDiscovery.Extensions.UnitTest.Implementations
{
    public class MultipleInterfaceImplementation : IMultipleInterface
    {
        public object X => throw new System.NotImplementedException();

        public long Get()
        {
            throw new System.NotImplementedException();
        }
    }
}