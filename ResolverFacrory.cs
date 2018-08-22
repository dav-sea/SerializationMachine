using System;
using System.Collections.Generic;
using SerializeMachine.Core;
using System.Reflection;
using SerializeMachine.Resolvers;

namespace SerializeMachine
{
    public sealed class ResolverFacrory
    {
        private readonly SortedList<string, ConstructorInfo> Constructors;

        private readonly Serializator Serializator;

        public IResolver CreateResolver(string convention)
        {
            return new RuntimeResolver(Serializator.TypeManager.TypeOf(convention), Serializator);
        }

        public ResolverFacrory(Serializator serializator)
        {
            this.Serializator = serializator;
            Constructors = new SortedList<string, ConstructorInfo>(10);
        }
    }
}
