using System;
using System.Collections.Generic;
using SerializeMachine.Core;
using SerializeMachine.Utility;

namespace SerializeMachine.Resolvers
{
    public abstract class Resolver : IResolver
    {
        protected readonly Serializator Serializator;

        public Resolver(IFactory instanceFactory, Type resolveType, Serializator serializator)
            :base(instanceFactory,resolveType)
        {
            this.Serializator = serializator;
        }
        public Resolver(Type resolveType,bool useConstructor, Serializator serializator)
            : base(resolveType, useConstructor)
        {
            this.Serializator = serializator;
        }
        public Resolver(Type resolveType, Func<object> customFactory, Serializator serializator)
            : base(resolveType,customFactory)
        {
            this.Serializator = serializator;
        }
    }
}
