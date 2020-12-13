using System;
using System.Collections.Generic;
using FluentAssertions;
using FluentInjection.Extensions.UnitTest.Implementations;
using FluentInjection.Extensions.UnitTest.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FluentInjection.Extensions.UnitTest
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
            services.RegisterImplementationsOf<IBasicInterface>().AsSingleton();
            
            var provider = services.BuildServiceProvider();
            
            provider.GetService<IBasicInterface>()
                .Should().BeOfType<BasicImplementation>();
        }

        [Fact]
        public void GivenIRegisteredAllBasicInterfaces_WhenIGetFromInjectedInterface_ThenIShouldGetAllTheImplementations()
        {
            services.RegisterImplementationsOf<IMultipleInterface>().AsTransient();
            
            var provider = services.BuildServiceProvider();
            
            provider.GetService<IEnumerable<IMultipleInterface>>()
                .Should().Contain(x => x.GetType() == typeof(MultipleInterfaceImplementation))
                    .And.Contain(x => x.GetType() == typeof(MultipleInterfaceImplementation2));
        }

        [Fact]
        public void GivenIRegisteredAllInterfacesWithGenerics_WhenIGetFromInjectedInterface_ThenIShouldGetAllTheImplementations()
        {
            services.RegisterImplementationsOf(typeof(IInterfaceWithGeneric<,>)).AsScoped();
            
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
            Action act = () => services.RegisterImplementationsOf<BasicImplementation>();
            act.Should().Throw<NotSupportedException>()
                .WithMessage("The service for type FluentInjection.Extensions.UnitTest.Implementations.BasicImplementation is not supported due to not being an interface.");
        }

        [Fact]
        public void GivenAServiceCollection_WhenITryToInjectANullServiceType_ThenAnExceptionShouldBeThrown()
        {
            Action act = () => services.RegisterImplementationsOf(null);
            act.Should().Throw<ArgumentNullException>()
                .WithMessage("Value cannot be null. (Parameter 'serviceType')");
        }
    }
}
