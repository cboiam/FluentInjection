using System.Collections.Generic;
using ImplementationDiscovery.Extensions.UnitTest.Interfaces;

namespace ImplementationDiscovery.Extensions.UnitTest.Implementations
{
    public class ImplementationWithGenericA : IInterfaceWithGeneric<string, int>,
        IInterfaceWithGeneric<IEnumerable<string>, IEnumerable<int>>
    {
        public IEnumerable<string> Invoke(IEnumerable<int> type)
        {
            throw new System.NotImplementedException();
        }

        public string Invoke(int type)
        {
            throw new System.NotImplementedException();
        }
    }
}