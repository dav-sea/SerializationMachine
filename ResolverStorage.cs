using System;
using System.Collections.Generic;
using SerializeMachine.Core;
using SerializeMachine.Resolvers.Primitives;
using SerializeMachine.Resolvers;
using SerializeMachine.Utility;

namespace SerializeMachine
{
    public sealed class ResolverStorage : IResolverStorage
    {
        private readonly TypeDictionary TypeDictionary;
        private readonly SortedList<string, IResolver> ResolverList;

        internal void AddResolverInternal(IResolver resolver, string convention)
        {
            ResolverList.Add(convention, resolver);
        }

        [Obsolete("Use SetResolver")]
        public void AddResolver(IResolver resolver, string convention)
        {
            SetResolver(resolver, convention);
        }

        public void SetResolver(IResolver resolver,string convention)
        {
            var index = ResolverList.IndexOfKey(convention);
            if (index < 0) AddResolverInternal(resolver, convention);
            else ResolverList.Values[index] = resolver;
        }

        public Core.IResolver GetResolver(string convention)
        {
            IResolver result;
            ResolverList.TryGetValue(convention, out result);
            return result;
        }

        public bool ContainsResolverFor(string convention)
        {
            return ResolverList.ContainsKey(convention);
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

        //public static void AttachPrimitiveTypesResolvers(Serializator targetSerializator)
        //{
        //    var typeManager = targetSerializator.TypeManager;
        //    var storage = targetSerializator.ResolverBank.Storage;

        //    storage.AddResolver(new BooleanResolver(),typeManager.ConventionOf(TypeOf<bool>.Type));
        //    storage.AddResolver(new ByteResolver(), typeManager.ConventionOf(TypeOf<byte>.Type));
        //    storage.AddResolver(new SByteResolver(), typeManager.ConventionOf(TypeOf<sbyte>.Type));
        //    storage.AddResolver(new CharResolver(), typeManager.ConventionOf(TypeOf<char>.Type));
        //    storage.AddResolver(new DecimalResolver(), typeManager.ConventionOf(TypeOf<decimal>.Type));
        //    storage.AddResolver(new DoubleResolver(), typeManager.ConventionOf(TypeOf<double>.Type));
        //    storage.AddResolver(new SingleResolver(), typeManager.ConventionOf(TypeOf<float>.Type));
        //    storage.AddResolver(new Int32Resolver(), typeManager.ConventionOf(TypeOf<int>.Type));
        //    storage.AddResolver(new UInt32Resolver(), typeManager.ConventionOf(TypeOf<uint>.Type));
        //    storage.AddResolver(new Int64Resolver(), typeManager.ConventionOf(TypeOf<long>.Type));
        //    storage.AddResolver(new UInt64Resolver(), typeManager.ConventionOf(TypeOf<ulong>.Type));
        //    storage.AddResolver(new ObjectResolver(), typeManager.ConventionOf(TypeOf<object>.Type));
        //    storage.AddResolver(new Int16Resolver(), typeManager.ConventionOf(TypeOf<short>.Type));
        //    storage.AddResolver(new UInt16Resolver(), typeManager.ConventionOf(TypeOf<ushort>.Type));
        //    storage.AddResolver(new StringResolver(), typeManager.ConventionOf(TypeOf<string>.Type));
        //}
        
    }
}
