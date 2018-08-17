using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SerializeMachine.Core
{
    internal sealed class Serializator
    {
        public SerializedHeap Heap;
        public ResolverStorage ResolverStorage;
        public TypeDictionary TypeDictionary;


        public XElement GetSerialized(object obj)
        {
            var guid = Heap.GetOriginalHeap().GuidOf(obj);
            if (guid != TypeDictionary.GUID_NULL)
                return Heap.GetSerialized(guid);
            return Resolve(obj);
        }
        public XElement Resolve(object obj)
        {
            var convetion = TypeDictionary.ConventionOf(obj.GetType());
            var resolver = ResolverStorage.GetResolver(convetion);
            var serialized = TypeDictionary.GetHead(convetion);

            resolver.Serialize(serialized, obj);

            return serialized;
        }
    }
}
