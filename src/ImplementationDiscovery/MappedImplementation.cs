using System;

namespace ImplementationDiscovery.Extensions
{
    internal class MappedImplementation
    {
        internal readonly Type abstraction;
        internal readonly Type implementation;

        public MappedImplementation(Type abstraction, Type implementation)
        {
            this.abstraction = abstraction;
            this.implementation = implementation;
        }
    }
}