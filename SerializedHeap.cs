using System;
using System.Collections.Generic;
using System.Xml.Linq;
using SerializeMachine.Utility;

namespace SerializeMachine
{
    public sealed class SerializedHeap
    {
        public const string XML_ELEMENTNAME_SERIALIZEDHEAP = "HEAP";
        private const string XML_ELEMENTNAME_ELEMENT = "E";

        private readonly Heap OriginalHeap;

        private readonly SortedList<Guid, XElement> HeapList;

        public void Push(Guid guid, XElement serialized)
        {
            var index = HeapList.IndexOfKey(guid);
            if (index < 0)
                HeapList.Add(guid, serialized);
            else HeapList.Values[index] = serialized;
        }
        public void SafeAdd(Guid guid, XElement serialized)
        {
            if (HeapList.IndexOfKey(guid) < 0)
                HeapList.Add(guid, serialized);
        }

        public bool TryGetSerialized(Guid guid, out XElement serialized)
        {
            return HeapList.TryGetValue(guid,out serialized);
        }

        public XElement GetSerialized(Guid guid)
        {
            XElement result;
            HeapList.TryGetValue(guid,out result);
            return result;
        }

        public SerializedHeap(Heap heap)
        {
            OriginalHeap = heap;
            HeapList = new SortedList<Guid, XElement>();//TODO typeDictionary replace to heap???? capcacity too
        }

        public Heap GetOriginalHeap()
        {
            return OriginalHeap;
        }

        public void ClearSerialized()
        {
            HeapList.Clear();//TODO?
        }

        public void LoadHeapSerialized(XElement serializedHeap,bool overloadSerialized)
        {
            if (serializedHeap == null || serializedHeap.Name != XML_ELEMENTNAME_SERIALIZEDHEAP) return;

            if (overloadSerialized)
                foreach (var element in serializedHeap.Elements())
                    Push(new Guid(XMLUtility.GuidOfInternal(element)), element);
            else
                foreach (var element in serializedHeap.Elements())
                    SafeAdd(new Guid(XMLUtility.GuidOfInternal(element)), element);
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
            var serialized = new XElement(XML_ELEMENTNAME_SERIALIZEDHEAP);
            const string elementName = XML_ELEMENTNAME_ELEMENT;
            XElement element;

            foreach (var pair in heap)
            {
                element = new XElement(elementName, pair.Value);
                XMLUtility.AttachGUIDInternal(element, pair.Key.ToString());
                serialized.Add(element);
            }

            return serialized;
        }
    }
}
