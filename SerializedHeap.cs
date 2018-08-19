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
            if (index < 0)
            {
                HeapList.Add(guid, serialized);
            }
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
            HeapList = new SortedList<Guid, XElement>(typeDictionary.Capacity);//TODO typeDictionary replace to heap????
        }

        public Heap GetOriginalHeap()
        {
            return OriginalHeap;
        }

        public void ClearSerialized()
        {
            HeapList.Clear();//TODO
        }

        public IDictionary<Guid, XElement> ToDictionary()
        {
            return HeapList;
        }

        public static XElement CreateSerializedHeap(SerializedHeap heap)
        {
            if (heap == null) throw new ArgumentNullException("heap");
            var target = heap.ToDictionary();
            if (target == null) throw new InvalidOperationException("heap method ToDictionary() return null");
            return CreateSerializedHeap(target);
        }
        internal static XElement CreateSerializedHeap(IDictionary<Guid, XElement> heap)
        {
            var serialized = new XElement("Heap");

            foreach (var pair in heap)
                serialized.Add(
                    new XElement("E",pair.Value, new XAttribute("GUID",pair.Key.ToString()))
                );

            return serialized;
        }

    }
}
