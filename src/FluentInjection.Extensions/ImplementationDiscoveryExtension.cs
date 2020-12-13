using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace FluentInjection.Extensions
{
    public static class ImplementationDiscoveryExtension
    {
        private const string NotInterfaceMessage = "The service for type {0} is not supported due to not being an interface.";

        public static ServiceAggregator RegisterImplementationsOf<TAbstraction>(this IServiceCollection services)
            => GetImplementations(services, typeof(TAbstraction), Assembly.GetCallingAssembly());

        public static ServiceAggregator RegisterImplementationsOf(this IServiceCollection services, Type abstractionType)
            => GetImplementations(services, abstractionType, Assembly.GetCallingAssembly());

        public static IServiceCollection AsTransient(this ServiceAggregator injectionData)
            => Inject(injectionData, ServiceLifetime.Transient);

        public static IServiceCollection AsScoped(this ServiceAggregator injectionData)
            => Inject(injectionData, ServiceLifetime.Scoped);

        public static IServiceCollection AsSingleton(this ServiceAggregator injectionData)
            => Inject(injectionData, ServiceLifetime.Singleton);

        private static ServiceAggregator GetImplementations(IServiceCollection services, Type serviceType, Assembly assembly)
        {
            if(serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (!serviceType.IsInterface)
            {
                throw new NotSupportedException(string.Format(NotInterfaceMessage, serviceType.FullName));
            }

            IEnumerable<MappedImplementation> mappedImplementations = assembly.GetTypes()
                .Where(type => !type.IsInterface &&
                    !type.IsAbstract &&
                    type.GetInterfaces().Any(service => service.Name == serviceType.Name))
                .SelectMany(type => type.GetInterfaces()
                    .Where(service => service.Name == serviceType.Name)
                    .Select(service => new MappedImplementation(service, type)));

            return new ServiceAggregator(services, mappedImplementations);
        }

        private static IServiceCollection Inject(ServiceAggregator injectionData, ServiceLifetime serviceLifetime)
        {
            foreach (MappedImplementation item in injectionData.mappedImplementations)
            {
                injectionData.services.Add(new ServiceDescriptor(item.abstraction, item.implementation, serviceLifetime));
            }
            return injectionData.services;
        }
    }
}
