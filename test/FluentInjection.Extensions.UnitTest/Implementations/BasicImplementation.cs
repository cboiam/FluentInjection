using FluentInjection.Extensions.UnitTest.Interfaces;

namespace FluentInjection.Extensions.UnitTest.Implementations
{
    public class BasicImplementation : IBasicInterface
    {
        public string A { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}