using FluentInjection.Extensions.UnitTest.Interfaces;

namespace FluentInjection.Extensions.UnitTest.Implementations
{
    public class MultipleInterfaceImplementation : IMultipleInterface
    {
        public object X => throw new System.NotImplementedException();

        public long Get()
        {
            throw new System.NotImplementedException();
        }
    }
}