using System;
using System.Collections.Generic;
using SerializeMachine.Core;

namespace SerializeMachine
{
    public sealed class ResolverStorage : IResolverStorage
    {
        private readonly TypeDictionary TypeDictionary;
        private readonly SortedList<string, IResolver> ResolverList;

        public void AddResolver(IResolver resolver, string convention)
        {
            ResolverList.Add(convention, resolver);
        }

        public Core.IResolver GetResolver(string convention)
        {
            IResolver result;
            ResolverList.TryGetValue(convention, out result);
            return result;
        }

        public TypeDictionary GetTypeDictionary()
        {
            return TypeDictionary;
        }

        public ResolverStorage(TypeDictionary typeDictionary)
        {
            this.TypeDictionary = typeDictionary;
            this.ResolverList = new SortedList<string, IResolver>(typeDictionary.Capacity);
        }
    }
}
