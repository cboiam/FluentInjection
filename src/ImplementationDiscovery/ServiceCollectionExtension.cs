using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace ImplementationDiscovery.Extensions
{
    public static class ServiceCollectionExtension
    {
        private const string NotInterfaceMessage = "The service for type {0} is not supported due to not being an interface.";

        /// <summary>
        /// Map the implementations of provided interface.
        /// </summary>
        /// <typeparam name="TInterface">Interface for implementation discovery</typeparam>
        /// <param name="services"></param>
        /// <returns>Mapped services.</returns>
        public static ServiceAggregator MapImplementationsOf<TInterface>(this IServiceCollection services)
            => GetImplementations(services, typeof(TInterface), Assembly.GetCallingAssembly());

        /// <summary>
        /// Map the implementations of provided interface.
        /// </summary>
        /// <typeparam name="TInterface">Interface for implementation discovery</typeparam>
        /// <param name="services"></param>
        /// <returns>Mapped services.</returns>
        public static ServiceAggregator MapImplementationsOf(this IServiceCollection services, Type abstractionType)
            => GetImplementations(services, abstractionType, Assembly.GetCallingAssembly());

        /// <summary>
        /// Register mapped services as transient
        /// </summary>
        /// <param name="injectionData">Mapped services</param>
        /// <returns></returns>
        public static IServiceCollection AsTransient(this ServiceAggregator injectionData)
            => Inject(injectionData, ServiceLifetime.Transient);

        /// <summary>
        /// Register mapped services as scoped
        /// </summary>
        /// <param name="injectionData">Mapped services</param>
        /// <returns></returns>
        public static IServiceCollection AsScoped(this ServiceAggregator injectionData)
            => Inject(injectionData, ServiceLifetime.Scoped);

        /// <summary>
        /// Register mapped services as singleton
        /// </summary>
        /// <param name="injectionData">Mapped services</param>
        /// <returns></returns>
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
