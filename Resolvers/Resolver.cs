using System;
using System.Collections.Generic;
using SerializationMachine.Core;
using SerializationMachine.Utility;

namespace SerializationMachine.Resolvers
{
    public abstract class Resolver : IResolver
    {
        protected readonly Serializator Serializator;
        private readonly IFactory InstanceFactory;

        public Resolver(IFactory instanceFactory, Type resolveType, Serializator serializator)
            :base(resolveType)
        {
            this.Serializator = serializator;
            this.InstanceFactory = instanceFactory;
        }
        public Resolver(Type resolveType,bool useConstructor, Serializator serializator)
            : base(resolveType)
        {
            this.Serializator = serializator;
            if (useConstructor)
                InstanceFactory = FactoryUtility.CreateConstructorFactory(resolveType);
            else
                InstanceFactory = FactoryUtility.CreateUninitializedFactory(resolveType);
        }
        public Resolver(Type resolveType, Func<object> customFactory, Serializator serializator)
            : base(resolveType)
        {
            this.Serializator = serializator;
            InstanceFactory = FactoryUtility.CreateCustomFactory(customFactory);
        }

        protected internal override sealed object ManagedObjectOf(System.Xml.Linq.XElement serializedObject)
        {
            return InstanceFactory.Instantiate();
        }
    }
}
