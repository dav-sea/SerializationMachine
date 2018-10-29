using System;
using System.Collections.Generic;
using System.Xml.Linq;
using SerializationMachine.Core;
using SerializationMachine.Utility;

namespace SerializationMachine.Resolvers
{
    public abstract class Resolver : IResolver
    {
        protected readonly Serializator Serializator;
        private readonly ITemplateInstanceFactory TemplateInstanceFactory;

        public Resolver(ITemplateInstanceFactory instanceFactory, Type resolveType, Serializator serializator)
            :base(resolveType)
        {
            this.Serializator = serializator;
            this.TemplateInstanceFactory = instanceFactory;
        }
        public Resolver(Type resolveType,bool useConstructor, Serializator serializator)
            : base(resolveType)
        {
            this.Serializator = serializator;
            if (useConstructor)
                TemplateInstanceFactory = new ConstructorInstanceFactory(resolveType);
            else
                TemplateInstanceFactory = new UninitializedInstanceFactory(resolveType);
        }
        public Resolver(Type resolveType, Func<XElement,object> customFactory, Serializator serializator)
            : base(resolveType)
        {
            this.Serializator = serializator;
            TemplateInstanceFactory = new FuncInstanceFactory(customFactory);
        }

        protected internal override sealed object GetTemplateInstance(XElement serializedObject)
        {
            return TemplateInstanceFactory.Instantiate(serializedObject);
        }
    }
}
