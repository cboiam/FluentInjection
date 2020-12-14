using System;
using ImplementationDiscovery.Extensions.UnitTest.Interfaces;

namespace ImplementationDiscovery.Extensions.UnitTest.Implementations
{
    public class ImplementationWithGenericB : IInterfaceWithGeneric<object, DateTime>
    {
        public object Invoke(DateTime type)
        {
            throw new NotImplementedException();
        }
    }
}