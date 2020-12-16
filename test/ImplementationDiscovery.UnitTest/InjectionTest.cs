using System;
using System.Collections.Generic;
using FluentAssertions;
using ImplementationDiscovery.Extensions.UnitTest.Implementations;
using ImplementationDiscovery.Extensions.UnitTest.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ImplementationDiscovery.Extensions.UnitTest
{
    public class InjectionTest
    {
        private readonly ServiceCollection services;

        public InjectionTest()
        {
            services = new ServiceCollection();
        }

        [Fact]
        public void GivenIRegisteredBasicInterface_WhenIGetFromInjectedInterface_ThenIShouldGetTheImplementation()
        {
            services.MapImplementationsOf<IBasicInterface>().AsSingleton();
            
            var provider = services.BuildServiceProvider();
            
            provider.GetService<IBasicInterface>()
                .Should().BeOfType<BasicImplementation>();
        }

        [Fact]
        public void GivenIRegisteredAllBasicInterfaces_WhenIGetFromInjectedInterface_ThenIShouldGetAllTheImplementations()
        {
            services.MapImplementationsOf<IMultipleInterface>().AsTransient();
            
            var provider = services.BuildServiceProvider();
            
            provider.GetService<IEnumerable<IMultipleInterface>>()
                .Should().Contain(x => x.GetType() == typeof(MultipleInterfaceImplementation))
                    .And.Contain(x => x.GetType() == typeof(MultipleInterfaceImplementation2));
        }

        [Fact]
        public void GivenIRegisteredAllInterfacesWithGenerics_WhenIGetFromInjectedInterface_ThenIShouldGetAllTheImplementations()
        {
            services.MapImplementationsOf(typeof(IInterfaceWithGeneric<,>)).AsScoped();
            
            var provider = services.BuildServiceProvider();
            
            provider.GetService<IInterfaceWithGeneric<string, int>>()
                .Should().BeOfType<ImplementationWithGenericA>();

            provider.GetService<IInterfaceWithGeneric<IEnumerable<string>, IEnumerable<int>>>()
                .Should().BeOfType<ImplementationWithGenericA>();

            provider.GetService<IInterfaceWithGeneric<object, DateTime>>()
                .Should().BeOfType<ImplementationWithGenericB>();
        }

        [Fact]
        public void GivenAServiceCollection_WhenITryToInjectAConcreteClass_ThenAnExceptionShouldBeThrown()
        {
            Action act = () => services.MapImplementationsOf<BasicImplementation>();
            act.Should().Throw<NotSupportedException>()
                .WithMessage("The service for type ImplementationDiscovery.Extensions.UnitTest.Implementations.BasicImplementation is not supported due to not being an interface.");
        }

        [Fact]
        public void GivenAServiceCollection_WhenITryToInjectANullServiceType_ThenAnExceptionShouldBeThrown()
        {
            Action act = () => services.MapImplementationsOf(null);
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'serviceType')");
        }
    }
}
