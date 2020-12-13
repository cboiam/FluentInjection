using FluentInjection.Extensions.UnitTest.Interfaces;

namespace FluentInjection.Extensions.UnitTest.Implementations
{
    public class MultipleInterfaceImplementation2 : IMultipleInterface
    {
        public object X => throw new System.NotImplementedException();

        public long Get()
        {
            throw new System.NotImplementedException();
        }
    }
}