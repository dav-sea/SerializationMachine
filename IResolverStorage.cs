using System;
using SerializeMachine.Core;

namespace SerializeMachine
{
    public interface IResolverStorage : IReadOnlyResolverStorage
    {
        void SetResolver(IResolver resolver, string convention);
    }
}
