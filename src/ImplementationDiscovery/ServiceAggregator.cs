using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace ImplementationDiscovery.Extensions
{
    public class ServiceAggregator
    {
        public readonly IServiceCollection services;
        internal readonly IEnumerable<MappedImplementation> mappedImplementations;

        internal ServiceAggregator(IServiceCollection services, IEnumerable<MappedImplementation> mappedImplementations)
        {
            this.services = services;
            this.mappedImplementations = mappedImplementations;
        }
    }
}