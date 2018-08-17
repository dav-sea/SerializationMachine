using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SerializeMachine
{
    public sealed class SerializedHeap
    {
        private readonly Heap OriginalHeap;
        private readonly TypeDictionary ValidTypeDictionary;
        private readonly ResolverStorage ValidResolverStorage;

        private readonly SortedList<Guid, XElement> HeapList;

        public void Push(Guid guid, XElement serialized)
        {
            var index = HeapList.IndexOfKey(guid);
            if (index < 0) HeapList.Add(guid, serialized);
            else HeapList.Values[index] = serialized;
        }

        public XElement GetSerialized(Guid guid)
        {
            XElement result;
            HeapList.TryGetValue(guid,out result);
            return result;
        }

        public SerializedHeap(Heap heap,TypeDictionary typeDictionary,ResolverStorage resolverStorage)
        {
            OriginalHeap = heap;
            ValidTypeDictionary = typeDictionary;
            ValidResolverStorage = resolverStorage;
        }

        public Heap GetOriginalHeap()
        {
            return OriginalHeap;
        }
        
    }
}
