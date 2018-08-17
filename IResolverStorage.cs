using System;
using SerializeMachine.Core;

namespace SerializeMachine
{
    public interface IResolverStorage : IReadOnlyResolverStorage
    {
        void AddResolver(IResolver resolver, string convention);
    }
}
