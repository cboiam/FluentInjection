using System;
using FluentInjection.Extensions.UnitTest.Interfaces;

namespace FluentInjection.Extensions.UnitTest.Implementations
{
    public class ImplementationWithGenericB : IInterfaceWithGeneric<object, DateTime>
    {
        public object Invoke(DateTime type)
        {
            throw new NotImplementedException();
        }
    }
}