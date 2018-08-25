using System;
using System.Collections.Generic;
using SerializeMachine.Core;
using SerializeMachine.Resolvers.Primitives;

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

        public static void AttachPrimitiveTypesResolvers(Serializator targetSerializator)
        {
            var typeManager = targetSerializator.TypeManager;
            var storage = targetSerializator.ResolverBank.Storage;

            storage.AddResolver(new BooleanResolver(),typeManager.ConventionOf(typeof(bool)));
            storage.AddResolver(new ByteResolver(), typeManager.ConventionOf(typeof(byte)));
            storage.AddResolver(new SByteResolver(), typeManager.ConventionOf(typeof(sbyte)));
            storage.AddResolver(new CharResolver(), typeManager.ConventionOf(typeof(char)));
            storage.AddResolver(new DecimalResolver(), typeManager.ConventionOf(typeof(decimal)));
            storage.AddResolver(new DoubleResolver(), typeManager.ConventionOf(typeof(double)));
            storage.AddResolver(new SingleResolver(), typeManager.ConventionOf(typeof(float)));
            storage.AddResolver(new Int32Resolver(), typeManager.ConventionOf(typeof(int)));
            storage.AddResolver(new UInt32Resolver(), typeManager.ConventionOf(typeof(uint)));
            storage.AddResolver(new Int64Resolver(), typeManager.ConventionOf(typeof(long)));
            storage.AddResolver(new UInt64Resolver(), typeManager.ConventionOf(typeof(ulong)));
            storage.AddResolver(new ObjectResolver(), typeManager.ConventionOf(typeof(object)));
            storage.AddResolver(new Int16Resolver(), typeManager.ConventionOf(typeof(short)));
            storage.AddResolver(new UInt16Resolver(), typeManager.ConventionOf(typeof(ushort)));
            storage.AddResolver(new StringResolver(), typeManager.ConventionOf(typeof(string)));
        }
        
    }
}
