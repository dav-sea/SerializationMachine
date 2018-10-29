using System;
using SerializationMachine.Core;

namespace SerializationMachine
{
    public interface IResolverStorage : IReadOnlyResolverStorage
    {
        void SetResolver(IResolver resolver, string convention);
    }
}
